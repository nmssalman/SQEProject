using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SQE.Migrations
{
    public partial class createdskils : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4df40efa-6d04-4454-9438-a3257184f51b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8477d7b9-ed86-4bbf-8752-57749ad028bf");

            migrationBuilder.CreateTable(
                name: "Skils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    SkilsName = table.Column<string>(type: "text", nullable: true),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ApiUserId = table.Column<string>(type: "varchar(767)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skils_AspNetUsers_ApiUserId",
                        column: x => x.ApiUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "08e93b0d-0ae6-42c5-931b-ae0cab0906d8", "693c2734-39bb-4812-bacf-ccf401fc14cf", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13e6c58a-f565-4a6c-85d1-8959393c3ccd", "6bebaca9-4294-43f7-affc-2810002e8168", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Skils_ApiUserId",
                table: "Skils",
                column: "ApiUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skils");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08e93b0d-0ae6-42c5-931b-ae0cab0906d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13e6c58a-f565-4a6c-85d1-8959393c3ccd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8477d7b9-ed86-4bbf-8752-57749ad028bf", "8d5b1174-7014-4d44-8616-c7eca8cf8622", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4df40efa-6d04-4454-9438-a3257184f51b", "08b12621-5a20-4b00-93cc-8784c240c1f2", "Administrator", "ADMINISTRATOR" });
        }
    }
}
