using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLink_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initiamig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseName",
                table: "License");

            migrationBuilder.AddColumn<string>(
                name: "LicenseName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LicenseName",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "LicenseName",
                table: "License",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
