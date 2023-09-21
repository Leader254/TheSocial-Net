using Frontend.Models;
using Frontend.Models.Authentication;

namespace Frontend.Services.Interfaces
{
    public interface IAuthInterface
    {
        Task<List<UserDto>> GetUserAll();
        Task <ResponseDto> Register(RegisterUserDto registerUserDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
