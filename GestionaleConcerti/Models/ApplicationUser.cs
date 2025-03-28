using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateOnly? BirthDate { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }

        public ICollection<Biglietto> Biglietti { get; set; }

    }
}