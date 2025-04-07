using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deopeia.Identity.Infrastructure.Migrations
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
                    created_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    ip_address = table.Column<IPAddress>(type: "inet", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: true),
                    keys = table.Column<string>(type: "jsonb", nullable: true),
                    old_values = table.Column<string>(type: "jsonb", nullable: true),
                    new_values = table.Column<string>(type: "jsonb", nullable: true),
                    property_names = table.Column<string>(type: "jsonb", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_trail", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "authenticator",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    secret_key_ciphertext = table.Column<string>(type: "text", nullable: true),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    error_count = table.Column<int>(type: "integer", nullable: false),
                    locked_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authenticator", x => x.user_id);
                }
            );

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    secret = table.Column<string>(type: "text", nullable: true),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    grant_types = table.Column<int>(type: "integer", nullable: false),
                    scopes = table.Column<string>(type: "jsonb", nullable: false),
                    redirect_uris = table.Column<string>(type: "jsonb", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: true),
                    decimals = table.Column<int>(type: "integer", nullable: false),
                    exchange_rate = table.Column<decimal>(type: "numeric", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency", x => x.code);
                }
            );

            migrationBuilder.CreateTable(
                name: "event_log",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_type_namespace = table.Column<string>(type: "text", nullable: false),
                    event_type_name = table.Column<string>(type: "text", nullable: false),
                    event_content = table.Column<string>(type: "text", nullable: false),
                    publish_state = table.Column<int>(type: "integer", nullable: false),
                    publish_count = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_log", x => x.event_id);
                }
            );

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
                name: "grant",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    key = table.Column<string>(type: "text", nullable: false),
                    subject_id = table.Column<Guid>(type: "uuid", nullable: true),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    scopes = table.Column<string>(
                        type: "character varying(256)",
                        maxLength: 256,
                        nullable: false
                    ),
                    created_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    expires_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    consumed_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                    redirect_uri = table.Column<string>(type: "text", nullable: true),
                    nonce = table.Column<string>(type: "text", nullable: true),
                    code_challenge = table.Column<string>(type: "text", nullable: true),
                    code_challenge_method = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_grant", x => x.id);
                }
            );

            migrationBuilder.CreateTable(
                name: "locale_resource",
                columns: table => new
                {
                    culture = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "pk_locale_resource",
                        x => new
                        {
                            x.culture,
                            x.type,
                            x.code,
                        }
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "permission",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission", x => x.code);
                }
            );

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.code);
                }
            );

            migrationBuilder.CreateTable(
                name: "unit",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit", x => x.code);
                }
            );

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_name = table.Column<string>(type: "text", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    avatar_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    authenticator_id = table.Column<Guid>(type: "uuid", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_authenticator_authenticator_id",
                        column: x => x.authenticator_id,
                        principalTable: "authenticator",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "currency_locale",
                columns: table => new
                {
                    currency_code = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_locale", x => new { x.currency_code, x.culture });
                    table.ForeignKey(
                        name: "fk_currency_locale_currency_currency_id",
                        column: x => x.currency_code,
                        principalTable: "currency",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "permission_locale",
                columns: table => new
                {
                    permission_code = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "pk_permission_locale",
                        x => new { x.permission_code, x.culture }
                    );
                    table.ForeignKey(
                        name: "fk_permission_locale_permission_permission_id",
                        column: x => x.permission_code,
                        principalTable: "permission",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "role_locale",
                columns: table => new
                {
                    role_code = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_locale", x => new { x.role_code, x.culture });
                    table.ForeignKey(
                        name: "fk_role_locale_role_role_id",
                        column: x => x.role_code,
                        principalTable: "role",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "role_permission",
                columns: table => new
                {
                    role_code = table.Column<string>(type: "text", nullable: false),
                    permission_code = table.Column<string>(type: "text", nullable: false),
                    permission_id = table.Column<string>(type: "text", nullable: true),
                    role_id = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "pk_role_permission",
                        x => new { x.role_code, x.permission_code }
                    );
                    table.ForeignKey(
                        name: "fk_role_permission_permission_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permission",
                        principalColumn: "code"
                    );
                    table.ForeignKey(
                        name: "fk_role_permission_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "code"
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "unit_locale",
                columns: table => new
                {
                    unit_code = table.Column<string>(type: "text", nullable: false),
                    culture = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unit_locale", x => new { x.unit_code, x.culture });
                    table.ForeignKey(
                        name: "fk_unit_locale_unit_unit_id",
                        column: x => x.unit_code,
                        principalTable: "unit",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_refresh_token",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    issued_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    expired_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: false
                    ),
                    revoked_at = table.Column<DateTimeOffset>(
                        type: "timestamp with time zone",
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_refresh_token", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_refresh_token_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_code = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<string>(type: "text", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => new { x.user_id, x.role_code });
                    table.ForeignKey(
                        name: "fk_user_role_role_role_id",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "code"
                    );
                    table.ForeignKey(
                        name: "fk_user_role_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
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
                name: "ix_grant_client_id",
                table: "grant",
                column: "client_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_grant_key",
                table: "grant",
                column: "key",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_permission_id",
                table: "role_permission",
                column: "permission_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_role_permission_role_id",
                table: "role_permission",
                column: "role_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_authenticator_id",
                table: "user",
                column: "authenticator_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_user_name",
                table: "user",
                column: "user_name",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_refresh_token_refresh_token",
                table: "user_refresh_token",
                column: "refresh_token",
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_refresh_token_user_id",
                table: "user_refresh_token",
                column: "user_id"
            );

            migrationBuilder.CreateIndex(
                name: "ix_user_role_role_id",
                table: "user_role",
                column: "role_id"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "audit_trail");

            migrationBuilder.DropTable(name: "client");

            migrationBuilder.DropTable(name: "currency_locale");

            migrationBuilder.DropTable(name: "event_log");

            migrationBuilder.DropTable(name: "file_resource");

            migrationBuilder.DropTable(name: "grant");

            migrationBuilder.DropTable(name: "locale_resource");

            migrationBuilder.DropTable(name: "permission_locale");

            migrationBuilder.DropTable(name: "role_locale");

            migrationBuilder.DropTable(name: "role_permission");

            migrationBuilder.DropTable(name: "unit_locale");

            migrationBuilder.DropTable(name: "user_refresh_token");

            migrationBuilder.DropTable(name: "user_role");

            migrationBuilder.DropTable(name: "currency");

            migrationBuilder.DropTable(name: "permission");

            migrationBuilder.DropTable(name: "unit");

            migrationBuilder.DropTable(name: "role");

            migrationBuilder.DropTable(name: "user");

            migrationBuilder.DropTable(name: "authenticator");
        }
    }
}
