using Microsoft.EntityFrameworkCore.Migrations;

namespace SQE.Migrations
{
    public partial class createdskils2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59c04a2b-26f6-459f-9ef4-d1dd16b7862a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98d24558-1ea5-4285-8101-5cec1c9cc5c0");

            migrationBuilder.AddColumn<int>(
                name: "PersonalDetailsId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "781b5a6d-d1e8-45cb-bfd4-2eba81e0ad01", "d03ac622-2e69-4181-84f7-24b529f29f54", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7e0c0960-8015-4396-9b2e-39fc3c8c1888", "ec29e37d-ac44-4dd6-95fd-c4ca92859ecc", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonalDetailsId",
                table: "AspNetUsers",
                column: "PersonalDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PersonalDetails_PersonalDetailsId",
                table: "AspNetUsers",
                column: "PersonalDetailsId",
                principalTable: "PersonalDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PersonalDetails_PersonalDetailsId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonalDetailsId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "781b5a6d-d1e8-45cb-bfd4-2eba81e0ad01");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e0c0960-8015-4396-9b2e-39fc3c8c1888");

            migrationBuilder.DropColumn(
                name: "PersonalDetailsId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59c04a2b-26f6-459f-9ef4-d1dd16b7862a", "9460282b-af24-4401-a6bc-e74bba7f25d8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "98d24558-1ea5-4285-8101-5cec1c9cc5c0", "dda31e90-b274-4560-9f50-f10d00a6161f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
