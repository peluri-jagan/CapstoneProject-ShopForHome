using Microsoft.AspNetCore.Identity;
using ShopForHomeBackend.Helpers;
using ShopForHomeBackend.Models;

namespace ShopForHomeBackend.Data
{
    public static class Seeder
    {
        public static void Seed(AppDbContext db)
        {
            db.Database.EnsureCreated();

            // Check if the admin user exists
            if (!db.Users.Any(u => u.Email == "admin01@admin.com"))
            {
                var passwordHasher = new PasswordHasher<User>();

                var admin = new User
                {
                    Username = "admin01",
                    Email = "admin01@admin.com",
                    Role = "Admin",
                    PasswordHash = PasswordHasher.HashPassword("admin01")
                };

                // Hash the password
               
                db.Users.Add(admin);
                db.SaveChanges();
            }
        }
    }
}
