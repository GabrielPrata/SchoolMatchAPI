using SearchService.Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MatchService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Ping : ControllerBase
    {
        [HttpGet(Name = "Ping")]
        public PingPongDTO Get()
        {
          return new PingPongDTO("Pong");
        }
    }
}
