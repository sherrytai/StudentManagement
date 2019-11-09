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

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Shop> Shops { get; set; }

        public DbSet<Product> Products { get; set; }

        /*
         * https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli#create-a-migration
         * Commands to add migrations:
         * windows(Powershell, https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/powershell):
         *  Add-Migration AddPhoneNumber
         *  Update-Database
         *  Remove-Migration
         */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Student");
        }
    }
}
