using Frontend.Models;
using Frontend.Models.Posts;

namespace Frontend.Services.Interfaces
{
    public interface IPostInterface
    {
        Task<ResponseDto> AddPostAsync(PostRequestDto newPost);
        Task<ResponseDto> UpdatePostAsync(Guid id,PostRequestDto UpdatePost);
        Task<ResponseDto> DeletePostAsync(Guid id);
        Task<List<PostDto>> GetAllPostsAsync();
     

    }
}
