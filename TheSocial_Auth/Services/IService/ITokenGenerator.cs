using TheSocial_Auth.Models;

namespace TheSocial_Auth.Services.IService
{
    public interface ITokenGenerator
    {
        string GenerateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}
