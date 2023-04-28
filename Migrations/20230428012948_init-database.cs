using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleApi.Migrations
{
    public partial class initdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VehicleMakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VIN = table.Column<string>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Model = table.Column<string>(maxLength: 100, nullable: true),
                    Pass = table.Column<bool>(nullable: false),
                    InspectionDate = table.Column<DateTime>(nullable: false),
                    InspectorName = table.Column<string>(maxLength: 100, nullable: true),
                    InspectionLocation = table.Column<string>(maxLength: 100, nullable: true),
                    Notes = table.Column<string>(maxLength: 32768, nullable: true),
                    MakerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VIN);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleMakers_MakerId",
                        column: x => x.MakerId,
                        principalTable: "VehicleMakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_MakerId",
                table: "Vehicles",
                column: "MakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleMakers");
        }
    }
}
