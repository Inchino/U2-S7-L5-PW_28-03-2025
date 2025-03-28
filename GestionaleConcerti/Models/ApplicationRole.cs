using Microsoft.AspNetCore.Identity;

namespace GestionaleHotel.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}