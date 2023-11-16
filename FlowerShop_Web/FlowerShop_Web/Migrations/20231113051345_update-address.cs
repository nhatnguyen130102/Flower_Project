using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class updateaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "District",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Bills");

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
    }
}
