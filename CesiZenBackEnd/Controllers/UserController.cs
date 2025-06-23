using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Services.Abstraction;

namespace CesiZenBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        // Inscription
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var result = await _userService.RegisterAsync(dto);
                result.Token = _tokenService.GenerateToken(result);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur." });
            }
        }

        // Connexion
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var result = await _userService.LoginAsync(dto);

                if (!result.Success)
                {
                    return Unauthorized(new
                    {
                        message = result.ErrorCode == "ACCOUNT_DELETED_PENDING"
                            ? "Votre compte est en attente de suppression. Voulez-vous le récupérer ?"
                            : "Échec de l'authentification.",
                        errorCode = result.ErrorCode
                    });
                }

                result.Token = _tokenService.GenerateToken(result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erreur serveur : {ex.Message}" });
            }
        }
        
        // GET: api/pageinformation
        [HttpGet("GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            var pages = await _userService.GetUserByIdAsync(id);
            return Ok(pages);
        }
    }
    
    
}
