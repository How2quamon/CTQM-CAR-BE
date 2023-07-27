﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using CTQM_CAR.Domain;
using Microsoft.EntityFrameworkCore;

namespace CTQM__CAR_API.CTQM_CAR.Infrastructure;

public partial class MEC_DBContext : DbContext
{
    public MEC_DBContext(DbContextOptions<MEC_DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarDetail> CarDetails { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.ToTable("Car");

            entity.Property(e => e.CarId)
                .ValueGeneratedNever()
                .HasColumnName("Car_ID");
            entity.Property(e => e.CarAmount).HasColumnName("Car_amount");
            entity.Property(e => e.CarClass)
                .IsRequired()
                .HasColumnName("Car_class");
            entity.Property(e => e.CarEngine)
                .IsRequired()
                .HasColumnName("Car_engine");
            entity.Property(e => e.CarModel)
                .IsRequired()
                .HasColumnName("Car_model");
            entity.Property(e => e.CarName)
                .IsRequired()
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
            entity.Property(e => e.Head1).IsRequired();
            entity.Property(e => e.Title1).IsRequired();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Cart");

            entity.Property(e => e.CartId)
                .ValueGeneratedNever()
                .HasColumnName("Cart_ID");
            entity.Property(e => e.CarId).HasColumnName("Car_ID");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_ID");
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
                .IsRequired()
                .HasColumnName("Customer_email");
            entity.Property(e => e.CustomerLicense)
                .IsRequired()
                .HasColumnName("Customer_license");
            entity.Property(e => e.CustomerName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Customer_name");
            entity.Property(e => e.CustomerPassword).HasColumnName("Customer_password");
            entity.Property(e => e.CustomerPhone)
                .IsRequired()
                .HasColumnName("Customer_phone");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}