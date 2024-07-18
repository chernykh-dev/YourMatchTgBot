using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YourMatchTgBot.Migrations
{
    /// <inheritdoc />
    public partial class FromUserMessageIdInMatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FromUserMessageId",
                table: "Matches",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FromUserMessageId",
                table: "Matches");
        }
    }
}
