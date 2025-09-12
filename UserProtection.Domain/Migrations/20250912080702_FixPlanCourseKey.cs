using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserProtection.Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixPlanCourseKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanCourses",
                schema: "billing",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanCourses", x => new { x.PlanId, x.CourseId });
                    table.ForeignKey(
                        name: "FK_PlanCourses_Courses_CourseId",
                        column: x => x.CourseId,
                        principalSchema: "learning",
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanCourses_Plans_PlanId",
                        column: x => x.PlanId,
                        principalSchema: "billing",
                        principalTable: "Plans",
                        principalColumn: "PlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanCourses_CourseId",
                schema: "billing",
                table: "PlanCourses",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanCourses",
                schema: "billing");
        }
    }
}
