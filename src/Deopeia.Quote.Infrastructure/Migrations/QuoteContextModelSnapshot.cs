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
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Deopeia.Common.Domain.Auditing.AuditTrail", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<IPAddress>("IPAddress")
                        .IsRequired()
                        .HasColumnType("inet");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AuditTrail");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Files.FileResource", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type");

                    b.ToTable("FileResource");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Common.Localization.LocaleResource", b =>
                {
                    b.Property<string>("Culture")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Culture", "Type", "Code");

                    b.ToTable("LocaleResource");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("SubIndustry")
                        .HasColumnType("integer");

                    b.Property<string>("Website")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.CompanyLocale", b =>
                {
                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Culture")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EntityId", "Culture");

                    b.ToTable("CompanyLocale");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.Exchange", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("ClosingTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeOnly>("OpeningTime")
                        .HasColumnType("time without time zone");

                    b.Property<string>("TimeZone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Exchange");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.ExchangeLocale", b =>
                {
                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("ExchangeId");

                    b.Property<string>("Culture")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EntityId", "Culture");

                    b.ToTable("ExchangeLocale");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.Instrument", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExchangeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Type", "ExchangeId", "Symbol")
                        .IsUnique();

                    b.ToTable("Instrument");

                    b.HasDiscriminator<int>("Type");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.InstrumentLocale", b =>
                {
                    b.Property<Guid>("EntityId")
                        .HasColumnType("uuid")
                        .HasColumnName("InstrumentId");

                    b.Property<string>("Culture")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("EntityId", "Culture");

                    b.ToTable("InstrumentLocale");
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Ohlcvs.Ohlcv", b =>
                {
                    b.Property<string>("Symbol")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("RecordedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("Close")
                        .HasColumnType("numeric");

                    b.Property<decimal>("High")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Low")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Open")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Volume")
                        .HasColumnType("numeric");

                    b.HasKey("Symbol", "RecordedAt");

                    b.ToTable("Ohlcv");
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Auditing.DataAccessAuditTrail", b =>
                {
                    b.HasBaseType("Deopeia.Common.Domain.Auditing.AuditTrail");

                    b.Property<string>("EntityType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Keys")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("NewValues")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("OldValues")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("PropertyNames")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Deopeia.Common.Domain.Files.Image", b =>
                {
                    b.HasBaseType("Deopeia.Common.Domain.Files.FileResource");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.ExchangeTradedFund", b =>
                {
                    b.HasBaseType("Deopeia.Quote.Domain.Instruments.Instrument");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.Stock", b =>
                {
                    b.HasBaseType("Deopeia.Quote.Domain.Instruments.Instrument");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Companies.CompanyLocale", b =>
                {
                    b.HasOne("Deopeia.Quote.Domain.Companies.Company", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Exchanges.ExchangeLocale", b =>
                {
                    b.HasOne("Deopeia.Quote.Domain.Exchanges.Exchange", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Deopeia.Quote.Domain.Instruments.InstrumentLocale", b =>
                {
                    b.HasOne("Deopeia.Quote.Domain.Instruments.Instrument", null)
                        .WithMany("Locales")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
