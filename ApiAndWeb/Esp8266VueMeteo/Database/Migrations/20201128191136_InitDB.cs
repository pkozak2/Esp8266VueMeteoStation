using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Esp8266VueMeteo.Database.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Username = table.Column<string>(maxLength: 256, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false),
                    InsertDate = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2020, 11, 28, 20, 11, 36, 100, DateTimeKind.Local).AddTicks(7860)),
                    LastLoginDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    UserId = table.Column<Guid>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    Esp8266Id = table.Column<string>(nullable: false),
                    HttpUserName = table.Column<string>(maxLength: 256, nullable: false),
                    HttpPassword = table.Column<string>(maxLength: 256, nullable: false),
                    DeviceName = table.Column<string>(maxLength: 256, nullable: false),
                    DeviceNormalizedName = table.Column<string>(maxLength: 256, nullable: false, defaultValue: ""),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    ExtraDescription = table.Column<string>(maxLength: 500, nullable: true),
                    LocationProvided = table.Column<bool>(nullable: false, defaultValue: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longtitude = table.Column<double>(nullable: true),
                    Radius = table.Column<int>(nullable: true),
                    Elevation = table.Column<int>(nullable: true),
                    InsertDateTime = table.Column<DateTimeOffset>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false, defaultValue: true),
                    SendToAqiEco = table.Column<bool>(nullable: false),
                    AqiEcoUpdateUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                    Pm1 = table.Column<double>(nullable: true),
                    Pm4 = table.Column<double>(nullable: true),
                    N1 = table.Column<double>(nullable: true),
                    N25 = table.Column<double>(nullable: true),
                    N4 = table.Column<double>(nullable: true),
                    N10 = table.Column<double>(nullable: true),
                    Co2 = table.Column<double>(nullable: true),
                    Temperature = table.Column<double>(nullable: true),
                    Humidity = table.Column<double>(nullable: true),
                    Pressure = table.Column<double>(nullable: true),
                    HeaterTemperature = table.Column<double>(nullable: true),
                    HeaterHumidity = table.Column<double>(nullable: true),
                    WifiRssi = table.Column<double>(nullable: true),
                    CellVoltage = table.Column<double>(nullable: true)
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
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JsonUpdates_DeviceId",
                table: "JsonUpdates",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_DeviceId",
                table: "Measurements",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JsonUpdates");

            migrationBuilder.DropTable(
                name: "Measurements");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
