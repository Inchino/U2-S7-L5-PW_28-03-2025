using GestionaleConcerti.Data;
using GestionaleConcerti.DTOs;
using GestionaleConcerti.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionaleConcerti.Services
{
    public class BigliettoService
    {
        private readonly ApplicationDbContext _context;

        public BigliettoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BigliettoDto>> GetAllByUserAsync(string userId)
        {
            return await _context.Biglietti
                .Where(b => b.UserId == userId)
                .Select(b => new BigliettoDto
                {
                    BigliettoId = b.BigliettoId,
                    EventoId = b.EventoId,
                    UserId = b.UserId,
                    DataAcquisto = b.DataAcquisto
                }).ToListAsync();
        }

        public async Task<BigliettoDto?> GetByIdAsync(Guid id)
        {
            var biglietto = await _context.Biglietti.FindAsync(id);
            if (biglietto == null) return null;

            return new BigliettoDto
            {
                BigliettoId = biglietto.BigliettoId,
                EventoId = biglietto.EventoId,
                UserId = biglietto.UserId,
                DataAcquisto = biglietto.DataAcquisto
            };
        }

        public async Task<List<BigliettoDto>> GetAllAsync()
        {
            return await _context.Biglietti
                .Include(b => b.Evento)
                .Include(b => b.User)
                .Select(b => new BigliettoDto
                {
                    BigliettoId = b.BigliettoId,
                    EventoId = b.EventoId,
                    UserId = b.UserId,
                    DataAcquisto = b.DataAcquisto
                })
                .ToListAsync();
        }

        public async Task<bool> AcquistaBigliettoAsync(string userId, AcquistoBigliettoDto dto)
        {
            try
            {
                var evento = await _context.Eventi.FindAsync(dto.EventId);
                if (evento == null) return false;

                for (int i = 0; i < dto.Quantita; i++)
                {
                    var biglietto = new Biglietto
                    {
                        BigliettoId = Guid.NewGuid(),
                        EventoId = dto.EventId,
                        UserId = userId,
                        DataAcquisto = DateTime.UtcNow
                    };

                    _context.Biglietti.Add(biglietto);
                }

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
                var biglietto = await _context.Biglietti.FindAsync(id);
                if (biglietto == null) return false;

                _context.Biglietti.Remove(biglietto);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteByUserOrAdminAsync(Guid bigliettoId, string userId, IEnumerable<string> userRoles)
        {
            var biglietto = await _context.Biglietti.FindAsync(bigliettoId);
            if (biglietto == null)
                return false;

            bool isAdmin = userRoles.Contains("Admin");
            bool isOwner = biglietto.UserId == userId;

            if (!isAdmin && !isOwner)
                return false;

            _context.Biglietti.Remove(biglietto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}