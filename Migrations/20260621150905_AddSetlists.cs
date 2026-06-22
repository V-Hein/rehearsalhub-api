using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSetlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers",
                columns: new[] { "BandId", "UserId" });

            migrationBuilder.CreateTable(
                name: "Setlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BandId = table.Column<int>(type: "int", nullable: false),
                    SongQuantity = table.Column<int>(type: "int", nullable: false),
                    TimeSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setlists_Bands_BandId",
                        column: x => x.BandId,
                        principalTable: "Bands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Setlists_BandId",
                table: "Setlists",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_Setlists_Name",
                table: "Setlists",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers",
                columns: new[] { "BandId", "UserId", "RoleId" });
        }
    }
}
