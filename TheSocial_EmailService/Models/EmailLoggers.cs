namespace TheSocial_EmailService.Models
{
    public class EmailLoggers
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime EmailSentDate { get; set; } = DateTime.Now;
    }
}
