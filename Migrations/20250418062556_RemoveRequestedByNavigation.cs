using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRequestedByNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PolicyRequests_Users_RequestedById",
                table: "PolicyRequests");

            migrationBuilder.DropIndex(
                name: "IX_PolicyRequests_RequestedById",
                table: "PolicyRequests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PolicyRequests_RequestedById",
                table: "PolicyRequests",
                column: "RequestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PolicyRequests_Users_RequestedById",
                table: "PolicyRequests",
                column: "RequestedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
