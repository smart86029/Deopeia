using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deopeia.Product.Infrastructure.Migrations;

/// <inheritdoc />
public partial class Init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "file_resource",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<int>(type: "integer", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
                extension = table.Column<string>(type: "text", nullable: false),
                size = table.Column<int>(type: "integer", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_file_resource", x => x.id);
            }
        );

        migrationBuilder.CreateTable(
            name: "instrument",
            columns: table => new
            {
                id = table.Column<Guid>(type: "uuid", nullable: false),
                type = table.Column<int>(type: "integer", nullable: false),
                symbol = table.Column<string>(type: "text", nullable: false),
                base_asset = table.Column<string>(type: "text", nullable: false),
                quote_asset = table.Column<string>(type: "text", nullable: false),
                price_precision = table.Column<int>(type: "integer", nullable: false),
                quantity_precision = table.Column<int>(type: "integer", nullable: false),
                min_quantity = table.Column<decimal>(type: "numeric", nullable: false),
                min_notional = table.Column<decimal>(type: "numeric", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_instrument", x => x.id);
            }
        );

        migrationBuilder.CreateTable(
            name: "instrument_localization",
            columns: table => new
            {
                instrument_id = table.Column<Guid>(type: "uuid", nullable: false),
                culture = table.Column<string>(type: "text", nullable: false),
                name = table.Column<string>(type: "text", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey(
                    "pk_instrument_localization",
                    x => new { x.instrument_id, x.culture }
                );
                table.ForeignKey(
                    name: "fk_instrument_localization_instrument_instrument_id",
                    column: x => x.instrument_id,
                    principalTable: "instrument",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade
                );
            }
        );

        migrationBuilder.CreateIndex(
            name: "ix_file_resource_type",
            table: "file_resource",
            column: "type"
        );

        migrationBuilder.CreateIndex(
            name: "ix_instrument_symbol",
            table: "instrument",
            column: "symbol",
            unique: true
        );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "file_resource");

        migrationBuilder.DropTable(name: "instrument_localization");

        migrationBuilder.DropTable(name: "instrument");
    }
}
