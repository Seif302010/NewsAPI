using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NewsAPI.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e0fea292-f33c-45b3-8fdd-d16888a4885b", "61ac0785-b23d-4350-9b9d-c042d49ae037", "User", "USER" });

            migrationBuilder.InsertData(
                schema: "security",
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e9c78178-5964-4e3e-8636-8a558d25596e", "05388d2f-ce35-40ae-b6ed-a061dea2d70d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e0fea292-f33c-45b3-8fdd-d16888a4885b");

            migrationBuilder.DeleteData(
                schema: "security",
                table: "Roles",
                keyColumn: "Id",
                keyValue: "e9c78178-5964-4e3e-8636-8a558d25596e");
        }
    }
}
