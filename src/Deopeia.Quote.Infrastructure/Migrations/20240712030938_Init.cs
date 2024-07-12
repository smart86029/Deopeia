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
            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.EnsureSchema(
                name: "Quote");

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "Common",
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
                schema: "Quote",
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
                name: "FileResource",
                schema: "Common",
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
                schema: "Quote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Exchange = table.Column<Guid>(type: "uuid", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instrument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocaleResource",
                schema: "Common",
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
                schema: "Quote",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("PK_Ohlcv", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLocale",
                schema: "Quote",
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
                        principalSchema: "Quote",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstrumentLocale",
                schema: "Quote",
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
                        principalSchema: "Quote",
                        principalTable: "Instrument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileResource_Type",
                schema: "Common",
                table: "FileResource",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Instrument_Type_Exchange_Symbol",
                schema: "Quote",
                table: "Instrument",
                columns: new[] { "Type", "Exchange", "Symbol" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ohlcv_Symbol_RecordedAt",
                schema: "Quote",
                table: "Ohlcv",
                columns: new[] { "Symbol", "RecordedAt" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrail",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "CompanyLocale",
                schema: "Quote");

            migrationBuilder.DropTable(
                name: "FileResource",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "InstrumentLocale",
                schema: "Quote");

            migrationBuilder.DropTable(
                name: "LocaleResource",
                schema: "Common");

            migrationBuilder.DropTable(
                name: "Ohlcv",
                schema: "Quote");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Quote");

            migrationBuilder.DropTable(
                name: "Instrument",
                schema: "Quote");
        }
    }
}
