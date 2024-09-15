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
                name: "asset",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asset", x => x.id);
                });

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
                name: "asset_locale",
                columns: table => new
                {
                    asset_id = table.Column<Guid>(type: "uuid", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asset_locale", x => new { x.asset_id, x.culture });
                    table.ForeignKey(
                        name: "fk_asset_locale_asset_asset_id",
                        column: x => x.asset_id,
                        principalTable: "asset",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_asset_code",
                table: "asset",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_file_resource_type",
                table: "file_resource",
                column: "type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "asset_locale");

            migrationBuilder.DropTable(
                name: "audit_trail");

            migrationBuilder.DropTable(
                name: "file_resource");

            migrationBuilder.DropTable(
                name: "locale_resource");

            migrationBuilder.DropTable(
                name: "asset");
        }
    }
}
