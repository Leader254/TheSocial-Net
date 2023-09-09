using Microsoft.EntityFrameworkCore;
using TheSocial_EmailService.Models;

namespace TheSocial_EmailService.Context
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<EmailLoggers> EmailLoggers { get; set; }
    }
}
