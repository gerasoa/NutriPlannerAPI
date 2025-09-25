using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCRS.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConsultationConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConsultationConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsultationType = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeBetweenConsults = table.Column<TimeSpan>(type: "time", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LunchBreakStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    LunchBreakEnd = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AvailableSlot",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Slot = table.Column<TimeSpan>(type: "time", nullable: false),
                    ConsultationConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailableSlot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AvailableSlot_ConsultationConfigs_ConsultationConfigId",
                        column: x => x.ConsultationConfigId,
                        principalTable: "ConsultationConfigs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OfficeLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsultationConfigId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfficeLocation_ConsultationConfigs_ConsultationConfigId",
                        column: x => x.ConsultationConfigId,
                        principalTable: "ConsultationConfigs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailableSlot_ConsultationConfigId",
                table: "AvailableSlot",
                column: "ConsultationConfigId");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeLocation_ConsultationConfigId",
                table: "OfficeLocation",
                column: "ConsultationConfigId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailableSlot");

            migrationBuilder.DropTable(
                name: "OfficeLocation");

            migrationBuilder.DropTable(
                name: "ConsultationConfigs");
        }
    }
}
