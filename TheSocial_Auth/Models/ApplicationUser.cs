using Microsoft.AspNetCore.Identity;

namespace TheSocial_Auth.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; } = string.Empty;
    }
}
