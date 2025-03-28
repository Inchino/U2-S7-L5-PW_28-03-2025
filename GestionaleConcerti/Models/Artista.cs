using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.Models
{
    public class Artista
    {
        [Key]
        public Guid ArtistaId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genere { get; set; }

        [MaxLength(1000)]
        public string? Biografia { get; set; }

        public ICollection<Evento> Eventi { get; set; }
    }
}
