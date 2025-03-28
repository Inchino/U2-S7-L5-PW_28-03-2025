using System;
using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.DTOs
{
    public class CreateEventoDto
    {
        [Required]
        [MaxLength(100)]
        public string Titolo { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        [MaxLength(200)]
        public string Luogo { get; set; }

        [Required]
        public Guid ArtistaId { get; set; }
    }
}
