using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddRehearsalStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RehearsalStatus",
                table: "RehearsalStatus");

            migrationBuilder.RenameTable(
                name: "RehearsalStatus",
                newName: "RehearsalStatuses");

            migrationBuilder.RenameIndex(
                name: "IX_RehearsalStatus_Name",
                table: "RehearsalStatuses",
                newName: "IX_RehearsalStatuses_Name");

            migrationBuilder.AddColumn<int>(
                name: "RehearsalStatusId",
                table: "Rehearsals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RehearsalStatuses",
                table: "RehearsalStatuses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rehearsals_RehearsalStatusId",
                table: "Rehearsals",
                column: "RehearsalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rehearsals_RehearsalStatuses_RehearsalStatusId",
                table: "Rehearsals",
                column: "RehearsalStatusId",
                principalTable: "RehearsalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rehearsals_RehearsalStatuses_RehearsalStatusId",
                table: "Rehearsals");

            migrationBuilder.DropIndex(
                name: "IX_Rehearsals_RehearsalStatusId",
                table: "Rehearsals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RehearsalStatuses",
                table: "RehearsalStatuses");

            migrationBuilder.DropColumn(
                name: "RehearsalStatusId",
                table: "Rehearsals");

            migrationBuilder.RenameTable(
                name: "RehearsalStatuses",
                newName: "RehearsalStatus");

            migrationBuilder.RenameIndex(
                name: "IX_RehearsalStatuses_Name",
                table: "RehearsalStatus",
                newName: "IX_RehearsalStatus_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RehearsalStatus",
                table: "RehearsalStatus",
                column: "Id");
        }
    }
}
