using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Models
{
    public class Wellbore
    {
        public string UWI { get; set; }
        public double? SURFACE_LONGITUDE { get; set; }
        public double? SURFACE_LATITUDE { get; set; }
        public double? BOTTOM_HOLE_LATITUDE { get; set; }
        public double? BOTTOM_HOLE_LONGITUDE { get; set; }
        public string LEASE_NAME { get; set; }
        public string CURRENT_STATUS { get; set; } = "UNKNOWN";
        public string OPERATOR { get; set; } = "UNKNOWN";
        public string ASSIGNED_FIELD { get; set; } = "UNKNOWN";
        public string WELL_NUM { get; set; }
        public decimal? FINAL_TD { get; set; }
        public decimal? DEPTH_DATUM_ELEV { get; set; }
        public string DEPTH_DATUM { get; set; } = "UNKNOWN";
        public DateTime? COMPLETION_DATE { get; set; }
        public DateTime? SPUD_DATE { get; set; }
        public string WELL_GOVERNMENT_ID { get; set; }
        public string REMARK { get; set; }
        public string WELL_NAME { get; set; }
    }
}
