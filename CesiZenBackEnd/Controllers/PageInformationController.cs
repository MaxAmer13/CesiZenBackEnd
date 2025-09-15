using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CesiZenBackEnd.Core.DTO;
using CesiZenBackEnd.Services.Abstraction;

namespace CesiZenBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageInformationController : ControllerBase
    {
        private readonly IInformationService _informationService;

        public PageInformationController(IInformationService InformationService)
        {
            _informationService = InformationService;
        }

        // GET: api/pageinformation
        [HttpGet("GetAllPage")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPages()
        {
            var pages = await _informationService.GetAllPagesAsync();
            return Ok(pages);
        }

        // GET: api/pageinformation/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            var page = await _informationService.GetPageByIdAsync(id);
            return page != null ? Ok(page) : NotFound(new { message = "Page non trouvée." });
        }

        // POST: api/pageinformation
        [HttpPost]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Create([FromBody] CreateInformationPageDto dto)
        {
            await _informationService.CreatePageAsync(dto);
            return Ok(new { message = "Page ajoutée avec succès." });
        }

        // PUT: api/pageinformation/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Update(int id, [FromBody] InformationPageDto dto)
        {
            var success = await _informationService.UpdatePageAsync(id, dto);
            return success ? Ok(new { message = "Page mise à jour." }) : NotFound(new { message = "Page non trouvée." });
        }

        // DELETE: api/pageinformation/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrateur")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _informationService.DeletePageAsync(id);
            return success ? Ok(new { message = "Page supprimée." }) : NotFound(new { message = "Page non trouvée." });
        }
    }
}
