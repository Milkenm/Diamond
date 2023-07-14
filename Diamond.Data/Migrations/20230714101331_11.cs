using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Data.Migrations
{
    /// <inheritdoc />
    public partial class _11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonStrategies_PokemonFormats_FormatId",
                table: "PokemonStrategies");

            migrationBuilder.AlterColumn<long>(
                name: "FormatId",
                table: "PokemonStrategies",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonStrategies_PokemonFormats_FormatId",
                table: "PokemonStrategies",
                column: "FormatId",
                principalTable: "PokemonFormats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonStrategies_PokemonFormats_FormatId",
                table: "PokemonStrategies");

            migrationBuilder.AlterColumn<long>(
                name: "FormatId",
                table: "PokemonStrategies",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonStrategies_PokemonFormats_FormatId",
                table: "PokemonStrategies",
                column: "FormatId",
                principalTable: "PokemonFormats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
