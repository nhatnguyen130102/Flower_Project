using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class DeliveredAt_Not_Null : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveredAt",
                table: "Bills",
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
                    { "355e7f1c-b691-4d2b-8ba5-95557d87add7", null, "User", "USER" },
                    { "c305469a-90ce-43a1-85dd-ae06635abbff", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6ecaeb25-5ac3-4614-90f6-2d836de08edd", 0, null, "604a8d16-ece9-4341-a2ec-ddd9ef874b97", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEPGNIvfWbo6xOPCGHA1e1dI7HiYAHQsVU+KmDKvOsxUaavnwQV1/HAngU2rVoh5J1g==", null, false, "10126690-8bfd-421f-84d9-a65087c641ef", null, false, "user2@hotmail.com" },
                    { "b55dec88-c9df-4d6f-92c5-cdcbe61e1912", 0, null, "bb01425b-4add-425b-8006-80e27ab8e6a5", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAELBUxy66f0HLB1YZEnISxIgJ0J6AFrIXqXILjXZjnQ1D/XQXsym4cHbCOVDGWAwy9w==", null, false, "8e7f20cc-424b-4a2a-900b-eea81d3ca502", null, false, "user3@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "355e7f1c-b691-4d2b-8ba5-95557d87add7", "6ecaeb25-5ac3-4614-90f6-2d836de08edd" },
                    { "c305469a-90ce-43a1-85dd-ae06635abbff", "b55dec88-c9df-4d6f-92c5-cdcbe61e1912" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "355e7f1c-b691-4d2b-8ba5-95557d87add7", "6ecaeb25-5ac3-4614-90f6-2d836de08edd" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c305469a-90ce-43a1-85dd-ae06635abbff", "b55dec88-c9df-4d6f-92c5-cdcbe61e1912" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "355e7f1c-b691-4d2b-8ba5-95557d87add7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c305469a-90ce-43a1-85dd-ae06635abbff");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ecaeb25-5ac3-4614-90f6-2d836de08edd");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b55dec88-c9df-4d6f-92c5-cdcbe61e1912");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveredAt",
                table: "Bills",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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
    }
}
