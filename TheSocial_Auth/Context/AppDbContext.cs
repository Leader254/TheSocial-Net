using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheSocial_Auth.Models;

namespace TheSocial_Auth.Context
{
    public class AppDbContext:IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options){ }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
