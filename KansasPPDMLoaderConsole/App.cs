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

        public async Task Run()
        {
            await _dataTransfer.Transferdata();
        }
    }
}
