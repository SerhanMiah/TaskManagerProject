using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Model.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BackendApp.Models;
using BackendApp.Models.Task;
using BackendApp.Data.Seed;
using Microsoft.AspNetCore.Identity;

namespace BackendApp.Data
{
   public class TaskManagerContext : IdentityDbContext<ApplicationUser> 
{
    public TaskManagerContext(DbContextOptions<TaskManagerContext> options)
        : base(options)
    {
    }

     public TaskManagerContext(DbContextOptions<TaskManagerContext> options, IPasswordHasher<ApplicationUser> passwordHasher) 
            : base(options)
        {
            this.PasswordHasher = passwordHasher;
        }

    public IPasswordHasher<ApplicationUser> PasswordHasher { get; }

    // DbSets
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Priority> Priorities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var (users, adminId, userId) = UserSeedData.Seed(modelBuilder, this.PasswordHasher);

        // One-to-Many between TaskItem and Project
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId);

        // One-to-Many between TaskItem and Priority
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Priority)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.PriorityId);

        // One-to-Many between TaskItem and Tag
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Tag)
            .WithMany(tag => tag.Tasks)
            .HasForeignKey(t => t.TagId);

        // One-to-Many between TaskItem and Comment
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.Task)
            .WithMany(t => t.Comments)
            .HasForeignKey(c => c.TaskItemId);

        // One-to-Many between Comment and ApplicationUser
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.PostedBy)
            .WithMany() // Add a navigation property in ApplicationUser if needed
            .HasForeignKey(c => c.PostedById);

            // Seed Tags
    var tags = new List<Tag>
    {
        new Tag { Id = 1, Name = "Urgent" },
        new Tag { Id = 2, Name = "Review" },
        new Tag { Id = 3, Name = "Documentation" }
    };

    modelBuilder.Entity<Tag>().HasData(tags);

    // Seed Projects
    var projects = new List<Project>
    {
        new Project { Id = 1, Name = "Frontend Development" },
        new Project { Id = 2, Name = "Backend Development" },
        new Project { Id = 3, Name = "Deployment" }
    };

    modelBuilder.Entity<Project>().HasData(projects);

    // Seed Priorities
    var priorities = new List<Priority>
    {
        new Priority { Id = 1, Level = PriorityLevel.High },
        new Priority { Id = 2, Level = PriorityLevel.Medium },
        new Priority { Id = 3, Level = PriorityLevel.Low }
    };

    modelBuilder.Entity<Priority>().HasData(priorities);

    // Seed Tasks
    var tasks = new List<TaskItem>
    {
        new TaskItem 
        {
            Id = 1,
            Title = "Design Homepage",
            Description = "Design the main landing page using Figma.",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(7),
            ProjectId = projects[0].Id,
            PriorityId = priorities[0].Id,
            TagId = tags[0].Id
        },
        new TaskItem 
        {
            Id = 2,
            Title = "API Implementation",
            Description = "Create RESTful API for the user management system.",
            IsCompleted = false,
            DueDate = DateTime.Now.AddDays(10),
            ProjectId = projects[1].Id,
            PriorityId = priorities[1].Id,
            TagId = tags[1].Id
        },
    };

    modelBuilder.Entity<TaskItem>().HasData(tasks);

    // Seed Comments
    var comments = new List<Comment>
    {
        new Comment 
        {
            Id = 1,
            Content = "Initial design completed, waiting for feedback.",
            DatePosted = DateTime.Now.AddDays(-2),
            TaskItemId = tasks[0].Id,
            PostedById = adminId
        },
        new Comment 
        {
            Id = 2,
            Content = "API endpoints defined, starting with the implementation.",
            DatePosted = DateTime.Now.AddDays(-3),
            TaskItemId = tasks[1].Id,
            PostedById = userId
        },
    };

    modelBuilder.Entity<Comment>().HasData(comments);
    }
}
}