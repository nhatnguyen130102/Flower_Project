using Flower_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Emit;

namespace Flower_Repository
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Seed();
            //SeedRoles(builder);
            //SeedUser(builder);
        }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<FlashSale> FlashSales { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetails> BillDetails { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<StockReceivedDocket> StockReceivedDockets { get; set; }
        public DbSet<StockReceivedDocketDetails> StockReceivedDocketDetails { get; set; }
        public DbSet<MaterialWarehouse> MaterialWarehouses { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }
        public DbSet<ManagerUserProduct> ManagerUserProducts { get; set; }

      
        //private static void SeedRoles(ModelBuilder builder)
        //{
        //    builder.Entity<IdentityRole>().HasData(
        //        new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
        //        new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
        //        new IdentityRole() { Name = "Manager", ConcurrencyStamp = "3", NormalizedName = "Manager" }
        //        );
        //}

        //private static void SeedUser(ModelBuilder builder)
        //{
        //    var hasher = new PasswordHasher<IdentityUser>();

        //    builder.Entity<IdentityUser>().HasData(
        //    new IdentityUser
        //    {
        //        UserName = "Admin1234@gmail",
        //        NormalizedUserName = "Admin1234@gmail",
        //        PasswordHash = hasher.HashPassword(null, "Admin1234@gmail"),
        //        Email = "Admin1234@gmail",
        //        EmailConfirmed = true,
        //        NormalizedEmail = "Admin1234@gmail",
        //        LockoutEnabled = false,
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    });
        //    List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            

        //}
    }
}
