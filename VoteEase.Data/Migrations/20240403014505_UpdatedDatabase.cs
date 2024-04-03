using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoteEase.Data.Migrations
{
    public partial class UpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Members_Groups_GroupId",
            //    table: "Members");

            //migrationBuilder.DropIndex(
            //    name: "IX_Members_GroupId",
            //    table: "Members");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Members");

            migrationBuilder.CreateTable(
                name: "MembersInGroups",
                columns: table => new
                {
                    MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    GroupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersInGroups", x => new { x.MemberId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_MembersInGroups_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersInGroups_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups",
                column: "LeaderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MembersInGroups_GroupId",
                table: "MembersInGroups",
                column: "GroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembersInGroups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Members",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupId",
                table: "Members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups",
                column: "LeaderId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Members_Groups_GroupId",
            //    table: "Members",
            //    column: "GroupId",
            //    principalTable: "Groups",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
