using AccountService.Model.SqlModels;
using AppDataService.Model.SqlModels;
using Dapper;
using Microsoft.Data.SqlClient;
namespace AccountService.Repository
{
    public class CourseDataRepository : ICourseDataRepository
    {
        private SqlConnection _connection;


        public CourseDataRepository(SqlConnection sqlConnection)
        {
            _connection = sqlConnection;
        }


        public async Task<IEnumerable<SqlCourseData>> GetCourses()
        {
            try
            {
                const string query = "SELECT * FROM CURSOS";

                return await _connection.QueryAsync<SqlCourseData?>(query);
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException(e.Message);
            }
        }

        public async Task<SqlCourseDuration> GetCourseDuration(int courseId)
        {
            const string query = "SELECT TOTALPERIODOSCURSO FROM CURSOS WHERE IDCURSO = @id";

            return await _connection.QuerySingleOrDefaultAsync<SqlCourseDuration?>(query, new { id = courseId });

        }
    }
}
