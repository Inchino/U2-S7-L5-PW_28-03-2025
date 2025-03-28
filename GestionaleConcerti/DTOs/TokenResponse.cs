using System.ComponentModel.DataAnnotations;

namespace GestionaleConcerti.DTOs
{
    public class TokenResponse
    {

        [Required]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
