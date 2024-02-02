using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ajay_Chouhan.Model;
using System;

namespace Ajay_Chouhan.Data
{
    public class Ajay_ChouhanContext : DbContext
    {
        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Ajay_Chouhan;Persist Security Info=True;user id=sa;password=12345;Integrated Security=true;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Books>().HasKey(a => a.Id);
            modelBuilder.Entity<Books>().HasOne(a => a.Author).WithMany(b => b.Bookss).HasForeignKey(c => c.AuthorId);
        }

        public DbSet<Author> Author { get; set; }
        public DbSet<Books> Books { get; set; }
    }
}