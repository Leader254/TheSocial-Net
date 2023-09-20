using Frontend.Models.Comments;

namespace Frontend.Services.Interfaces
{
    public interface ICommentInterface
    {
        //get all comments
        Task<List<Comment>> GetAllComments();
        //create comment
        Task<string> CreateComment(Comment comment);
    }
}
