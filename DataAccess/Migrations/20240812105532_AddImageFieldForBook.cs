using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddImageFieldForBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                schema: "public",
                table: "Books",
                type: "bytea",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "public",
                table: "Authors",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                schema: "public",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9abcde2c-e422-4406-b37a-7832ec952b08", null, "User", "USER" },
                    { "e69381ef-24f7-4ee5-9504-597bdfdf91b6", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9abcde2c-e422-4406-b37a-7832ec952b08");

            migrationBuilder.DeleteData(
                schema: "public",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e69381ef-24f7-4ee5-9504-597bdfdf91b6");

            migrationBuilder.DropColumn(
                name: "Image",
                schema: "public",
                table: "Books");

            migrationBuilder.AlterColumn<string>(
                name: "Country",
                schema: "public",
                table: "Authors",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
