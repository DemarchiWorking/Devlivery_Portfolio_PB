using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Devlivery.Infraestrutura.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Criacao",
                table: "Produtos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Estoque",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Produtos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Produtos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Criacao",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Estoque",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Produtos");
        }
    }
}
