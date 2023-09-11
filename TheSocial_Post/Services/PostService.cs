using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheSocial_Post.Context;
using TheSocial_Post.Models;
using TheSocial_Post.Models.Dtos;
using TheSocial_Post.Services.IService;

namespace TheSocial_Post.Services
{
    public class PostService : IPostService
    {
        private readonly AppDbContext _context;
        private readonly ICommentInterface _commentInterface;
        private readonly IMapper _mapper;

        public PostService(AppDbContext context, ICommentInterface commentsInerface, IMapper mapper)
        {
            _context = context;
            _commentInterface = commentsInerface;
            _mapper = mapper;
        }
        // create a post
        public async Task<string> CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return "Post created successfully";
        }
        // delete a post
        public async Task<string> DeletePostAsync(Post post)
        {
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return "Post Removed Successfully";
        }
        // get all posts
        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts;
        }
        // get post by id
        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            var post = await _context.Posts.Where(x => x.PostId == postId).FirstOrDefaultAsync();
            if (post != null)
            {
                post.Comments = await _commentInterface.GetCommentsAsync(postId);
                return post;
            }
            return new Post();
        }
        // get posts by user id
        public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(Guid userId)
        {
            var result = await _context.Posts.Where(x => x.UserId == userId).ToListAsync();
            return result;
        }
        // update a post
        public async Task<string> UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return "Post Updated Successfully";
        }
    }
}