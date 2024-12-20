﻿// <auto-generated />
using System;
using System.Collections.Generic;
using System.Net;
using Deopeia.Trading.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Deopeia.Trading.Infrastructure.Migrations
{
    [DbContext(typeof(TradingContext))]
    [Migration("20241211145352_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
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

            modelBuilder.Entity("Deopeia.Common.Domain.Finance.Currency", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<int>("Decimals")
                        .HasColumnType("integer")
                        .HasColumnName("decimals");

                    b.Property<string>("Symbol")
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.HasKey("Id")
                        .HasName("pk_currency");

                    b.ToTable("currency", (string)null);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Finance.CurrencyLocale", b =>
                {
                    b.Property<string>("EntityId")
                        .HasColumnType("text")
                        .HasColumnName("currency_code");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EntityId", "Culture")
                        .HasName("pk_currency_locale");

                    b.ToTable("currency_locale", (string)null);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Measurement.Unit", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("code");

                    b.Property<string>("Symbol")
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.HasKey("Id")
                        .HasName("pk_unit");

                    b.ToTable("unit", (string)null);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Measurement.UnitLocale", b =>
                {
                    b.Property<string>("EntityId")
                        .HasColumnType("text")
                        .HasColumnName("unit_code");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EntityId", "Culture")
                        .HasName("pk_unit_locale");

                    b.ToTable("unit_locale", (string)null);
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

            modelBuilder.Entity("Deopeia.Trading.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("account_number");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("is_enabled");

                    b.ComplexProperty<Dictionary<string, object>>("Balance", "Deopeia.Trading.Domain.Accounts.Account.Balance#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("balance_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("balance_currency_code");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Margin", "Deopeia.Trading.Domain.Accounts.Account.Margin#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("margin_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("margin_currency_code");
                        });

                    b.HasKey("Id")
                        .HasName("pk_account");

                    b.ToTable("account", (string)null);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Contracts.Contract", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency_code");

                    b.Property<string>("Leverages")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("leverages");

                    b.Property<decimal>("TickSize")
                        .HasColumnType("numeric")
                        .HasColumnName("tick_size");

                    b.Property<int>("UnderlyingType")
                        .HasColumnType("integer")
                        .HasColumnName("underlying_type");

                    b.ComplexProperty<Dictionary<string, object>>("ContractSize", "Deopeia.Trading.Domain.Contracts.Contract.ContractSize#ContractSize", b1 =>
                        {
                            b1.Property<decimal>("Quantity")
                                .HasColumnType("numeric")
                                .HasColumnName("contract_size_quantity");

                            b1.Property<string>("UnitCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("contract_size_unit_code");
                        });

                    b.HasKey("Id")
                        .HasName("pk_contract");

                    b.ToTable("contract", (string)null);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Contracts.ContractLocale", b =>
                {
                    b.Property<string>("EntityId")
                        .HasColumnType("text")
                        .HasColumnName("symbol");

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
                        .HasName("pk_contract_locale");

                    b.ToTable("contract_locale", (string)null);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Orders.Order", b =>
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

                    b.Property<DateTimeOffset?>("ExecutedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("executed_at");

                    b.Property<Guid>("PositionId")
                        .HasColumnType("uuid")
                        .HasColumnName("position_id");

                    b.Property<int>("Side")
                        .HasColumnType("integer")
                        .HasColumnName("side");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("volume");

                    b.ComplexProperty<Dictionary<string, object>>("Price", "Deopeia.Trading.Domain.Orders.Order.Price#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("price_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("price_currency_code");
                        });

                    b.HasKey("Id")
                        .HasName("pk_order");

                    b.HasIndex("PositionId")
                        .HasDatabaseName("ix_order_position_id");

                    b.ToTable("order", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Positions.Position", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset?>("ClosedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("closed_at");

                    b.Property<string>("InstrumentId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("instrument_id");

                    b.Property<DateTimeOffset>("OpenedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("opened_at");

                    b.Property<Guid>("OpenedBy")
                        .HasColumnType("uuid")
                        .HasColumnName("opened_by");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("volume");

                    b.ComplexProperty<Dictionary<string, object>>("Margin", "Deopeia.Trading.Domain.Positions.Position.Margin#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("margin_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("margin_currency_code");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("OpenPrice", "Deopeia.Trading.Domain.Positions.Position.OpenPrice#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("open_price_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("open_price_currency_code");
                        });

                    b.HasKey("Id")
                        .HasName("pk_position");

                    b.ToTable("position", (string)null);
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

            modelBuilder.Entity("Deopeia.Trading.Domain.Orders.LimitOrders.LimitOrder", b =>
                {
                    b.HasBaseType("Deopeia.Trading.Domain.Orders.Order");

                    b.ComplexProperty<Dictionary<string, object>>("LimitPrice", "Deopeia.Trading.Domain.Orders.LimitOrders.LimitOrder.LimitPrice#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("limit_price_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("limit_price_currency_code");
                        });

                    b.ToTable("order", (string)null);

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Orders.MarketOrders.MarketOrder", b =>
                {
                    b.HasBaseType("Deopeia.Trading.Domain.Orders.Order");

                    b.ToTable("order", (string)null);

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Orders.StopOrders.StopOrder", b =>
                {
                    b.HasBaseType("Deopeia.Trading.Domain.Orders.Order");

                    b.Property<Guid>("Triggeredby")
                        .HasColumnType("uuid")
                        .HasColumnName("triggeredby");

                    b.ComplexProperty<Dictionary<string, object>>("StopPrice", "Deopeia.Trading.Domain.Orders.StopOrders.StopOrder.StopPrice#Money", b1 =>
                        {
                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric")
                                .HasColumnName("stop_price_amount");

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("stop_price_currency_code");
                        });

                    b.HasIndex("Triggeredby")
                        .HasDatabaseName("ix_order_triggeredby");

                    b.ToTable("order", (string)null);

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Finance.CurrencyLocale", b =>
                {
                    b.HasOne("Deopeia.Common.Domain.Finance.Currency", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_currency_locale_currency_currency_id");
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Measurement.UnitLocale", b =>
                {
                    b.HasOne("Deopeia.Common.Domain.Measurement.Unit", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_unit_locale_unit_unit_id");
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Contracts.ContractLocale", b =>
                {
                    b.HasOne("Deopeia.Trading.Domain.Contracts.Contract", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_contract_locale_contract_contract_id");
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Orders.Order", b =>
                {
                    b.HasOne("Deopeia.Trading.Domain.Positions.Position", null)
                        .WithMany("Orders")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_order_position_position_id");
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

            modelBuilder.Entity("Deopeia.Common.Domain.Finance.Currency", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Measurement.Unit", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Contracts.Contract", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Trading.Domain.Positions.Position", b =>
                {
                    b.Navigation("Orders");
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
