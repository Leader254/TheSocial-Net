using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheSocial_Post.Models;
using TheSocial_Post.Models.Dtos;

namespace TheSocial_Post.Services.IService
{
    public interface IPostService
    {
        // Get all posts
        Task<IEnumerable<Post>> GetAllPostsAsync();
        //get post by id
        Task<Post> GetPostByIdAsync(Guid postId);
        //get posts by user id
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId);
        //create post
        Task<string> CreatePostAsync(Post post);
        //update post
        Task<string> UpdatePostAsync(Post post);
        //delete post
        Task<string> DeletePostAsync(Post post);
    }
}