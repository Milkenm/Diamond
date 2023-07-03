using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Data.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonFormats_Pokemons_DbPokemonId",
                table: "PokemonFormats");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonGenerations_Pokemons_DbPokemonId",
                table: "PokemonGenerations");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonPassives_Pokemons_DbPokemonId",
                table: "PokemonPassives");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonTypes_Pokemons_DbPokemonId",
                table: "PokemonTypes");

            migrationBuilder.DropIndex(
                name: "IX_PokemonTypes_DbPokemonId",
                table: "PokemonTypes");

            migrationBuilder.DropIndex(
                name: "IX_PokemonPassives_DbPokemonId",
                table: "PokemonPassives");

            migrationBuilder.DropIndex(
                name: "IX_PokemonGenerations_DbPokemonId",
                table: "PokemonGenerations");

            migrationBuilder.DropIndex(
                name: "IX_PokemonFormats_DbPokemonId",
                table: "PokemonFormats");

            migrationBuilder.DropColumn(
                name: "DbPokemonId",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "DbPokemonId",
                table: "PokemonPassives");

            migrationBuilder.DropColumn(
                name: "DbPokemonId",
                table: "PokemonGenerations");

            migrationBuilder.DropColumn(
                name: "DbPokemonId",
                table: "PokemonFormats");

            migrationBuilder.CreateTable(
                name: "DbPokemonDbPokemonFormat",
                columns: table => new
                {
                    FormatsListId = table.Column<long>(type: "bigint", nullable: false),
                    PokemonsWithFormatListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonDbPokemonFormat", x => new { x.FormatsListId, x.PokemonsWithFormatListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonFormat_PokemonFormats_FormatsListId",
                        column: x => x.FormatsListId,
                        principalTable: "PokemonFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonFormat_Pokemons_PokemonsWithFormatListId",
                        column: x => x.PokemonsWithFormatListId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonDbPokemonGeneration",
                columns: table => new
                {
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false),
                    PokemonsWithGenerationListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonDbPokemonGeneration", x => new { x.GenerationsListId, x.PokemonsWithGenerationListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonGeneration_PokemonGenerations_GenerationsL~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonGeneration_Pokemons_PokemonsWithGeneration~",
                        column: x => x.PokemonsWithGenerationListId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonDbPokemonPassive",
                columns: table => new
                {
                    AbilitiesListId = table.Column<long>(type: "bigint", nullable: false),
                    PokemonsWithPassiveListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonDbPokemonPassive", x => new { x.AbilitiesListId, x.PokemonsWithPassiveListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonPassive_PokemonPassives_AbilitiesListId",
                        column: x => x.AbilitiesListId,
                        principalTable: "PokemonPassives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonPassive_Pokemons_PokemonsWithPassiveListId",
                        column: x => x.PokemonsWithPassiveListId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonDbPokemonType",
                columns: table => new
                {
                    PokemonsWithTypeListId = table.Column<long>(type: "bigint", nullable: false),
                    TypesListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonDbPokemonType", x => new { x.PokemonsWithTypeListId, x.TypesListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonType_PokemonTypes_TypesListId",
                        column: x => x.TypesListId,
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemonType_Pokemons_PokemonsWithTypeListId",
                        column: x => x.PokemonsWithTypeListId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonDbPokemonFormat_PokemonsWithFormatListId",
                table: "DbPokemonDbPokemonFormat",
                column: "PokemonsWithFormatListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonDbPokemonGeneration_PokemonsWithGenerationListId",
                table: "DbPokemonDbPokemonGeneration",
                column: "PokemonsWithGenerationListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonDbPokemonPassive_PokemonsWithPassiveListId",
                table: "DbPokemonDbPokemonPassive",
                column: "PokemonsWithPassiveListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonDbPokemonType_TypesListId",
                table: "DbPokemonDbPokemonType",
                column: "TypesListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbPokemonDbPokemonFormat");

            migrationBuilder.DropTable(
                name: "DbPokemonDbPokemonGeneration");

            migrationBuilder.DropTable(
                name: "DbPokemonDbPokemonPassive");

            migrationBuilder.DropTable(
                name: "DbPokemonDbPokemonType");

            migrationBuilder.AddColumn<long>(
                name: "DbPokemonId",
                table: "PokemonTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbPokemonId",
                table: "PokemonPassives",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbPokemonId",
                table: "PokemonGenerations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DbPokemonId",
                table: "PokemonFormats",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_DbPokemonId",
                table: "PokemonTypes",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonPassives_DbPokemonId",
                table: "PokemonPassives",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonGenerations_DbPokemonId",
                table: "PokemonGenerations",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonFormats_DbPokemonId",
                table: "PokemonFormats",
                column: "DbPokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonFormats_Pokemons_DbPokemonId",
                table: "PokemonFormats",
                column: "DbPokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonGenerations_Pokemons_DbPokemonId",
                table: "PokemonGenerations",
                column: "DbPokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonPassives_Pokemons_DbPokemonId",
                table: "PokemonPassives",
                column: "DbPokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonTypes_Pokemons_DbPokemonId",
                table: "PokemonTypes",
                column: "DbPokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id");
        }
    }
}
