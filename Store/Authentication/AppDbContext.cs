using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Store.Models;

namespace Store.Authentication
{
    public class AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
      : IdentityDbContext<AppUser>(options)
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("AuthenticationConnectionString"));
        }
        public DbSet<AppUser> Users { get; set; }

    }
}
