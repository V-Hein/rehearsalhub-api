using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddInstrumentTuningRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tunings_Instruments_InstrumentId",
                table: "Tunings");

            migrationBuilder.DropIndex(
                name: "IX_Tunings_InstrumentId",
                table: "Tunings");

            migrationBuilder.DropColumn(
                name: "InstrumentId",
                table: "Tunings");

            migrationBuilder.AlterColumn<int>(
                name: "TuningId",
                table: "Tabs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "InstrumentTunings",
                columns: table => new
                {
                    InstrumentId = table.Column<int>(type: "int", nullable: false),
                    TuningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTunings", x => new { x.InstrumentId, x.TuningId });
                    table.ForeignKey(
                        name: "FK_InstrumentTunings_Instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstrumentTunings_Tunings_TuningId",
                        column: x => x.TuningId,
                        principalTable: "Tunings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTunings_TuningId",
                table: "InstrumentTunings",
                column: "TuningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentTunings");

            migrationBuilder.AddColumn<int>(
                name: "InstrumentId",
                table: "Tunings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "TuningId",
                table: "Tabs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tunings_InstrumentId",
                table: "Tunings",
                column: "InstrumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tunings_Instruments_InstrumentId",
                table: "Tunings",
                column: "InstrumentId",
                principalTable: "Instruments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
