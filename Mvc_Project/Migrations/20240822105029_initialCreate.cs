using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_Project.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aankoop",
                columns: table => new
                {
                    AankoopId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VakId = table.Column<int>(type: "int", nullable: false),
                    NaamAanvragerId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reden = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GoedGekeurd = table.Column<bool>(type: "bit", nullable: false),
                    Verwijderd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aankoop", x => x.AankoopId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aankoop");
        }
    }
}
