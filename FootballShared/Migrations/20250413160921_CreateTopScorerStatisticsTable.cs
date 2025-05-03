using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FootballShared.Migrations
{
    /// <inheritdoc />
    public partial class CreateTopScorerStatisticsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "football");

            migrationBuilder.CreateTable(
                name: "top_scorer_statistics",
                schema: "football",
                columns: table => new
                {
                    competition_id = table.Column<int>(type: "integer", nullable: false),
                    person_id = table.Column<int>(type: "integer", nullable: false),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    played_matches = table.Column<int>(type: "integer", nullable: true),
                    goals = table.Column<int>(type: "integer", nullable: false),
                    assists = table.Column<int>(type: "integer", nullable: true),
                    penalties = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_top_scorer_statistics", x => new { x.competition_id, x.person_id, x.team_id });
                    table.ForeignKey(
                        name: "fk_top_scorer_statistics_competition_competition_id",
                        column: x => x.competition_id,
                        principalSchema: "football",
                        principalTable: "competition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_top_scorer_statistics_person_person_id",
                        column: x => x.person_id,
                        principalSchema: "football",
                        principalTable: "person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_top_scorer_statistics_team_team_id",
                        column: x => x.team_id,
                        principalSchema: "football",
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_top_scorer_statistics_person_id",
                schema: "football",
                table: "top_scorer_statistics",
                column: "person_id");

            migrationBuilder.CreateIndex(
                name: "ix_top_scorer_statistics_team_id",
                schema: "football",
                table: "top_scorer_statistics",
                column: "team_id");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "matches",
                schema: "football");

            migrationBuilder.DropTable(
                name: "team_competition",
                schema: "football");

            migrationBuilder.DropTable(
                name: "top_scorer_statistics",
                schema: "football");

            migrationBuilder.DropTable(
                name: "competition",
                schema: "football");

            migrationBuilder.DropTable(
                name: "person",
                schema: "football");

            migrationBuilder.DropTable(
                name: "team",
                schema: "football");

            migrationBuilder.DropTable(
                name: "area",
                schema: "football");
        }
    }
}
