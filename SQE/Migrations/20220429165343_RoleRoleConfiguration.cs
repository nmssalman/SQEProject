using Microsoft.EntityFrameworkCore.Migrations;

namespace SQE.Migrations
{
    public partial class RoleRoleConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d6dfffe1-9c7c-432b-a271-cb8a56eb7024", "e46b292a-757c-445e-8084-1b3a2f031ebd", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e0fe7554-4eee-4d07-988b-03c2bd9a6b39", "740388dd-92f6-409a-bdbd-3b0a3f20f423", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6dfffe1-9c7c-432b-a271-cb8a56eb7024");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e0fe7554-4eee-4d07-988b-03c2bd9a6b39");
        }
    }
}
