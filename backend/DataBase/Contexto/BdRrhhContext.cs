using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Data.Models;

namespace Data.Contexto
{
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
        public virtual DbSet<Asistencia> Asistencia { get; set; }
        public virtual DbSet<LLegadaTarde> LLegadaTarde { get; set; }
        public virtual DbSet<Vacaciones> Vacaciones { get; set; }
    }
}