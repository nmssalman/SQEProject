using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SQE.Migrations
{
    public partial class createdskils1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Skills",
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
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_AspNetUsers_ApiUserId",
                        column: x => x.ApiUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "59c04a2b-26f6-459f-9ef4-d1dd16b7862a", "9460282b-af24-4401-a6bc-e74bba7f25d8", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "98d24558-1ea5-4285-8101-5cec1c9cc5c0", "dda31e90-b274-4560-9f50-f10d00a6161f", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ApiUserId",
                table: "Skills",
                column: "ApiUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59c04a2b-26f6-459f-9ef4-d1dd16b7862a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98d24558-1ea5-4285-8101-5cec1c9cc5c0");

            migrationBuilder.CreateTable(
                name: "Skils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ApiUserId = table.Column<string>(type: "varchar(767)", nullable: true),
                    SkilsName = table.Column<string>(type: "text", nullable: true)
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
    }
}
