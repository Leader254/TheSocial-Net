using Frontend.Models.Comments;

namespace Frontend.Models.Posts
{
    public class PostDto
    {

        public Guid postId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public List<Comment>? Comments = new List<Comment>();
    }
}
