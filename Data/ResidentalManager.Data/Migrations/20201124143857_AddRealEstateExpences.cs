using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentalManager.Data.Migrations
{
    public partial class AddRealEstateExpences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RealEstateExpences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalExpences = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RealEstateId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateExpences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateExpences_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateExpencesOther",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealEstateExpenceId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateExpencesOther", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateExpencesOther_RealEstateExpences_RealEstateExpenceId",
                        column: x => x.RealEstateExpenceId,
                        principalTable: "RealEstateExpences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RealEstateExpencesRegular",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyProperty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Decription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RealEstateExpenceId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstateExpencesRegular", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstateExpencesRegular_RealEstateExpences_RealEstateExpenceId",
                        column: x => x.RealEstateExpenceId,
                        principalTable: "RealEstateExpences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateExpences_IsDeleted",
                table: "RealEstateExpences",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateExpences_RealEstateId",
                table: "RealEstateExpences",
                column: "RealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateExpencesOther_IsDeleted",
                table: "RealEstateExpencesOther",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateExpencesOther_RealEstateExpenceId",
                table: "RealEstateExpencesOther",
                column: "RealEstateExpenceId");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateExpencesRegular_IsDeleted",
                table: "RealEstateExpencesRegular",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstateExpencesRegular_RealEstateExpenceId",
                table: "RealEstateExpencesRegular",
                column: "RealEstateExpenceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstateExpencesOther");

            migrationBuilder.DropTable(
                name: "RealEstateExpencesRegular");

            migrationBuilder.DropTable(
                name: "RealEstateExpences");
        }
    }
}
