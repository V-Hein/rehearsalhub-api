using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTimeSecodnsAndSongQuanityFromSetlistModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongQuantity",
                table: "Setlists");

            migrationBuilder.DropColumn(
                name: "TimeSeconds",
                table: "Setlists");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongQuantity",
                table: "Setlists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TimeSeconds",
                table: "Setlists",
                type: "int",
                nullable: true);
        }
    }
}
