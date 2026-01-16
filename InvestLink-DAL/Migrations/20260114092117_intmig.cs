using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLink_DAL.Migrations
{
    /// <inheritdoc />
    public partial class intmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Project",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Project",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
