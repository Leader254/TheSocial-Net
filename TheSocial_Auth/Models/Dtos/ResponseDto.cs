namespace TheSocial_Auth.Models.Dtos
{
    public class ResponseDto
    {
        public object? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; } = true;
    }
}
