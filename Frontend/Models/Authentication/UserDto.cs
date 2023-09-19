namespace Frontend.Models.Authentication
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
