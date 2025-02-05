using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RecursosHumanos.Migrations
{
    /// <inheritdoc />
    public partial class agregoInasistencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Paso 1: Crear una nueva columna temporal
            migrationBuilder.AddColumn<int>(
                name: "TipoTemp",
                table: "PermisoAusencia",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Paso 2: Copiar los datos a la nueva columna temporal
            migrationBuilder.Sql("UPDATE \"PermisoAusencia\" SET \"TipoTemp\" = CAST(\"Tipo\" AS integer)");

            // Paso 3: Eliminar la columna original
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "PermisoAusencia");

            // Paso 4: Renombrar la columna temporal
            migrationBuilder.RenameColumn(
                name: "TipoTemp",
                table: "PermisoAusencia",
                newName: "Tipo");

            // Otros cambios de la migración
            migrationBuilder.AddColumn<int>(
                name: "InasistenciaId",
                table: "Documentacion",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Inasistencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Detalles = table.Column<string>(type: "text", nullable: false),
                    IdEmpleado = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inasistencia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentacion_InasistenciaId",
                table: "Documentacion",
                column: "InasistenciaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documentacion_Inasistencia_InasistenciaId",
                table: "Documentacion",
                column: "InasistenciaId",
                principalTable: "Inasistencia",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documentacion_Inasistencia_InasistenciaId",
                table: "Documentacion");

            migrationBuilder.DropTable(
                name: "Inasistencia");

            migrationBuilder.DropIndex(
                name: "IX_Documentacion_InasistenciaId",
                table: "Documentacion");

            migrationBuilder.DropColumn(
                name: "InasistenciaId",
                table: "Documentacion");

            migrationBuilder.AlterColumn<string>(
                name: "Tipo",
                table: "PermisoAusencia",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}