using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaNumberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.CreateTable(
                name: "villaNumber",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    SpecialDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_villaNumber", x => x.VillaNo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "villaNumber");

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "Sqft", "UpdateDate" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(1977), "Royal Villa Details", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fik.imagekit.io%2F5tgxhsqev%2Fbackup-bucket%2Ftr%3Aw-800%2Ch-460%2Cq-62%2Fimage%2Fupload%2Fr2jetjmjhj5i6yijouhp.webp&tbnid=mawYSi6wALGu5M&vet=12ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg..i&imgrefurl=https%3A%2F%2Fwww.saffronstays.com%2Fvillas%2Fvillas-in-Nagaon%2520Beach%3Flat%3D18.563400%26lon%3D72.871400%26nearby%3Dtrue&docid=WBtW53B88S-OrM&w=800&h=460&q=villas&ved=2ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg", "Royal Villa", 5, 200.0, 500, new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(2008) },
                    { 2, "", new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(2019), "Royal Villa Details2", "https://www.google.com/imgres?imgurl=https%3A%2F%2Fik.imagekit.io%2F5tgxhsqev%2Fbackup-bucket%2Ftr%3Aw-800%2Ch-460%2Cq-62%2Fimage%2Fupload%2Fr2jetjmjhj5i6yijouhp.webp&tbnid=mawYSi6wALGu5M&vet=12ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg..i&imgrefurl=https%3A%2F%2Fwww.saffronstays.com%2Fvillas%2Fvillas-in-Nagaon%2520Beach%3Flat%3D18.563400%26lon%3D72.871400%26nearby%3Dtrue&docid=WBtW53B88S-OrM&w=800&h=460&q=villas&ved=2ahUKEwjQ3eXOhsL-AhUU5XMBHQe9BWQQMygVegUIARCIAg", "Royal Villa2", 5, 200.0, 500, new DateTime(2023, 4, 24, 13, 42, 51, 743, DateTimeKind.Local).AddTicks(2021) }
                });
        }
    }
}
