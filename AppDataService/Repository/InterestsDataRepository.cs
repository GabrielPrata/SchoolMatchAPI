using AppDataService.Model.SqlModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AppDataService.Repository
{
    public class InterestsDataRepository : IInterestsDataRepository
    {
        private SqlConnection _connection;

        public InterestsDataRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }

        public async Task<IEnumerable<SqlInterestsData>> GetAllInterests()
        {
            try
            {
                const string query = "SELECT * FROM INTERESSES";

                return await _connection.QueryAsync<SqlInterestsData?>(query);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
