using KansasPPDMLoaderLibrary.Data;
using KansasPPDMLoaderLibrary.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary
{
    public class DataTransfer : IDataTransfer
    {
        private readonly ILogger<DataTransfer> _log;
        private readonly IConfiguration _configuration;

        public DataTransfer(ILogger<DataTransfer> log, IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task Transferdata(string connectionString, string datatype)
        {
            try
            {
                _log.LogInformation("Start Data Transfer for {DataType}", datatype);
                IDataAccess da = new DapperDataAccess();
                IWellData wellData = new Welldata(da, _log);

                if (datatype.Equals("Wellbore", StringComparison.OrdinalIgnoreCase))
                {
                    await wellData.CopyWellbores(connectionString);
                }
                else if (datatype.Equals("Markerpick", StringComparison.OrdinalIgnoreCase))
                {
                    await wellData.CopyMarkerpicks(connectionString);
                }
                else
                {                    
                    _log.LogWarning("Unknown datatype: {DataType}", datatype);
                }

                _log.LogInformation("Data transfer for {DataType} completed.", datatype);
            }
            catch (Exception ex)
            {
                _log.LogError(ex, "Error transferring {DataType} data", datatype);
            }
        }
    }
}
