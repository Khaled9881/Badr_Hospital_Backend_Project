using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadrHospital.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPharmacistAndLabTechnician : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispensings_ApplicationUsers_PharmacistId",
                table: "Dispensings");

            migrationBuilder.CreateTable(
                name: "LabTechnicians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTechnicians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTechnicians_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pharmacists_ApplicationUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabResults_TechnicianId",
                table: "LabResults",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTechnicians_ApplicationUserId",
                table: "LabTechnicians",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LabTechnicians_CertificationNumber",
                table: "LabTechnicians",
                column: "CertificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LabTechnicians_Email",
                table: "LabTechnicians",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_Email",
                table: "Pharmacists",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_LicenseNumber",
                table: "Pharmacists",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Dispensings_Pharmacists_PharmacistId",
                table: "Dispensings",
                column: "PharmacistId",
                principalTable: "Pharmacists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LabResults_LabTechnicians_TechnicianId",
                table: "LabResults",
                column: "TechnicianId",
                principalTable: "LabTechnicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dispensings_Pharmacists_PharmacistId",
                table: "Dispensings");

            migrationBuilder.DropForeignKey(
                name: "FK_LabResults_LabTechnicians_TechnicianId",
                table: "LabResults");

            migrationBuilder.DropTable(
                name: "LabTechnicians");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropIndex(
                name: "IX_LabResults_TechnicianId",
                table: "LabResults");

            migrationBuilder.AddForeignKey(
                name: "FK_Dispensings_ApplicationUsers_PharmacistId",
                table: "Dispensings",
                column: "PharmacistId",
                principalTable: "ApplicationUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
