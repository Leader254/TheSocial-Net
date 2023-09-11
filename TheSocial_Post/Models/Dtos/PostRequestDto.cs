using System.ComponentModel.DataAnnotations;

namespace TheSocial_Post.Models.Dtos
{
    public class PostRequestDto
    {
        //public Guid UserId { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}