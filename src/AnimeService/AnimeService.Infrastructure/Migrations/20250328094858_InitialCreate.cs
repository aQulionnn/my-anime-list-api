using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnimeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "anime_serial",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    season = table.Column<int>(type: "integer", nullable: false),
                    part = table.Column<int>(type: "integer", nullable: false),
                    episodes = table.Column<int>(type: "integer", nullable: false),
                    watched_episodes = table.Column<int>(type: "integer", nullable: false),
                    fillers = table.Column<int>(type: "integer", nullable: false),
                    release_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    viewing_year = table.Column<int>(type: "integer", nullable: false),
                    viewing_order = table.Column<int>(type: "integer", nullable: false),
                    poster_url = table.Column<string>(type: "text", nullable: false),
                    franchise_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anime_serial", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "anime_serial_info",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    language = table.Column<int>(type: "integer", nullable: false),
                    anime_serial_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anime_serial_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_anime_serial_info_anime_serial_anime_serial_id",
                        column: x => x.anime_serial_id,
                        principalTable: "anime_serial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "re_watched_anime_serial",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    re_watched_episodes = table.Column<int>(type: "integer", nullable: false),
                    viewing_order = table.Column<int>(type: "integer", nullable: false),
                    anime_serial_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_re_watched_anime_serial", x => x.id);
                    table.ForeignKey(
                        name: "FK_re_watched_anime_serial_anime_serial_anime_serial_id",
                        column: x => x.anime_serial_id,
                        principalTable: "anime_serial",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_anime_serial_info_anime_serial_id",
                table: "anime_serial_info",
                column: "anime_serial_id");

            migrationBuilder.CreateIndex(
                name: "IX_re_watched_anime_serial_anime_serial_id",
                table: "re_watched_anime_serial",
                column: "anime_serial_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "anime_serial_info");

            migrationBuilder.DropTable(
                name: "re_watched_anime_serial");

            migrationBuilder.DropTable(
                name: "anime_serial");
        }
    }
}
