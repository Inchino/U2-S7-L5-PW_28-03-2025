using System.Threading.Tasks;
using System;
using GestionaleConcerti.DTOs;
using GestionaleConcerti.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GestionaleConcerti.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ArtistaController : ControllerBase
    {
        private readonly ArtistaService _service;

        public ArtistaController(ArtistaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var artisti = await _service.GetAllAsync();
            return Ok(artisti);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var artista = await _service.GetByIdAsync(id);
            if (artista == null) return NotFound();
            return Ok(artista);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateArtistaDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.ArtistaId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateArtistaDto dto)
        {
            var result = await _service.UpdateAsync(dto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
