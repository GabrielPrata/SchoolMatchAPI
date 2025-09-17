using MatchService.Data.DTO;
using MatchService.Model.Base;
using MatchService.Service;
using Microsoft.AspNetCore.Mvc;

namespace MatchService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MatchController : Controller
    {
        private readonly ILogger<MatchController> _logger;
        private IMatchService _matchService;

        public MatchController(ILogger<MatchController> logger, IMatchService matchService)
        {
            _logger = logger;
            _matchService = matchService;
        }

        //private readonly ILogger<BlockDataController> _logger;
        //private IBlockDataService _blockDataService;
        [HttpPost]
        [Route("/Match")]
        public async Task<IActionResult> UserData([FromBody] SendLikeDTO likeDTO)
        {
            try
            {

                LikeResponseDTO likeResponse = await _matchService.SendUserLike(likeDTO);

                if (likeResponse.IsMatch)
                {
                    //Melhorar esse retorno
                    return Ok(likeResponse);
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
