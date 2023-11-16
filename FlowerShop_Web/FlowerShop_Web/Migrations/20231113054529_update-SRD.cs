using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class updateSRD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "21777a75-8d95-4fa3-9133-8a063b44ffcf", "2ef72e7d-34dc-4046-a33b-a8f9776af18a" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a2149f69-6b48-424e-a7f7-d2d3ea7f0d54", "a4812a60-9464-4050-981e-48d47cce98dc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21777a75-8d95-4fa3-9133-8a063b44ffcf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2149f69-6b48-424e-a7f7-d2d3ea7f0d54");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ef72e7d-34dc-4046-a33b-a8f9776af18a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a4812a60-9464-4050-981e-48d47cce98dc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StockReceivedDockets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StockReceivedDockets",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StockReceivedDockets");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "StockReceivedDockets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21777a75-8d95-4fa3-9133-8a063b44ffcf", null, "Admin", "ADMIN" },
                    { "a2149f69-6b48-424e-a7f7-d2d3ea7f0d54", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2ef72e7d-34dc-4046-a33b-a8f9776af18a", 0, null, "8d6e9f01-f4c3-4ab5-94e8-807dcbd95a84", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAENl79+5l4v+gLl8c+AFHLZN4X7FAARXr1OJ5o3Y/jRmLPwdICdDIibzhVmcCa2ZknQ==", null, false, "688c0d40-f2bb-4c91-be62-b7149052c677", null, false, "user3@hotmail.com" },
                    { "a4812a60-9464-4050-981e-48d47cce98dc", 0, null, "28d5724a-cdad-45f7-b716-ec2ae699eaed", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEKEfejjrMXHQ+ssX1jANCY6q1beZlH8dGvuceWEq02V9uzb57LHRxhGVU5bdE8w0YQ==", null, false, "fb4059a4-d3db-4b02-bffd-f6ff600e28be", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "21777a75-8d95-4fa3-9133-8a063b44ffcf", "2ef72e7d-34dc-4046-a33b-a8f9776af18a" },
                    { "a2149f69-6b48-424e-a7f7-d2d3ea7f0d54", "a4812a60-9464-4050-981e-48d47cce98dc" }
                });
        }
    }
}
