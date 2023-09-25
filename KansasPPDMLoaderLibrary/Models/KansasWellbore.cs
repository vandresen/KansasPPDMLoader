using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Models
{
    public class KansasWellbore
    {
        public string KID { get; set; }
        public string API_NUMBER { get; set; }
        public string API_NUM_NODASH { get; set; }
        public string LEASE { get; set; }
        public string WELL { get; set; }
        public string FIELD { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public string LONG_LAT_SOURCE { get; set; }
        public string TOWNSHIP { get; set; }
        public string TWN_DIR { get; set; }
        public string RANGE { get; set; }
        public string RANGE_DIR { get; set; }
        public string SECTION { get; set; }
        public string SPOT { get; set; }
        public string FEET_NORTH { get; set; }
        public string FEET_EAST { get; set; }
        public string FOOT_REF { get; set; }
        public string ORIG_OPERATOR { get; set; }
        public string CURR_OPERATOR { get; set; }
        public string ELEVATION { get; set; }
        public string ELEV_REF { get; set; }
        public string DEPTH { get; set; }
        public string FORMATION_AT_TOTAL_DEPTH { get; set; }
        public string PRODUCE_FORM { get; set; }
        public string IP_OIL { get; set; }
        public string IP_GAS { get; set; }
        public string IP_WATER { get; set; }
        public string PERMIT { get; set; }
        public string SPUD { get; set; }
        public string COMPLETION { get; set; }
        public string PLUGGING { get; set; }
        public string MODIFIED { get; set; }
        public string OIL_KID { get; set; }
        public string OIL_DOR_ID { get; set; }
        public string GAS_KID { get; set; }
        public string GAS_DOR_ID { get; set; }
        public string KCC_PERMIT { get; set; }
        public string STATUS { get; set; }
        public string STATUS2 { get; set; }
        public string COMMENTS { get; set; }
        public string LEASE_WELL_NAME { get; set; }
    }
}
