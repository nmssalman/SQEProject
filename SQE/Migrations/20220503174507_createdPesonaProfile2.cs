using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace SQE.Migrations
{
    public partial class createdPesonaProfile2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "276f313d-e987-4af2-ad03-dfae73292e27");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfce7a65-0ba0-4587-9ad5-20db10b2f197");

            migrationBuilder.AddColumn<int>(
                name: "EducationId",
                table: "PersonalDetails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceId",
                table: "PersonalDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Major = table.Column<string>(type: "text", nullable: true),
                    Institute = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Start_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    End_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PersonalDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_PersonalDetails_PersonalDetailsId",
                        column: x => x.PersonalDetailsId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Company_Name = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Years_of_Experience = table.Column<double>(type: "double", nullable: true),
                    ActiveStatus = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PersonalDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_PersonalDetails_PersonalDetailsId",
                        column: x => x.PersonalDetailsId,
                        principalTable: "PersonalDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                values: new object[] { "2499f484-ebd7-4b68-b080-9430925dbde9", "a73eee28-99a4-4ba7-b5be-a047321987fd", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a2d43ace-d834-4d31-a2b1-0fae0214cfab", "6b0a99e3-cf0e-47e7-a095-4a81419a0756", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_EducationId",
                table: "PersonalDetails",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDetails_ExperienceId",
                table: "PersonalDetails",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_PersonalDetailsId",
                table: "Education",
                column: "PersonalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_PersonalDetailsId",
                table: "Experience",
                column: "PersonalDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ApiUserId",
                table: "Skills",
                column: "ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDetails_Education_EducationId",
                table: "PersonalDetails",
                column: "EducationId",
                principalTable: "Education",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDetails_Experience_ExperienceId",
                table: "PersonalDetails",
                column: "ExperienceId",
                principalTable: "Experience",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDetails_Education_EducationId",
                table: "PersonalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDetails_Experience_ExperienceId",
                table: "PersonalDetails");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_EducationId",
                table: "PersonalDetails");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDetails_ExperienceId",
                table: "PersonalDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2499f484-ebd7-4b68-b080-9430925dbde9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2d43ace-d834-4d31-a2b1-0fae0214cfab");

            migrationBuilder.DropColumn(
                name: "EducationId",
                table: "PersonalDetails");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "PersonalDetails");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "276f313d-e987-4af2-ad03-dfae73292e27", "087ad337-3792-4911-8f56-fcf63ef515de", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfce7a65-0ba0-4587-9ad5-20db10b2f197", "bee7f583-615b-40f0-af7a-4c413c010a31", "Administrator", "ADMINISTRATOR" });
        }
    }
}
