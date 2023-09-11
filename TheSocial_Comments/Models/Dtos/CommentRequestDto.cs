namespace TheSocial_Comments.Models.Dtos
{
    public class CommentRequestDto
    {
        public Guid PostId { get; set; }
        //public Guid UserId { get; set; }
        public string CommentText { get; set; } = "";
    }
}
