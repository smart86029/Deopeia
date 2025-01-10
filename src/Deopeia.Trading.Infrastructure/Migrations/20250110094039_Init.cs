using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deopeia.Trading.Infrastructure.Migrations
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
                name: "contract",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "text", nullable: false),
                    underlying_type = table.Column<int>(type: "integer", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: false),
                    price_precision = table.Column<decimal>(type: "numeric", nullable: false),
                    tick_size = table.Column<decimal>(type: "numeric", nullable: false),
                    leverages = table.Column<string>(type: "jsonb", nullable: false),
                    contract_size_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    contract_size_unit_code = table.Column<string>(type: "text", nullable: false),
                    volume_restriction_max = table.Column<decimal>(type: "numeric", nullable: false),
                    volume_restriction_min = table.Column<decimal>(type: "numeric", nullable: false),
                    volume_restriction_step = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract", x => x.symbol);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: true),
                    decimals = table.Column<int>(type: "integer", nullable: false),
                    exchange_rate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency", x => x.code);
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
                name: "position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false),
                    opened_by = table.Column<Guid>(type: "uuid", nullable: false),
                    opened_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    closed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    margin_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    margin_currency_code = table.Column<string>(type: "text", nullable: false),
                    open_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    open_price_currency_code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "strategy",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    open_expression = table.Column<string>(type: "text", nullable: false),
                    close_expression = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_strategy", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trader",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trader", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "contract_locale",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contract_locale", x => new { x.symbol, x.culture });
                    table.ForeignKey(
                        name: "fk_contract_locale_contract_contract_id",
                        column: x => x.symbol,
                        principalTable: "contract",
                        principalColumn: "symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session",
                columns: table => new
                {
                    symbol = table.Column<string>(type: "text", nullable: false),
                    open_day = table.Column<int>(type: "integer", nullable: false),
                    open_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    close_day = table.Column<int>(type: "integer", nullable: false),
                    close_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_session", x => new { x.symbol, x.open_day, x.open_time });
                    table.ForeignKey(
                        name: "fk_session_contract_symbol",
                        column: x => x.symbol,
                        principalTable: "contract",
                        principalColumn: "symbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "currency_locale",
                columns: table => new
                {
                    currency_code = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_locale", x => new { x.currency_code, x.culture });
                    table.ForeignKey(
                        name: "fk_currency_locale_currency_currency_id",
                        column: x => x.currency_code,
                        principalTable: "currency",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", nullable: false),
                    side = table.Column<int>(type: "integer", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false),
                    created_by = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    executed_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    price_currency_code = table.Column<string>(type: "text", nullable: false),
                    limit_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    limit_price_currency_code = table.Column<string>(type: "text", nullable: true),
                    triggeredby = table.Column<Guid>(type: "uuid", nullable: true),
                    stop_price_amount = table.Column<decimal>(type: "numeric", nullable: true),
                    stop_price_currency_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_position_position_id",
                        column: x => x.position_id,
                        principalTable: "position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "strategy_leg",
                columns: table => new
                {
                    strategy_id = table.Column<Guid>(type: "uuid", nullable: false),
                    serial_number = table.Column<int>(type: "integer", nullable: false),
                    side = table.Column<int>(type: "integer", nullable: false),
                    ticks = table.Column<decimal>(type: "numeric", nullable: false),
                    volume = table.Column<decimal>(type: "numeric", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_strategy_leg", x => new { x.strategy_id, x.serial_number });
                    table.ForeignKey(
                        name: "fk_strategy_leg_strategy_strategy_id",
                        column: x => x.strategy_id,
                        principalTable: "strategy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "strategy_locale",
                columns: table => new
                {
                    strategy_id = table.Column<Guid>(type: "uuid", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_strategy_locale", x => new { x.strategy_id, x.culture });
                    table.ForeignKey(
                        name: "fk_strategy_locale_strategy_strategy_id",
                        column: x => x.strategy_id,
                        principalTable: "strategy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    trader_id = table.Column<Guid>(type: "uuid", nullable: false),
                    currency_code = table.Column<string>(type: "text", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_account", x => new { x.trader_id, x.currency_code });
                    table.ForeignKey(
                        name: "fk_account_trader_trader_id",
                        column: x => x.trader_id,
                        principalTable: "trader",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trader_symbol",
                columns: table => new
                {
                    trader_id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trader_symbol", x => new { x.trader_id, x.symbol });
                    table.ForeignKey(
                        name: "fk_trader_symbol_trader_trader_id",
                        column: x => x.trader_id,
                        principalTable: "trader",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "unit_locale",
                columns: table => new
                {
                    unit_code = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit_locale", x => new { x.unit_code, x.culture });
                    table.ForeignKey(
                        name: "fk_unit_locale_unit_unit_id",
                        column: x => x.unit_code,
                        principalTable: "unit",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_file_resource_type",
                table: "file_resource",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "ix_order_position_id",
                table: "order",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_triggeredby",
                table: "order",
                column: "triggeredby");

            migrationBuilder.CreateIndex(
                name: "ix_strategy_leg_order_id",
                table: "strategy_leg",
                column: "order_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "audit_trail");

            migrationBuilder.DropTable(
                name: "contract_locale");

            migrationBuilder.DropTable(
                name: "currency_locale");

            migrationBuilder.DropTable(
                name: "file_resource");

            migrationBuilder.DropTable(
                name: "locale_resource");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "strategy_leg");

            migrationBuilder.DropTable(
                name: "strategy_locale");

            migrationBuilder.DropTable(
                name: "trader_symbol");

            migrationBuilder.DropTable(
                name: "unit_locale");

            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "position");

            migrationBuilder.DropTable(
                name: "contract");

            migrationBuilder.DropTable(
                name: "strategy");

            migrationBuilder.DropTable(
                name: "trader");

            migrationBuilder.DropTable(
                name: "unit");
        }
    }
}
