using System;

namespace GestionaleConcerti.DTOs
{
    public class EventoDto
    {
        public Guid EventoId { get; set; }
        public string Titolo { get; set; }
        public DateTime Data { get; set; }
        public string Luogo { get; set; }

        public Guid ArtistaId { get; set; }
    }
}
