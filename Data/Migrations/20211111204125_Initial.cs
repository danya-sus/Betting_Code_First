using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Betting.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 128, nullable: false),
                    Password = table.Column<string>(unicode: false, nullable: false),
                    Username = table.Column<string>(unicode: false, maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Towns",
                columns: table => new
                {
                    TownID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Towns", x => x.TownID);
                    table.ForeignKey(
                        name: "FK_Countries_Towns",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Budjet = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Initials = table.Column<string>(nullable: true),
                    LogoUrl = table.Column<string>(type: "varchar(max)", nullable: false),
                    PrimaryKitColorId = table.Column<int>(nullable: true),
                    SecondaryKitColorId = table.Column<int>(nullable: true),
                    TownId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                    table.ForeignKey(
                        name: "FK_PColors_PTeams",
                        column: x => x.PrimaryKitColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorID");
                    table.ForeignKey(
                        name: "FK_SColors_STeams",
                        column: x => x.SecondaryKitColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorID");
                    table.ForeignKey(
                        name: "FK_Towns_Teams",
                        column: x => x.TownId,
                        principalTable: "Towns",
                        principalColumn: "TownID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AwayTeamBetRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    AwayTeamGoals = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(nullable: false),
                    DrawBetRate = table.Column<decimal>(nullable: false),
                    HomeTeamBetRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    HomeTeamGoals = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(nullable: false),
                    Result = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(type: "smalldatetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_ATeams_AGames",
                        column: x => x.AwayTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamID");
                    table.ForeignKey(
                        name: "FK_HTeams_MGames",
                        column: x => x.HomeTeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamID");
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(unicode: false, maxLength: 64, nullable: false),
                    PositionId = table.Column<int>(nullable: true),
                    SquadNumber = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_Positions_Players",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Teams_Players",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    BetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    Prediction = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.BetID);
                    table.ForeignKey(
                        name: "FK_Games_Bets",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameID");
                    table.ForeignKey(
                        name: "FK_Users_Bets",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    PlayerID = table.Column<int>(nullable: false),
                    GameID = table.Column<int>(nullable: false),
                    Assists = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    ScoredGoals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => new { x.GameID, x.PlayerID });
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Games",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID");
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Players",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "PlayerID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_GameId",
                table: "Bets",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_AwayTeamId",
                table: "Games",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_HomeTeamId",
                table: "Games",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_PositionId",
                table: "Players",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_PlayerID",
                table: "PlayerStatistics",
                column: "PlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_PrimaryKitColorId",
                table: "Teams",
                column: "PrimaryKitColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_SecondaryKitColorId",
                table: "Teams",
                column: "SecondaryKitColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_TownId",
                table: "Teams",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_Towns_CountryId",
                table: "Towns",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Towns");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
