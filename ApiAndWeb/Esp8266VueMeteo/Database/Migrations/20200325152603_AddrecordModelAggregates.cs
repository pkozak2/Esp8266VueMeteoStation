using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class AddrecordModelAggregates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 25, 16, 26, 3, 115, DateTimeKind.Local).AddTicks(6875),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 13, 16, 1, 27, 615, DateTimeKind.Local).AddTicks(3117));

            migrationBuilder.AddColumn<double>(
                name: "Co2",
                table: "Measurements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Elevation",
                table: "Devices",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Aggregates",
                columns: table => new
                {
                    AggregateId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: false),
                    Resolution = table.Column<int>(nullable: false),
                    Pm25 = table.Column<double>(nullable: true),
                    Pm10 = table.Column<double>(nullable: true),
                    Co2 = table.Column<double>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    Pressure = table.Column<double>(nullable: true),
                    HeaterTemperature = table.Column<double>(nullable: true),
                    HeaterHumidity = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aggregates", x => x.AggregateId);
                    table.ForeignKey(
                        name: "FK_Aggregates_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    RecordId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: false),
                    Pm25 = table.Column<double>(nullable: true),
                    Pm10 = table.Column<double>(nullable: true),
                    Co2 = table.Column<double>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    Pressure = table.Column<double>(nullable: true),
                    HeaterTemperature = table.Column<double>(nullable: true),
                    HeaterHumidity = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Records_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aggregates_DeviceId",
                table: "Aggregates",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_DeviceId",
                table: "Records",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aggregates");

            migrationBuilder.DropTable(
                name: "Records");

            migrationBuilder.DropColumn(
                name: "Co2",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "Elevation",
                table: "Devices");

            migrationBuilder.AlterColumn<DateTime>(
                name: "InsertDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(2020, 3, 13, 16, 1, 27, 615, DateTimeKind.Local).AddTicks(3117),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 3, 25, 16, 26, 3, 115, DateTimeKind.Local).AddTicks(6875));
        }
    }
}
