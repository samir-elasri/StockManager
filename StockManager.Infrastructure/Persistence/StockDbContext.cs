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
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Email).HasMaxLength(200).IsRequired();
                b.Property(u => u.PasswordHash).IsRequired();
                b.HasMany(u => u.Products).WithOne(p => p.User).HasForeignKey(p => p.UserId);
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).HasMaxLength(200).IsRequired();
                b.Property(p => p.Price).HasPrecision(18, 2);
            });
        }
    }
}
