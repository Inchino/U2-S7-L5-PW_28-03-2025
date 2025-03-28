using System.ComponentModel.DataAnnotations;

namespace GestioneConcerti.Controllers
{
    public class TokenResponse
    {

        [Required]
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
