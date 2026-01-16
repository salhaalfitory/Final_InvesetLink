using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLink_DAL.Migrations
{
    /// <inheritdoc />
    public partial class employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Nationality_NationalityId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_NationalityId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SecondPhoneNumber",
                table: "Employee");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Employee",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecondPhoneNumber",
                table: "Employee",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_NationalityId",
                table: "Employee",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Nationality_NationalityId",
                table: "Employee",
                column: "NationalityId",
                principalTable: "Nationality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
