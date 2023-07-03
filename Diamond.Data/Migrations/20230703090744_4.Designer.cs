﻿// <auto-generated />
using System;
using Diamond.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Diamond.Data.Migrations
{
    [DbContext(typeof(DiamondContext))]
    [Migration("20230703090744_4")]
    partial class _4
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DbPokemonDbPokemon", b =>
                {
                    b.Property<long>("AltsListId")
                        .HasColumnType("bigint");

                    b.Property<long>("EvolutionsListId")
                        .HasColumnType("bigint");

                    b.HasKey("AltsListId", "EvolutionsListId");

                    b.HasIndex("EvolutionsListId");

                    b.ToTable("DbPokemonDbPokemon");
                });

            modelBuilder.Entity("Diamond.Data.Models.AutoPublisher.DbAutoPublisherChannel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<ulong>("AddedByUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("ChannelId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("GuildId")
                        .HasColumnType("bigint unsigned");

                    b.Property<long>("TrackingSinceUnix")
                        .HasColumnType("bigint")
                        .HasColumnName("TrackingSince");

                    b.HasKey("Id");

                    b.ToTable("AutoPublisher");
                });

            modelBuilder.Entity("Diamond.Data.Models.CsgoItems.DbCsgoItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("ClassId")
                        .HasColumnType("bigint");

                    b.Property<long>("FirstSaleDateUnix")
                        .HasColumnType("bigint");

                    b.Property<string>("IconUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RarityHexColor")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CsgoItems");
                });

            modelBuilder.Entity("Diamond.Data.Models.CsgoItems.DbCsgoItemPrice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<double>("Average")
                        .HasColumnType("double");

                    b.Property<int>("Currency")
                        .HasColumnType("int");

                    b.Property<string>("Epoch")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("HighestPrice")
                        .HasColumnType("double");

                    b.Property<long>("ItemId")
                        .HasColumnType("bigint");

                    b.Property<double>("LowestPrice")
                        .HasColumnType("double");

                    b.Property<double>("Median")
                        .HasColumnType("double");

                    b.Property<long>("Sold")
                        .HasColumnType("bigint");

                    b.Property<float>("StandardDeviation")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("CsgoItemPrices");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemon", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int?>("DexNumber")
                        .HasColumnType("int");

                    b.Property<bool?>("HasStrategies")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("float");

                    b.Property<bool>("IsNonstandard")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SpecialAttack")
                        .HasColumnType("int");

                    b.Property<int>("SpecialDefense")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Pokemons");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonAttackEffectiveness", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("AttackerTypeId")
                        .HasColumnType("bigint");

                    b.Property<long>("TargetTypeId")
                        .HasColumnType("bigint");

                    b.Property<float>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AttackerTypeId");

                    b.HasIndex("TargetTypeId");

                    b.ToTable("PokemonAttackEffectives");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonFormat", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("DbPokemonId")
                        .HasColumnType("bigint");

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonId");

                    b.ToTable("PokemonFormats");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonGeneration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("DbPokemonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonId");

                    b.ToTable("PokemonGenerations");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonStrategyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsNonstandard")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonStrategyId");

                    b.ToTable("PokemonItems");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonLearnset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("MoveId")
                        .HasColumnType("bigint");

                    b.Property<long>("PokemonId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MoveId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonLearnsets");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonMove", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<int>("Accuracy")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<int>("PowerPoints")
                        .HasColumnType("int");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PokemonAbilities");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonNature", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<float>("Attack")
                        .HasColumnType("float");

                    b.Property<long?>("DbPokemonStrategyId")
                        .HasColumnType("bigint");

                    b.Property<float>("Defense")
                        .HasColumnType("float");

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("HealthPoints")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<float>("SpecialAttack")
                        .HasColumnType("float");

                    b.Property<float>("SpecialDefense")
                        .HasColumnType("float");

                    b.Property<float>("Speed")
                        .HasColumnType("float");

                    b.Property<string>("Summary")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonStrategyId");

                    b.ToTable("PokemonNatures");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonPassive", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonStrategyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsNonstandard")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonId");

                    b.HasIndex("DbPokemonStrategyId");

                    b.ToTable("PokemonPassives");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EVsAttack")
                        .HasColumnType("int");

                    b.Property<int>("EVsDefense")
                        .HasColumnType("int");

                    b.Property<int>("EVsHealth")
                        .HasColumnType("int");

                    b.Property<int>("EVsSpecialAttack")
                        .HasColumnType("int");

                    b.Property<int>("EVsSpecialDefense")
                        .HasColumnType("int");

                    b.Property<int>("EVsSpeed")
                        .HasColumnType("int");

                    b.Property<long>("FormatId")
                        .HasColumnType("bigint");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("MovesetName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool?>("Outdated")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Overview")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("PokemonId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FormatId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonStrategies");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategyCreditsTeam", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonStrategyId")
                        .HasColumnType("bigint");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonStrategyId");

                    b.ToTable("PokemonStrategyCreditsTeams");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategyMoveset", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonStrategyId")
                        .HasColumnType("bigint");

                    b.Property<long>("MoveId")
                        .HasColumnType("bigint");

                    b.Property<long?>("TypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonStrategyId");

                    b.HasIndex("MoveId");

                    b.HasIndex("TypeId");

                    b.ToTable("PokemonStrategyMovesets");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonId");

                    b.ToTable("PokemonTypes");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.PokemonSmogonUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonStrategyCreditsTeamId")
                        .HasColumnType("bigint");

                    b.Property<long?>("DbPokemonStrategyId")
                        .HasColumnType("bigint");

                    b.Property<long>("SmogonUserId")
                        .HasColumnType("bigint");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DbPokemonStrategyCreditsTeamId");

                    b.HasIndex("DbPokemonStrategyId");

                    b.ToTable("PokemonSmogonUsers");
                });

            modelBuilder.Entity("Diamond.Data.Models.Polls.DbPoll", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<ulong>("DiscordMessageId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("DiscordUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("UpdatedAt")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Poll");
                });

            modelBuilder.Entity("Diamond.Data.Models.Polls.DbPollOption", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("OptionDescription");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("OptionName");

                    b.Property<long>("TargetPollId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("TargetPollId");

                    b.ToTable("PollOption");
                });

            modelBuilder.Entity("Diamond.Data.Models.Polls.DbPollVote", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("PollId")
                        .HasColumnType("bigint");

                    b.Property<long>("PollOptionId")
                        .HasColumnType("bigint");

                    b.Property<ulong>("UserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<long>("VotedAt")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PollId");

                    b.HasIndex("PollOptionId");

                    b.ToTable("PollVote");
                });

            modelBuilder.Entity("Diamond.Data.Models.Settings.DbCacheRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("UpdatedAt")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Value")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.HasKey("Id");

                    b.ToTable("CacheRecord");
                });

            modelBuilder.Entity("Diamond.Data.Models.Settings.DbSetting", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Setting");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Setting");
                });

            modelBuilder.Entity("DbPokemonDbPokemon", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", null)
                        .WithMany()
                        .HasForeignKey("AltsListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", null)
                        .WithMany()
                        .HasForeignKey("EvolutionsListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Diamond.Data.Models.CsgoItems.DbCsgoItemPrice", b =>
                {
                    b.HasOne("Diamond.Data.Models.CsgoItems.DbCsgoItem", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonAttackEffectiveness", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonType", "AttackerType")
                        .WithMany()
                        .HasForeignKey("AttackerTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonType", "TargetType")
                        .WithMany()
                        .HasForeignKey("TargetTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttackerType");

                    b.Navigation("TargetType");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonFormat", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", null)
                        .WithMany("FormatsList")
                        .HasForeignKey("DbPokemonId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonGeneration", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", null)
                        .WithMany("GenerationsList")
                        .HasForeignKey("DbPokemonId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonItem", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategy", null)
                        .WithMany("ItemsList")
                        .HasForeignKey("DbPokemonStrategyId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonLearnset", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonMove", "Move")
                        .WithMany()
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Move");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonNature", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategy", null)
                        .WithMany("NaturesList")
                        .HasForeignKey("DbPokemonStrategyId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonPassive", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", null)
                        .WithMany("AbilitiesList")
                        .HasForeignKey("DbPokemonId");

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategy", null)
                        .WithMany("PassivesList")
                        .HasForeignKey("DbPokemonStrategyId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategy", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonFormat", "Format")
                        .WithMany()
                        .HasForeignKey("FormatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", "Pokemon")
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Format");

                    b.Navigation("Pokemon");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategyCreditsTeam", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategy", null)
                        .WithMany("TeamsList")
                        .HasForeignKey("DbPokemonStrategyId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategyMoveset", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategy", null)
                        .WithMany("Movesets")
                        .HasForeignKey("DbPokemonStrategyId");

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonMove", "Move")
                        .WithMany()
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId");

                    b.Navigation("Move");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonType", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemon", null)
                        .WithMany("TypesList")
                        .HasForeignKey("DbPokemonId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.PokemonSmogonUser", b =>
                {
                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategyCreditsTeam", null)
                        .WithMany("TeamMembersList")
                        .HasForeignKey("DbPokemonStrategyCreditsTeamId");

                    b.HasOne("Diamond.Data.Models.Pokemons.DbPokemonStrategy", null)
                        .WithMany("WrittenByUsersList")
                        .HasForeignKey("DbPokemonStrategyId");
                });

            modelBuilder.Entity("Diamond.Data.Models.Polls.DbPollOption", b =>
                {
                    b.HasOne("Diamond.Data.Models.Polls.DbPoll", "TargetPoll")
                        .WithMany()
                        .HasForeignKey("TargetPollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TargetPoll");
                });

            modelBuilder.Entity("Diamond.Data.Models.Polls.DbPollVote", b =>
                {
                    b.HasOne("Diamond.Data.Models.Polls.DbPoll", "Poll")
                        .WithMany()
                        .HasForeignKey("PollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Diamond.Data.Models.Polls.DbPollOption", "PollOption")
                        .WithMany()
                        .HasForeignKey("PollOptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Poll");

                    b.Navigation("PollOption");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemon", b =>
                {
                    b.Navigation("AbilitiesList");

                    b.Navigation("FormatsList");

                    b.Navigation("GenerationsList");

                    b.Navigation("TypesList");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategy", b =>
                {
                    b.Navigation("ItemsList");

                    b.Navigation("Movesets");

                    b.Navigation("NaturesList");

                    b.Navigation("PassivesList");

                    b.Navigation("TeamsList");

                    b.Navigation("WrittenByUsersList");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonStrategyCreditsTeam", b =>
                {
                    b.Navigation("TeamMembersList");
                });
#pragma warning restore 612, 618
        }
    }
}
