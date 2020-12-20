using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentalManager.Data.Migrations
{
    public partial class AddPropertyIdAndRealEsateIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RealEstateId",
                table: "AspNetUsers");
        }
    }
}
