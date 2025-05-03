using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FootballShared.Migrations
{
    /// <inheritdoc />
    public partial class CreateTeamPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "team_positions",
                schema: "football",
                columns: table => new
                {
                    competition_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    team_id = table.Column<int>(type: "integer", nullable: false),
                    position = table.Column<int>(type: "integer", nullable: false),
                    played_games = table.Column<int>(type: "integer", nullable: false),
                    form = table.Column<string>(type: "text", nullable: false),
                    won = table.Column<int>(type: "integer", nullable: true),
                    draw = table.Column<int>(type: "integer", nullable: true),
                    lost = table.Column<int>(type: "integer", nullable: true),
                    points = table.Column<int>(type: "integer", nullable: false),
                    goals_for = table.Column<int>(type: "integer", nullable: true),
                    goals_against = table.Column<int>(type: "integer", nullable: true),
                    goal_difference = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_team_positions", x => new { x.competition_id, x.type, x.team_id });
                    table.ForeignKey(
                        name: "fk_team_positions_competition_competition_id",
                        column: x => x.competition_id,
                        principalSchema: "football",
                        principalTable: "competition",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_team_positions_team_team_id",
                        column: x => x.team_id,
                        principalSchema: "football",
                        principalTable: "team",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_team_positions_competition_id",
                schema: "football",
                table: "team_positions",
                column: "competition_id");

            migrationBuilder.CreateIndex(
                name: "ix_team_positions_team_id",
                schema: "football",
                table: "team_positions",
                column: "team_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "team_positions",
                schema: "football");
        }
    }
}
