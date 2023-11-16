using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class addID_ShopApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "055ee4e3-2d15-44a0-a5ff-73a62b8086aa", "eb155625-60c3-4a1d-b456-411cdf21b1dd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "0034f74c-06b4-4f0c-8596-b77a1a6a5c08", "f982ae96-81da-4683-a984-3090c38fa050" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0034f74c-06b4-4f0c-8596-b77a1a6a5c08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "055ee4e3-2d15-44a0-a5ff-73a62b8086aa");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "eb155625-60c3-4a1d-b456-411cdf21b1dd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "f982ae96-81da-4683-a984-3090c38fa050");

            migrationBuilder.AddColumn<int>(
                name: "ID_Shop",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ID_Shop",
                table: "AspNetUsers",
                column: "ID_Shop");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Shops_ID_Shop",
                table: "AspNetUsers",
                column: "ID_Shop",
                principalTable: "Shops",
                principalColumn: "ID_Shop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Shops_ID_Shop",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ID_Shop",
                table: "AspNetUsers");

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

            migrationBuilder.DropColumn(
                name: "ID_Shop",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0034f74c-06b4-4f0c-8596-b77a1a6a5c08", null, "Admin", "ADMIN" },
                    { "055ee4e3-2d15-44a0-a5ff-73a62b8086aa", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "eb155625-60c3-4a1d-b456-411cdf21b1dd", 0, null, "80cd1817-b333-47e6-ba4b-a40113f369e0", "user2@hotmail.com", true, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEJjNEibCsLWJ4gAiccOdYafk9rZ1S1ft6wJc56cfQlcL4xZhKgMu7DEvEEGVNMY+hA==", null, false, "ea86aae9-0500-4c38-a941-487a49083407", null, false, "user2@hotmail.com" },
                    { "f982ae96-81da-4683-a984-3090c38fa050", 0, null, "a7420bd2-3a91-4009-b567-143213420b8e", "user3@hotmail.com", true, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEBLBnw942o1duPb74Uqfnjcv2sYW8oX8sdjI0ndD7a9R+9770T8Hz9Hd/51nOSPZzQ==", null, false, "44b0732f-4606-4f99-a1d4-a3215d0339b2", null, false, "user3@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "055ee4e3-2d15-44a0-a5ff-73a62b8086aa", "eb155625-60c3-4a1d-b456-411cdf21b1dd" },
                    { "0034f74c-06b4-4f0c-8596-b77a1a6a5c08", "f982ae96-81da-4683-a984-3090c38fa050" }
                });
        }
    }
}
