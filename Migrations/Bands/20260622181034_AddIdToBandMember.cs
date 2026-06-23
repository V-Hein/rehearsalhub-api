using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToBandMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Roles_RoleId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RehearsalMembers_Users_UserId",
                table: "RehearsalMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RehearsalMembers",
                table: "RehearsalMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RehearsalMembers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "BandMemberId",
                table: "RehearsalMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BandId",
                table: "RehearsalMembers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BandMembers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RehearsalMembers",
                table: "RehearsalMembers",
                columns: new[] { "RehearsalId", "BandMemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMembers_BandId",
                table: "RehearsalMembers",
                column: "BandId");

            migrationBuilder.CreateIndex(
                name: "IX_RehearsalMembers_BandMemberId",
                table: "RehearsalMembers",
                column: "BandMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BandMembers_BandId_UserId",
                table: "BandMembers",
                columns: new[] { "BandId", "UserId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Roles_RoleId",
                table: "BandMembers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RehearsalMembers_BandMembers_BandMemberId",
                table: "RehearsalMembers",
                column: "BandMemberId",
                principalTable: "BandMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RehearsalMembers_Bands_BandId",
                table: "RehearsalMembers",
                column: "BandId",
                principalTable: "Bands",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RehearsalMembers_Users_UserId",
                table: "RehearsalMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BandMembers_Roles_RoleId",
                table: "BandMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RehearsalMembers_BandMembers_BandMemberId",
                table: "RehearsalMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RehearsalMembers_Bands_BandId",
                table: "RehearsalMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RehearsalMembers_Users_UserId",
                table: "RehearsalMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RehearsalMembers",
                table: "RehearsalMembers");

            migrationBuilder.DropIndex(
                name: "IX_RehearsalMembers_BandId",
                table: "RehearsalMembers");

            migrationBuilder.DropIndex(
                name: "IX_RehearsalMembers_BandMemberId",
                table: "RehearsalMembers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers");

            migrationBuilder.DropIndex(
                name: "IX_BandMembers_BandId_UserId",
                table: "BandMembers");

            migrationBuilder.DropColumn(
                name: "BandMemberId",
                table: "RehearsalMembers");

            migrationBuilder.DropColumn(
                name: "BandId",
                table: "RehearsalMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BandMembers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "RehearsalMembers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RehearsalMembers",
                table: "RehearsalMembers",
                columns: new[] { "RehearsalId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BandMembers",
                table: "BandMembers",
                columns: new[] { "BandId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BandMembers_Roles_RoleId",
                table: "BandMembers",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RehearsalMembers_Users_UserId",
                table: "RehearsalMembers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
