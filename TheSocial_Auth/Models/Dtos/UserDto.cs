namespace TheSocial_Auth.Models.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
