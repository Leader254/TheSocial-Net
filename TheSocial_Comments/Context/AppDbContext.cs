using Microsoft.EntityFrameworkCore;
using TheSocial_Comments.Models;

namespace TheSocial_Comments.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Comment> Comments { get; set; }
    }
}
