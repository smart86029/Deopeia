using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deopeia.Trading.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    side = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                    side = table.Column<int>(type: "integer", nullable: false),
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
                name: "position_order",
                columns: table => new
                {
                    position_id = table.Column<Guid>(type: "uuid", nullable: false),
                    order_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_position_order", x => new { x.position_id, x.order_id });
                    table.ForeignKey(
                        name: "fk_position_order_position_position_id",
                        column: x => x.position_id,
                        principalTable: "position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_order_triggeredby",
                table: "order",
                column: "triggeredby");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "position_order");

            migrationBuilder.DropTable(
                name: "position");
        }
    }
}
