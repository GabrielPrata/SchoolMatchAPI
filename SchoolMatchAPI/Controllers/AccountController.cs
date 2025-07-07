using AccountService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    //TODO: [Authorization]
    [Route("api/v1/[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private IUserDataService _userDataService;

        public AccountController(ILogger<UserController> logger, IUserDataService userDataService)
        {
            _logger = logger;
            _userDataService = userDataService;
        }

       
    }
}
