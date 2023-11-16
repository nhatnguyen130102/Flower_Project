using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class db_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7fb63ac0-49b8-49b6-bd05-4b418fe17600", "21996b92-f46a-4011-aba9-abbd057eb3d9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d38c90b7-de1e-4a8d-bed9-50a8d8c3b722", "f51d566f-502b-4725-93f2-21d19af4e55e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fb63ac0-49b8-49b6-bd05-4b418fe17600");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d38c90b7-de1e-4a8d-bed9-50a8d8c3b722");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "21996b92-f46a-4011-aba9-abbd057eb3d9");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f51d566f-502b-4725-93f2-21d19af4e55e");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7fb63ac0-49b8-49b6-bd05-4b418fe17600", null, "Admin", "ADMIN" },
                    { "d38c90b7-de1e-4a8d-bed9-50a8d8c3b722", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "21996b92-f46a-4011-aba9-abbd057eb3d9", 0, null, "aa25cbd6-1157-470c-95c5-3693da6d483f", "ApplicationUser", "user3@hotmail.com", true, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEIyyyFe6rBiYte6uIRFLK1MJS1F71H79vPGz5MQzNin1ETRuOe3QxDYlHpXTzwps9A==", null, false, "b1bc46dd-f734-4610-842c-dfb6d48b3808", null, false, "user3@hotmail.com" },
                    { "f51d566f-502b-4725-93f2-21d19af4e55e", 0, null, "c4c32212-f4fc-459a-a1ac-2da4215791a6", "ApplicationUser", "user2@hotmail.com", true, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEPn9P7H8NkqhZ/FVF9oSdp/Mn3qmE6XgGEWfYhCEayf6Cd2wz5Q5E1J2jK8jV7zexw==", null, false, "1cdec7be-f32e-4e2c-88f4-f02a1d287dcf", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "7fb63ac0-49b8-49b6-bd05-4b418fe17600", "21996b92-f46a-4011-aba9-abbd057eb3d9" },
                    { "d38c90b7-de1e-4a8d-bed9-50a8d8c3b722", "f51d566f-502b-4725-93f2-21d19af4e55e" }
                });
        }
    }
}
