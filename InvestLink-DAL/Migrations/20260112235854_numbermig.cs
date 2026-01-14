using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLink_DAL.Migrations
{
    /// <inheritdoc />
    public partial class numbermig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IDNumber",
                table: "Investor");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Investor",
                newName: "PersonalcardImage");

            migrationBuilder.AddColumn<string>(
                name: "PassportImage",
                table: "Investor",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Passportnumber",
                table: "Investor",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Personalcardnumber",
                table: "Investor",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CoordinatorReport",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassportImage",
                table: "Investor");

            migrationBuilder.DropColumn(
                name: "Passportnumber",
                table: "Investor");

            migrationBuilder.DropColumn(
                name: "Personalcardnumber",
                table: "Investor");

            migrationBuilder.RenameColumn(
                name: "PersonalcardImage",
                table: "Investor",
                newName: "ImageName");

            migrationBuilder.AddColumn<string>(
                name: "IDNumber",
                table: "Investor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CoordinatorReport",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
