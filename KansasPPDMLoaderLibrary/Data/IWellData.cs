using KansasPPDMLoaderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Data
{
    public interface IWellData
    {
        Task CopyWellbores(string connectionString);
    }
}
