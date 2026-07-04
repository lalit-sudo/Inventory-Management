using Inventory_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryManagement.Models
{
    public class InventoryManagementContext : DbContext
    {
        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderItem> OrderItems{ get; set; }
        public DbSet<Category> Categories{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = InventoryManagementDB; 
            Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = False; 
            Application Intent = ReadWrite; Multi Subnet Failover = False; Command Timeout = 30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configuring Product Entity
            modelBuilder.Entity<Product>()
                .HasKey(p => p.ProductId);
            
            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName)
                .IsUnique();

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Quantity)
                .IsRequired();

            //Configuring Category Entity
            modelBuilder.Entity<Category>()
                .HasKey(c => c.CategoryId);

            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Category>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            // Product-Category Relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            //Configuring Order Entity
            modelBuilder.Entity<Order>()
                .HasKey(o => o.OrderId);

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.OrderDate)
                .IsRequired();

            //Configuring OrderItems Entity
            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.OrderItemId);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Quantity)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .IsRequired()
                .HasPrecision(10, 2);

            // Order-OrderItems Relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            //Product-OrderItems Relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
        }

    }
}
