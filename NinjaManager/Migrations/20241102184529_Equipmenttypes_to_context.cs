using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinjaManager.Migrations
{
    /// <inheritdoc />
    public partial class Equipmenttypes_to_context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentType",
                table: "EquipmentType");

            migrationBuilder.RenameTable(
                name: "EquipmentType",
                newName: "EquipmentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId",
                principalTable: "EquipmentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipment_EquipmentTypes_EquipmentTypeId",
                table: "Equipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentTypes",
                table: "EquipmentTypes");

            migrationBuilder.RenameTable(
                name: "EquipmentTypes",
                newName: "EquipmentType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentType",
                table: "EquipmentType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipment_EquipmentType_EquipmentTypeId",
                table: "Equipment",
                column: "EquipmentTypeId",
                principalTable: "EquipmentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
