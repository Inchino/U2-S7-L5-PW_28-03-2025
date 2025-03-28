using System;

namespace GestionaleConcerti.DTOs
{
    public class ArtistaDto
    {
        public Guid ArtistaId { get; set; }
        public string Nome { get; set; }
        public string Genere { get; set; }
        public string? Biografia { get; set; }
    }
}
