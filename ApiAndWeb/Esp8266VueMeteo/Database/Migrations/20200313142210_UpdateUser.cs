using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class UpdateUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                table: "Users",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 13, 15, 22, 10, 163, DateTimeKind.Local).AddTicks(1814),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 13, 15, 18, 54, 263, DateTimeKind.Local).AddTicks(9474));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Devices",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLoginDate",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 13, 15, 18, 54, 263, DateTimeKind.Local).AddTicks(9474),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 13, 15, 22, 10, 163, DateTimeKind.Local).AddTicks(1814));

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
