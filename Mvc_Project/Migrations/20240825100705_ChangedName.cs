using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mvc_Project.Migrations
{
    public partial class ChangedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Aankoop_NaamAanvragerId",
                table: "Aankoop",
                column: "NaamAanvragerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aankoop_Gebruiker_NaamAanvragerId",
                table: "Aankoop",
                column: "NaamAanvragerId",
                principalTable: "Gebruiker",
                principalColumn: "GebruikerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aankoop_Gebruiker_NaamAanvragerId",
                table: "Aankoop");

            migrationBuilder.DropIndex(
                name: "IX_Aankoop_NaamAanvragerId",
                table: "Aankoop");
        }
    }
}
