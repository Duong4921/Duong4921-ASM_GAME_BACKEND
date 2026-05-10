using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Game106.Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTestUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LinkAvatar", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "OTP", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegionId", "RoleId", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "7c22218b-3869-484f-b5a0-8220b0e9db87", "user1@gmail.com", true, false, "https://cdn-icons-png.flaticon.com/512/149/149071.png", false, null, "Người chơi số 1", "USER1@GMAIL.COM", "USER1", "", "AQAAAAIAAYagAAAAEDM9KPV1pbZCdkbfHcBOtB5uwm0C9Z0IFmOJYNHMz8R0Cxh5GRQzYrpjk2P0iT4xyw==", null, false, 1, 2, "3b9c9a2c-a405-4d1b-8557-98c3b30989f3", false, "user1" },
                    { "2", 0, "d53f73b8-a786-41f4-902a-a98a8cbee6ae", "user2@gmail.com", true, false, "https://cdn-icons-png.flaticon.com/512/149/149071.png", false, null, "Người chơi số 2", "USER2@GMAIL.COM", "USER2", "", "AQAAAAIAAYagAAAAEOuMOQ0sO6w+zTYZnLLnj8YDtS0Yn0OVmRcbsIN9eIlkS1dv0AyP+DmdzQY657OzvQ==", null, false, 1, 2, "9392348c-a9f1-4991-80cd-91fa4d37b03a", false, "user2" },
                    { "3", 0, "d4b4a33b-00bd-411f-ae98-b5bfca4511ac", "user3@gmail.com", true, false, "https://cdn-icons-png.flaticon.com/512/149/149071.png", false, null, "Người chơi số 3", "USER3@GMAIL.COM", "USER3", "", "AQAAAAIAAYagAAAAECJiq1JkffNedM985ytrWjNs016s05fpZekLaQCHQDZPxCbuUS+YlgYVeFPoRALNVw==", null, false, 1, 2, "03a453ef-71af-4aca-82d1-5247d9c615ce", false, "user3" },
                    { "4", 0, "d0363d02-a7e6-4601-9ede-8ddd0f8b1748", "user4@gmail.com", true, false, "https://cdn-icons-png.flaticon.com/512/149/149071.png", false, null, "Người chơi số 4", "USER4@GMAIL.COM", "USER4", "", "AQAAAAIAAYagAAAAEAIWRxc7SQp5Lj5rCCN6LqIX8y2Yv++ivIbBWiV/OGrQ4NbXpT2jV61XAzQxIsJVlg==", null, false, 1, 2, "613e84b4-e06b-4d4c-b1d2-a8efef90dd39", false, "user4" },
                    { "5", 0, "2eaefdea-8099-481d-9721-ed75d3cc1915", "user5@gmail.com", true, false, "https://cdn-icons-png.flaticon.com/512/149/149071.png", false, null, "Người chơi số 5", "USER5@GMAIL.COM", "USER5", "", "AQAAAAIAAYagAAAAEMypx/K6ohRe8V6HS3jPvMAG6gTVS21+1LSDIKYmZoZazNqR/mFDq2k61pW0cSn26w==", null, false, 1, 2, "8ef72df4-60f7-4278-8303-dd5669addf66", false, "user5" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5");
        }
    }
}
