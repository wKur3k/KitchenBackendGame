﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleBackendGame.Entities;

namespace SimpleBackendGame.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20210827102313_siema2")]
    partial class siema2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HeroItem", b =>
                {
                    b.Property<int>("HeroesId")
                        .HasColumnType("int");

                    b.Property<int>("ItemsId")
                        .HasColumnType("int");

                    b.HasKey("HeroesId", "ItemsId");

                    b.HasIndex("ItemsId");

                    b.ToTable("HeroItem");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Enemy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Art")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<int>("Crit")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Enemies");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionLeft")
                        .HasColumnType("int");

                    b.Property<string>("Art")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Atk")
                        .HasColumnType("int");

                    b.Property<int>("Crit")
                        .HasColumnType("int");

                    b.Property<int>("Def")
                        .HasColumnType("int");

                    b.Property<int>("Gold")
                        .HasColumnType("int");

                    b.Property<int>("Hp")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Art")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Slot")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stat")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MessageContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EnemyId")
                        .HasColumnType("int");

                    b.Property<int>("GoldReward")
                        .HasColumnType("int");

                    b.Property<int>("Map")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnemyId")
                        .IsUnique();

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanUseChat")
                        .HasColumnType("bit");

                    b.Property<string>("HashedPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HeroId")
                        .HasColumnType("int");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeroId")
                        .IsUnique()
                        .HasFilter("[HeroId] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HeroItem", b =>
                {
                    b.HasOne("SimpleBackendGame.Entities.Hero", null)
                        .WithMany()
                        .HasForeignKey("HeroesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SimpleBackendGame.Entities.Item", null)
                        .WithMany()
                        .HasForeignKey("ItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Message", b =>
                {
                    b.HasOne("SimpleBackendGame.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Quest", b =>
                {
                    b.HasOne("SimpleBackendGame.Entities.Enemy", "Enemy")
                        .WithOne("Quest")
                        .HasForeignKey("SimpleBackendGame.Entities.Quest", "EnemyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enemy");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.User", b =>
                {
                    b.HasOne("SimpleBackendGame.Entities.Hero", "Hero")
                        .WithOne("User")
                        .HasForeignKey("SimpleBackendGame.Entities.User", "HeroId");

                    b.Navigation("Hero");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Enemy", b =>
                {
                    b.Navigation("Quest");
                });

            modelBuilder.Entity("SimpleBackendGame.Entities.Hero", b =>
                {
                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
