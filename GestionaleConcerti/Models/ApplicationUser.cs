using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }
    }
}
