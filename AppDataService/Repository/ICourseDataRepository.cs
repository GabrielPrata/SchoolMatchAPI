using AccountService.Data.DTO;
using AccountService.Model.SqlModels;

namespace AccountService.Repository
{
    public interface ICourseDataRepository
    {
        Task<IEnumerable<SqlCourseData>> GetCourses();

    }
}
