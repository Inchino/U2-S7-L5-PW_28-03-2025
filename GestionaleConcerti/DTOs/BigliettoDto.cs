using System;

namespace GestionaleConcerti.DTOs
{
    public class BigliettoDto
    {
        public Guid BigliettoId { get; set; }
        public Guid EventoId { get; set; }
        public string UserId { get; set; }
        public DateTime DataAcquisto { get; set; }
    }
}
