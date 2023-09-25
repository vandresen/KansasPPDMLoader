using KansasPPDMLoaderLibrary.Data;
using KansasPPDMLoaderLibrary.DataAccess;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary
{
    public class DataTransfer : IDataTransfer
    {
        private readonly ILogger<DataTransfer> _log;
        private readonly IConfiguration _configuration;

        public DataTransfer(ILogger<DataTransfer> log, IConfiguration configuration)
        {
            _log = log;
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task Transferdata()
        {
            try
            {
                _log.LogInformation("Start Data Transfer");
                string connectionString = _configuration["ConnectionString"];
                IDataAccess da = new DapperDataAccess();
                IWellData wellData = new Welldata(da, _log);
                _log.LogInformation("Start Data Copy");
                await wellData.CopyWellbores(connectionString);
                _log.LogInformation("Data has been Copied");
            }
            catch (Exception ex)
            {
                string errors = "Error transferring data: " + ex.ToString();
                _log.LogError(errors);
            }
            
        }
    }
}
