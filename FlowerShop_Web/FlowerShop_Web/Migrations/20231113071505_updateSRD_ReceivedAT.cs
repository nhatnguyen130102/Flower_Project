using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class updateSRD_ReceivedAT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "19ad506b-4ab3-4067-97d9-c98d8a4db261", "70648e95-343e-4c49-9880-3e5fdc51ed93" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9c84c74f-0c9e-4cc0-abb5-21dbd0926e01", "80b6b868-c391-4be0-a833-ba012abe1443" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19ad506b-4ab3-4067-97d9-c98d8a4db261");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c84c74f-0c9e-4cc0-abb5-21dbd0926e01");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "70648e95-343e-4c49-9880-3e5fdc51ed93");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "80b6b868-c391-4be0-a833-ba012abe1443");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReceivedAt",
                table: "StockReceivedDockets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5852a84b-8c47-43b6-ae47-b52e0174f76a", null, "User", "USER" },
                    { "ef4f08b6-3fc5-4263-aa26-e764a94b1762", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "017a4a6f-afb5-40ff-9306-045ee00c61ba", 0, null, "fd6e609d-35a4-457b-9b4a-c1eff19bb35f", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFnhxtdVKXsI6BPAvwPPwzy1QlGFI7AyDVP8rCPUs0T9su2ckBMq9CF71z8wn+Seug==", null, false, "92d11942-6e85-4780-8634-eb47da480f4c", null, false, "user3@hotmail.com" },
                    { "e32bb518-2515-4bd5-9790-fc771301eebd", 0, null, "c971240a-aee2-4c42-a94f-4842b12616dc", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEOO5OVLUhwrZyHpulpTlR8flwwVnPaA4F5Dpc7XbsAftONSWMHRoy0+D7ZAowxSTMg==", null, false, "c44fa641-9e17-4509-b65b-b082b1067e96", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ef4f08b6-3fc5-4263-aa26-e764a94b1762", "017a4a6f-afb5-40ff-9306-045ee00c61ba" },
                    { "5852a84b-8c47-43b6-ae47-b52e0174f76a", "e32bb518-2515-4bd5-9790-fc771301eebd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ef4f08b6-3fc5-4263-aa26-e764a94b1762", "017a4a6f-afb5-40ff-9306-045ee00c61ba" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5852a84b-8c47-43b6-ae47-b52e0174f76a", "e32bb518-2515-4bd5-9790-fc771301eebd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5852a84b-8c47-43b6-ae47-b52e0174f76a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef4f08b6-3fc5-4263-aa26-e764a94b1762");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "017a4a6f-afb5-40ff-9306-045ee00c61ba");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e32bb518-2515-4bd5-9790-fc771301eebd");

            migrationBuilder.DropColumn(
                name: "ReceivedAt",
                table: "StockReceivedDockets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19ad506b-4ab3-4067-97d9-c98d8a4db261", null, "Admin", "ADMIN" },
                    { "9c84c74f-0c9e-4cc0-abb5-21dbd0926e01", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "70648e95-343e-4c49-9880-3e5fdc51ed93", 0, null, "17ed8242-1e2d-4abf-82fb-538859b240d9", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEAECGVk7TVgsc9XKfHBd4fh2aM9ZuolPQlzBzrsdUt9UysOaBPR+4cz3JsAK7PIPQA==", null, false, "e1e4f984-b52c-4b07-ba5e-43fe00d71601", null, false, "user3@hotmail.com" },
                    { "80b6b868-c391-4be0-a833-ba012abe1443", 0, null, "725507f5-42a3-4d99-8a50-5e3f260894ff", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEIpfIqmNC24oe0Q/ROLZYl6EbsY8t3pvl7HP0UiyKsDTCPxoySNwejzvbLHjexc+uQ==", null, false, "796f102e-3dea-4b66-94a9-e4cfdfee1ab1", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "19ad506b-4ab3-4067-97d9-c98d8a4db261", "70648e95-343e-4c49-9880-3e5fdc51ed93" },
                    { "9c84c74f-0c9e-4cc0-abb5-21dbd0926e01", "80b6b868-c391-4be0-a833-ba012abe1443" }
                });
        }
    }
}
