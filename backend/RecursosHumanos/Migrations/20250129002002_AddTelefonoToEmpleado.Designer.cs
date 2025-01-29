﻿// <auto-generated />
using System;
using Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecursosHumanos.Migrations
{
    [DbContext(typeof(BdRrhhContext))]
    [Migration("20250129002002_AddTelefonoToEmpleado")]
    partial class AddTelefonoToEmpleado
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Data.Models.Asistencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("integer");

                    b.Property<bool>("Presente")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdEmpleado");

                    b.ToTable("Asistencia");
                });

            modelBuilder.Entity("Data.Models.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Edad")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("integer");

                    b.Property<int>("Legajo")
                        .HasColumnType("integer");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Puesto")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdEmpresa");

                    b.ToTable("Empleado");
                });

            modelBuilder.Entity("Data.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Empresa");
                });

            modelBuilder.Entity("Data.Models.LLegadaTarde", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("integer");

                    b.Property<int>("MinutosTarde")
                        .HasColumnType("integer");

                    b.Property<string>("Motivo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdEmpleado");

                    b.ToTable("LLegadaTarde");
                });

            modelBuilder.Entity("Data.Models.Vacaciones", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Aprobado")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("IdEmpleado")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("IdEmpleado");

                    b.ToTable("Vacaciones");
                });

            modelBuilder.Entity("Data.Models.Asistencia", b =>
                {
                    b.HasOne("Data.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("Data.Models.Empleado", b =>
                {
                    b.HasOne("Data.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("IdEmpresa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("Data.Models.LLegadaTarde", b =>
                {
                    b.HasOne("Data.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });

            modelBuilder.Entity("Data.Models.Vacaciones", b =>
                {
                    b.HasOne("Data.Models.Empleado", "Empleado")
                        .WithMany()
                        .HasForeignKey("IdEmpleado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empleado");
                });
#pragma warning restore 612, 618
        }
    }
}
