using AccountService.Model.SqlModels;
using AppDataService.Model.SqlModels;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace AppDataService.Repository
{
    public class BlocksDataRepository : IBlocksDataRepository
    {
        private SqlConnection _connection;


        public BlocksDataRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }

        public async Task<IEnumerable<SqlBlockData>> GetMainBlocks()
        {
            try
            {
                const string query = "SELECT * FROM BLOCOS WHERE BLOCOFACULDADE = 1";

                return await _connection.QueryAsync<SqlBlockData?>(query);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public async Task<IEnumerable<SqlBlockData>> GetSecondaryBlocks()
        {
            try
            {
                const string query = "SELECT * FROM BLOCOS WHERE BLOCOFACULDADE = 0";

                return await _connection.QueryAsync<SqlBlockData?>(query);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
