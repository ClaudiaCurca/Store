using Microsoft.EntityFrameworkCore;
using Store.Models;
using System.Reflection.Metadata;

namespace Store
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating( modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
    }
}
