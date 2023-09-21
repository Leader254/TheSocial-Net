namespace Frontend.Models.Authentication
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
