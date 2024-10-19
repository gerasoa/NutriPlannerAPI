using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCRS.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Specialty = table.Column<string>(type: "varchar(100)", nullable: false),
                    OffersOnlineConsultations = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Gender = table.Column<string>(type: "varchar(200)", nullable: false),
                    DoB = table.Column<DateTime>(type: "DateTime", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Street = table.Column<string>(type: "varchar(200)", nullable: false),
                    HouseNumber = table.Column<string>(type: "varchar(50)", nullable: false),
                    AdditionalInfo = table.Column<string>(type: "varchar(100)", nullable: true),
                    District = table.Column<string>(type: "varchar(100)", nullable: true),
                    City = table.Column<string>(type: "varchar(100)", nullable: true),
                    State = table.Column<string>(type: "varchar(100)", nullable: true),
                    PostalCode = table.Column<string>(type: "varchar(10)", nullable: false),
                    Country = table.Column<string>(type: "varchar(100)", nullable: true),
                    PrimaryPhone = table.Column<string>(type: "varchar(100)", nullable: true),
                    SecondaryPhone = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_PatientId",
                table: "Address",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
