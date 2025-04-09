using AccountService.Data.DTO;
using AccountService.Model.SqlModels;
using AppDataService.Model.SqlModels;

namespace AccountService.Repository
{
    public interface ICourseDataRepository
    {
        Task<IEnumerable<SqlCourseData>> GetCourses();
        Task<SqlCourseDuration> GetCourseDuration(int courseId);


    }
}
