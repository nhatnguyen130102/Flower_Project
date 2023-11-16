using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class db_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6eb413b5-5294-4980-b2f7-f57682056224", "0c171a6c-238c-4e17-b81a-606c5a29118c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "64031944-a188-4e59-a750-4a70b8339015", "6837f745-aaa3-4977-bc8c-a4f5a4068dc7" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64031944-a188-4e59-a750-4a70b8339015");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6eb413b5-5294-4980-b2f7-f57682056224");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0c171a6c-238c-4e17-b81a-606c5a29118c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6837f745-aaa3-4977-bc8c-a4f5a4068dc7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2e842a28-2cc0-43d4-b247-68d359611e93", null, "User", "USER" },
                    { "c420ccab-7f5c-432d-b356-2163f0674938", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "947a74f5-25b8-40b1-af00-fc72111f2d42", 0, null, "0cb52e8a-9527-48ae-8ee8-0a94de16e853", "user3@hotmail.com", true, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEH2e5GOv8FvUlsPSH/BjRM/WRcpj6wUHPp6RdXa7Qhe90kABRC/41/Zbq0LUva8AXQ==", null, false, "ccd27239-d563-46b6-879f-1d01740f1d3b", null, false, "user3@hotmail.com" },
                    { "eda81f99-3eb7-4a75-8fbf-f2b2d43b4119", 0, null, "630dff77-2ce2-42c1-8a44-0dbfa87a9817", "user2@hotmail.com", true, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEDCyt9NUDTgqKAsMgEZM8OadXDdzJjkgArCTK1K0YPqE531KRYEVTPry645ViEMLPw==", null, false, "9ca0bfcb-aad7-4193-a85c-7288b9a0d151", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "c420ccab-7f5c-432d-b356-2163f0674938", "947a74f5-25b8-40b1-af00-fc72111f2d42" },
                    { "2e842a28-2cc0-43d4-b247-68d359611e93", "eda81f99-3eb7-4a75-8fbf-f2b2d43b4119" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedDocketDetails_ID_StockReceivedDocket",
                table: "StockReceivedDocketDetails",
                column: "ID_StockReceivedDocket");

            migrationBuilder.AddForeignKey(
                name: "FK_StockReceivedDocketDetails_StockReceivedDockets_ID_StockReceivedDocket",
                table: "StockReceivedDocketDetails",
                column: "ID_StockReceivedDocket",
                principalTable: "StockReceivedDockets",
                principalColumn: "ID_StockReceivedDocket",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockReceivedDocketDetails_StockReceivedDockets_ID_StockReceivedDocket",
                table: "StockReceivedDocketDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockReceivedDocketDetails_ID_StockReceivedDocket",
                table: "StockReceivedDocketDetails");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c420ccab-7f5c-432d-b356-2163f0674938", "947a74f5-25b8-40b1-af00-fc72111f2d42" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2e842a28-2cc0-43d4-b247-68d359611e93", "eda81f99-3eb7-4a75-8fbf-f2b2d43b4119" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2e842a28-2cc0-43d4-b247-68d359611e93");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c420ccab-7f5c-432d-b356-2163f0674938");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "947a74f5-25b8-40b1-af00-fc72111f2d42");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eda81f99-3eb7-4a75-8fbf-f2b2d43b4119");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "64031944-a188-4e59-a750-4a70b8339015", null, "Admin", "ADMIN" },
                    { "6eb413b5-5294-4980-b2f7-f57682056224", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0c171a6c-238c-4e17-b81a-606c5a29118c", 0, null, "c2471620-babc-4fab-80a0-343ca11e76e2", "user2@hotmail.com", true, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEHsB8G3BW08XOLXtR5+wJnu8D1fhPuN9ZSEkoEqciP3nmmyYhEaae5Gc5tQSFLf4MA==", null, false, "c859cd00-a004-48ff-8bfa-26fe1905ca30", null, false, "user2@hotmail.com" },
                    { "6837f745-aaa3-4977-bc8c-a4f5a4068dc7", 0, null, "f9d28a09-002a-4989-a84b-c308c95ddc3e", "user3@hotmail.com", true, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEPHqkfE9ZYKxYwyhnUpxgYJK/ubLPOS7WZJaFxPX/h+McGUb/WshNVipP/QDSkCWIA==", null, false, "85b61402-5c41-4d9f-86c2-07c5fde28ce9", null, false, "user3@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "6eb413b5-5294-4980-b2f7-f57682056224", "0c171a6c-238c-4e17-b81a-606c5a29118c" },
                    { "64031944-a188-4e59-a750-4a70b8339015", "6837f745-aaa3-4977-bc8c-a4f5a4068dc7" }
                });
        }
    }
}
