using Microsoft.EntityFrameworkCore.Migrations;

namespace SQE.Migrations
{
    public partial class createdata1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "438b0f40-5c45-415c-9bc3-8d45f958f605");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1b5d901-4045-4c8c-bc8d-292c8fb83ae9");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "PersonalDetails",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "PersonalDetails",
                type: "text",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1978ca8a-4ebc-44cb-82ce-ad16033371a5", "49285bc2-51a0-4aff-af9c-57e4fe3f9768", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ebeb5661-23bb-4f74-a898-6f5c38629f57", "cefb5235-7115-4d6e-b3a1-9fe75ae71449", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1978ca8a-4ebc-44cb-82ce-ad16033371a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ebeb5661-23bb-4f74-a898-6f5c38629f57");

            migrationBuilder.DropColumn(
                name: "city",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "country",
                table: "PersonalDetails");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d1b5d901-4045-4c8c-bc8d-292c8fb83ae9", "101617bd-c29d-4d8b-9008-bd0b17bc61cf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "438b0f40-5c45-415c-9bc3-8d45f958f605", "0e2a3716-1c54-42a2-a792-0a36909b285a", "Administrator", "ADMINISTRATOR" });
        }
    }
}
