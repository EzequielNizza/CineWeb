using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.data.Migrations;

/// <inheritdoc />
public partial class _20261116040254_AgregarBorradoLogicoSalas : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<bool>(
            name: "Borrado",
            table: "Salas",
            type: "bit",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Borrado",
            table: "Salas");
    }
}
