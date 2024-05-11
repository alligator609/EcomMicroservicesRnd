using Ecom.Services.CouponApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Services.CouponApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    Id = 1,
                    Code = "10OFF",
                    DiscountAmount = 10,
                    MinAmount = 100
                },
                new Coupon
                {
                    Id = 2,
                    Code = "20OFF",
                    DiscountAmount = 20,
                    MinAmount = 200
                },
                new Coupon
                {
                    Id = 3,
                    Code = "30OFF",
                    DiscountAmount = 30,
                    MinAmount = 300
                }
            );
        }
    }
}
