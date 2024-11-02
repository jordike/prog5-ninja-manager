using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinjaManager.Migrations
{
    /// <inheritdoc />
    public partial class Equipment_Propperties_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Agility",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Intelligence",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Strength",
                table: "Equipment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Agility",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Intelligence",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Strength",
                table: "Equipment");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Equipment");
        }
    }
}
