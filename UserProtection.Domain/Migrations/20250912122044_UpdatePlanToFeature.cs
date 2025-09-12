using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProtection.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanToFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                schema: "billing",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "PlanFeatures",
                schema: "billing",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanFeatures", x => new { x.PlanId, x.FeatureId });
                    table.ForeignKey(
                        name: "FK_PlanFeatures_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalSchema: "billing",
                        principalTable: "Features",
                        principalColumn: "FeatureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanFeatures_Plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "billing",
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanFeatures_FeatureId",
                schema: "billing",
                table: "PlanFeatures",
                column: "FeatureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanFeatures",
                schema: "billing");

            migrationBuilder.DropTable(
                name: "Features",
                schema: "billing");
        }
    }
}
