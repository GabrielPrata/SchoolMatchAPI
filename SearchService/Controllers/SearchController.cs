using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SearchService.Data.DTO;
using SearchService.Data.DTO.Profile;
using SearchService.Model.Base;
using SearchService.Service;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private ISearchService _service;

        public SearchController(ILogger<SearchController> logger, ISearchService service)
        {
            _logger = logger;
            _service = service;
        }

        //private readonly ILogger<BlockDataController> _logger;
        //private IBlockDataService _blockDataService;
        [HttpPost]
        [Authorize]
        [Route("/search/DefaultSearch")]
        public async Task<IActionResult> DefaultSearch([FromBody] UserPreferencesDTO dto)
        {
            try
            {
                List<UserDataDTO> usersFinded = await _service.DefaultSearch(dto);

                return Ok(usersFinded);

            }
            catch (ApiException ex)
            {
                return StatusCode(ex.ErrorModel.StatusCode, new {ex.ErrorModel.StackTrace, ex.ErrorModel.Message} );
            }
            catch(ArgumentException ex)
            {
                return BadRequest(new ApiErrorModel(ex.Message, ex.StackTrace));
            }

        }
    }
}
