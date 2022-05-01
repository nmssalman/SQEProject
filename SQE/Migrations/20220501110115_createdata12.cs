using Microsoft.EntityFrameworkCore.Migrations;

namespace SQE.Migrations
{
    public partial class createdata12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1978ca8a-4ebc-44cb-82ce-ad16033371a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebeb5661-23bb-4f74-a898-6f5c38629f57");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveStatus",
                table: "PersonalDetails",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8477d7b9-ed86-4bbf-8752-57749ad028bf", "8d5b1174-7014-4d44-8616-c7eca8cf8622", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4df40efa-6d04-4454-9438-a3257184f51b", "08b12621-5a20-4b00-93cc-8784c240c1f2", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4df40efa-6d04-4454-9438-a3257184f51b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8477d7b9-ed86-4bbf-8752-57749ad028bf");

            migrationBuilder.DropColumn(
                name: "ActiveStatus",
                table: "PersonalDetails");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1978ca8a-4ebc-44cb-82ce-ad16033371a5", "49285bc2-51a0-4aff-af9c-57e4fe3f9768", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebeb5661-23bb-4f74-a898-6f5c38629f57", "cefb5235-7115-4d6e-b3a1-9fe75ae71449", "Administrator", "ADMINISTRATOR" });
        }
    }
}
