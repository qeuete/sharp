using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Magaz.Models;

public partial class TestContext : DbContext
{
    public TestContext()
    {
    }

    public TestContext(DbContextOptions<TestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderPosition> OrderPositions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-CMO5NDV\\SQLEXPRESS;Initial Catalog=test;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Category__6DB3A68A60E46907");

            entity.ToTable("Category");

            entity.Property(e => e.IdCategory).HasColumnName("ID_Category");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Orders__EC9FA95588A2B304");

            entity.Property(e => e.IdOrder).HasColumnName("ID_Order");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.TotalSum).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("User_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Orders__User_ID__4222D4EF");
        });

        modelBuilder.Entity<OrderPosition>(entity =>
        {
            entity.HasKey(e => e.IdOrderPosition).HasName("PK__OrderPos__FD3AA9D67D091E6C");

            entity.Property(e => e.IdOrderPosition).HasColumnName("ID_OrderPosition");
            entity.Property(e => e.OrderId).HasColumnName("Order_ID");
            entity.Property(e => e.ProductId).HasColumnName("Product_ID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderPositions)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderPosi__Order__44FF419A");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderPositions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderPosi__Produ__45F365D3");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__Product__522DE49619507CF5");

            entity.ToTable("Product");

            entity.Property(e => e.IdProduct).HasColumnName("ID_Product");
            entity.Property(e => e.CategoryId).HasColumnName("Category_ID");
            entity.Property(e => e.NameProduct).HasMaxLength(255);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductDescription).IsUnicode(false);
            entity.Property(e => e.ProductUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Size).HasMaxLength(10);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__3D5E1FD2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("PK__Roles__43DCD32DF1A95381");

            entity.Property(e => e.IdRole).HasColumnName("ID_Role");
            entity.Property(e => e.NameRole)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__ED4DE442BB897424");

            entity.Property(e => e.IdUser).HasColumnName("ID_User");
            entity.Property(e => e.LoginUs)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordUs)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
