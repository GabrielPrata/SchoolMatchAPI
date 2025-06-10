using MatchService.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MatchService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ping : ControllerBase
    {
        [HttpGet(Name = "Ping")]
        public PinngPongDTO Get()
        {
          return new PinngPongDTO("Pong");
        }
    }
}
