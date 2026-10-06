using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurancePortalRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddSsnAndZipCheckConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Policyholders_SSN",
                table: "Policyholders",
                sql: "SSN NOT LIKE '%[^0-9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Policyholders_ZipCode",
                table: "Policyholders",
                sql: "ZipCode NOT LIKE '%[^0-9]%'");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Contacts_ZipCode",
                table: "Contacts",
                sql: "ZipCode NOT LIKE '%[^0-9]%'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Policyholders_SSN",
                table: "Policyholders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Policyholders_ZipCode",
                table: "Policyholders");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Contacts_ZipCode",
                table: "Contacts");
        }
    }
}
