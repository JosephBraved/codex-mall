using Bulky.Models.Models;
using Bulky.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace BulkyDataAccess.Data
{
	public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Company> Companies { get; set; }

		public DbSet<ApplicationUser> ApplicationUsers {  get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<Product>().HasData(
				new Product
				{
					Id = 1,
					Title="Fortune of Time",
					Author = "Billy Sparks",
					Description = "Some sort of Lorem Ipsum",
					ISBN = "SWD999001",
					Price = 90,
					Price50 = 85,
					Price100 = 80,
					CategoryId = 4,
					ImageUrl = ""
				}
				);
        }


    }
}
