using Flower_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flower_Repository
{
    public static class DbSeeder
    {
        public static void Seed(this ModelBuilder builder)
        {

            // Seed Roles

            List<IdentityRole> roles = new List<IdentityRole>()
    {
        new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
        new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" }
    };

            builder.Entity<IdentityRole>().HasData(roles);

            // -----------------------------------------------------------------------------

            // Seed Users

            var passwordHasher = new PasswordHasher<ApplicationUser>();

            List<ApplicationUser> users = new List<ApplicationUser>()
    {
         // imporant: don't forget NormalizedUserName, NormalizedEmail 
                 new ApplicationUser {
                    UserName = "user2@hotmail.com",
                    NormalizedUserName = "USER2@HOTMAIL.COM",
                    Email = "user2@hotmail.com",
                    NormalizedEmail = "USER2@HOTMAIL.COM",
                    EmailConfirmed = true,
                   // Spend = 0,

                },

                new ApplicationUser {
                    UserName = "user3@hotmail.com",
                    NormalizedUserName = "USER3@HOTMAIL.COM",
                    Email = "user3@hotmail.com",
                    NormalizedEmail = "USER3@HOTMAIL.COM",
                    EmailConfirmed = true,
                   //  Spend = 0,
                },
    };


            builder.Entity<ApplicationUser>().HasData(users);

            ///----------------------------------------------------

            // Seed UserRoles


            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            // Add Password For All Users

            users[0].PasswordHash = passwordHasher.HashPassword(users[0], "123456789");
            users[1].PasswordHash = passwordHasher.HashPassword(users[1], "123456789");

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId =
            roles.First(q => q.Name == "Manager").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId =
            roles.First(q => q.Name == "Admin").Id
            });


            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);

        }
    }
}