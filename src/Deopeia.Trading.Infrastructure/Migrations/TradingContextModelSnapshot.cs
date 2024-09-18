﻿// <auto-generated />
using System;
using System.Net;
using Deopeia.Trading.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Deopeia.Trading.Infrastructure.Migrations
{
    [DbContext(typeof(TradingContext))]
    partial class TradingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Deopeia.Common.Domain.Auditing.AuditTrail", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by");

                    b.Property<IPAddress>("IPAddress")
                        .IsRequired()
                        .HasColumnType("inet")
                        .HasColumnName("ip_address");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_audit_trail");

                    b.ToTable("audit_trail", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Files.FileResource", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("extension");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Size")
                        .HasColumnType("integer")
                        .HasColumnName("size");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_file_resource");

                    b.HasIndex("Type")
                        .HasDatabaseName("ix_file_resource_type");

                    b.ToTable("file_resource", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Common.Localization.LocaleResource", b =>
                {
                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("content");

                    b.HasKey("Culture", "Type", "Code")
                        .HasName("pk_locale_resource");

                    b.ToTable("locale_resource", (string)null);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Strategies.Strategy", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("CloseExpression")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("close_expression");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("is_enabled");

                    b.Property<string>("OpenExpression")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("open_expression");

                    b.HasKey("Id")
                        .HasName("pk_strategy");

                    b.ToTable("strategy", (string)null);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Strategies.StrategyLeg", b =>
                {
                    b.Property<Guid>("StrategyId")
                        .HasColumnType("uuid")
                        .HasColumnName("strategy_id");

                    b.Property<int>("SerialNumber")
                        .HasColumnType("integer")
                        .HasColumnName("serial_number");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uuid")
                        .HasColumnName("order_id");

                    b.Property<int>("Side")
                        .HasColumnType("integer")
                        .HasColumnName("side");

                    b.Property<decimal>("Ticks")
                        .HasColumnType("numeric")
                        .HasColumnName("ticks");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("volume");

                    b.HasKey("StrategyId", "SerialNumber")
                        .HasName("pk_strategy_leg");

                    b.HasIndex("OrderId")
                        .IsUnique()
                        .HasDatabaseName("ix_strategy_leg_order_id");

                    b.ToTable("strategy_leg", (string)null);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Strategies.StrategyLocale", b =>
                {
                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("strategy_id");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EntityId", "Culture")
                        .HasName("pk_strategy_locale");

                    b.ToTable("strategy_locale", (string)null);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Auditing.DataAccessAuditTrail", b =>
                {
                    b.HasBaseType("Deopeia.Common.Domain.Auditing.AuditTrail");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("entity_type");

                    b.Property<string>("Keys")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("keys");

                    b.Property<string>("NewValues")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("new_values");

                    b.Property<string>("OldValues")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("old_values");

                    b.Property<string>("PropertyNames")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("property_names");

                    b.ToTable("audit_trail", (string)null);

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Files.Image", b =>
                {
                    b.HasBaseType("Deopeia.Common.Domain.Files.FileResource");

                    b.ToTable("file_resource", (string)null);

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Strategies.StrategyLeg", b =>
                {
                    b.HasOne("Deopeia.Trading.Domain.Strategies.Strategy", null)
                        .WithMany("Legs")
                        .HasForeignKey("StrategyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_strategy_leg_strategy_strategy_id");
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Strategies.StrategyLocale", b =>
                {
                    b.HasOne("Deopeia.Trading.Domain.Strategies.Strategy", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_strategy_locale_strategy_strategy_id");
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Strategies.Strategy", b =>
                {
                    b.Navigation("Legs");

                    b.Navigation("Locales");
                });
#pragma warning restore 612, 618
        }
    }
}
