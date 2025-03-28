using System;
using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.DTOs
{
    public class UpdateArtistaDto
    {
        [Required]
        public Guid ArtistaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genere { get; set; }

        [MaxLength(1000)]
        public string? Biografia { get; set; }
    }
}
