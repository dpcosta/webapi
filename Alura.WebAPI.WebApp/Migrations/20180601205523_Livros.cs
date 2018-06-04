using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alura.WebAPI.WebApp.Migrations
{
    public partial class Livros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Subtitulo = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Resumo = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    Capa = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    Autor = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Lista = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Autor", "Capa", "Lista", "Resumo", "Subtitulo", "Titulo" },
                values: new object[,]
                {
                    { 1, "J.K. Rowling", null, "ParaLer", null, "E a Pedra Filosofal", "Harry Potter 1" },
                    { 2, "J.K. Rowling", null, "ParaLer", null, "E a Câmara Secreta", "Harry Potter 2" },
                    { 3, "J.K. Rowling", null, "ParaLer", null, "E o Prisioneiro de Askaban", "Harry Potter 3" },
                    { 4, "J.K. Rowling", null, "ParaLer", null, "E o Cálice Sagrado", "Harry Potter 4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
