using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Game106.Backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedPlayersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "49f846e7-d9d0-4330-8be8-1d068535ba38", "AQAAAAIAAYagAAAAEElkUF1TQ3LLwcjkwOCYj+BQGTE1fWNxphkeSerOw/Uu8PW+9mL87sMI7ChsXa+yxg==", "19037974-6202-4a8f-b134-cca8f6f742f9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3ab08a48-51a8-452b-9b5f-7d2b60d4f0bd", "AQAAAAIAAYagAAAAEGU2AziBn+d6Uqby0vg8VNS4eoyhtNTjF74TL8VyoROo+SyLASEDXYJBS20gtGm58A==", "785c98f6-fe04-48c3-9606-e13ef6fa6085" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a90055b4-23c6-4c44-a09f-2d78badf884b", "AQAAAAIAAYagAAAAEJgxwmGF4oVSQwNl9pDXwPyIbCP/GVDbFoPgkAHHz/6zN+qc3RqqlcMj/Cc4W2uAsQ==", "61e06fbf-1c26-44a0-bcfb-936f319141b6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17f6460d-652a-4e8a-a8ae-de92e6c8c607", "AQAAAAIAAYagAAAAENfodsIkokg707sqn2l3bAHZDwBW9mqD99D9MSH068ZqIom3tC7nieZBSyhs9bAz5g==", "d7603683-c8ea-46a4-8aed-cf74f123b4ed" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0dd48135-cd68-41ed-9938-1a281115b55c", "AQAAAAIAAYagAAAAELXBUumnyS30KWq2ipfyWt3v58r2dm/CERdmJT9XxkXCKKH2VDWAe1lgJrfg7j0SAQ==", "f975947b-e5e3-4712-8337-e510d8a31a3f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c22218b-3869-484f-b5a0-8220b0e9db87", "AQAAAAIAAYagAAAAEDM9KPV1pbZCdkbfHcBOtB5uwm0C9Z0IFmOJYNHMz8R0Cxh5GRQzYrpjk2P0iT4xyw==", "3b9c9a2c-a405-4d1b-8557-98c3b30989f3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d53f73b8-a786-41f4-902a-a98a8cbee6ae", "AQAAAAIAAYagAAAAEOuMOQ0sO6w+zTYZnLLnj8YDtS0Yn0OVmRcbsIN9eIlkS1dv0AyP+DmdzQY657OzvQ==", "9392348c-a9f1-4991-80cd-91fa4d37b03a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4b4a33b-00bd-411f-ae98-b5bfca4511ac", "AQAAAAIAAYagAAAAECJiq1JkffNedM985ytrWjNs016s05fpZekLaQCHQDZPxCbuUS+YlgYVeFPoRALNVw==", "03a453ef-71af-4aca-82d1-5247d9c615ce" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d0363d02-a7e6-4601-9ede-8ddd0f8b1748", "AQAAAAIAAYagAAAAEAIWRxc7SQp5Lj5rCCN6LqIX8y2Yv++ivIbBWiV/OGrQ4NbXpT2jV61XAzQxIsJVlg==", "613e84b4-e06b-4d4c-b1d2-a8efef90dd39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2eaefdea-8099-481d-9721-ed75d3cc1915", "AQAAAAIAAYagAAAAEMypx/K6ohRe8V6HS3jPvMAG6gTVS21+1LSDIKYmZoZazNqR/mFDq2k61pW0cSn26w==", "8ef72df4-60f7-4278-8303-dd5669addf66" });
        }
    }
}
