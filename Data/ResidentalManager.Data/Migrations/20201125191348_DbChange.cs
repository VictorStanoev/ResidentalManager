namespace ResidentalManager.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DbChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExpenceType",
                table: "RealEstateExpences",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpenceType",
                table: "RealEstateExpences");
        }
    }
}
