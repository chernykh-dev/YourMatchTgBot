using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class CitiesPlaceIdAndDisplayName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.RenameColumn(
                name: "TemporaryInterestsFLags",
                table: "Users",
                newName: "TemporaryInterestsFlags");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "Cities",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "TemporaryInterestsFlags",
                table: "Users",
                newName: "TemporaryInterestsFLags");

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Belgorod" });
        }
    }
}
