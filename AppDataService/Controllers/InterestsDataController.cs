using AccountService.Model.Base;
using AccountService.Service;
using AppDataService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]

    public class InterestsDataController : ControllerBase
    {

        private readonly ILogger<InterestsDataController> _logger;
        private IInterestsDataService _interestsDataService;

        public InterestsDataController(ILogger<InterestsDataController> logger, IInterestsDataService interestsDataService)
        {
            _logger = logger;
            _interestsDataService = interestsDataService;
        }

        [HttpGet]
        [Route("/appdata/interests/GetAllInterests")]
        public async Task<IActionResult> GetAllInterests()
        {
            try
            {

                var interests = await _interestsDataService.GetAllInterests();

                if (interests != null)
                {
                    return Ok(interests);
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