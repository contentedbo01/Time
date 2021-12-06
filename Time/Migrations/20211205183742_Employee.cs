using Microsoft.EntityFrameworkCore.Migrations;

namespace Time.Migrations
{
    public partial class Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeWorked_EmployeeId",
                table: "TimeWorked");

            migrationBuilder.CreateIndex(
                name: "IX_TimeWorked_EmployeeId",
                table: "TimeWorked",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TimeWorked_EmployeeId",
                table: "TimeWorked");

            migrationBuilder.CreateIndex(
                name: "IX_TimeWorked_EmployeeId",
                table: "TimeWorked",
                column: "EmployeeId",
                unique: true,
                filter: "[EmployeeId] IS NOT NULL");
        }
    }
}
