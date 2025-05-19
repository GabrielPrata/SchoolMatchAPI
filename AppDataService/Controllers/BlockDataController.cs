using AccountService.Model.Base;
using AccountService.Service;
using AppDataService.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/[controller]")]

    public class BlockDataController : ControllerBase
    {

        private readonly ILogger<BlockDataController> _logger;
        private IBlockDataService _blockDataService;

        public BlockDataController(ILogger<BlockDataController> logger, IBlockDataService blockDataService)
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