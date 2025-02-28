﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250228165811_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("Domain.Aggregates.UserForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT")
                        .HasColumnName("created");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("TEXT")
                        .HasColumnName("modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("p_k_form");

                    b.ToTable("form");
                });

            modelBuilder.Entity("Domain.Entities.BaseComponentChoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT")
                        .HasColumnName("description");

                    b.Property<Guid>("FormId")
                        .HasColumnType("TEXT")
                        .HasColumnName("form_id");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT")
                        .HasColumnName("label");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER")
                        .HasColumnName("required");

                    b.Property<string>("type")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("TEXT")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("p_k_base_component_choice");

                    b.HasIndex("FormId")
                        .HasDatabaseName("i_x_base_component_choice_form_id");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator<string>("type").HasValue("BaseComponentChoice");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Entities.ButtonComponentChoice", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseComponentChoice");

                    b.Property<string>("ButtonText")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("button_text");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator().HasValue("button");
                });

            modelBuilder.Entity("Domain.Entities.CheckboxInput", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseComponentChoice");

                    b.Property<bool>("Checked")
                        .HasColumnType("INTEGER")
                        .HasColumnName("checked");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator().HasValue("switch");
                });

            modelBuilder.Entity("Domain.Entities.DateInput", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseComponentChoice");

                    b.Property<string>("InputType")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("TEXT")
                        .HasColumnName("input_type");

                    b.Property<DateTime>("SelectedDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("selected_date");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator().HasValue("date");
                });

            modelBuilder.Entity("Domain.Entities.NumberInput", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseComponentChoice");

                    b.Property<float>("Max")
                        .HasColumnType("REAL")
                        .HasColumnName("max");

                    b.Property<float>("Min")
                        .HasColumnType("REAL")
                        .HasColumnName("min");

                    b.Property<float>("SelectedNumericValue")
                        .HasColumnType("REAL")
                        .HasColumnName("selected_numeric_value");

                    b.Property<float>("Step")
                        .HasColumnType("REAL")
                        .HasColumnName("step");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator().HasValue("number");
                });

            modelBuilder.Entity("Domain.Entities.SelectInput", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseComponentChoice");

                    b.PrimitiveCollection<string>("Choices")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("choices");

                    b.Property<string>("SelectedOption")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("selected_option");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator().HasValue("select");
                });

            modelBuilder.Entity("Domain.Entities.TextInput", b =>
                {
                    b.HasBaseType("Domain.Entities.BaseComponentChoice");

                    b.Property<string>("InputType")
                        .IsRequired()
                        .ValueGeneratedOnUpdateSometimes()
                        .HasColumnType("TEXT")
                        .HasColumnName("input_type");

                    b.Property<string>("Placeholder")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("placeholder");

                    b.Property<string>("TextValue")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("text_value");

                    b.ToTable("base_component_choice");

                    b.HasDiscriminator().HasValue("text");
                });

            modelBuilder.Entity("Domain.Entities.BaseComponentChoice", b =>
                {
                    b.HasOne("Domain.Aggregates.UserForm", null)
                        .WithMany("Fields")
                        .HasForeignKey("FormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_base_component_choice_form_form_id");
                });

            modelBuilder.Entity("Domain.Aggregates.UserForm", b =>
                {
                    b.Navigation("Fields");
                });
#pragma warning restore 612, 618
        }
    }
}
