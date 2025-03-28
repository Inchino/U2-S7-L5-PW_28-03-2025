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
    public class EventoController : ControllerBase
    {
        private readonly EventoService _service;

        public EventoController(EventoService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventi = await _service.GetAllAsync();
            return Ok(eventi);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var evento = await _service.GetByIdAsync(id);
            if (evento == null) return NotFound();
            return Ok(evento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEventoDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EventoId }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEventoDto dto)
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
