using Microsoft.EntityFrameworkCore;
using BulkywebRazor_temp.Models;
namespace BulkywebRazor_temp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Swallow & Semo", DisplayOrder = 1 }
                );
        }
    }
}
