using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProtection.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDomainEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "security",
                table: "SuspiciousLinks",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDomainEntries",
                schema: "security",
                columns: table => new
                {
                    EntryId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Domain = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Safe = table.Column<bool>(type: "bit", nullable: true),
                    EntryType = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(sysutcdatetime())"),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDomainEntries", x => x.EntryId);
                    table.ForeignKey(
                        name: "FK_UserDomainEntries_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "core",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UX_UDE_User_Domain_Type",
                schema: "security",
                table: "UserDomainEntries",
                columns: new[] { "UserId", "Domain", "EntryType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDomainEntries",
                schema: "security");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "security",
                table: "SuspiciousLinks");
        }
    }
}
