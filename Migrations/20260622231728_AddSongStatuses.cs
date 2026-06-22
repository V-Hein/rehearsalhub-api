using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSongStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SongStatus",
                table: "SongStatus");

            migrationBuilder.RenameTable(
                name: "SongStatus",
                newName: "SongStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_SongStatus_Name",
                table: "SongStatuses",
                newName: "IX_SongStatuses_Name");

            migrationBuilder.AddColumn<int>(
                name: "SongStatusId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongStatuses",
                table: "SongStatuses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SongStatusId",
                table: "Songs",
                column: "SongStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_SongStatuses_SongStatusId",
                table: "Songs",
                column: "SongStatusId",
                principalTable: "SongStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_SongStatuses_SongStatusId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_SongStatusId",
                table: "Songs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SongStatuses",
                table: "SongStatuses");

            migrationBuilder.DropColumn(
                name: "SongStatusId",
                table: "Songs");

            migrationBuilder.RenameTable(
                name: "SongStatuses",
                newName: "SongStatus");

            migrationBuilder.RenameIndex(
                name: "IX_SongStatuses_Name",
                table: "SongStatus",
                newName: "IX_SongStatus_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SongStatus",
                table: "SongStatus",
                column: "Id");
        }
    }
}
