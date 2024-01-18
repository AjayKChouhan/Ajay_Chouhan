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
            modelBuilder.Entity<Category>().HasKey(a => a.Id);
            modelBuilder.Entity<Product>().HasKey(a => a.Id);
            modelBuilder.Entity<DemoProduct>().HasKey(a => a.Id);
            modelBuilder.Entity<Product>().HasOne(a => a.Category).WithMany(b => b.Products).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<DemoProduct>().HasOne(a => a.Product).WithMany(b => b.DemoProducts).HasForeignKey(c => c.ProductId);
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<DemoProduct> DemoProduct { get; set; }
    }
}