using CsvHelper.Configuration;

namespace KansasPPDMLoaderLibrary.Models
{
    public class WellboreMap : ClassMap<Wellbore>
    {
        public WellboreMap()
        {
            Map(m => m.UWI).Name("API_NUM_NODASH");
            Map(m => m.LEASE_NAME).Name("LEASE");
            Map(m => m.FINAL_TD).Name("DEPTH");
            Map(m => m.WELL_NUM).Name("WELL");
            Map(m => m.WELL_GOVERNMENT_ID).Name("KID");
            Map(m => m.ASSIGNED_FIELD).Name("FIELD");
            Map(m => m.SURFACE_LONGITUDE).Name("LONGITUDE");
            Map(m => m.SURFACE_LATITUDE).Name("LATITUDE");
            Map(m => m.OPERATOR).Name("ORIG_OPERATOR");
            Map(m => m.DEPTH_DATUM_ELEV).Name("ELEVATION");
            Map(m => m.DEPTH_DATUM).Name("ELEV_REF");
            Map(m => m.SPUD_DATE).Name("SPUD");
            Map(m => m.COMPLETION_DATE).Name("COMPLETION");
            Map(m => m.CURRENT_STATUS).Name("STATUS");
            Map(m => m.REMARK).Name("COMMENTS");
            Map(m => m.WELL_NAME).Name("LEASE_WELL_NAME");
        }
    }
}
