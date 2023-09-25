﻿using KansasPPDMLoaderLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using KansasPPDMLoaderLibrary.DataAccess;
using System.Runtime.Intrinsics.Arm;
using KansasPPDMLoaderLibrary.Extensions;
using System.Data.SqlTypes;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace KansasPPDMLoaderLibrary.Data
{
    public class Welldata : IWellData
    {
        private readonly string wellBoreUrl = @"https://www.kgs.ku.edu/PRS/Ora_Archive/ks_wells.zip";
        private readonly IDataAccess _da;
        private readonly ILogger _log;

        public Welldata(IDataAccess da, ILogger log)
        {
            _da = da;
            _log = log;
        }

        public async Task CopyWellbores(string connectionString)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] zipData = await client.GetByteArrayAsync(wellBoreUrl);
                using (MemoryStream zipStream = new MemoryStream(zipData))
                {
                    using (ZipArchive archive = new ZipArchive(zipStream))
                    {
                        if (archive != null)
                        {
                            _log.LogInformation("Data has been downloaded from Kansas website");
                            if (archive.Entries.Count > 0)
                            {
                                ZipArchiveEntry? entry = archive.GetEntry("ks_wells.txt");
                                if (entry != null)
                                {
                                    _log.LogInformation("Start processing data");
                                    await ProcessWellbores(entry, connectionString);
                                }
                                else
                                {
                                    _log.LogError("File not found in the zip archive.");
                                }
                            }
                            else
                            {
                                _log.LogError("The zip archive is empty.");
                            }
                        }
                        else
                        {
                            _log.LogError("Failed to create the ZipArchive.");
                        }
                    }
                }
            }
        }

        private async Task ProcessWellbores(ZipArchiveEntry entry, string connectionString)
        {
            IEnumerable<TableSchema> tableAttributeInfo = await GetColumnInfo(connectionString, "WELL");
            TableSchema? dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "WELL_NUM");
            int wellNumLength = dataProperty == null ? 4 : dataProperty.PRECISION;
            dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "LEASE_NAME");
            int leaseNameLength = dataProperty == null ? 4 : dataProperty.PRECISION;
            dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "OPERATOR");
            int operatorLength = dataProperty == null ? 4 : dataProperty.PRECISION;
            dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "ASSIGNED_FIELD");
            int fieldLength = dataProperty == null ? 4 : dataProperty.PRECISION;
            dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "DEPTH_DATUM");
            int datumLength = dataProperty == null ? 4 : dataProperty.PRECISION;
            dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "REMARK");
            int remarkLength = dataProperty == null ? 4 : dataProperty.PRECISION;
            dataProperty = tableAttributeInfo.FirstOrDefault(x => x.COLUMN_NAME == "WELL_NAME");
            int wellNameLength = dataProperty == null ? 4 : dataProperty.PRECISION;

            using (Stream entryStream = entry.Open())
            using (StreamReader reader = new StreamReader(entryStream))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<WellboreMap>();
                var records = csv.GetRecords<Wellbore>();

                List<Wellbore> wells = new List<Wellbore>();
                foreach (var record in records) 
                {
                    if (string.IsNullOrEmpty(record.OPERATOR)) record.OPERATOR = "UNKNOWN";
                    if (string.IsNullOrEmpty(record.ASSIGNED_FIELD)) record.ASSIGNED_FIELD = "UNKNOWN";
                    if (string.IsNullOrEmpty(record.DEPTH_DATUM)) record.DEPTH_DATUM = "UNKNOWN";
                    if (string.IsNullOrEmpty(record.CURRENT_STATUS)) record.CURRENT_STATUS = "UNKNOWN";
                    if (string.IsNullOrEmpty(record.UWI)) record.UWI = "UNK" + record.WELL_GOVERNMENT_ID;

                    if (record.WELL_NUM.Length > wellNumLength) 
                        record.WELL_NUM = record.WELL_NUM.Substring(0, wellNumLength);
                    if (record.LEASE_NAME.Length > leaseNameLength)
                        record.LEASE_NAME = record.LEASE_NAME.Substring(0, leaseNameLength);
                    if (record.OPERATOR.Length > operatorLength)
                        record.OPERATOR = record.OPERATOR.Substring(0, operatorLength);
                    if (record.ASSIGNED_FIELD.Length > fieldLength)
                        record.ASSIGNED_FIELD = record.ASSIGNED_FIELD.Substring(0, fieldLength);
                    if (record.DEPTH_DATUM.Length > datumLength)
                        record.DEPTH_DATUM = record.DEPTH_DATUM.Substring(0, datumLength);
                    if (record.REMARK.Length > remarkLength)
                        record.REMARK = record.REMARK.Substring(0, remarkLength);
                    if (record.WELL_NAME.Length > wellNameLength)
                        record.WELL_NAME = record.WELL_NAME.Substring(0, wellNameLength);

                    if (!(record.COMPLETION_DATE >= SqlDateTime.MinValue.Value && record.COMPLETION_DATE <= SqlDateTime.MaxValue.Value))
                    {
                        record.COMPLETION_DATE = null;
                    }
                    if (!(record.SPUD_DATE >= SqlDateTime.MinValue.Value && record.SPUD_DATE <= SqlDateTime.MaxValue.Value))
                    {
                        record.SPUD_DATE = null;
                    }

                    if (record.DEPTH_DATUM_ELEV != null && Math.Abs((decimal)record.DEPTH_DATUM_ELEV) > 99999)
                    {
                        record.DEPTH_DATUM_ELEV = null;
                    }

                    wells.Add(record);
                }
                await SaveWellboreRefData(wells, connectionString);
                await SaveWellbores(wells, connectionString);
            }
        }

        public async Task SaveWellboreRefData(List<Wellbore> wellbores, string connectionString)
        {
            _log.LogInformation("Start saving reference data");
            Dictionary<string, List<ReferenceData>> refDict = new Dictionary<string, List<ReferenceData>>();
            ReferenceTables tables = new ReferenceTables();

            List<ReferenceData> refs = wellbores.Select(x => x.OPERATOR).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[0].Table, refs);
            refs = wellbores.Select(x => x.ASSIGNED_FIELD).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[1].Table, refs);
            refs = wellbores.Select(x => x.DEPTH_DATUM).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[2].Table, refs);
            refs = wellbores.Select(x => x.CURRENT_STATUS).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[3].Table, refs);

            foreach (var table in tables.RefTables)
            {
                refs = refDict[table.Table];
                string sql = "";
                if (table.Table == "R_WELL_STATUS")
                {
                    sql = $"IF NOT EXISTS(SELECT 1 FROM {table.Table} WHERE {table.KeyAttribute} = @Reference) " +
                $"INSERT INTO {table.Table} " +
                $"(STATUS_TYPE, {table.KeyAttribute}, {table.ValueAttribute}) " +
                $"VALUES('STATUS', @Reference, @Reference)";
                }
                else
                {
                    sql = $"IF NOT EXISTS(SELECT 1 FROM {table.Table} WHERE {table.KeyAttribute} = @Reference) " +
                $"INSERT INTO {table.Table} " +
                $"({table.KeyAttribute}, {table.ValueAttribute}) " +
                $"VALUES(@Reference, @Reference)";
                }
                await _da.SaveData(connectionString, refs, sql);
            }
        }

        private async Task SaveWellbores(IEnumerable<Wellbore> wellbores, string connectionString)
        {
            _log.LogInformation("Start saving wellbore data");
            string sql = "IF NOT EXISTS(SELECT 1 FROM WELL WHERE UWI = @UWI) " +
                "INSERT INTO WELL (UWI, WELL_NUM, WELL_GOVERNMENT_ID, " +
                "OPERATOR, ASSIGNED_FIELD, DEPTH_DATUM, DEPTH_DATUM_ELEV," +
                "COMPLETION_DATE, SPUD_DATE, REMARK, WELL_NAME," +
                "FINAL_TD, LEASE_NAME, SURFACE_LONGITUDE, SURFACE_LATITUDE, CURRENT_STATUS) " +
                "VALUES(@UWI, @WELL_NUM, @WELL_GOVERNMENT_ID, " +
                "@OPERATOR, @ASSIGNED_FIELD, @DEPTH_DATUM, @DEPTH_DATUM_ELEV, " +
                "@COMPLETION_DATE, @SPUD_DATE, @REMARK, @WELL_NAME, " +
                "@FINAL_TD, @LEASE_NAME, @SURFACE_LONGITUDE, @SURFACE_LATITUDE, @CURRENT_STATUS)";
            await _da.SaveData<IEnumerable<Wellbore>>(connectionString, wellbores, sql);
        }

        public Task<IEnumerable<TableSchema>> GetColumnInfo(string connectionString, string table) =>
            _da.LoadData<TableSchema, dynamic>("dbo.sp_columns", new { TABLE_NAME = table }, connectionString);
    }
}
