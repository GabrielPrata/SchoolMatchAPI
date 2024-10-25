using AccountService.Model.Base;
using Microsoft.AspNetCore.Mvc;
using AccountService.Services;
using AccountService.Service;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        
        private readonly ILogger<UserController> _logger;
        private IUserDataService _userDataService;

        public UserController(ILogger<UserController> logger, IUserDataService userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

        [HttpGet]
        [Route("/users/data/{userId:int}")]
        public async Task<IActionResult> UserData([FromRoute] int userId)
        {
            try
            {
                
                var userData = await _userDataService.GetUserDataById(userId);

                if (userData != null)
                {
                    return Ok(userData);
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