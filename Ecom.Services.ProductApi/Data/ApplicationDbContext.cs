using Ecom.Services.ProductApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services.ProductApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "IPhone 12",
                    Price = 1000,
                    Description = "This is the description of iPhone 12",
                    CategoryName = "Smart Phone",
                    ImageUrl = "product-1.png"
                },
                new Product
                {
                    Id = 2,
                    Name = "Samsung Galaxy S20",
                    Price = 900,
                    Description = "This is the description of Samsung Galaxy S20",
                    CategoryName = "Smart Phone",
                    ImageUrl = "product-2.png"
                },
                new Product
                {
                    Id = 3,
                    Name = "Huawei P30",
                    Price = 800,
                    Description = "This is the description of Huawei P30",
                    CategoryName = "Smart Phone",
                    ImageUrl = "product-3.png"
                },
                new Product
                {
                    Id = 4,
                    Name = "Xiaomi Mi 10",
                    Price = 700,
                    Description = "This is the description of Xiaomi Mi 10",
                    CategoryName = "Smart Phone",
                    ImageUrl = "product-4.png"
                },
                new Product
                {
                    Id = 5,
                    Name = "OnePlus 8",
                    Price = 600,
                    Description = "This is the description of OnePlus 8",
                    CategoryName = "Smart Phone",
                    ImageUrl = "product-5.png"
                }
            );
        }
    }
}
