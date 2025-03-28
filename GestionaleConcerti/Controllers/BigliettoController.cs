using GestionaleConcerti.DTOs;
using GestionaleConcerti.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace GestionaleConcerti.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BigliettoController : ControllerBase
    {
        private readonly BigliettoService _service;

        public BigliettoController(BigliettoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var biglietti = await _service.GetAllByUserAsync(userId);
            return Ok(biglietti);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("tutti")]
        public async Task<IActionResult> GetAllBiglietti()
        {
            var biglietti = await _service.GetAllAsync();
            return Ok(biglietti);
        }

        [HttpPost("acquista")]
        public async Task<IActionResult> Acquista([FromBody] AcquistoBigliettoDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _service.AcquistaBigliettoAsync(userId, dto);
            if (!result)
                return BadRequest("Acquisto fallito.");
            return Ok("Acquisto completato.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);

            var result = await _service.DeleteByUserOrAdminAsync(id, userId, userRoles);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
