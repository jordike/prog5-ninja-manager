using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NinjaManager.Migrations
{
    /// <inheritdoc />
    public partial class add_foreign_keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NinjaHasEquipment_EquipmentId",
                table: "NinjaHasEquipment",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NinjaHasEquipment_Equipment_EquipmentId",
                table: "NinjaHasEquipment",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NinjaHasEquipment_Ninjas_NinjaId",
                table: "NinjaHasEquipment",
                column: "NinjaId",
                principalTable: "Ninjas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NinjaHasEquipment_Equipment_EquipmentId",
                table: "NinjaHasEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_NinjaHasEquipment_Ninjas_NinjaId",
                table: "NinjaHasEquipment");

            migrationBuilder.DropIndex(
                name: "IX_NinjaHasEquipment_EquipmentId",
                table: "NinjaHasEquipment");
        }
    }
}
