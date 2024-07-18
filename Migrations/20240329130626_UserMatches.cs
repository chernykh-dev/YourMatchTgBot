using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class UserMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentFoundUsersCount",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "CurrentUserForMatchId",
                table: "Users",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    User1Id = table.Column<long>(type: "INTEGER", nullable: false),
                    User2Id = table.Column<long>(type: "INTEGER", nullable: false),
                    Time = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => new { x.User1Id, x.User2Id, x.Time });
                    table.ForeignKey(
                        name: "FK_Matches_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Users_User2Id",
                        column: x => x.User2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -65L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -64L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -63L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -62L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -61L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -60L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -59L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -58L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -57L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -56L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -55L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -54L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -53L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -52L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -51L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -50L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -49L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -48L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -47L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -46L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -45L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -44L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -43L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -42L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -41L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -40L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -39L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -38L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -37L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -36L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -35L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -34L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -33L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -32L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -31L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -30L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -29L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -28L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -27L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -26L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -25L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -24L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -23L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -22L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -21L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -20L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -19L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -18L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -17L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -16L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -15L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -14L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -13L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -12L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -11L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -10L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -9L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -8L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -7L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -6L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -5L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -4L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -3L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -2L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1L,
                columns: new[] { "CurrentFoundUsersCount", "CurrentUserForMatchId" },
                values: new object[] { 0, null });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CurrentUserForMatchId",
                table: "Users",
                column: "CurrentUserForMatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_User2Id",
                table: "Matches",
                column: "User2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CurrentUserForMatchId",
                table: "Users",
                column: "CurrentUserForMatchId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CurrentUserForMatchId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Users_CurrentUserForMatchId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentFoundUsersCount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CurrentUserForMatchId",
                table: "Users");
        }
    }
}
