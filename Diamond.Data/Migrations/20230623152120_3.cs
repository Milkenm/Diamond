using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Data.Migrations
{
    /// <inheritdoc />
    public partial class _3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DbPokemonStrategyId",
                table: "PokemonPassives",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbPokemonStrategyId",
                table: "PokemonNatures",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbPokemonStrategyId",
                table: "PokemonItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PokemonStrategies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FormatId = table.Column<long>(type: "bigint", nullable: false),
                    Outdated = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Overview = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MovesetName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EVsHealth = table.Column<int>(type: "int", nullable: false),
                    EVsAttack = table.Column<int>(type: "int", nullable: false),
                    EVsDefense = table.Column<int>(type: "int", nullable: false),
                    EVsSpecialAttack = table.Column<int>(type: "int", nullable: false),
                    EVsSpecialDefense = table.Column<int>(type: "int", nullable: false),
                    EVsSpeed = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStrategies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStrategies_PokemonFormats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "PokemonFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonStrategyCreditsTeams",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TeamName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonStrategyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStrategyCreditsTeams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStrategyCreditsTeams_PokemonStrategies_DbPokemonStrat~",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonStrategyMovesets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MoveId = table.Column<long>(type: "bigint", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonStrategyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStrategyMovesets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStrategyMovesets_PokemonAbilities_MoveId",
                        column: x => x.MoveId,
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonStrategyMovesets_PokemonStrategies_DbPokemonStrategyId",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonStrategyCredits",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SmogonUserId = table.Column<long>(type: "bigint", nullable: false),
                    DbPokemonStrategyCreditsTeamId = table.Column<long>(type: "bigint", nullable: true),
                    DbPokemonStrategyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonStrategyCredits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonStrategyCredits_PokemonStrategies_DbPokemonStrategyId",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonStrategyCredits_PokemonStrategyCreditsTeams_DbPokemon~",
                        column: x => x.DbPokemonStrategyCreditsTeamId,
                        principalTable: "PokemonStrategyCreditsTeams",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonPassives_DbPokemonStrategyId",
                table: "PokemonPassives",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonNatures_DbPokemonStrategyId",
                table: "PokemonNatures",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonItems_DbPokemonStrategyId",
                table: "PokemonItems",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategies_FormatId",
                table: "PokemonStrategies",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategyCredits_DbPokemonStrategyCreditsTeamId",
                table: "PokemonStrategyCredits",
                column: "DbPokemonStrategyCreditsTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategyCredits_DbPokemonStrategyId",
                table: "PokemonStrategyCredits",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategyCreditsTeams_DbPokemonStrategyId",
                table: "PokemonStrategyCreditsTeams",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategyMovesets_DbPokemonStrategyId",
                table: "PokemonStrategyMovesets",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategyMovesets_MoveId",
                table: "PokemonStrategyMovesets",
                column: "MoveId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonItems_PokemonStrategies_DbPokemonStrategyId",
                table: "PokemonItems",
                column: "DbPokemonStrategyId",
                principalTable: "PokemonStrategies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonNatures_PokemonStrategies_DbPokemonStrategyId",
                table: "PokemonNatures",
                column: "DbPokemonStrategyId",
                principalTable: "PokemonStrategies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonPassives_PokemonStrategies_DbPokemonStrategyId",
                table: "PokemonPassives",
                column: "DbPokemonStrategyId",
                principalTable: "PokemonStrategies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonItems_PokemonStrategies_DbPokemonStrategyId",
                table: "PokemonItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonNatures_PokemonStrategies_DbPokemonStrategyId",
                table: "PokemonNatures");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonPassives_PokemonStrategies_DbPokemonStrategyId",
                table: "PokemonPassives");

            migrationBuilder.DropTable(
                name: "PokemonStrategyCredits");

            migrationBuilder.DropTable(
                name: "PokemonStrategyMovesets");

            migrationBuilder.DropTable(
                name: "PokemonStrategyCreditsTeams");

            migrationBuilder.DropTable(
                name: "PokemonStrategies");

            migrationBuilder.DropIndex(
                name: "IX_PokemonPassives_DbPokemonStrategyId",
                table: "PokemonPassives");

            migrationBuilder.DropIndex(
                name: "IX_PokemonNatures_DbPokemonStrategyId",
                table: "PokemonNatures");

            migrationBuilder.DropIndex(
                name: "IX_PokemonItems_DbPokemonStrategyId",
                table: "PokemonItems");

            migrationBuilder.DropColumn(
                name: "DbPokemonStrategyId",
                table: "PokemonPassives");

            migrationBuilder.DropColumn(
                name: "DbPokemonStrategyId",
                table: "PokemonNatures");

            migrationBuilder.DropColumn(
                name: "DbPokemonStrategyId",
                table: "PokemonItems");
        }
    }
}
