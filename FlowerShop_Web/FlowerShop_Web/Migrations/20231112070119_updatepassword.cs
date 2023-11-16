using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class updatepassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3e57f71a-f96e-4506-9efe-21e054bbb324", "83eaa754-e2f9-404e-bb94-52833ca199b1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7c3dc0ae-83cf-412d-9e79-dc8b8be53b69", "c282ccaa-f6e5-4d2b-b02e-85df59253cf3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e57f71a-f96e-4506-9efe-21e054bbb324");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c3dc0ae-83cf-412d-9e79-dc8b8be53b69");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "83eaa754-e2f9-404e-bb94-52833ca199b1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c282ccaa-f6e5-4d2b-b02e-85df59253cf3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9a4d19e6-b379-480b-bcb3-7ca5edc3c6ed", null, "Admin", "ADMIN" },
                    { "c6861367-de93-4a8b-be90-05f83777bd6b", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "66e874a3-b293-458b-bf21-5a2115267b9f", 0, null, "0e89dfa2-db55-4d53-8a16-bcb94ab42c5e", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEJFxLcYUJVB+si92lup2glZaITTs4J7KQAM6B4aFD7BvAs9QG6JS8qt7zJiwTzKyBw==", null, false, "e90c997f-5b0a-4aff-9729-bf54a575e941", null, false, "user3@hotmail.com" },
                    { "bcaee24c-fc75-4bc9-9be9-d559998cf4b6", 0, null, "61fd69ac-188e-4ffd-b578-ce71ee0b7973", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEHSoXpbe5t4KGo+sg5zWaw7wclVGqjGIHY6W9x16lJituvBV0JShqWhcSNeaHOPM9w==", null, false, "3408984f-1cfe-4948-ba75-cae28f1667c0", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9a4d19e6-b379-480b-bcb3-7ca5edc3c6ed", "66e874a3-b293-458b-bf21-5a2115267b9f" },
                    { "c6861367-de93-4a8b-be90-05f83777bd6b", "bcaee24c-fc75-4bc9-9be9-d559998cf4b6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9a4d19e6-b379-480b-bcb3-7ca5edc3c6ed", "66e874a3-b293-458b-bf21-5a2115267b9f" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c6861367-de93-4a8b-be90-05f83777bd6b", "bcaee24c-fc75-4bc9-9be9-d559998cf4b6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a4d19e6-b379-480b-bcb3-7ca5edc3c6ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6861367-de93-4a8b-be90-05f83777bd6b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "66e874a3-b293-458b-bf21-5a2115267b9f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bcaee24c-fc75-4bc9-9be9-d559998cf4b6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e57f71a-f96e-4506-9efe-21e054bbb324", null, "Admin", "ADMIN" },
                    { "7c3dc0ae-83cf-412d-9e79-dc8b8be53b69", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "83eaa754-e2f9-404e-bb94-52833ca199b1", 0, null, "041bd1a0-0f53-4af3-a5cd-643ff7e62095", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEAIcM2JbzO/Davhc+K4V6LeUcmhJ9pso4x7g9a0VzczYfWXf5hd2cM3fegK4+3zpTw==", null, false, "b1e99944-185c-4515-9ede-aa247ce24627", null, false, "user3@hotmail.com" },
                    { "c282ccaa-f6e5-4d2b-b02e-85df59253cf3", 0, null, "bde5ec36-0326-4183-bf94-4c7fb3b04f4a", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAECnUcTRib456RctwWmgtMibbxiBprM0ULshzZNIuzP430mxqPBXWshvAp50Mq9h+Ew==", null, false, "0992f5b7-9861-45d4-a388-daa7b253c3f8", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "3e57f71a-f96e-4506-9efe-21e054bbb324", "83eaa754-e2f9-404e-bb94-52833ca199b1" },
                    { "7c3dc0ae-83cf-412d-9e79-dc8b8be53b69", "c282ccaa-f6e5-4d2b-b02e-85df59253cf3" }
                });
        }
    }
}
