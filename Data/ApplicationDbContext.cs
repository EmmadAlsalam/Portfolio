using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial skills
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "ASP.NET Core", Proficiency = 90, Category = "Backend", YearsOfExperience = 5 },
                new Skill { Id = 2, Name = "C#", Proficiency = 90, Category = "Backend", YearsOfExperience = 5 },
                new Skill { Id = 3, Name = "JavaScript", Proficiency = 85, Category = "Frontend", YearsOfExperience = 4 },
                new Skill { Id = 4, Name = "React", Proficiency = 80, Category = "Frontend", YearsOfExperience = 3 },
                new Skill { Id = 5, Name = "SQL Server", Proficiency = 85, Category = "Database", YearsOfExperience = 4 }
            );

            // Configure Project entity
            modelBuilder.Entity<Project>()
                .Property(p => p.Technologies)
                .HasColumnType("nvarchar(max)");

            // Configure BlogPost entity
            modelBuilder.Entity<BlogPost>()
                .Property(b => b.Slug)
                .IsRequired();
        }
    }
}
