using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddTabTuningRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TuningId",
                table: "Tabs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tabs_TuningId",
                table: "Tabs",
                column: "TuningId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tabs_Tunings_TuningId",
                table: "Tabs",
                column: "TuningId",
                principalTable: "Tunings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tabs_Tunings_TuningId",
                table: "Tabs");

            migrationBuilder.DropIndex(
                name: "IX_Tabs_TuningId",
                table: "Tabs");

            migrationBuilder.DropColumn(
                name: "TuningId",
                table: "Tabs");
        }
    }
}
