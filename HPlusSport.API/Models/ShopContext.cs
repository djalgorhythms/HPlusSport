using HPlusSport.API.Controllers;
using Microsoft.EntityFrameworkCore;

namespace HPlusSport.API.Models
{
    public class ShopContext : DbContext
    {

        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        // tells system how products and categories are related to each other

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId);

            modelBuilder.Seed();   // uses ModelBuilderExtensions, imported from sample code
        }
        public DbSet<Product> Products
        {
            get; set;
        }

        public DbSet<Category> Categories
        {
            get; set;
        }
    }

   
}
