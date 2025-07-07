using AccountService.Model.Base;
using Microsoft.AspNetCore.Mvc;
using AccountService.Service;
using AccountService.Data.DTO;
using Microsoft.AspNetCore.Authorization;

namespace AccountService.Controllers
{
    [ApiController]
    //TODO: [Authorization]
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

        //TODO: Adicionar autenticacao nesta rota após o identity service ser finalizado
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

        [HttpPost]
        [Route("/users/data")]
        public async Task<IActionResult> UserData([FromBody] UserDataDTO dto)
        {
            try
            {
                await _userDataService.SaveUserData(dto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                //TODO: poderia ser um catch
                if (ex is ApiException apiEx)
                {
                    // Se é uma ApiException, trate-a de forma específica
                    return StatusCode(apiEx.ErrorModel.StatusCode, apiEx.ErrorModel);
                }
                else if (ex is ArgumentException argEx)
                {
                    // Trate outras ArgumentExceptions de forma genérica
                    return BadRequest(new ApiErrorModel(argEx.Message, 400));
                }
                else
                {
                    // Trate todas outras exceções não esperadas
                    return StatusCode(500, new ApiErrorModel("An unexpected error occurred", 500));
                }
            }

        }

        [Authorize]
        [HttpPut]
        [Route("/users/data")]
        public async Task<IActionResult> UserDataPut([FromBody] UserDataDTO dto)
        {
            try
            {
                //TODO: FINALIZAR A IMPLEMENTACAO CORRETA DO METODO
                await _userDataService.UpdateUserData(dto);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                //TODO: poderia ser um catch
                if (ex is ApiException apiEx)
                {
                    // Se é uma ApiException, trate-a de forma específica
                    return StatusCode(apiEx.ErrorModel.StatusCode, apiEx.ErrorModel);
                }
                else if (ex is ArgumentException argEx)
                {
                    // Trate outras ArgumentExceptions de forma genérica
                    return BadRequest(new ApiErrorModel(argEx.Message, 400));
                }
                else
                {
                    // Trate todas outras exceções não esperadas
                    return StatusCode(500, new ApiErrorModel("An unexpected error occurred", 500));
                }
            }

        }

        //TODO: V maiusculo fugiu do padrão
        //TODO:  analisar outro nome para a URL
        //TODO:  Refatorar para colocar as funcionalidades de email em outra controller, service, repository e etc
        [HttpPost]
        [Route("/users/data/VerifyEmail")]
        public async Task<IActionResult> SaveEmailToVerify(string userEmail)
        {
            try
            {
                await _userDataService.SaveEmailToVerify(userEmail);
                return Ok();
            }
            catch (ArgumentException ex)
            {

                if (ex is ApiException apiEx)
                {
                    // Se é uma ApiException, trate-a de forma específica
                    return StatusCode(apiEx.ErrorModel.StatusCode, apiEx.ErrorModel);
                }
                else if (ex is ArgumentException argEx)
                {
                    // Trate outras ArgumentExceptions de forma genérica
                    return BadRequest(new ApiErrorModel(argEx.Message, 400));
                }
                else
                {
                    // Trate todas outras exceções não esperadas
                    return StatusCode(500, new ApiErrorModel("An unexpected error occurred", 500));
                }
            }

        }

        [HttpGet]
        [Route("/users/data/VerifyEmail")]
        public async Task<IActionResult> CheckIfEmailIsVerified(string userEmail)
        {
            try
            {
                if(await _userDataService.CheckIfEmailIsVerified(userEmail))
                {
                    return Ok();
                } else
                {
                    return StatusCode(403, new ApiErrorModel("Esse e-mail ainda não foi verificado!", 403));
                }
               
            }
            catch (ArgumentException ex)
            {

                if (ex is ApiException apiEx)
                {
                    // Se é uma ApiException, trate-a de forma específica
                    return StatusCode(apiEx.ErrorModel.StatusCode, apiEx.ErrorModel);
                }
                else if (ex is ArgumentException argEx)
                {
                    // Trate outras ArgumentExceptions de forma genérica
                    return BadRequest(new ApiErrorModel(argEx.Message, 400));
                }
                else
                {
                    // Trate todas outras exceções não esperadas
                    return StatusCode(500, new ApiErrorModel("An unexpected error occurred", 500));
                }
            }

        }
    }
}