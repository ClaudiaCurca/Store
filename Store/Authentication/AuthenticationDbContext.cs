using Microsoft.EntityFrameworkCore;
using Store.Models;

namespace Store.Authentication
{
	public class AuthenticationDbContext : DbContext
	{
		public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
		{ }
		public DbSet<UserAccount> UserAccounts { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	    public DbSet<Store.Models.RegistrationViewModel> RegistrationViewModel { get; set; } = default!;
	}
}
