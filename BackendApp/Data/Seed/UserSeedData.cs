using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BackendApp.Models;
using BackendApp.Model.Auth;

namespace BackendApp.Data.Seed
{
public class UserSeedData
{
    public static (List<ApplicationUser>, string adminId, string userId) Seed(ModelBuilder builder, IPasswordHasher<ApplicationUser> passwordHasher)
    {
        var adminId = Guid.NewGuid().ToString();
        var userId = Guid.NewGuid().ToString();

        // Seeding an admin user
        var admin = new ApplicationUser
        {
            Id = adminId,
            UserName = "admin",
            Email = "admin@task.com",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "ADMIN@task.COM",
            EmailConfirmed = true,
            FirstName = "Admin",
            LastName = "User",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };
        admin.PasswordHash = passwordHasher.HashPassword(admin, "Admin123!");
        builder.Entity<ApplicationUser>().HasData(admin);

        // Seeding a regular user
        var user = new ApplicationUser
        {
            Id = userId,
            UserName = "user",
            Email = "user@task.com",
            NormalizedUserName = "USER",
            NormalizedEmail = "USER@task.COM",
            EmailConfirmed = true,
            FirstName = "Regular",
            LastName = "User",
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };
        user.PasswordHash = passwordHasher.HashPassword(user, "User123!");
        builder.Entity<ApplicationUser>().HasData(user);

        return (new List<ApplicationUser> { admin, user }, adminId, userId);
    }
    
    }
}