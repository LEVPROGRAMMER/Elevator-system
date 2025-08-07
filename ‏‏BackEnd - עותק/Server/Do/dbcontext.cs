using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Server.Do;

public partial class dbcontext : DbContext
{
    public dbcontext()
    {
    }

    public dbcontext(DbContextOptions<dbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Elevator> Elevators { get; set; }

    public virtual DbSet<ElevatorCall> ElevatorCalls { get; set; }

    public virtual DbSet<ElevatorCallAssignment> ElevatorCallAssignments { get; set; }

    public virtual DbSet<TargetFloor> TargetFloors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4BA570G\\SQLEXPRESS;Initial Catalog=debts_system;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Building__3214EC07C3FA1C91");

            entity.ToTable("Building");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Buildings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Building__UserId__5EBF139D");
        });

        modelBuilder.Entity<Elevator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Elevator__3214EC078FFE0274");

            entity.ToTable("Elevator");

            entity.HasOne(d => d.Building).WithMany(p => p.Elevators)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Elevator__Buildi__08B54D69");
        });

        modelBuilder.Entity<ElevatorCall>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Elevator__3214EC07974BF17F");

            entity.ToTable("ElevatorCall");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CallTime).HasColumnType("datetime");

            entity.HasOne(d => d.Building).WithMany(p => p.ElevatorCalls)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK__ElevatorC__Build__6754599E");
        });

        modelBuilder.Entity<ElevatorCallAssignment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Elevator__3214EC076D3084D0");

            entity.ToTable("ElevatorCallAssignment");

            entity.HasIndex(e => new { e.Id, e.ElevatorId }, "UQ__Elevator__FB0F4FF36B4544B7").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AssignmentTime).HasColumnType("datetime");

            entity.HasOne(d => d.Elevator).WithMany(p => p.ElevatorCallAssignments)
                .HasForeignKey(d => d.ElevatorId)
                .HasConstraintName("FK__ElevatorC__Eleva__0C85DE4D");
        });

        modelBuilder.Entity<TargetFloor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TargetFl__3214EC07922E3729");

            entity.HasOne(d => d.Elevator).WithMany(p => p.TargetFloors)
                .HasForeignKey(d => d.ElevatorId)
                .HasConstraintName("FK__TargetFlo__Floor__0F624AF8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07C8190C33");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
