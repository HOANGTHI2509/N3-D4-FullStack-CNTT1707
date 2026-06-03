using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartCampus.DAL.Models;

public partial class N3D4StudentMgmtContext : DbContext
{
    public N3D4StudentMgmtContext()
    {
    }

    public N3D4StudentMgmtContext(DbContextOptions<N3D4StudentMgmtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC07313ADF13");

            entity.Property(e => e.ClassName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MaxStudents).HasDefaultValue(40);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Đang mở");

            entity.HasOne(d => d.Course).WithMany(p => p.Classes)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classes__CourseI__6477ECF3");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Courses__3214EC0798537CA8");

            entity.HasIndex(e => e.CourseCode, "UQ__Courses__FC00E000569E837C").IsUnique();

            entity.Property(e => e.CourseCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CourseName).HasMaxLength(150);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Credits).HasDefaultValue(0);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC074BC079E8");

            entity.HasIndex(e => e.StudentCode, "UQ__Students__1FC88604C98AF5EE").IsUnique();

            entity.HasIndex(e => e.IdentityCardNumber, "UQ__Students__59CD512166976418").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Students__A9D10534375B32D7").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.IdentityCardNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Major).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Đang học");
            entity.Property(e => e.StudentCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
