using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class MatchRefactorAndAddHandledColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_User1Id",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_User2Id",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_User2Id",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "User2Id",
                table: "Matches",
                newName: "Handled");

            migrationBuilder.RenameColumn(
                name: "User1Id",
                table: "Matches",
                newName: "MatchToUserId");

            migrationBuilder.AddColumn<long>(
                name: "MatchFromUserId",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                columns: new[] { "MatchFromUserId", "MatchToUserId", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MatchToUserId",
                table: "Matches",
                column: "MatchToUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_MatchFromUserId",
                table: "Matches",
                column: "MatchFromUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_MatchToUserId",
                table: "Matches",
                column: "MatchToUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_MatchFromUserId",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_MatchToUserId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_MatchToUserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "MatchFromUserId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "Handled",
                table: "Matches",
                newName: "User2Id");

            migrationBuilder.RenameColumn(
                name: "MatchToUserId",
                table: "Matches",
                newName: "User1Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                columns: new[] { "User1Id", "User2Id", "Time" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_User2Id",
                table: "Matches",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_User1Id",
                table: "Matches",
                column: "User1Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_User2Id",
                table: "Matches",
                column: "User2Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
