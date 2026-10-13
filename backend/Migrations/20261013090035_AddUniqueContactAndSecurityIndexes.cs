using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurancePortalRegistration.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueContactAndSecurityIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SecurityInformation_PolicyholderId",
                table: "SecurityInformation");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PolicyholderId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityInformation_PolicyholderId",
                table: "SecurityInformation",
                column: "PolicyholderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PolicyholderId",
                table: "Contacts",
                column: "PolicyholderId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SecurityInformation_PolicyholderId",
                table: "SecurityInformation");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PolicyholderId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityInformation_PolicyholderId",
                table: "SecurityInformation",
                column: "PolicyholderId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PolicyholderId",
                table: "Contacts",
                column: "PolicyholderId");
        }
    }
}
