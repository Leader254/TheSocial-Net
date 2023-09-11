using TheSocial_Post.Models.Dtos;

namespace TheSocial_Post.Services.IService
{
    public interface ICommentInterface
    {
       Task <IEnumerable<CommentsDto>> GetCommentsAsync (Guid postId);
    }
}
