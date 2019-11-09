using System;
using ItemManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
    
       
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
