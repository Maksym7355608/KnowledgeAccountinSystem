﻿// <auto-generated />
using System;
using KnowledgeAccountinSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KnowledgeAccountinSystem.Data.Migrations
{
    [DbContext(typeof(KnowledgeAccountinSystemContext))]
    [Migration("20210325194333_Initialize")]
    partial class Initialize
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Programmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("UserId");

                    b.ToTable("Programmers");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("Name")
                        .HasColumnType("int");

                    b.Property<int?>("ProgrammerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgrammerId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Manager", b =>
                {
                    b.HasOne("KnowledgeAccountinSystem.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Programmer", b =>
                {
                    b.HasOne("KnowledgeAccountinSystem.Data.Entities.Manager", null)
                        .WithMany("Programmers")
                        .HasForeignKey("ManagerId");

                    b.HasOne("KnowledgeAccountinSystem.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Skill", b =>
                {
                    b.HasOne("KnowledgeAccountinSystem.Data.Entities.Programmer", "Programmer")
                        .WithMany("Skills")
                        .HasForeignKey("ProgrammerId");

                    b.Navigation("Programmer");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Manager", b =>
                {
                    b.Navigation("Programmers");
                });

            modelBuilder.Entity("KnowledgeAccountinSystem.Data.Entities.Programmer", b =>
                {
                    b.Navigation("Skills");
                });
#pragma warning restore 612, 618
        }
    }
}
