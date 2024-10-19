using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCRS.Data.Migrations
{
    /// <inheritdoc />
    public partial class IdentityDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityDocumenty",
                table: "Patients",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentityDocumenty",
                table: "Patients");
        }
    }
}
