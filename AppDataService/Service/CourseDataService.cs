using AccountService.Service;
using AccountService.Data.DTO;
using AccountService.Repository;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using AccountService.Model.Base;
using AccountService.Mappers;
using AccountService.Model.SqlModels;
using System.Linq;

namespace AccountService.Services;

public class CourseDataService : ICourseDataService
{
    private readonly CourseDataRepository _courseRepository;

    public CourseDataService(string sqlConnection)
    {
        _courseRepository = new CourseDataRepository(sqlConnection);
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