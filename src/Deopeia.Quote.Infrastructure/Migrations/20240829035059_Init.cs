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
                name: "audit_trail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ip_address = table.Column<IPAddress>(type: "inet", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: true),
                    keys = table.Column<string>(type: "jsonb", nullable: true),
                    old_values = table.Column<string>(type: "jsonb", nullable: true),
                    new_values = table.Column<string>(type: "jsonb", nullable: true),
                    property_names = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_trail", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sub_industry = table.Column<int>(type: "integer", nullable: false),
                    website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exchange",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    time_zone = table.Column<string>(type: "text", nullable: false),
                    opening_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    closing_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exchange", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "file_resource",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    extension = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file_resource", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "instrument",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    exchange_id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    company_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrument", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "locale_resource",
                columns: table => new
                {
                    culture = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locale_resource", x => new { x.culture, x.type, x.code });
                });

            migrationBuilder.CreateTable(
                name: "ohlcv",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "text", nullable: false),
                    recorded_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    open = table.Column<decimal>(type: "numeric", nullable: false),
                    high = table.Column<decimal>(type: "numeric", nullable: false),
                    low = table.Column<decimal>(type: "numeric", nullable: false),
                    close = table.Column<decimal>(type: "numeric", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ohlcv", x => new { x.symbol, x.recorded_at });
                });

            migrationBuilder.CreateTable(
                name: "company_locale",
                columns: table => new
                {
                    company_id = table.Column<Guid>(type: "uuid", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_company_locale", x => new { x.company_id, x.culture });
                    table.ForeignKey(
                        name: "fk_company_locale_company_company_id",
                        column: x => x.company_id,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exchange_locale",
                columns: table => new
                {
                    exchange_id = table.Column<Guid>(type: "uuid", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exchange_locale", x => new { x.exchange_id, x.culture });
                    table.ForeignKey(
                        name: "fk_exchange_locale_exchange_exchange_id",
                        column: x => x.exchange_id,
                        principalTable: "exchange",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instrument_locale",
                columns: table => new
                {
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instrument_locale", x => new { x.instrument_id, x.culture });
                    table.ForeignKey(
                        name: "fk_instrument_locale_instrument_instrument_id",
                        column: x => x.instrument_id,
                        principalTable: "instrument",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_exchange_code",
                table: "exchange",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_file_resource_type",
                table: "file_resource",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_instrument_type_exchange_id_symbol",
                table: "instrument",
                columns: new[] { "type", "exchange_id", "symbol" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_trail");

            migrationBuilder.DropTable(
                name: "company_locale");

            migrationBuilder.DropTable(
                name: "exchange_locale");

            migrationBuilder.DropTable(
                name: "file_resource");

            migrationBuilder.DropTable(
                name: "instrument_locale");

            migrationBuilder.DropTable(
                name: "locale_resource");

            migrationBuilder.DropTable(
                name: "ohlcv");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "exchange");

            migrationBuilder.DropTable(
                name: "instrument");
        }
    }
}
