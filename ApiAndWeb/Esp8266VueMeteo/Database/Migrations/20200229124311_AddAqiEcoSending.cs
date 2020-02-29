using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class AddAqiEcoSending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AqiEcoUpdateUrl",
                table: "Devices",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SendToAqiEco",
                table: "Devices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AqiEcoUpdateUrl",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "SendToAqiEco",
                table: "Devices");
        }
    }
}
