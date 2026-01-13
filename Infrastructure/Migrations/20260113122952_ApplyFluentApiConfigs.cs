using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplyFluentApiConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfiles_Customers_CustomerId",
                table: "CustomerProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfiles",
                table: "CustomerProfiles");

            migrationBuilder.RenameTable(
                name: "CustomerProfiles",
                newName: "CustomerProfile");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProfiles_CustomerId",
                table: "CustomerProfile",
                newName: "IX_CustomerProfile_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfile_Customers_CustomerId",
                table: "CustomerProfile",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerProfile_Customers_CustomerId",
                table: "CustomerProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerProfile",
                table: "CustomerProfile");

            migrationBuilder.RenameTable(
                name: "CustomerProfile",
                newName: "CustomerProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerProfile_CustomerId",
                table: "CustomerProfiles",
                newName: "IX_CustomerProfiles_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerProfiles",
                table: "CustomerProfiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerProfiles_Customers_CustomerId",
                table: "CustomerProfiles",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
