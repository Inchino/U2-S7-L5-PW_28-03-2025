using GestionaleConcerti.Data;
using GestionaleConcerti.DTOs;
using GestionaleConcerti.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionaleConcerti.Services
{
    public class ArtistaService
    {
        private readonly ApplicationDbContext _context;

        public ArtistaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ArtistaDto>> GetAllAsync()
        {
            return await _context.Artisti
                .Select(a => new ArtistaDto
                {
                    ArtistaId = a.ArtistaId,
                    Nome = a.Nome,
                    Genere = a.Genere,
                    Biografia = a.Biografia
                })
                .ToListAsync();
        }

        public async Task<ArtistaDto?> GetByIdAsync(Guid id)
        {
            var artista = await _context.Artisti.FindAsync(id);
            if (artista == null) return null;

            return new ArtistaDto
            {
                ArtistaId = artista.ArtistaId,
                Nome = artista.Nome,
                Genere = artista.Genere,
                Biografia = artista.Biografia
            };
        }

        public async Task<ArtistaDto> CreateAsync(CreateArtistaDto dto)
        {
            try
            {
                var artista = new Artista
                {
                    ArtistaId = Guid.NewGuid(),
                    Nome = dto.Nome,
                    Genere = dto.Genere,
                    Biografia = dto.Biografia
                };

                _context.Artisti.Add(artista);
                await _context.SaveChangesAsync();

                return new ArtistaDto
                {
                    ArtistaId = artista.ArtistaId,
                    Nome = artista.Nome,
                    Genere = artista.Genere,
                    Biografia = artista.Biografia
                };
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateAsync(UpdateArtistaDto dto)
        {
            try
            {
                var artista = await _context.Artisti.FindAsync(dto.ArtistaId);
                if (artista == null) return false;

                artista.Nome = dto.Nome;
                artista.Genere = dto.Genere;
                artista.Biografia = dto.Biografia;

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
                var artista = await _context.Artisti.FindAsync(id);
                if (artista == null) return false;

                _context.Artisti.Remove(artista);
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
