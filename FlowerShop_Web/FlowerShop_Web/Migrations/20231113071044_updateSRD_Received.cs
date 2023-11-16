using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class updateSRD_Received : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "41da5707-1aac-4fc1-988f-19091734457b", "e3d18b2e-cbea-471e-ba45-8dddbd195c02" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3e0d4a8a-9caf-47ba-a68c-18dae495503c", "ec93be50-4a21-49c3-ba18-4dcf3833f053" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e0d4a8a-9caf-47ba-a68c-18dae495503c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "41da5707-1aac-4fc1-988f-19091734457b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e3d18b2e-cbea-471e-ba45-8dddbd195c02");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ec93be50-4a21-49c3-ba18-4dcf3833f053");

            migrationBuilder.AddColumn<bool>(
                name: "Received",
                table: "StockReceivedDockets",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Received",
                table: "StockReceivedDockets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e0d4a8a-9caf-47ba-a68c-18dae495503c", null, "User", "USER" },
                    { "41da5707-1aac-4fc1-988f-19091734457b", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "e3d18b2e-cbea-471e-ba45-8dddbd195c02", 0, null, "597fac21-2e25-4174-9bf8-0dcec068f2d5", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEJZKN6H1BH8+3U70YbzQMqmftS5+DtaAQ5+oD9925916gIZZcIJqx14xWrcfyBGTnA==", null, false, "daab6f7c-8ffb-46ff-b517-5c4645fd6384", null, false, "user3@hotmail.com" },
                    { "ec93be50-4a21-49c3-ba18-4dcf3833f053", 0, null, "7db65833-3237-4baf-8476-912cc911a7ff", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEIpMnYmyOflRsoLLsWp5Ol86GKhCtVQHRMaMy8okVXQip80anx7F1hlbZaUShIbAtA==", null, false, "7f309780-d97a-4290-b92c-809332ab3cd1", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "41da5707-1aac-4fc1-988f-19091734457b", "e3d18b2e-cbea-471e-ba45-8dddbd195c02" },
                    { "3e0d4a8a-9caf-47ba-a68c-18dae495503c", "ec93be50-4a21-49c3-ba18-4dcf3833f053" }
                });
        }
    }
}
