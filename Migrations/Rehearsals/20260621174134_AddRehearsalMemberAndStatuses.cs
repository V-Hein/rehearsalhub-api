using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRehearsalMemberAndStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RehearsalMemberStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalMemberStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RehearsalStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RehearsalMembers",
                columns: table => new
                {
                    RehearsalId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RehearsalMemberStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehearsalMembers", x => new { x.RehearsalId, x.UserId });
                    table.ForeignKey(
                        name: "FK_RehearsalMembers_RehearsalMemberStatuses_RehearsalMemberStatusId",
                        column: x => x.RehearsalMemberStatusId,
                        principalTable: "RehearsalMemberStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RehearsalMembers_Rehearsals_RehearsalId",
                        column: x => x.RehearsalId,
                        principalTable: "Rehearsals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RehearsalMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMembers_RehearsalMemberStatusId",
                table: "RehearsalMembers",
                column: "RehearsalMemberStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMembers_UserId",
                table: "RehearsalMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMemberStatuses_Name",
                table: "RehearsalMemberStatuses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalStatus_Name",
                table: "RehearsalStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SongStatus_Name",
                table: "SongStatus",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RehearsalMembers");

            migrationBuilder.DropTable(
                name: "RehearsalStatus");

            migrationBuilder.DropTable(
                name: "SongStatus");

            migrationBuilder.DropTable(
                name: "RehearsalMemberStatuses");
        }
    }
}
