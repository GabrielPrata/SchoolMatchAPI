using AccountService.Model.Base;
using AccountService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    //TODO: [Authorization]
    [Route("api/v1/[controller]")]
    public class CourseDataController : ControllerBase
    {

        private readonly ILogger<CourseDataController> _logger;
        private ICourseDataService _courseDataService;

        public CourseDataController(ILogger<CourseDataController> logger, ICourseDataService courseDataService)
        {
            _logger = logger;
            _courseDataService = courseDataService;
        }

        //TODO: Adicionar autenticacao nesta rota após o identity service ser finalizado
        [HttpGet]
        [Route("/appdata/courses/AppCourses")]
        public async Task<IActionResult> AppCourses()
        {
            try
            {

                var courses = await _courseDataService.GetCourses();

                if (courses != null)
                {
                    return Ok(courses);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiErrorModel(ex.Message, ex.StackTrace));
            }

        }
    }
}