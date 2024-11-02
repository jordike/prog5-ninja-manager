using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinjaManager.Migrations
{
    /// <inheritdoc />
    public partial class create_ninja_has_equipment_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_equipment",
                table: "equipment");

            migrationBuilder.RenameTable(
                name: "equipment",
                newName: "Equipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "NinjaHasEquipment",
                columns: table => new
                {
                    NinjaId = table.Column<int>(type: "int", nullable: false),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NinjaHasEquipment", x => new { x.NinjaId, x.EquipmentId });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NinjaHasEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipment",
                table: "Equipment");

            migrationBuilder.RenameTable(
                name: "Equipment",
                newName: "equipment");

            migrationBuilder.AddPrimaryKey(
                name: "PK_equipment",
                table: "equipment",
                column: "Id");
        }
    }
}
