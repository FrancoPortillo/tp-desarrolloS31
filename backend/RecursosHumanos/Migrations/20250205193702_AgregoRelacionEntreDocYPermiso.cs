using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecursosHumanos.Migrations
{
    /// <inheritdoc />
    public partial class AgregoRelacionEntreDocYPermiso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar columna PermisoAusenciaId a la tabla Documentacion
            migrationBuilder.AddColumn<int>(
                name: "PermisoAusenciaId",
                table: "Documentacion",
                type: "integer",
                nullable: true);

            // Crear índice en la columna PermisoAusenciaId de la tabla Documentacion
            migrationBuilder.CreateIndex(
                name: "IX_Documentacion_PermisoAusenciaId",
                table: "Documentacion",
                column: "PermisoAusenciaId");

            // Agregar clave foránea entre Documentacion.PermisoAusenciaId y PermisoAusencia.Id
            migrationBuilder.AddForeignKey(
                name: "FK_Documentacion_PermisoAusencia_PermisoAusenciaId",
                table: "Documentacion",
                column: "PermisoAusenciaId",
                principalTable: "PermisoAusencia",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar clave foránea
            migrationBuilder.DropForeignKey(
                name: "FK_Documentacion_PermisoAusencia_PermisoAusenciaId",
                table: "Documentacion");

            // Eliminar índice
            migrationBuilder.DropIndex(
                name: "IX_Documentacion_PermisoAusenciaId",
                table: "Documentacion");

            // Eliminar columna PermisoAusenciaId
            migrationBuilder.DropColumn(
                name: "PermisoAusenciaId",
                table: "Documentacion");
        }
    }
}