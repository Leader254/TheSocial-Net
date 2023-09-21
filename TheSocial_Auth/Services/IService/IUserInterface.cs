using TheSocial_Auth.Models;
using TheSocial_Auth.Models.Dtos;

namespace TheSocial_Auth.Services.IService
{
    public interface IUserInterface
    {
        //register user method
        Task<string> RegisterUser(RegisterUserDto registerUserDto);
        //login user method
        Task<LoginResponseDto> LoginUser(LoginRequestDto loginRequestDto);
        //assign role method
        Task<bool> AssignRole(string email, string role);

        //added end point to get users
        Task<IEnumerable<ApplicationUser>> GetUsers();
    }
}
