using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deopeia.Quote.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IPAddress = table.Column<IPAddress>(type: "inet", nullable: false),
                    EntityType = table.Column<string>(type: "text", nullable: true),
                    Keys = table.Column<string>(type: "jsonb", nullable: true),
                    OldValues = table.Column<string>(type: "jsonb", nullable: true),
                    NewValues = table.Column<string>(type: "jsonb", nullable: true),
                    PropertyNames = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubIndustry = table.Column<int>(type: "integer", nullable: false),
                    Website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exchange",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    TimeZone = table.Column<string>(type: "text", nullable: false),
                    OpeningTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ClosingTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileResource",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instrument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExchangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocaleResource",
                columns: table => new
                {
                    Culture = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocaleResource", x => new { x.Culture, x.Type, x.Code });
                });

            migrationBuilder.CreateTable(
                name: "Ohlcv",
                columns: table => new
                {
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    RecordedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Open = table.Column<decimal>(type: "numeric", nullable: false),
                    High = table.Column<decimal>(type: "numeric", nullable: false),
                    Low = table.Column<decimal>(type: "numeric", nullable: false),
                    Close = table.Column<decimal>(type: "numeric", nullable: false),
                    Volume = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ohlcv", x => new { x.Symbol, x.RecordedAt });
                });

            migrationBuilder.CreateTable(
                name: "CompanyLocale",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    Culture = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocale", x => new { x.CompanyId, x.Culture });
                    table.ForeignKey(
                        name: "FK_CompanyLocale_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeLocale",
                columns: table => new
                {
                    ExchangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Culture = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeLocale", x => new { x.ExchangeId, x.Culture });
                    table.ForeignKey(
                        name: "FK_ExchangeLocale_Exchange_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentLocale",
                columns: table => new
                {
                    InstrumentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Culture = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentLocale", x => new { x.InstrumentId, x.Culture });
                    table.ForeignKey(
                        name: "FK_InstrumentLocale_Instrument_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "Instrument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchange_Code",
                table: "Exchange",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileResource_Type",
                table: "FileResource",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_Type_ExchangeId_Symbol",
                table: "Instrument",
                columns: new[] { "Type", "ExchangeId", "Symbol" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrail");

            migrationBuilder.DropTable(
                name: "CompanyLocale");

            migrationBuilder.DropTable(
                name: "ExchangeLocale");

            migrationBuilder.DropTable(
                name: "FileResource");

            migrationBuilder.DropTable(
                name: "InstrumentLocale");

            migrationBuilder.DropTable(
                name: "LocaleResource");

            migrationBuilder.DropTable(
                name: "Ohlcv");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Exchange");

            migrationBuilder.DropTable(
                name: "Instrument");
        }
    }
}
