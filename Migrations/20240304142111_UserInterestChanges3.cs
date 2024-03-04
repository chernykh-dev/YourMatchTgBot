using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class UserInterestChanges3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempInterestUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "TempInterest",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest",
                columns: new[] { "UserId", "InterestId" });

            migrationBuilder.CreateIndex(
                name: "IX_TempInterest_InterestId",
                table: "TempInterest",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TempInterest_Users_UserId",
                table: "TempInterest",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempInterest_Users_UserId",
                table: "TempInterest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest");

            migrationBuilder.DropIndex(
                name: "IX_TempInterest_InterestId",
                table: "TempInterest");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TempInterest");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest",
                column: "InterestId");

            migrationBuilder.CreateTable(
                name: "TempInterestUser",
                columns: table => new
                {
                    TemporaryInterestsInterestId = table.Column<int>(type: "INTEGER", nullable: false),
                    UsersId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempInterestUser", x => new { x.TemporaryInterestsInterestId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_TempInterestUser_TempInterest_TemporaryInterestsInterestId",
                        column: x => x.TemporaryInterestsInterestId,
                        principalTable: "TempInterest",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempInterestUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempInterestUser_UsersId",
                table: "TempInterestUser",
                column: "UsersId");
        }
    }
}
