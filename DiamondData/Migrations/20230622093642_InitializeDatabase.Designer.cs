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
    [Migration("20230622093642_InitializeDatabase")]
    partial class InitializeDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.Property<string>("AbilitiesList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Attack")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int?>("DexNumber")
                        .HasColumnType("int");

                    b.Property<string>("EvolutionsList")
                        .HasColumnType("longtext");

                    b.Property<string>("FormatsList")
                        .HasColumnType("longtext");

                    b.Property<string>("GenerationsList")
                        .HasColumnType("longtext");

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

                    b.Property<string>("TypesList")
                        .IsRequired()
                        .HasColumnType("longtext");

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

                    b.Property<string>("GenerationsList")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PokemonFormats");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonGenerations", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("PokemonGenerations");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonItem", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.ToTable("PokemonItems");
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

                    b.ToTable("PokemonNatures");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonPassive", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.ToTable("PokemonPassives");
                });

            modelBuilder.Entity("Diamond.Data.Models.Pokemons.DbPokemonType", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.ToTable("PokemonTypes");
                });

            modelBuilder.Entity("Diamond.Data.Models.Polls.DbPoll", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<ulong>("DiscordMessageId")
                        .HasColumnType("bigint unsigned");

                    b.Property<ulong>("DiscordUserId")
                        .HasColumnType("bigint unsigned");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long>("UpdatedAt")
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
#pragma warning restore 612, 618
        }
    }
}
