using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NinjaManager.Migrations
{
    /// <inheritdoc />
    public partial class Equipment_seeds_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Equipment",
                columns: new[] { "Id", "Agility", "Intelligence", "Name", "Strength", "Type", "Value" },
                values: new object[,]
                {
                    { 1, 6, 5, "Ninja Hood of Shadows", 2, "Head", 80 },
                    { 2, 4, 1, "Samurai Helm", 5, "Head", 100 },
                    { 3, 10, 4, "Cursed Mask of Stealth", 3, "Head", 90 },
                    { 4, 3, 2, "Dragon Scale Armor", 10, "Chest", 200 },
                    { 5, 12, 2, "Leather Tunic of Agility", 4, "Chest", 120 },
                    { 6, 5, 10, "Mystic Robe of Wisdom", 1, "Chest", 150 },
                    { 7, 4, 1, "Bracers of Strength", 8, "Hand", 110 },
                    { 8, 9, 4, "Gloves of Precision", 2, "Hand", 95 },
                    { 9, 11, 2, "Assassin's Gloves", 3, "Hand", 100 },
                    { 10, 15, 2, "Boots of Silent Steps", 1, "Feet", 130 },
                    { 11, 7, 2, "Warrior's Sandals", 5, "Feet", 90 },
                    { 12, 8, 3, "Frostwalker Boots", 4, "Feet", 140 },
                    { 13, 1, 0, "Ring of Strength", 5, "Ring", 70 },
                    { 14, 1, 5, "Ring of Intelligence", 0, "Ring", 80 },
                    { 15, 5, 0, "Ring of Agility", 1, "Ring", 75 },
                    { 16, 2, 8, "Amulet of the Night", 2, "Necklace", 110 },
                    { 17, 3, 5, "Necklace of the Elements", 3, "Necklace", 120 },
                    { 18, 6, 3, "Talisman of Protection", 1, "Necklace", 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Equipment",
                keyColumn: "Id",
                keyValue: 18);
        }
    }
}
