using Microsoft.EntityFrameworkCore.Migrations;

namespace PayCompute.Persistence.Migrations
{
    public partial class UpdatedEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentLoad",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentLoan",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "StudentLoan",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "StudentLoad",
                table: "Employees",
                nullable: false,
                defaultValue: 0);
        }
    }
}
