using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLink_DAL.Migrations
{
    /// <inheritdoc />
    public partial class t1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SourcesOfRawMaterial",
                table: "Project");

            migrationBuilder.RenameColumn(
                name: "ProjectsCapitalCosts",
                table: "Project",
                newName: "TotalCost");

            migrationBuilder.AddColumn<string>(
                name: "CostLandBuild",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CostMachine",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CostSetup",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ForeignLoans",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocalLoans",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RawMaterialForeign",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RawMaterialLocal",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Technology",
                table: "Project",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsUpdated",
                table: "CoordinatorReport",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateData",
                table: "CoordinatorReport",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostLandBuild",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CostMachine",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CostSetup",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ForeignLoans",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LocalLoans",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "RawMaterialForeign",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "RawMaterialLocal",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Technology",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsUpdated",
                table: "CoordinatorReport");

            migrationBuilder.DropColumn(
                name: "UpdateData",
                table: "CoordinatorReport");

            migrationBuilder.RenameColumn(
                name: "TotalCost",
                table: "Project",
                newName: "ProjectsCapitalCosts");

            migrationBuilder.AddColumn<string>(
                name: "SourcesOfRawMaterial",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
