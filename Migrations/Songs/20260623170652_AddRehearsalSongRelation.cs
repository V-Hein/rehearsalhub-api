using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRehearsalSongRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RehearsalSongs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RehearsalId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false),
                    RatingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalSongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RehearsalSongs_Ratings_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Ratings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RehearsalSongs_Rehearsals_RehearsalId",
                        column: x => x.RehearsalId,
                        principalTable: "Rehearsals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RehearsalSongs_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_Name",
                table: "Ratings",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalSongs_RatingId",
                table: "RehearsalSongs",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalSongs_RehearsalId",
                table: "RehearsalSongs",
                column: "RehearsalId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalSongs_SongId",
                table: "RehearsalSongs",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RehearsalSongs");

            migrationBuilder.DropTable(
                name: "Ratings");
        }
    }
}
