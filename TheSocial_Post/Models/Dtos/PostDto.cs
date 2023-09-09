namespace TheSocial_Post.Models.Dtos
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public IEnumerable<CommentsDto>? Comments { get; set; }


    }
}
