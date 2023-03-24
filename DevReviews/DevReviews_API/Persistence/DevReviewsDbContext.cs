using DevReviews_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevReviews_API.Persistence
{
    public class DevReviewsDbContext : DbContext
    {
        public DevReviewsDbContext(DbContextOptions<DevReviewsDbContext> options) : base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product> ProductReviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(p => {
                p.ToTable("Tb_Product");
                p.HasKey(p => p.Id);
            });
            modelBuilder.Entity<ProductReviews>(pr => {
                pr.ToTable("Tb_ProductReviews");
                pr.HasKey(p => p.Id);
                pr.Property(p => p.Author)
                .HasMaxLength(50);
            });
        }
    }
}
