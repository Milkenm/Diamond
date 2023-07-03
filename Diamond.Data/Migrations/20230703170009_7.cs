using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Data.Migrations
{
    /// <inheritdoc />
    public partial class _7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenerationsList",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "GenerationsList",
                table: "PokemonNatures");

            migrationBuilder.DropColumn(
                name: "GenerationsList",
                table: "PokemonItems");

            migrationBuilder.DropColumn(
                name: "GenerationsList",
                table: "PokemonFormats");

            migrationBuilder.DropColumn(
                name: "GenerationsList",
                table: "PokemonAbilities");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "Pokemons",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonFormatDbPokemonGeneration",
                columns: table => new
                {
                    FormatsWithGenerationListId = table.Column<long>(type: "bigint", nullable: false),
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonFormatDbPokemonGeneration", x => new { x.FormatsWithGenerationListId, x.GenerationsListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonFormatDbPokemonGeneration_PokemonFormats_FormatsWit~",
                        column: x => x.FormatsWithGenerationListId,
                        principalTable: "PokemonFormats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonFormatDbPokemonGeneration_PokemonGenerations_Genera~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonGenerationDbPokemonItem",
                columns: table => new
                {
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false),
                    ItemsWithGenerationListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonGenerationDbPokemonItem", x => new { x.GenerationsListId, x.ItemsWithGenerationListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonItem_PokemonGenerations_Generati~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonItem_PokemonItems_ItemsWithGener~",
                        column: x => x.ItemsWithGenerationListId,
                        principalTable: "PokemonItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonGenerationDbPokemonMove",
                columns: table => new
                {
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false),
                    MovesWithGenerationListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonGenerationDbPokemonMove", x => new { x.GenerationsListId, x.MovesWithGenerationListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonMove_PokemonAbilities_MovesWithG~",
                        column: x => x.MovesWithGenerationListId,
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonMove_PokemonGenerations_Generati~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonGenerationDbPokemonNature",
                columns: table => new
                {
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false),
                    NaturesWithGenerationListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonGenerationDbPokemonNature", x => new { x.GenerationsListId, x.NaturesWithGenerationListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonNature_PokemonGenerations_Genera~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonNature_PokemonNatures_NaturesWit~",
                        column: x => x.NaturesWithGenerationListId,
                        principalTable: "PokemonNatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonGenerationDbPokemonType",
                columns: table => new
                {
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false),
                    TypesWithGenerationListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonGenerationDbPokemonType", x => new { x.GenerationsListId, x.TypesWithGenerationListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonType_PokemonGenerations_Generati~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonType_PokemonTypes_TypesWithGener~",
                        column: x => x.TypesWithGenerationListId,
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonFormatDbPokemonGeneration_GenerationsListId",
                table: "DbPokemonFormatDbPokemonGeneration",
                column: "GenerationsListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonGenerationDbPokemonItem_ItemsWithGenerationListId",
                table: "DbPokemonGenerationDbPokemonItem",
                column: "ItemsWithGenerationListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonGenerationDbPokemonMove_MovesWithGenerationListId",
                table: "DbPokemonGenerationDbPokemonMove",
                column: "MovesWithGenerationListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonGenerationDbPokemonNature_NaturesWithGenerationList~",
                table: "DbPokemonGenerationDbPokemonNature",
                column: "NaturesWithGenerationListId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonGenerationDbPokemonType_TypesWithGenerationListId",
                table: "DbPokemonGenerationDbPokemonType",
                column: "TypesWithGenerationListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbPokemonFormatDbPokemonGeneration");

            migrationBuilder.DropTable(
                name: "DbPokemonGenerationDbPokemonItem");

            migrationBuilder.DropTable(
                name: "DbPokemonGenerationDbPokemonMove");

            migrationBuilder.DropTable(
                name: "DbPokemonGenerationDbPokemonNature");

            migrationBuilder.DropTable(
                name: "DbPokemonGenerationDbPokemonType");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "Pokemons");

            migrationBuilder.AddColumn<string>(
                name: "GenerationsList",
                table: "PokemonTypes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationsList",
                table: "PokemonNatures",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationsList",
                table: "PokemonItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationsList",
                table: "PokemonFormats",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationsList",
                table: "PokemonAbilities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
