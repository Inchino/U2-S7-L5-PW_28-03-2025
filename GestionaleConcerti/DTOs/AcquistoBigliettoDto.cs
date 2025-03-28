using System;

namespace GestionaleConcerti.DTOs
{
    public class AcquistoBigliettoDto
    {
        public Guid EventId { get; set; }
        public int Quantita { get; set; } = 1;
    }
}
