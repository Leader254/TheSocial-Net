namespace Frontend.Models.Authentication
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; } = default!;

        public string Token { get; set; } = string.Empty;
    }
}
