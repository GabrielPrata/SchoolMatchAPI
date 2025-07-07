using Microsoft.AspNetCore.Mvc;

namespace MatchService.Controllers
{
    public class MatchController : Controller
    {
        //private readonly ILogger<BlockDataController> _logger;
        //private IBlockDataService _blockDataService;
        public IActionResult Index()
        {
            return View();
        }
    }
}
