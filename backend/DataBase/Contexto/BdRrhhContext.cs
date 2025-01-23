using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.Contexto;

public partial class BdRrhhContext : DbContext
{
    public BdRrhhContext()
    {
    }

    public BdRrhhContext(DbContextOptions<BdRrhhContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Empresa> Empresa { get; set; }
    public virtual DbSet<Empleado> Empleado { get; set; }
    public virtual DbSet<Asistencia> Asistencia{ get; set; }
    public virtual DbSet<LLegadaTarde> LLegadaTarde { get; set; }

    public virtual DbSet<Vacaciones> Vacaciones { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=bd_rrhh;Username=postgres;Password=2242");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empleado>().ToTable("Empleado");
        modelBuilder.Entity<Empresa>().ToTable("Empresa");
        modelBuilder.Entity<Asistencia>().ToTable("Asistencia");
        modelBuilder.Entity<LLegadaTarde>().ToTable("LLegadaTarde");
        modelBuilder.Entity<Vacaciones>().ToTable("Vacaciones");
    

    
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Models.Empresa>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
            .ValueGeneratedOnAdd();
           
           
        });
    }



}

/*protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Empleado>().ToTable("Empleado");
    modelBuilder.Entity<Empresa>().ToTable("Empresa");
    modelBuilder.Entity<Asistencia>().ToTable("Asistencia");
    modelBuilder.Entity<LLegadaTarde>().ToTable("LLegadaTarde");
    modelBuilder.Entity<Vacaciones>().ToTable("Vacaciones");

    base.OnModelCreating(modelBuilder);

    // Configuración de la entidad Empresa
    modelBuilder.Entity<Models.Empresa>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
    });

    // Configuración de la entidad Empleado
    modelBuilder.Entity<Models.Empleado>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
    });

    // Configuración de la entidad Vacaciones
    modelBuilder.Entity<Models.Vacaciones>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.FechaInicio).IsRequired();
        entity.Property(e => e.FechaFin).IsRequired();
    });

    // Configuración de la entidad Asistencia
    modelBuilder.Entity<Models.Asistencia>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.FechaAsistencia).IsRequired();
    });

    // Configuración de la entidad LLegadaTarde
    modelBuilder.Entity<Models.LLegadaTarde>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Fecha).IsRequired();
        entity.Property(e => e.Motivo).HasMaxLength(500);
    });

    // Relaciones entre las entidades
    modelBuilder.Entity<Models.Vacaciones>()
        .HasOne(v => v.Empleado)
        .WithMany(e => e.Vacaciones)
        .HasForeignKey(v => v.EmpleadoId);

    modelBuilder.Entity<Models.Asistencia>()
        .HasOne(a => a.Empleado)
        .WithMany(e => e.Asistencias)
        .HasForeignKey(a => a.EmpleadoId);

    modelBuilder.Entity<Models.LLegadaTarde>()
        .HasOne(l => l.Empleado)
        .WithMany(e => e.LLegadasTarde)
        .HasForeignKey(l => l.EmpleadoId);
}
*/ 

