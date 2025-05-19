using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Models
{
    public class MarkerpickMap: ClassMap<Markerpick>
    {
        public MarkerpickMap() 
        {
            Map(m => m.KID).Name("KID");
            Map(m => m.UWI).Name("API_NUM_NODASH");
            Map(m => m.STRAT_UNIT_ID).Name("FORMATION");
            Map(m => m.PICK_DEPTH).Name("TOP");
        }
        
    }
}
