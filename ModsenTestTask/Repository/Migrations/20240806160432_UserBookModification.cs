using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserBookModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdUser",
                schema: "public",
                table: "UserBook",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_IdUser",
                schema: "public",
                table: "UserBook",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_IdUser",
                schema: "public",
                table: "UserBook",
                column: "IdUser",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_IdUser",
                schema: "public",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_IdUser",
                schema: "public",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "IdUser",
                schema: "public",
                table: "UserBook");
        }
    }
}
