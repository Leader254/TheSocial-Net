namespace Frontend.Models.Comments
{
    public class CommentsRequestDto
    {
        public Guid PostId { get; set; }
        //public Guid UserId { get; set; }
        public string CommentText { get; set; } = "";
    }
}
