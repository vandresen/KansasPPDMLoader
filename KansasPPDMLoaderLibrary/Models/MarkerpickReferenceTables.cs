using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Models
{
    public class MarkerpickReferenceTables
    {
        public List<ReferenceTable> RefTables { get; }
        public MarkerpickReferenceTables()
        {
            this.RefTables = new List<ReferenceTable>()
            {
                new ReferenceTable()
                { KeyAttribute = "STRAT_NAME_SET_ID", Table = "STRAT_NAME_SET", ValueAttribute= "STRAT_NAME_SET_NAME"},
                new ReferenceTable()
                { KeyAttribute = "STRAT_UNIT_ID", Table = "STRAT_UNIT", ValueAttribute= "LONG_NAME"},
            };
        }
    }
}
