using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AnimalRefugeFinal.Migrations
{
    /// <inheritdoc />
    public partial class SeedingPets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "someRoleId");

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "Age", "BondedBuddyStatus", "Description", "IsAdopted", "Name", "SpecialCareInstructions", "Species" },
                values: new object[,]
                {
                    { 9, 2, "None", "enchants with her silky midnight-blue fur and eyes that gleam like twin crescent moons, exuding an aura of serene elegance and quiet wisdom.", false, "Raven", "Needs attention", "Cat" },
                    { 10, 3, "None", "a gentle soul, displaying a heartwarming affection by leaning into every pat and responding to commands with a calm and eager demeanor.", false, "Worm", "Afraid of worms", "Dog" },
                    { 11, 2, "None", "a calm observer, finding solace in quiet corners and displaying a gentle, affectionate nature by curling up in laps during cozy evenings.", false, "Jack", "Needs litter to be extra clean", "Cat" },
                    { 12, 3, "None", "a lively bundle of joy, his tail in a perpetual state of enthusiastic wagging as he greets everyone with boundless energy.", false, "Inu", "Is diabetic", "Dog" },
                    { 13, 22, "Daisey", "a patient and resilient friend, gracefully gliding through the water with a quiet determination and occasionally retreating into her shell to reflect", false, "Yoshi", "Needs alone time", "Reptile" },
                    { 14, 39, "None", "embodies an enigmatic grace, sinuously navigating her surroundings with hypnotic movements.", false, "Touka", "Good with kids", "Reptile" },
                    { 15, 20, "None", "moves through life with a calm and deliberate pace, his shell adorned with the wisdom of ages ", false, "Meryl", "Good with other pets", "Reptile" },
                    { 16, 3, "Dusty", "a curious and nimble explorer, darting around with playful hops.", false, "Patches", "Is skitish", "Bunny" },
                    { 17, 5, "Patches", "a mischievous delight, showcasing a penchant for binkying through open spaces, executing playful jumps with contagious excitement", false, "Dusty", "Needs extra love", "Bunny" },
                    { 18, 13, "None", "a serene and gentle companion, preferring quiet moments of nibbling on fresh greens and lounging in sunlit spots.", false, "Pixie", "Needs to be the only pet", "Bunny" },
                    { 19, 4, "None", "Mystique, the cat, boasts a sleek onyx coat and captivating green eyes, exuding an air of graceful mystery and feline elegance.", false, "Destiny", "None", "Cat" },
                    { 20, 6, "None", "an adventurous spirit, always ready for a new escapade, whether it's exploring the outdoors or chasing after frisbees.", false, "Chilli", "None", "Dog" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Pets",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "someRoleId", null, "USER", "USER" });
        }
    }
}
