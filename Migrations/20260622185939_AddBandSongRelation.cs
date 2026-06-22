using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBandSongRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Band",
                table: "Songs");

            migrationBuilder.AddColumn<int>(
                name: "BandId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_BandId",
                table: "Songs",
                column: "BandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Bands_BandId",
                table: "Songs",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Bands_BandId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_BandId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "Band",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
