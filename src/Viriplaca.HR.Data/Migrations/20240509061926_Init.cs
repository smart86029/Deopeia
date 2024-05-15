using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Viriplaca.HR.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "Common");

            migrationBuilder.EnsureSchema(name: "HR");

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IPAddress = table.Column<string>(type: "nvarchar(45)", nullable: false),
                    EntityType = table.Column<string>(
                        type: "nvarchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                    Keys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyNames = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(32)",
                        maxLength: 32,
                        nullable: false
                    ),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "FileResource",
                schema: "Common",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Extension = table.Column<string>(
                        type: "nvarchar(16)",
                        maxLength: 16,
                        nullable: false
                    ),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileResource", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Job",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(
                        type: "nvarchar(32)",
                        maxLength: 32,
                        nullable: false
                    ),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Leave",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovalStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leave", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "LeaveEntitlement",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedOn = table.Column<DateOnly>(type: "date", nullable: false),
                    EndedOn = table.Column<DateOnly>(type: "date", nullable: false),
                    GrantedTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UsedTime = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveEntitlement", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "LeaveType",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanCarryForward = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "LocaleResource",
                schema: "Common",
                columns: table => new
                {
                    Culture = table.Column<string>(
                        type: "nvarchar(16)",
                        maxLength: 16,
                        nullable: false
                    ),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(
                        type: "nvarchar(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Content = table.Column<string>(
                        type: "nvarchar(1024)",
                        maxLength: 1024,
                        nullable: false
                    )
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_LocaleResource",
                        x => new
                        {
                            x.Culture,
                            x.Type,
                            x.Code
                        }
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(
                        type: "nvarchar(32)",
                        maxLength: 32,
                        nullable: false
                    ),
                    LastName = table.Column<string>(
                        type: "nvarchar(32)",
                        maxLength: 32,
                        nullable: true
                    ),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Sex = table.Column<int>(type: "int", nullable: false),
                    MaritalStatus = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AvatarId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "LeaveTypeLocale",
                schema: "HR",
                columns: table => new
                {
                    LeaveTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Culture = table.Column<string>(
                        type: "nvarchar(16)",
                        maxLength: 16,
                        nullable: false
                    ),
                    Name = table.Column<string>(
                        type: "nvarchar(32)",
                        maxLength: 32,
                        nullable: false
                    ),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeLocale", x => new { x.LeaveTypeId, x.Culture });
                    table.ForeignKey(
                        name: "FK_LeaveTypeLocale_LeaveType_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "HR",
                        principalTable: "LeaveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "JobChange",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsHead = table.Column<bool>(type: "bit", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobChange_Person_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "HR",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Department_ParentId",
                schema: "HR",
                table: "Department",
                column: "ParentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_FileResource_Type",
                schema: "Common",
                table: "FileResource",
                column: "Type"
            );

            migrationBuilder.CreateIndex(
                name: "IX_JobChange_DepartmentId",
                schema: "HR",
                table: "JobChange",
                column: "DepartmentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_JobChange_EmployeeId",
                schema: "HR",
                table: "JobChange",
                column: "EmployeeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_JobChange_JobId",
                schema: "HR",
                table: "JobChange",
                column: "JobId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Leave_EmployeeId",
                schema: "HR",
                table: "Leave",
                column: "EmployeeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_LeaveEntitlement_EmployeeId_LeaveTypeId_StartedOn_EndedOn",
                schema: "HR",
                table: "LeaveEntitlement",
                columns: new[] { "EmployeeId", "LeaveTypeId", "StartedOn", "EndedOn" },
                unique: true
            );

            migrationBuilder.CreateIndex(
                name: "IX_Person_DepartmentId",
                schema: "HR",
                table: "Person",
                column: "DepartmentId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Person_JobId",
                schema: "HR",
                table: "Person",
                column: "JobId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserId",
                schema: "HR",
                table: "Person",
                column: "UserId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "AuditTrail", schema: "Common");

            migrationBuilder.DropTable(name: "Department", schema: "HR");

            migrationBuilder.DropTable(name: "FileResource", schema: "Common");

            migrationBuilder.DropTable(name: "Job", schema: "HR");

            migrationBuilder.DropTable(name: "JobChange", schema: "HR");

            migrationBuilder.DropTable(name: "Leave", schema: "HR");

            migrationBuilder.DropTable(name: "LeaveEntitlement", schema: "HR");

            migrationBuilder.DropTable(name: "LeaveTypeLocale", schema: "HR");

            migrationBuilder.DropTable(name: "LocaleResource", schema: "Common");

            migrationBuilder.DropTable(name: "Person", schema: "HR");

            migrationBuilder.DropTable(name: "LeaveType", schema: "HR");
        }
    }
}
