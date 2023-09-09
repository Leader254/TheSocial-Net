namespace TheSocial_Post.Models.Dtos
{
    public class CommentsDto
    {
        public Guid CommentId { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
