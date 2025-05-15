using AccountService.Model.Base;
using AccountService.Service;
using AppDataService.Service;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [ServiceFilter(typeof(BasicAuthAttribute))]

    public class BlocDataController : ControllerBase
    {

        private readonly ILogger<BlocDataController> _logger;
        private IBlockDataService _blockDataService;

        public BlocDataController(ILogger<BlocDataController> logger, IBlockDataService blockDataService)
        {
            _logger = logger;
            _blockDataService = blockDataService;
        }

        [HttpGet]
        [Route("/appdata/blocks/MainBlocks")]
        public async Task<IActionResult> MainBlocks()
        {
            try
            {
                var blocks = await _blockDataService.GetMainBlocks();

                if (blocks != null)
                {
                    return Ok(blocks);
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
        [Route("/appdata/blocks/SecondaryBlocks")]
        public async Task<IActionResult> SecondaryBlocks()
        {
            try
            {
                var blocks = await _blockDataService.GetSecondaryBlocks();

                if (blocks != null)
                {
                    return Ok(blocks);
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