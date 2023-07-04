using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Data.Migrations
{
    /// <inheritdoc />
    public partial class _8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GenerationsList",
                table: "PokemonPassives",
                newName: "GenerationAbbreviation");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "PokemonTypes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "PokemonNatures",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "PokemonLearnsets",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "PokemonItems",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "PokemonAttackEffectives",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "GenerationAbbreviation",
                table: "PokemonAbilities",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonGenerationDbPokemonPassive",
                columns: table => new
                {
                    GenerationsListId = table.Column<long>(type: "bigint", nullable: false),
                    PassivesWithGenerationListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonGenerationDbPokemonPassive", x => new { x.GenerationsListId, x.PassivesWithGenerationListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonPassive_PokemonGenerations_Gener~",
                        column: x => x.GenerationsListId,
                        principalTable: "PokemonGenerations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonGenerationDbPokemonPassive_PokemonPassives_Passives~",
                        column: x => x.PassivesWithGenerationListId,
                        principalTable: "PokemonPassives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonGenerationDbPokemonPassive_PassivesWithGenerationLi~",
                table: "DbPokemonGenerationDbPokemonPassive",
                column: "PassivesWithGenerationListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbPokemonGenerationDbPokemonPassive");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "PokemonTypes");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "PokemonNatures");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "PokemonLearnsets");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "PokemonItems");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "PokemonAttackEffectives");

            migrationBuilder.DropColumn(
                name: "GenerationAbbreviation",
                table: "PokemonAbilities");

            migrationBuilder.RenameColumn(
                name: "GenerationAbbreviation",
                table: "PokemonPassives",
                newName: "GenerationsList");
        }
    }
}
