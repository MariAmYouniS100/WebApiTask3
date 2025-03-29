using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class intial333 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a301efc-1dbe-43d7-90c2-da2fd0b12d0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a5715a-5fca-474e-b017-d0d77ef38efa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "A1B2C3D4-E5F6-7890-1234-56789ABCDEF0", null, "Admin", "ADMIN" },
                    { "B1C2D3E4-F5G6-7890-2345-67890ABCDEF1", null, "Student", "STUDENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A1B2C3D4-E5F6-7890-1234-56789ABCDEF0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "B1C2D3E4-F5G6-7890-2345-67890ABCDEF1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7a301efc-1dbe-43d7-90c2-da2fd0b12d0f", null, "Admin", "ADMIN" },
                    { "f8a5715a-5fca-474e-b017-d0d77ef38efa", null, "Student", "STUDENT" }
                });
        }
    }
}
