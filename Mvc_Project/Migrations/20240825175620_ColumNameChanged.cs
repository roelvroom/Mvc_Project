using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_Project.Migrations
{
    public partial class ColumNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IDProduct",
                table: "Aankoop",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDProduct",
                table: "Aankoop");
        }
    }
}
