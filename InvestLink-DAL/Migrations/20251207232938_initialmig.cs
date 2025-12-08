using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvestLink_DAL.Migrations
{
    /// <inheritdoc />
    public partial class initialmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LegalBodyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeOfActivity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProposedSite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaWanted = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeOfBenefitFromSite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    projectTimeLine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConstructionPeriod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SourcOfFunding = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectsCapitalCosts = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutTheProject = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SourcesOfRawMaterial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnvironmentalImpact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalManpower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForeignManpower = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingPrograms = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExperienceOfInvestor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationData = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Nationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Investor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SecondPhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Investor_Nationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "Nationality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Id);
                    table.ForeignKey(
                        name: "FK_License_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advertisement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advertisement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Advertisement_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectFollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectFollowUps", x => new { x.Id, x.EmployeeId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectFollowUps_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectFollowUps_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectInvestors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    InvestorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectInvestors", x => new { x.Id, x.InvestorId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectInvestors_Investor_InvestorId",
                        column: x => x.InvestorId,
                        principalTable: "Investor",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectInvestors_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FollowUpReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreationData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectFollowUpId = table.Column<int>(type: "int", nullable: false),
                    ProjectFollowUpEmployeeId = table.Column<int>(type: "int", nullable: false),
                    ProjectFollowUpProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowUpReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowUpReport_ProjectFollowUps_ProjectFollowUpId_ProjectFollowUpEmployeeId_ProjectFollowUpProjectId",
                        columns: x => new { x.ProjectFollowUpId, x.ProjectFollowUpEmployeeId, x.ProjectFollowUpProjectId },
                        principalTable: "ProjectFollowUps",
                        principalColumns: new[] { "Id", "EmployeeId", "ProjectId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Advertisement_EmployeeId",
                table: "Advertisement",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_NationalityId",
                table: "Employee",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowUpReport_ProjectFollowUpId_ProjectFollowUpEmployeeId_ProjectFollowUpProjectId",
                table: "FollowUpReport",
                columns: new[] { "ProjectFollowUpId", "ProjectFollowUpEmployeeId", "ProjectFollowUpProjectId" });

            migrationBuilder.CreateIndex(
                name: "IX_Investor_NationalityId",
                table: "Investor",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_License_ProjectId",
                table: "License",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFollowUps_EmployeeId",
                table: "ProjectFollowUps",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectFollowUps_ProjectId",
                table: "ProjectFollowUps",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvestors_InvestorId",
                table: "ProjectInvestors",
                column: "InvestorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectInvestors_ProjectId",
                table: "ProjectInvestors",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Advertisement");

            migrationBuilder.DropTable(
                name: "FollowUpReport");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "ProjectInvestors");

            migrationBuilder.DropTable(
                name: "ProjectFollowUps");

            migrationBuilder.DropTable(
                name: "Investor");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "Nationality");
        }
    }
}
