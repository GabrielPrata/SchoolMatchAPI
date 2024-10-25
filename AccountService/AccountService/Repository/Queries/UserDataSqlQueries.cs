using AccountService.Model;
using Dapper;
using Microsoft.Data.SqlClient;

namespace AccountService.Repository.Queries
{
    public class UserDataSqlQueries
    {
        private readonly string _connectionString;
        public UserDataSqlQueries(string strConnection)
        {
            _connectionString = strConnection;
        }

        private SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }


        public async Task<SqlUserData?> GetUserDataById(int userId)
        {
            const string query = "SELECT * FROM USUARIOS WHERE IDUSUARIO= @id";

            await using var conn = GetOpenConnection();
            var userData = await conn.QuerySingleOrDefaultAsync<SqlUserData?>(query, new { id = userId });
            return userData;
        }
    }
}
