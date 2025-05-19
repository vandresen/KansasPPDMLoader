using KansasPPDMLoaderLibrary;
using System.Threading.Tasks;

namespace KansasPPDMLoaderConsole
{
    public class App
    {
        private readonly IDataTransfer _dataTransfer;

        public App(IDataTransfer dataTransfer)
        {
            _dataTransfer = dataTransfer;
        }

        public async Task Run(string connectionString, string datatype)
        {
            await _dataTransfer.Transferdata(connectionString, datatype);
        }
    }
}
