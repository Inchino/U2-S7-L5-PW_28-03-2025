using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionaleConcerti.Models
{
    public class Biglietto
    {
        [Key]
        public Guid BigliettoId { get; set; }

        [Required]
        public Guid EventoId { get; set; }

        [ForeignKey("EventoId")]
        public Evento Evento { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        [Required]
        public DateTime DataAcquisto { get; set; } = DateTime.UtcNow;
    }
}
