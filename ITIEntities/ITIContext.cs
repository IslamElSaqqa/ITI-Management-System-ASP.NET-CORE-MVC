using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITIEntities
{
    public class ITIContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=ITI46MVC;Integrated Security=True; TrustServerCertificate=True");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            base.OnConfiguring(optionsBuilder);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<StudentCourse>(sc =>
                {
                    sc.HasKey(sc => new { sc.CrsId, sc.StdId });
                    
                });
            modelBuilder.Entity<Course>(c =>
            {
                c.HasKey(c => c.CrsId);
                c.Property(c => c.CrsId).ValueGeneratedNever();
                c.Property(c => c.CrsName)
                .HasMaxLength(50)
                .IsRequired();
            });
                   
        }
    }
}
