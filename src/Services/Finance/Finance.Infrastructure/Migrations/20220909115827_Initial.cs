using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAD.Finance.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpsenseCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpsenseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expsenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    IsExcluded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expsenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expsenses_ExpsenseCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ExpsenseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ExpsenseCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("44f93169-8a8f-486f-b941-891d6ab7fb8a"), "Foodstuff" },
                    { new Guid("6992575c-29f4-4c78-aaf7-b50e2e80f3e9"), "Utility bills" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expsenses_CategoryId",
                table: "Expsenses",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expsenses");

            migrationBuilder.DropTable(
                name: "ExpsenseCategories");
        }
    }
}
