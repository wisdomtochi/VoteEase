using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteEase.Data.Migrations
{
    public partial class AddingMemberPasscode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Members",
                newName: "PhoneNumber");

            migrationBuilder.CreateTable(
                name: "MemberPasscodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PassCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberPasscodes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberPasscodes");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Members",
                newName: "EmailAddress");
        }
    }
}
