using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurancePortalRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddPolicyholderForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SecurityInformation_PolicyholderId",
                table: "SecurityInformation",
                column: "PolicyholderId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PolicyholderId",
                table: "Contacts",
                column: "PolicyholderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Policyholders_PolicyholderId",
                table: "Accounts",
                column: "PolicyholderId",
                principalTable: "Policyholders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Policyholders_PolicyholderId",
                table: "Contacts",
                column: "PolicyholderId",
                principalTable: "Policyholders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityInformation_Policyholders_PolicyholderId",
                table: "SecurityInformation",
                column: "PolicyholderId",
                principalTable: "Policyholders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Policyholders_PolicyholderId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Policyholders_PolicyholderId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityInformation_Policyholders_PolicyholderId",
                table: "SecurityInformation");

            migrationBuilder.DropIndex(
                name: "IX_SecurityInformation_PolicyholderId",
                table: "SecurityInformation");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PolicyholderId",
                table: "Contacts");
        }
    }
}
