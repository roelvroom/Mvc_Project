using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_Project.Migrations
{
    public partial class TotaalPrijsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PrijsAlleProducten",
                table: "Aankoop",
                newName: "TotaalPrijs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotaalPrijs",
                table: "Aankoop",
                newName: "PrijsAlleProducten");
        }
    }
}
