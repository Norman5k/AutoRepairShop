using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AutoRepairShop.Models;

public partial class AutomasterContext : DbContext
{
    public AutomasterContext()
    {
    }

    public AutomasterContext(DbContextOptions<AutomasterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Repair> Repairs { get; set; }

    public virtual DbSet<Worker> Workers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=automaster;Username=postgres;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cars_pkey");

            entity.ToTable("cars");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Admission).HasColumnName("admission");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.Defect).HasColumnName("defect");
            entity.Property(e => e.Ending).HasColumnName("ending");
            entity.Property(e => e.Mark).HasColumnName("mark");
            entity.Property(e => e.RegNumber).HasColumnName("reg_number");
        });

        modelBuilder.Entity<Repair>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("repairs_pkey");

            entity.ToTable("repairs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarId).HasColumnName("car_id");
            entity.Property(e => e.RepairType).HasColumnName("repair_type");
            entity.Property(e => e.WorkerId).HasColumnName("worker_id");

            entity.HasOne(d => d.Car).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("repairs_car_id_fkey");

            entity.HasOne(d => d.Worker).WithMany(p => p.Repairs)
                .HasForeignKey(d => d.WorkerId)
                .HasConstraintName("repairs_worker_id_fkey");
        });

        modelBuilder.Entity<Worker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("workers_pkey");

            entity.ToTable("workers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Recruitment).HasColumnName("recruitment");
            entity.Property(e => e.Spec).HasColumnName("spec");
            entity.Property(e => e.Surname).HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
