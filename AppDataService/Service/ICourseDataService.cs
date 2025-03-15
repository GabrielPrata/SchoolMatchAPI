using AccountService.Data.DTO;

namespace AccountService.Service
{
    public interface ICourseDataService
    {

        Task<List<CourseDataDTO>> GetCourses();

    }
}
