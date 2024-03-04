using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class UserInterestChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempInterestUser_TempInterest_TemporaryInterestsId",
                table: "TempInterestUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest");

            migrationBuilder.DropIndex(
                name: "IX_TempInterest_InterestId",
                table: "TempInterest");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TempInterest");

            migrationBuilder.RenameColumn(
                name: "TemporaryInterestsId",
                table: "TempInterestUser",
                newName: "TemporaryInterestsInterestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TempInterestUser_TempInterest_TemporaryInterestsInterestId",
                table: "TempInterestUser",
                column: "TemporaryInterestsInterestId",
                principalTable: "TempInterest",
                principalColumn: "InterestId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TempInterestUser_TempInterest_TemporaryInterestsInterestId",
                table: "TempInterestUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest");

            migrationBuilder.RenameColumn(
                name: "TemporaryInterestsInterestId",
                table: "TempInterestUser",
                newName: "TemporaryInterestsId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TempInterest",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempInterest",
                table: "TempInterest",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TempInterest_InterestId",
                table: "TempInterest",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_TempInterestUser_TempInterest_TemporaryInterestsId",
                table: "TempInterestUser",
                column: "TemporaryInterestsId",
                principalTable: "TempInterest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
