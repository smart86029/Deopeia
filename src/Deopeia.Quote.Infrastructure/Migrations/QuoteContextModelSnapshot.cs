﻿// <auto-generated />
using System;
using System.Net;
using Deopeia.Quote.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Deopeia.Quote.Infrastructure.Migrations
{
    [DbContext(typeof(QuoteContext))]
    partial class QuoteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Deopeia.Quote.Domain.Candles.Candle", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.Property<int>("TimeFrame")
                        .HasColumnType("integer")
                        .HasColumnName("time_frame");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<decimal>("Close")
                        .HasColumnType("numeric")
                        .HasColumnName("close");

                    b.Property<decimal>("High")
                        .HasColumnType("numeric")
                        .HasColumnName("high");

                    b.Property<decimal>("Low")
                        .HasColumnType("numeric")
                        .HasColumnName("low");

                    b.Property<decimal>("Open")
                        .HasColumnType("numeric")
                        .HasColumnName("open");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("volume");

                    b.HasKey("Symbol", "TimeFrame", "Timestamp")
                        .HasName("pk_candle");

                    b.ToTable("candle", (string)null);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Candles.Tick", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("timestamp");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric")
                        .HasColumnName("price");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric")
                        .HasColumnName("volume");

                    b.HasKey("Symbol", "Timestamp")
                        .HasName("pk_tick");

                    b.ToTable("tick", (string)null);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("SubIndustry")
                        .HasColumnType("integer")
                        .HasColumnName("sub_industry");

                    b.Property<string>("Website")
                        .HasColumnType("text")
                        .HasColumnName("website");

                    b.HasKey("Id")
                        .HasName("pk_company");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.CompanyLocale", b =>
                {
                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EntityId", "Culture")
                        .HasName("pk_company_locale");

                    b.ToTable("company_locale", (string)null);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.Exchange", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("time_zone");

                    b.HasKey("Id")
                        .HasName("pk_exchange");

                    b.ToTable("exchange", (string)null);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.ExchangeLocale", b =>
                {
                    b.Property<string>("EntityId")
                        .HasColumnType("text")
                        .HasColumnName("exchange_id");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Abbreviation")
                        .HasColumnType("text")
                        .HasColumnName("abbreviation");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EntityId", "Culture")
                        .HasName("pk_exchange_locale");

                    b.ToTable("exchange_locale", (string)null);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.Instrument", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<string>("CurrencyCode")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("currency_code");

                    b.Property<string>("ExchangeId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("exchange_id");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("symbol");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("pk_instrument");

                    b.HasIndex("Type", "ExchangeId", "Symbol")
                        .IsUnique()
                        .HasDatabaseName("ix_instrument_type_exchange_id_symbol");

                    b.ToTable("instrument", (string)null);

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.InstrumentLocale", b =>
                {
                    b.Property<string>("EntityId")
                        .HasColumnType("text")
                        .HasColumnName("instrument_id");

                    b.Property<string>("Culture")
                        .HasColumnType("text")
                        .HasColumnName("culture");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.HasKey("EntityId", "Culture")
                        .HasName("pk_instrument_locale");

                    b.ToTable("instrument_locale", (string)null);
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

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.ExchangeTradedFund", b =>
                {
                    b.HasBaseType("Deopeia.Quote.Domain.Instruments.Instrument");

                    b.ToTable("instrument", (string)null);

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.Stock", b =>
                {
                    b.HasBaseType("Deopeia.Quote.Domain.Instruments.Instrument");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid")
                        .HasColumnName("company_id");

                    b.ToTable("instrument", (string)null);

                    b.HasDiscriminator().HasValue(1);
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

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.CompanyLocale", b =>
                {
                    b.HasOne("Deopeia.Quote.Domain.Companies.Company", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_company_locale_company_company_id");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.ExchangeLocale", b =>
                {
                    b.HasOne("Deopeia.Quote.Domain.Exchanges.Exchange", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_exchange_locale_exchange_exchange_id");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.InstrumentLocale", b =>
                {
                    b.HasOne("Deopeia.Quote.Domain.Instruments.Instrument", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_instrument_locale_instrument_instrument_id");
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Finance.Currency", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Measurement.Unit", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.Company", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.Exchange", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.Instrument", b =>
                {
                    b.Navigation("Locales");
                });
#pragma warning restore 612, 618
        }
    }
}
