using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.data.Migrations;

/// <inheritdoc />
public partial class _20261116021820_EditarTablaUsuario : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Apellido",
            table: "Usuarios");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Apellido",
            table: "Usuarios",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");
    }
}
