using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class ChangePKInRehearsalSongModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RehearsalSongs",
                table: "RehearsalSongs");

            migrationBuilder.DropIndex(
                name: "IX_RehearsalSongs_RehearsalId",
                table: "RehearsalSongs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RehearsalSongs");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RehearsalSongs",
                table: "RehearsalSongs",
                columns: new[] { "RehearsalId", "SongId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RehearsalSongs",
                table: "RehearsalSongs");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RehearsalSongs",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RehearsalSongs",
                table: "RehearsalSongs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalSongs_RehearsalId",
                table: "RehearsalSongs",
                column: "RehearsalId");
        }
    }
}
