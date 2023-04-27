using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class addForeignKeyVillaNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VillaID",
                table: "villaNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_villaNumber_VillaID",
                table: "villaNumber",
                column: "VillaID");

            migrationBuilder.AddForeignKey(
                name: "FK_villaNumber_Villas_VillaID",
                table: "villaNumber",
                column: "VillaID",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_villaNumber_Villas_VillaID",
                table: "villaNumber");

            migrationBuilder.DropIndex(
                name: "IX_villaNumber_VillaID",
                table: "villaNumber");

            migrationBuilder.DropColumn(
                name: "VillaID",
                table: "villaNumber");
        }
    }
}
