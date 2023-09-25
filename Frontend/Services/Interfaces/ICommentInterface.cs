using Frontend.Models;
using Frontend.Models.Comments;

namespace Frontend.Services.Interfaces
{
    public interface ICommentInterface
    {
        //get all comments
        Task<List<Comment>> GetAllComments();
        //create comment
        Task<ResponseDto> CreateComment(CommentRequestDto commentRequestDto);

        // Delete comments
        Task<ResponseDto> DeleteCommentAsync(Guid id);
    }
}
