using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.DTOs
{
    public class LoginRequestDTO
    {

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
