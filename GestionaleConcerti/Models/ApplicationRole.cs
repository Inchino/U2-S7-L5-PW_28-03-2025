using Microsoft.AspNetCore.Identity;

namespace GestionaleConcerti.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
