using MatchService.Data.DTO;
using MatchService.Data.DTO.Profile;
using MatchService.Model.Base;
using MatchService.Service;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [Route("/matchs/SendUserLike")]
        public async Task<IActionResult> SendUserLike([FromBody] SendLikeDTO likeDTO)
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

        [HttpGet]
        [Authorize]
        [Route("/matchs/GetUserMatches/{userId}")]
        public async Task<IActionResult> GetUserMatches([FromRoute] int userId)
        {
            try
            {

                List<UserDataDTO> userMatches = await _matchService.GetUserMatches(userId);

                return Ok(userMatches);

            }
            catch (ApiException ex)
            {
                return StatusCode(ex.ErrorModel.StatusCode, new { ex.ErrorModel.StackTrace, ex.ErrorModel.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ApiErrorModel(ex.Message, ex.StackTrace));
            }

        }
    }
}
