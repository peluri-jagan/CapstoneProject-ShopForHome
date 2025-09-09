// Backend/Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using ShopForHomeBackend.Helpers;
using ShopForHomeBackend.Models;
using BCrypt.Net;

namespace ShopForHomeBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishlistItem> WishlistItems { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    // Composite key for CartItem (UserId + ProductId)
    builder.Entity<CartItem>()
        .HasKey(c => new { c.UserId, c.ProductId });

    // Composite key for WishlistItem (UserId + ProductId)
    builder.Entity<WishlistItem>()
        .HasKey(w => new { w.UserId, w.ProductId });

    // Composite key for OrderItem (OrderId + ProductId)
    builder.Entity<OrderItem>()
        .HasKey(oi => new { oi.OrderId, oi.ProductId });

    // Product ↔ Category
    builder.Entity<Product>()
        .HasOne(p => p.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Cascade);

    // Default role
    builder.Entity<User>()
        .Property(u => u.Role)
        .HasDefaultValue("User");

     
            // Seed categories
            builder.Entity<Category>().HasData(
        new Category { Id = 101, Name = "Furniture" },
        new Category { Id = 201, Name = "Home Décor" },
        new Category { Id = 301, Name = "Lighting" }
    );

    
    // User ↔ Coupon many-to-many
            builder.Entity<Coupon>()
        .HasMany(c => c.AssignedUsers)
        .WithMany(u => u.Coupons);
}

    }
}
