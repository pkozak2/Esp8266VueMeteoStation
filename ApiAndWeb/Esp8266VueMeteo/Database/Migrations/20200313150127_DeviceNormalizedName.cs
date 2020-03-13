using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class DeviceNormalizedName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 13, 16, 1, 27, 615, DateTimeKind.Local).AddTicks(3117),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 13, 15, 22, 10, 163, DateTimeKind.Local).AddTicks(1814));

            migrationBuilder.AddColumn<string>(
                name: "DeviceNormalizedName",
                table: "Devices",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeviceNormalizedName",
                table: "Devices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 13, 15, 22, 10, 163, DateTimeKind.Local).AddTicks(1814),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 13, 16, 1, 27, 615, DateTimeKind.Local).AddTicks(3117));
        }
    }
}
