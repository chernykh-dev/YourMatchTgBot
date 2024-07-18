using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class TestDataFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -31L,
                column: "InterestsFlags",
                value: 2576);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -31L,
                column: "InterestsFlags",
                value: 5276);
        }
    }
}
