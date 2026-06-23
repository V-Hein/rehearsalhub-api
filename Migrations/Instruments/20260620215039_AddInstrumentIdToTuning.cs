using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddInstrumentIdToTuning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_InstrumentTypes_InstrumentTypeId",
                table: "Instruments");

            migrationBuilder.AddColumn<int>(
                name: "InstrumentId",
                table: "Tunings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tunings_InstrumentId",
                table: "Tunings",
                column: "InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_InstrumentTypes_InstrumentTypeId",
                table: "Instruments",
                column: "InstrumentTypeId",
                principalTable: "InstrumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tunings_Instruments_InstrumentId",
                table: "Tunings",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instruments_InstrumentTypes_InstrumentTypeId",
                table: "Instruments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tunings_Instruments_InstrumentId",
                table: "Tunings");

            migrationBuilder.DropIndex(
                name: "IX_Tunings_InstrumentId",
                table: "Tunings");

            migrationBuilder.DropColumn(
                name: "InstrumentId",
                table: "Tunings");

            migrationBuilder.AddForeignKey(
                name: "FK_Instruments_InstrumentTypes_InstrumentTypeId",
                table: "Instruments",
                column: "InstrumentTypeId",
                principalTable: "InstrumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
