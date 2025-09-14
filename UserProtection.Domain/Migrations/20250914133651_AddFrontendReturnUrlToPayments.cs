using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProtection.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddFrontendReturnUrlToPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FrontendReturnUrl",
                schema: "billing",
                table: "Payments",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrontendReturnUrl",
                schema: "billing",
                table: "Payments");
        }
    }
}
