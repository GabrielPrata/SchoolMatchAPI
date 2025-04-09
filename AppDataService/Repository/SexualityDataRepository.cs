using AppDataService.Model.SqlModels;
using Dapper;
using Microsoft.Data.SqlClient;


namespace AppDataService.Repository
{
    public class SexualityDataRepository : ISexualityDataRepository
    {
        private SqlConnection _connection;

        public SexualityDataRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }

        public async Task<IEnumerable<SqlSexualityData>> GetSexualities()
        {
            try
            {
                const string query = "SELECT * FROM SEXUALIDADES";

                return await _connection.QueryAsync<SqlSexualityData?>(query);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
