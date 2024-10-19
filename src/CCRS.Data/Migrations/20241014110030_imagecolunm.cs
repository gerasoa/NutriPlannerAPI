using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CCRS.Data.Migrations
{
    /// <inheritdoc />
    public partial class imagecolunm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Patients",
                type: "varchar(100)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Patients");
        }
    }
}
