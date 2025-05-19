using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Models
{
    public class Markerpick
    {
        public string KID { get; set; }
        public string UWI { get; set; }
        public string STRAT_NAME_SET_ID { get; set; } = "KANSAS";
        public string STRAT_UNIT_ID { get; set; }
        public string INTERP_ID { get; set; } = "KANSAS";
        public string DOMINANT_LITHOLOGY { get; set; }
        public double? PICK_DEPTH { get; set; }
        public string REMARK { get; set; }
    }
}
