using System;
using System.Collections.Generic;
using CTQM_CAR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CTQM_CAR.Infrastructure;

public partial class MecDbContext : DbContext
{
    public MecDbContext()
    {
    }

    public MecDbContext(DbContextOptions<MecDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarDetail> CarDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=MEC_DB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("Car");

            entity.Property(e => e.CarId)
                .ValueGeneratedNever()
                .HasColumnName("Car_ID");
            entity.Property(e => e.CarAmount).HasColumnName("Car_amount");
            entity.Property(e => e.CarClass).HasColumnName("Car_class");
            entity.Property(e => e.CarEngine).HasColumnName("Car_engine");
            entity.Property(e => e.CarModel).HasColumnName("Car_model");
            entity.Property(e => e.CarName)
                .HasMaxLength(50)
                .HasColumnName("Car_name");
            entity.Property(e => e.CarPrice).HasColumnName("Car_price");
        });

        modelBuilder.Entity<CarDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId);

            entity.ToTable("CarDetail");

            entity.Property(e => e.DetailId)
                .ValueGeneratedNever()
                .HasColumnName("Detail_ID");
            entity.Property(e => e.CarId).HasColumnName("Car_ID");

            entity.HasOne(d => d.Car).WithMany(p => p.CarDetails).HasForeignKey(d => d.CarId);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("Cart_ID");
            entity.Property(e => e.CarId).HasColumnName("Car_ID");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");

            entity.HasOne(d => d.Car).WithMany(p => p.Carts).HasForeignKey(d => d.CarId);

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasForeignKey(d => d.CustomerId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("Customer_ID");
            entity.Property(e => e.CustomerAddress).HasColumnName("Customer_address");
            entity.Property(e => e.CustomerDate).HasColumnName("Customer_date");
            entity.Property(e => e.CustomerEmail)
                .HasDefaultValueSql("(N'')")
                .HasColumnName("Customer_email");
            entity.Property(e => e.CustomerLicense).HasColumnName("Customer_license");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("Customer_name");
            entity.Property(e => e.CustomerPassword).HasColumnName("Customer_password");
            entity.Property(e => e.CustomerPhone).HasColumnName("Customer_phone");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("Order_ID");
            entity.Property(e => e.CarId).HasColumnName("Car_ID");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
            entity.Property(e => e.OrderDate).HasColumnName("Order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("Order_status");
            entity.Property(e => e.TotalPrice).HasColumnName("Total_price");

            entity.HasOne(d => d.Car).WithMany(p => p.Orders).HasForeignKey(d => d.CarId);

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasForeignKey(d => d.CustomerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
