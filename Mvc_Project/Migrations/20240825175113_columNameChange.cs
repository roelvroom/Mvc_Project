using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_Project.Migrations
{
    public partial class columNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PrijsAlleProducten",
                table: "Aankoop",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Aankoop_ProductId",
                table: "Aankoop",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aankoop_Producten_ProductId",
                table: "Aankoop",
                column: "ProductId",
                principalTable: "Producten",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aankoop_Producten_ProductId",
                table: "Aankoop");

            migrationBuilder.DropIndex(
                name: "IX_Aankoop_ProductId",
                table: "Aankoop");

            migrationBuilder.DropColumn(
                name: "PrijsAlleProducten",
                table: "Aankoop");
        }
    }
}
