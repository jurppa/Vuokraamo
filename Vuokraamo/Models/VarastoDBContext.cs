using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Vuokraamo.Models
{
    public partial class VarastoDBContext : DbContext
    {
       

        public VarastoDBContext(DbContextOptions<VarastoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Admin");

                entity.Property(e => e.AdminId).ValueGeneratedOnAdd();

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(100);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Cart__CustomerID__619B8048");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ProductID_ID");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tel).HasMaxLength(100);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.DueDate).HasColumnType("date");

                entity.Property(e => e.Paid).HasDefaultValueSql("((0))");

                entity.Property(e => e.RentalId).HasColumnName("RentalID");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Done).HasDefaultValueSql("((0))");

                entity.Property(e => e.Message1)
                    .HasMaxLength(500)
                    .HasColumnName("Message");

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__Custome__6D0D32F4");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Category).HasMaxLength(100);

                entity.Property(e => e.Condition).HasMaxLength(200);

                entity.Property(e => e.Deductible).HasColumnName("deductible");

                entity.Property(e => e.ImageUrl).HasMaxLength(300);

                entity.Property(e => e.Info).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Rental>(entity =>
            {
                entity.ToTable("Rental");

                entity.Property(e => e.Rentaldate).HasColumnType("date");

                entity.Property(e => e.Returndate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Fk_CustomerId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Rentals)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Productid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
