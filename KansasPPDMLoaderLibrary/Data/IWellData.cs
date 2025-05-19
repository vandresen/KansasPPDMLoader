using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.Data
{
    public interface IWellData
    {
        Task CopyWellbores(string connectionString);
        Task CopyMarkerpicks(string connectionString);
    }
}
