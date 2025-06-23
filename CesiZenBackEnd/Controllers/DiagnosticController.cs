using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Services.Abstraction;

namespace CesiZenBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiagnosticController : ControllerBase
    {
        private readonly IDiagnosticService _diagnosticService;

        public DiagnosticController(IDiagnosticService diagnosticService)
        {
            _diagnosticService = diagnosticService;
        }

        // POST: api/diagnostic
        [HttpPost]
        [Authorize(Roles = "Utilisateur,Administrateur")]
        public async Task<IActionResult> CreateDiagnostic([FromBody] CreateDiagnosticDto dto,  int utilisateurId)
        {
            try
            {
                var id = await _diagnosticService.CreateDiagnosticAsync(dto, utilisateurId);
                return Ok(new { message = "Diagnostic enregistré", diagnosticId = id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erreur serveur : {ex.Message}" });
            }
        }

        // GET: api/diagnostic/user/5
        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Utilisateur,Administrateur")]
        public async Task<IActionResult> GetDiagnosticsByUser(int userId)
        {
            var diagnostics = await _diagnosticService.GetDiagnosticsByUserIdAsync(userId);
            return Ok(diagnostics);
        }

        // GET: api/diagnostic
        [HttpGet]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> GetAll()
        {
            var diagnostics = await _diagnosticService.GetAllDiagnosticsAsync();
            return Ok(diagnostics);
        }
    }
}