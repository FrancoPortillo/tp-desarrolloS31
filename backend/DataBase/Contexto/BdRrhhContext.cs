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
            //como hago para poner a llos demas empleado vacaciones etc
           
        });
    }



}