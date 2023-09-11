using TheSocial_Comments.Models;

namespace TheSocial_Comments.Services.IServices
{
    public interface ICommentsInterface
    {
        Task<string> CreateCommentAsync(Comment comment);
        Task<string> UpdateCommentAsync(Comment comment);
        Task<string> DeleteCommentAsync(Comment comment);
        Task<IEnumerable<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentByIdAsync(Guid commentId);
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(Guid postId);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(Guid userId);
    }
}
