using AccountService.Model.Base;
using AccountService.Service;
using AppDataService.Repository;
using AppDataService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ServiceFilter(typeof(BasicAuthAttribute))]

    public class SexualityDataController : ControllerBase
    {

        private readonly ILogger<SexualityDataController> _logger;
        private ISexualityDataService _sexualityDataService;

        public SexualityDataController(ILogger<SexualityDataController> logger, ISexualityDataService sexualityDataService)
        {
            _logger = logger;
            _sexualityDataService = sexualityDataService;
        }

        [HttpGet]
        [Route("/appdata/sexuality/GetAllSexualities")]
        public async Task<IActionResult> GetAllSexualities()
        {
            try
            {
                var sexualities = await _sexualityDataService.GetSexualities();

                if (sexualities != null)
                {
                    return Ok(sexualities);
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