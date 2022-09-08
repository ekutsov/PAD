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
                    Id = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<string>(type: "text", nullable: true),
                    AuthorId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
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
                        principalColumn: "Id");
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
