using AccountService.Data.DTO;
using AppDataService.Data.DTO;

namespace AccountService.Service
{
    public interface ICourseDataService
    {

        Task<List<CourseDataDTO>> GetCourses();
        Task<CourseDurationDTO> GetCourseDuration(int courseId);

    }
}
