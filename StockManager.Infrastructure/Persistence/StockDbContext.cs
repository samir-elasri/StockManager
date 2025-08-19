using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockManager.Domain.Entities;

namespace StockManager.Infrastructure.Persistence
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Email).HasMaxLength(200).IsRequired();
                b.Property(u => u.PasswordHash).IsRequired();
            });

            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Name).HasMaxLength(200).IsRequired();
                b.HasOne<User>().WithMany(u => u.Categories).HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200).IsRequired();
                b.Property(p => p.Price).HasPrecision(18, 2);
                b.HasMany(p => p.Variants).WithOne().HasForeignKey("ProductId").OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductVariant>(b =>
            {
                b.HasKey(v => v.Id);
                b.Property(v => v.Name).HasMaxLength(200).IsRequired();
                b.Property(v => v.Price).HasPrecision(18, 2);
            });
        }
    }
}
