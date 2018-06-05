using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alura.WebAPI.WebApp.Migrations
{
    public partial class CapaEmBytes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumns: new[] { "Id", "ConcurrencyStamp" },
                keyValues: new object[] { "ccffd30b-85a6-4232-a03d-9eab4839da4d", "9b42a4a1-c852-4326-894c-929dd67b8683" });

            migrationBuilder.DropColumn(
                name: "Capa",
                table: "Livros");

            migrationBuilder.AlterColumn<string>(
                name: "Lista",
                table: "Livros",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagemCapa",
                table: "Livros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemCapa",
                table: "Livros");

            migrationBuilder.AlterColumn<string>(
                name: "Lista",
                table: "Livros",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AddColumn<string>(
                name: "Capa",
                table: "Livros",
                type: "nvarchar(255)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UltimoLogin", "UserName" },
                values: new object[] { "ccffd30b-85a6-4232-a03d-9eab4839da4d", 0, "9b42a4a1-c852-4326-894c-929dd67b8683", "admin@example.org", false, false, null, "Administrador", null, null, "AQAAAAEAACcQAAAAEPn0TlXFzcsE54iwQw/M9i98Is7PDLrJpx0G4v1ftJSeQivf3+8wK0ACUhZanYd/GA==", null, false, null, false, null, "admin" });
        }
    }
}
