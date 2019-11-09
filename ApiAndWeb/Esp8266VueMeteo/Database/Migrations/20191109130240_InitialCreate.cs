using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Esp8266Id = table.Column<string>(nullable: false),
                    HttpUserName = table.Column<string>(maxLength: 256, nullable: true),
                    HttpPassword = table.Column<string>(maxLength: 256, nullable: true),
                    DeviceName = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    ExtraDescription = table.Column<string>(maxLength: 500, nullable: true),
                    LocationProvided = table.Column<bool>(nullable: false, defaultValue: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longtitude = table.Column<double>(nullable: true),
                    Radius = table.Column<int>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "JsonUpdates",
                columns: table => new
                {
                    JsonUpdateId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: false),
                    JsonValue = table.Column<string>(maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JsonUpdates", x => x.JsonUpdateId);
                    table.ForeignKey(
                        name: "FK_JsonUpdates_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    MeasurementId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(nullable: false),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: false),
                    Pm25 = table.Column<double>(nullable: true),
                    Pm10 = table.Column<double>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    Pressure = table.Column<double>(nullable: true),
                    HeaterTemperature = table.Column<double>(nullable: true),
                    HeaterHumidity = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.MeasurementId);
                    table.ForeignKey(
                        name: "FK_Measurements_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JsonUpdates_DeviceId",
                table: "JsonUpdates",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_DeviceId",
                table: "Measurements",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JsonUpdates");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
