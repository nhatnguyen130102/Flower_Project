using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class db_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "DeliveredStatus",
                table: "Bills",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HandleStatus",
                table: "Bills",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "DeliveredStatus",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "HandleStatus",
                table: "Bills");

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
        }
    }
}
