using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionaleConcerti.Models
{
    public class Evento
    {
        [Key]
        public Guid EventoId { get; set; }

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

        [ForeignKey("ArtistaId")]
        public Artista Artista { get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
