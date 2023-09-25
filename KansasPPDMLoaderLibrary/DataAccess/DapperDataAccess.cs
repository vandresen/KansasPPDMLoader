using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace KansasPPDMLoaderLibrary.DataAccess
{
    public class DapperDataAccess : IDataAccess
    {
        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString)
        {
            using IDbConnection cnn = new SqlConnection(connectionString);
            return await cnn.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<T>> ReadData<T>(string sql, string connectionString)
        {
            using IDbConnection cnn = new SqlConnection(connectionString);
            return await cnn.QueryAsync<T>(sql);
        }

        public async Task SaveData<T>(string connectionString, T parameters, string sql)
        {
            using IDbConnection cnn = new SqlConnection(connectionString);
            await cnn.ExecuteAsync(sql, parameters);
        }
    }
}
