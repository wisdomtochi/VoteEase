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

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Votes_Members_VoterId",
            //    table: "Votes");

            //migrationBuilder.DropIndex(
            //    name: "IX_Members_GroupId",
            //    table: "Members");

            //migrationBuilder.DropIndex(
            //    name: "IX_Groups_LeaderId",
            //    table: "Groups");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_AccreditedMembers",
            //    table: "AccreditedMembers");

            //migrationBuilder.DropColumn(
            //    name: "GroupId",
            //    table: "Members");

            //migrationBuilder.RenameColumn(
            //    name: "VoterId",
            //    table: "Votes",
            //    newName: "NominationId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Votes_VoterId",
            //    table: "Votes",
            //    newName: "IX_Votes_NominationId");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "Id",
            //    table: "AccreditedMembers",
            //    type: "char(36)",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
            //    collation: "ascii_general_ci");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_AccreditedMembers",
            //    table: "AccreditedMembers",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "MembersInGroups",
            //    columns: table => new
            //    {
            //        MemberId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
            //        GroupId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_MembersInGroups", x => new { x.MemberId, x.GroupId });
            //        table.ForeignKey(
            //            name: "FK_MembersInGroups_Groups_GroupId",
            //            column: x => x.GroupId,
            //            principalTable: "Groups",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_MembersInGroups_Members_MemberId",
            //            column: x => x.MemberId,
            //            principalTable: "Members",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Groups_LeaderId",
            //    table: "Groups",
            //    column: "LeaderId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_AccreditedMembers_MemberId",
            //    table: "AccreditedMembers",
            //    column: "MemberId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_MembersInGroups_GroupId",
            //    table: "MembersInGroups",
            //    column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccreditedMembers_Members_MemberId",
                table: "AccreditedMembers",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Nominations_NominationId",
                table: "Votes",
                column: "NominationId",
                principalTable: "Nominations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccreditedMembers_Members_MemberId",
                table: "AccreditedMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Nominations_NominationId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "MembersInGroups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccreditedMembers",
                table: "AccreditedMembers");

            migrationBuilder.DropIndex(
                name: "IX_AccreditedMembers_MemberId",
                table: "AccreditedMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AccreditedMembers");

            migrationBuilder.RenameColumn(
                name: "NominationId",
                table: "Votes",
                newName: "VoterId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_NominationId",
                table: "Votes",
                newName: "IX_Votes_VoterId");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                table: "Members",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccreditedMembers",
                table: "AccreditedMembers",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_GroupId",
                table: "Members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeaderId",
                table: "Groups",
                column: "LeaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Groups_GroupId",
                table: "Members",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Members_VoterId",
                table: "Votes",
                column: "VoterId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
