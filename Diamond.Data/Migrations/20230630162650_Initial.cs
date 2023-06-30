using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Diamond.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AutoPublisher",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GuildId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    ChannelId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    TrackingSince = table.Column<long>(type: "bigint", nullable: false),
                    AddedByUserId = table.Column<ulong>(type: "bigint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutoPublisher", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CacheRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<byte[]>(type: "longblob", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CacheRecord", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CsgoItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClassId = table.Column<long>(type: "bigint", nullable: false),
                    IconUrl = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RarityHexColor = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstSaleDateUnix = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsgoItems", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonAbilities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Accuracy = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    PowerPoints = table.Column<int>(type: "int", nullable: false),
                    GenerationsList = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAbilities", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pokemons",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsNonstandard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Defense = table.Column<int>(type: "int", nullable: false),
                    SpecialAttack = table.Column<int>(type: "int", nullable: false),
                    SpecialDefense = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: false),
                    Height = table.Column<float>(type: "float", nullable: false),
                    DexNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pokemons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiscordMessageId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    DiscordUserId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ThumbnailUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsPublished = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Setting = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CsgoItemPrices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Epoch = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Average = table.Column<double>(type: "double", nullable: false),
                    Median = table.Column<double>(type: "double", nullable: false),
                    Sold = table.Column<long>(type: "bigint", nullable: false),
                    StandardDeviation = table.Column<float>(type: "float", nullable: false),
                    LowestPrice = table.Column<double>(type: "double", nullable: false),
                    HighestPrice = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CsgoItemPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CsgoItemPrices_CsgoItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "CsgoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DbPokemonDbPokemon",
                columns: table => new
                {
                    AltsListId = table.Column<long>(type: "bigint", nullable: false),
                    EvolutionsListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbPokemonDbPokemon", x => new { x.AltsListId, x.EvolutionsListId });
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemon_Pokemons_AltsListId",
                        column: x => x.AltsListId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbPokemonDbPokemon_Pokemons_EvolutionsListId",
                        column: x => x.EvolutionsListId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonFormats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Abbreviation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GenerationsList = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonFormats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonFormats_Pokemons_DbPokemonId",
                        column: x => x.DbPokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonGenerations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Abbreviation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonGenerations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonGenerations_Pokemons_DbPokemonId",
                        column: x => x.DbPokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonLearnsets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
                    MoveId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonLearnsets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonLearnsets_PokemonAbilities_MoveId",
                        column: x => x.MoveId,
                        principalTable: "PokemonAbilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonLearnsets_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GenerationsList = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonTypes_Pokemons_DbPokemonId",
                        column: x => x.DbPokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollOption",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TargetPollId = table.Column<long>(type: "bigint", nullable: false),
                    OptionName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OptionDescription = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollOption", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollOption_Poll_TargetPollId",
                        column: x => x.TargetPollId,
                        principalTable: "Poll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonStrategies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FormatId = table.Column<long>(type: "bigint", nullable: false),
                    Outdated = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    PokemonId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_PokemonStrategies_Pokemons_PokemonId",
                        column: x => x.PokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonAttackEffectives",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    AttackerTypeId = table.Column<long>(type: "bigint", nullable: false),
                    TargetTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonAttackEffectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonAttackEffectives_PokemonTypes_AttackerTypeId",
                        column: x => x.AttackerTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PokemonAttackEffectives_PokemonTypes_TargetTypeId",
                        column: x => x.TargetTypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PollVote",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    PollId = table.Column<long>(type: "bigint", nullable: false),
                    PollOptionId = table.Column<long>(type: "bigint", nullable: false),
                    VotedAt = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollVote_PollOption_PollOptionId",
                        column: x => x.PollOptionId,
                        principalTable: "PollOption",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollVote_Poll_PollId",
                        column: x => x.PollId,
                        principalTable: "Poll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsNonstandard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GenerationsList = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonStrategyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonItems_PokemonStrategies_DbPokemonStrategyId",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonNatures",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Summary = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<float>(type: "float", nullable: false),
                    Defense = table.Column<float>(type: "float", nullable: false),
                    SpecialAttack = table.Column<float>(type: "float", nullable: false),
                    SpecialDefense = table.Column<float>(type: "float", nullable: false),
                    Speed = table.Column<float>(type: "float", nullable: false),
                    GenerationsList = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonStrategyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonNatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonNatures_PokemonStrategies_DbPokemonStrategyId",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonPassives",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsNonstandard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    GenerationsList = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DbPokemonId = table.Column<long>(type: "bigint", nullable: true),
                    DbPokemonStrategyId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokemonPassives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonPassives_PokemonStrategies_DbPokemonStrategyId",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonPassives_Pokemons_DbPokemonId",
                        column: x => x.DbPokemonId,
                        principalTable: "Pokemons",
                        principalColumn: "Id");
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
                    TypeId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_PokemonStrategyMovesets_PokemonTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "PokemonTypes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PokemonSmogonUsers",
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
                    table.PrimaryKey("PK_PokemonSmogonUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PokemonSmogonUsers_PokemonStrategies_DbPokemonStrategyId",
                        column: x => x.DbPokemonStrategyId,
                        principalTable: "PokemonStrategies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PokemonSmogonUsers_PokemonStrategyCreditsTeams_DbPokemonStra~",
                        column: x => x.DbPokemonStrategyCreditsTeamId,
                        principalTable: "PokemonStrategyCreditsTeams",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CsgoItemPrices_ItemId",
                table: "CsgoItemPrices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DbPokemonDbPokemon_EvolutionsListId",
                table: "DbPokemonDbPokemon",
                column: "EvolutionsListId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackEffectives_AttackerTypeId",
                table: "PokemonAttackEffectives",
                column: "AttackerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonAttackEffectives_TargetTypeId",
                table: "PokemonAttackEffectives",
                column: "TargetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonFormats_DbPokemonId",
                table: "PokemonFormats",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonGenerations_DbPokemonId",
                table: "PokemonGenerations",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonItems_DbPokemonStrategyId",
                table: "PokemonItems",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonLearnsets_MoveId",
                table: "PokemonLearnsets",
                column: "MoveId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonLearnsets_PokemonId",
                table: "PokemonLearnsets",
                column: "PokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonNatures_DbPokemonStrategyId",
                table: "PokemonNatures",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonPassives_DbPokemonId",
                table: "PokemonPassives",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonPassives_DbPokemonStrategyId",
                table: "PokemonPassives",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSmogonUsers_DbPokemonStrategyCreditsTeamId",
                table: "PokemonSmogonUsers",
                column: "DbPokemonStrategyCreditsTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonSmogonUsers_DbPokemonStrategyId",
                table: "PokemonSmogonUsers",
                column: "DbPokemonStrategyId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategies_FormatId",
                table: "PokemonStrategies",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategies_PokemonId",
                table: "PokemonStrategies",
                column: "PokemonId");

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

            migrationBuilder.CreateIndex(
                name: "IX_PokemonStrategyMovesets_TypeId",
                table: "PokemonStrategyMovesets",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PokemonTypes_DbPokemonId",
                table: "PokemonTypes",
                column: "DbPokemonId");

            migrationBuilder.CreateIndex(
                name: "IX_PollOption_TargetPollId",
                table: "PollOption",
                column: "TargetPollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollVote_PollId",
                table: "PollVote",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_PollVote_PollOptionId",
                table: "PollVote",
                column: "PollOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AutoPublisher");

            migrationBuilder.DropTable(
                name: "CacheRecord");

            migrationBuilder.DropTable(
                name: "CsgoItemPrices");

            migrationBuilder.DropTable(
                name: "DbPokemonDbPokemon");

            migrationBuilder.DropTable(
                name: "PokemonAttackEffectives");

            migrationBuilder.DropTable(
                name: "PokemonGenerations");

            migrationBuilder.DropTable(
                name: "PokemonItems");

            migrationBuilder.DropTable(
                name: "PokemonLearnsets");

            migrationBuilder.DropTable(
                name: "PokemonNatures");

            migrationBuilder.DropTable(
                name: "PokemonPassives");

            migrationBuilder.DropTable(
                name: "PokemonSmogonUsers");

            migrationBuilder.DropTable(
                name: "PokemonStrategyMovesets");

            migrationBuilder.DropTable(
                name: "PollVote");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "CsgoItems");

            migrationBuilder.DropTable(
                name: "PokemonStrategyCreditsTeams");

            migrationBuilder.DropTable(
                name: "PokemonAbilities");

            migrationBuilder.DropTable(
                name: "PokemonTypes");

            migrationBuilder.DropTable(
                name: "PollOption");

            migrationBuilder.DropTable(
                name: "PokemonStrategies");

            migrationBuilder.DropTable(
                name: "Poll");

            migrationBuilder.DropTable(
                name: "PokemonFormats");

            migrationBuilder.DropTable(
                name: "Pokemons");
        }
    }
}
