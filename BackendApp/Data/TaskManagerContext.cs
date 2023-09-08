using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Model.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackendApp.Models;
using BackendApp.Models.Task;

namespace BackendApp.Data
{
    public class TaskManagerContext : IdentityDbContext<ApplicationUser> 
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<ApplicationUser> applicationUsers { get; set;}
        public DbSet<TaskItem> TaskItems { get; set;}
        public DbSet<Project> Projects { get; set;}

        public DbSet<Comment> Comments { get; set;}

        public DbSet<Tag> tags { get; set;}

        public DbSet<Priority> priorities { get; set; }

    }
}