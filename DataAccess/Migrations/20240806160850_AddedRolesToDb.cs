using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddedRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e05a635-3ee3-4c22-a44b-985201d261e4", null, "User", "USER" },
                    { "a945da2b-93f6-4003-a5b5-8087d661510a", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e05a635-3ee3-4c22-a44b-985201d261e4");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a945da2b-93f6-4003-a5b5-8087d661510a");
        }
    }
}
