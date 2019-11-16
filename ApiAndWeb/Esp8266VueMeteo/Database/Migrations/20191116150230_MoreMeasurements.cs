using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class MoreMeasurements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CellVoltage",
                table: "Measurements",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "WifiRssi",
                table: "Measurements",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HttpUserName",
                table: "Devices",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HttpPassword",
                table: "Devices",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CellVoltage",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "WifiRssi",
                table: "Measurements");

            migrationBuilder.AlterColumn<string>(
                name: "HttpUserName",
                table: "Devices",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "HttpPassword",
                table: "Devices",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);
        }
    }
}
