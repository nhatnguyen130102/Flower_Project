using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "29ceee2a-e04b-4923-ad9d-5e7f8caa48ed", "0675e63e-9598-4cba-adaa-668344609d9c" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2ada5c26-d2a3-4f7f-8e5d-4bc09d79a556", "8fa270ce-0586-4065-82df-21bf966636b3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "29ceee2a-e04b-4923-ad9d-5e7f8caa48ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ada5c26-d2a3-4f7f-8e5d-4bc09d79a556");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0675e63e-9598-4cba-adaa-668344609d9c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8fa270ce-0586-4065-82df-21bf966636b3");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1767b1e9-9a84-4e60-b685-4c84f2b80b44", null, "Admin", "ADMIN" },
                    { "d9792d19-4d78-45a5-bf0c-12ceeed1f5e4", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1ec8c500-6a4d-4538-924a-021b12106e22", 0, null, null, "30e0b9de-c8f1-4d68-b4cb-a8d8b4a57967", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFhWHQnsw4mLCP/csJnL6UEZ/hIO6MhqYg5U3c9LgyeFky3QYJIlo3S7/pdA8XVvgg==", null, false, "840acfcb-cbfa-4057-ae94-752021ce58e2", null, false, "user2@hotmail.com" },
                    { "d8f8ea0b-e600-4f5a-aa38-77c1af54c5b6", 0, null, null, "7be70806-b61d-4c12-b8f0-4fd2d6cb0d0b", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEHy8ERIaPTH5zd/MpbO99g9PhH+LrBabtAkBTEmMulfOB7ijOIfqmZeplKJXmb6n4g==", null, false, "dc7c2253-6d49-4f27-83a2-64e3b56b5c09", null, false, "user3@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d9792d19-4d78-45a5-bf0c-12ceeed1f5e4", "1ec8c500-6a4d-4538-924a-021b12106e22" },
                    { "1767b1e9-9a84-4e60-b685-4c84f2b80b44", "d8f8ea0b-e600-4f5a-aa38-77c1af54c5b6" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d9792d19-4d78-45a5-bf0c-12ceeed1f5e4", "1ec8c500-6a4d-4538-924a-021b12106e22" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1767b1e9-9a84-4e60-b685-4c84f2b80b44", "d8f8ea0b-e600-4f5a-aa38-77c1af54c5b6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1767b1e9-9a84-4e60-b685-4c84f2b80b44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d9792d19-4d78-45a5-bf0c-12ceeed1f5e4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1ec8c500-6a4d-4538-924a-021b12106e22");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d8f8ea0b-e600-4f5a-aa38-77c1af54c5b6");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "29ceee2a-e04b-4923-ad9d-5e7f8caa48ed", null, "Admin", "ADMIN" },
                    { "2ada5c26-d2a3-4f7f-8e5d-4bc09d79a556", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0675e63e-9598-4cba-adaa-668344609d9c", 0, null, null, "84079ec7-7f7e-4d69-abb7-4a44a8ec39f3", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEOa28X8ccj2d/VE5JJou48oNhaqU4OZQLzEUNhMxrJEzj8b06+LnFSerwseizjfQLQ==", null, false, "71f097da-092c-45ae-8467-b0e61721ad97", null, false, "user3@hotmail.com" },
                    { "8fa270ce-0586-4065-82df-21bf966636b3", 0, null, null, "1e35ac6e-6fe5-4965-b000-1625b9f2d60e", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEMzWyX59vMIDm39+OI4riDpfvJYUUzyJEwSETlkgzF7o4OGxIePpYbtROQu33pdqkA==", null, false, "ab6dd929-eda8-4450-a029-39af1bed3716", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "29ceee2a-e04b-4923-ad9d-5e7f8caa48ed", "0675e63e-9598-4cba-adaa-668344609d9c" },
                    { "2ada5c26-d2a3-4f7f-8e5d-4bc09d79a556", "8fa270ce-0586-4065-82df-21bf966636b3" }
                });
        }
    }
}
