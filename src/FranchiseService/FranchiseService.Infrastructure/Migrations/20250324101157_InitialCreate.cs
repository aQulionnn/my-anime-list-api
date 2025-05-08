using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FranchiseService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "anime_franchise",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    viewing_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anime_franchise", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "anime_franchise_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<int>(type: "integer", nullable: false),
                    anime_franchise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anime_franchise_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_anime_franchise_info_anime_franchise_anime_franchise_id",
                        column: x => x.anime_franchise_id,
                        principalTable: "anime_franchise",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anime_franchise_id",
                table: "anime_franchise",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_anime_franchise_info_anime_franchise_id",
                table: "anime_franchise_info",
                column: "anime_franchise_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anime_franchise_info");

            migrationBuilder.DropTable(
                name: "anime_franchise");
        }
    }
}
