using GestionaleConcerti.Data;
using GestionaleConcerti.DTOs;
using GestionaleConcerti.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionaleConcerti.Services
{
    public class EventoService
    {
        private readonly ApplicationDbContext _context;

        public EventoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EventoDto>> GetAllAsync()
        {
            return await _context.Eventi
                .Select(e => new EventoDto
                {
                    EventoId = e.EventoId,
                    Titolo = e.Titolo,
                    Data = e.Data,
                    Luogo = e.Luogo,
                    ArtistaId = e.ArtistaId
                }).ToListAsync();
        }

        public async Task<EventoDto?> GetByIdAsync(Guid id)
        {
            var evento = await _context.Eventi.FindAsync(id);
            if (evento == null) return null;

            return new EventoDto
            {
                EventoId = evento.EventoId,
                Titolo = evento.Titolo,
                Data = evento.Data,
                Luogo = evento.Luogo,
                ArtistaId = evento.ArtistaId
            };
        }

        public async Task<EventoDto> CreateAsync(CreateEventoDto dto)
        {
            try
            {
                var evento = new Evento
                {
                    EventoId = Guid.NewGuid(),
                    Titolo = dto.Titolo,
                    Data = dto.Data,
                    Luogo = dto.Luogo,
                    ArtistaId = dto.ArtistaId
                };

                _context.Eventi.Add(evento);
                await _context.SaveChangesAsync();

                return new EventoDto
                {
                    EventoId = evento.EventoId,
                    Titolo = evento.Titolo,
                    Data = evento.Data,
                    Luogo = evento.Luogo,
                    ArtistaId = evento.ArtistaId
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UpdateEventoDto dto)
        {
            try
            {
                var evento = await _context.Eventi.FindAsync(dto.EventoId);
                if (evento == null) return false;

                evento.Titolo = dto.Titolo;
                evento.Data = dto.Data;
                evento.Luogo = dto.Luogo;
                evento.ArtistaId = dto.ArtistaId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var evento = await _context.Eventi.FindAsync(id);
                if (evento == null) return false;

                _context.Eventi.Remove(evento);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
