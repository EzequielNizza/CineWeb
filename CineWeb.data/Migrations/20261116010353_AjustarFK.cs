using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cine.data.Migrations;

/// <inheritdoc />
public partial class _20261116010353_AjustarFK : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Acciones",
            columns: table => new
            {
                IdAccion = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Acciones", x => x.IdAccion);
            });

        migrationBuilder.CreateTable(
            name: "Peliculas",
            columns: table => new
            {
                IdPelicula = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Descripcion = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: true),
                DuracionMin = table.Column<int>(type: "int", nullable: false),
                Borrado = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Peliculas", x => x.IdPelicula);
            });

        migrationBuilder.CreateTable(
            name: "Roles",
            columns: table => new
            {
                IdRol = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Descripcion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Borrado = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.IdRol);
            });

        migrationBuilder.CreateTable(
            name: "Salas",
            columns: table => new
            {
                IdSala = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CantidadButacas = table.Column<int>(type: "int", nullable: false),
                EsIMAX = table.Column<bool>(type: "bit", nullable: false),
                Es3D = table.Column<bool>(type: "bit", nullable: false),
                Es4DX = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Salas", x => x.IdSala);
            });

        migrationBuilder.CreateTable(
            name: "Usuarios",
            columns: table => new
            {
                IdUsuario = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FechaAlta = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
            });

        migrationBuilder.CreateTable(
            name: "RolesAcciones",
            columns: table => new
            {
                IdRolAccion = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdRol = table.Column<int>(type: "int", nullable: false),
                IdAccion = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolesAcciones", x => x.IdRolAccion);
                table.ForeignKey(
                    name: "FK_RolesAcciones_Acciones_IdAccion",
                    column: x => x.IdAccion,
                    principalTable: "Acciones",
                    principalColumn: "IdAccion",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_RolesAcciones_Roles_IdRol",
                    column: x => x.IdRol,
                    principalTable: "Roles",
                    principalColumn: "IdRol",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Funciones",
            columns: table => new
            {
                IdFuncion = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                Borrado = table.Column<bool>(type: "bit", nullable: false),
                IdPelicula = table.Column<int>(type: "int", nullable: false),
                IdSala = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Funciones", x => x.IdFuncion);
                table.ForeignKey(
                    name: "FK_Funciones_Peliculas_IdPelicula",
                    column: x => x.IdPelicula,
                    principalTable: "Peliculas",
                    principalColumn: "IdPelicula",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Funciones_Salas_IdSala",
                    column: x => x.IdSala,
                    principalTable: "Salas",
                    principalColumn: "IdSala",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UsuariosRoles",
            columns: table => new
            {
                IdUsuarioRol = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                IdUsuario = table.Column<int>(type: "int", nullable: false),
                IdRol = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UsuariosRoles", x => x.IdUsuarioRol);
                table.ForeignKey(
                    name: "FK_UsuariosRoles_Roles_IdRol",
                    column: x => x.IdRol,
                    principalTable: "Roles",
                    principalColumn: "IdRol",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UsuariosRoles_Usuarios_IdUsuario",
                    column: x => x.IdUsuario,
                    principalTable: "Usuarios",
                    principalColumn: "IdUsuario",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Entradas",
            columns: table => new
            {
                IdEntrada = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PrecioEntrada = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                IdFuncion = table.Column<int>(type: "int", nullable: false),
                IdUsuario = table.Column<int>(type: "int", nullable: false),
                FechaCompra = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Entradas", x => x.IdEntrada);
                table.ForeignKey(
                    name: "FK_Entradas_Funciones_IdFuncion",
                    column: x => x.IdFuncion,
                    principalTable: "Funciones",
                    principalColumn: "IdFuncion",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Entradas_Usuarios_IdUsuario",
                    column: x => x.IdUsuario,
                    principalTable: "Usuarios",
                    principalColumn: "IdUsuario",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Entradas_IdFuncion",
            table: "Entradas",
            column: "IdFuncion");

        migrationBuilder.CreateIndex(
            name: "IX_Entradas_IdUsuario",
            table: "Entradas",
            column: "IdUsuario");

        migrationBuilder.CreateIndex(
            name: "IX_Funciones_IdPelicula",
            table: "Funciones",
            column: "IdPelicula");

        migrationBuilder.CreateIndex(
            name: "IX_Funciones_IdSala",
            table: "Funciones",
            column: "IdSala");

        migrationBuilder.CreateIndex(
            name: "IX_RolesAcciones_IdAccion",
            table: "RolesAcciones",
            column: "IdAccion");

        migrationBuilder.CreateIndex(
            name: "IX_RolesAcciones_IdRol",
            table: "RolesAcciones",
            column: "IdRol");

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosRoles_IdRol",
            table: "UsuariosRoles",
            column: "IdRol");

        migrationBuilder.CreateIndex(
            name: "IX_UsuariosRoles_IdUsuario",
            table: "UsuariosRoles",
            column: "IdUsuario");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Entradas");

        migrationBuilder.DropTable(
            name: "RolesAcciones");

        migrationBuilder.DropTable(
            name: "UsuariosRoles");

        migrationBuilder.DropTable(
            name: "Funciones");

        migrationBuilder.DropTable(
            name: "Acciones");

        migrationBuilder.DropTable(
            name: "Roles");

        migrationBuilder.DropTable(
            name: "Usuarios");

        migrationBuilder.DropTable(
            name: "Peliculas");

        migrationBuilder.DropTable(
            name: "Salas");
    }
}
