using AccountService.Service;
using AccountService.Data.DTO;
using AccountService.Repository;
using AccountService.Mappers;
using AccountService.Model.SqlModels;
using AppDataService.Data.DTO;
using AppDataService.Mappers;
using Microsoft.Data.SqlClient;

namespace AccountService.Services;

public class CourseDataService : ICourseDataService
{
    private readonly CourseDataRepository _courseRepository;

    public CourseDataService(SqlConnection sqlConnection)
    {
        _courseRepository = new CourseDataRepository(sqlConnection);
    }

    public async Task<CourseDurationDTO> GetCourseDuration(int courseID)
    {
        var data = await _courseRepository.GetCourseDuration(courseID);

        return CourseDurationMapper.ToDto(data);
    }

    public async Task<List<CourseDataDTO>> GetCourses()
    {
        var data = await _courseRepository.GetCourses();

        List<CourseDataDTO> dataDTO = new List<CourseDataDTO>();

        foreach (SqlCourseData curso in data) {
            dataDTO.Add(CourseMapper.ToDto(curso));
        }

        return dataDTO;
    }
}