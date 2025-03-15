using AccountService.Data.DTO;
using AccountService.Mappers;
using AccountService.Model.SqlModels;
using Dapper;
using Microsoft.Data.SqlClient;
namespace AccountService.Repository
{
    public class CourseDataRepository : ICourseDataRepository
    {
        private readonly string _connectionString;
        private SqlConnection _connection;


        public CourseDataRepository(string sqlConnection)
        {
            _connectionString = sqlConnection;
        }


        private SqlConnection GetOpenConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
                _connection.Open();
                return _connection;
            }
            return _connection;
        }

        public async Task<IEnumerable<SqlCourseData>> GetCourses()
        {
            try
            {
   

                const string query = "SELECT * FROM CURSOS";

                await using var conn = GetOpenConnection();
                var courseSqlData = await conn.QueryAsync<SqlCourseData?>(query);

                return courseSqlData;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
