using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Database.Models;

public partial class RailwayTicketDbContext : DbContext
{
    public RailwayTicketDbContext()
    {
    }

    public RailwayTicketDbContext(DbContextOptions<RailwayTicketDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carriage> Carriages { get; set; }

    public virtual DbSet<MainRoute> MainRoutes { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RoleHasPermission> RoleHasPermissions { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<SubRoute> SubRoutes { get; set; }

    public virtual DbSet<Township> Townships { get; set; }

    public virtual DbSet<Train> Trains { get; set; }

    public virtual DbSet<TrainType> TrainTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=localhost;Database=RailwayTicket;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carriage>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.TypeEn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TypeEN");
            entity.Property(e => e.TypeMm)
                .HasMaxLength(255)
                .HasColumnName("TypeMM");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<MainRoute>(entity =>
        {
            entity.ToTable("Main_Routes");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Duration).HasMaxLength(255);
            entity.Property(e => e.RouteName).HasMaxLength(255);
            entity.Property(e => e.RouteType).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.GuardName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("Refresh_Token");

            entity.Property(e => e.Expire).HasColumnType("datetime");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameMm)
                .HasMaxLength(255)
                .HasColumnName("NameMM");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleHasPermission>(entity =>
        {
            entity.ToTable("Role_Has_Permissions");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Lat)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("lat");
            entity.Property(e => e.Long)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("long");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.StationNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<SubRoute>(entity =>
        {
            entity.ToTable("Sub_Routes");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Distance)
                .HasMaxLength(191)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(191)
                .IsUnicode(false);
            entity.Property(e => e.FirstClassFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LifeInsurance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OrdinaryClassFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SleeperClassFee).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpperClassFee).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Township>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.NameMm)
                .HasMaxLength(255)
                .HasColumnName("NameMM");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Train>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.ManufactureBy)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Power)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartedUsingDate).HasColumnType("datetime");
            entity.Property(e => e.TrainId).HasColumnName("Train_Id");
            entity.Property(e => e.TrainName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TrainNo).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TrainType>(entity =>
        {
            entity.ToTable("Train_Types");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("Password_Hash");
            entity.Property(e => e.SaltKey)
                .HasMaxLength(255)
                .HasColumnName("SALT_KEY");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("(NULL)");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
