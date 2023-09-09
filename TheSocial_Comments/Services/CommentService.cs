using Microsoft.EntityFrameworkCore;
using TheSocial_Comments.Context;
using TheSocial_Comments.Models;
using TheSocial_Comments.Services.IServices;

namespace TheSocial_Comments.Services
{
    public class CommentService : ICommentsInterface
    {
        private readonly AppDbContext _context;

        public CommentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return "Comment created successfully";
        }

        public async Task<string> DeleteCommentAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return "Comment Removed Successfully";
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _context.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentByIdAsync(Guid commentId)
        {
            return await _context.Comments.Where(x => x.CommentId == commentId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(Guid postId)
        {
            return await _context.Comments.Where(x => x.PostId == postId).ToListAsync();
        }
        public async Task<string> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return "Comment Updated Successfully";
        }
    }
}
