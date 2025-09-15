using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            try
            {
                var result = await _userService.RegisterAsync(dto);

                // Génération du token seulement si succès
                result.Token = _tokenService.GenerateToken(result);
                return Ok(result); // ou Created(...) si tu crées une ressource
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (DbUpdateException dbex) when (IsDuplicateEmail(dbex))
            {
                return Conflict(new { message = "Email déjà utilisé." });
            }
            catch (DbUpdateException dbex)
            {
                // autre erreur BDD (FK rôle inexistante, etc.)
                return StatusCode(500, new { message = "Erreur base de données.", detail = RootMessage(dbex) });
            }
            catch (SecurityTokenException ste)
            {
                return StatusCode(500, new { message = $"Erreur JWT: {ste.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erreur interne du serveur.", detail = ex.Message });
            }
        }

        private static bool IsDuplicateEmail(DbUpdateException ex)
        {
            // MySQL: erreur 1062
            var inner = ex.InnerException;
            while (inner != null)
            {
                if (inner.GetType().Name.Contains("MySqlException", StringComparison.OrdinalIgnoreCase)
                    && inner.Message.Contains("1062"))
                    return true;
                inner = inner.InnerException;
            }
            // Variante: inspecter le message pour "Duplicate entry" et le nom de l'index unique Email
            return ex.InnerException?.Message.Contains("Duplicate entry", StringComparison.OrdinalIgnoreCase) == true
                   && ex.InnerException.Message.Contains("Email", StringComparison.OrdinalIgnoreCase);
        }

        private static string RootMessage(Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex.Message;
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
        
        [HttpGet("GetUser/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
