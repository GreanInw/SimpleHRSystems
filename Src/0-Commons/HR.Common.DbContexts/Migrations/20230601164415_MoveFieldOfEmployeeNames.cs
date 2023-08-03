using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.Common.DbContexts.Migrations
{
    /// <inheritdoc />
    public partial class MoveFieldOfEmployeeNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserEmployee");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserEmployee");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "UserEmployee");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "UserEmployee");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "EmployeeNamesInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "EmployeeNamesInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "EmployeeNamesInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "EmployeeNamesInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "EmployeeNamesInfo");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "EmployeeNamesInfo");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "EmployeeNamesInfo");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "EmployeeNamesInfo");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserEmployee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserEmployee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "UserEmployee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "UserEmployee",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
