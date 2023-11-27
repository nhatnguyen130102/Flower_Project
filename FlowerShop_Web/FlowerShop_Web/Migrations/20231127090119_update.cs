using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "cd24ff99-3ec3-434f-b45b-3935b0847efc", "04936b57-13a8-4e8f-9ba9-52bc106f1814" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6c3edabf-7062-44d9-9329-0625e1f9513b", "6bfb28d4-feb5-4bd5-bf1c-008770aa65da" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c3edabf-7062-44d9-9329-0625e1f9513b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd24ff99-3ec3-434f-b45b-3935b0847efc");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "04936b57-13a8-4e8f-9ba9-52bc106f1814");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6bfb28d4-feb5-4bd5-bf1c-008770aa65da");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "District",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Ward",
                table: "AspNetUsers");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6c3edabf-7062-44d9-9329-0625e1f9513b", null, "Manager", "MANAGER" },
                    { "cd24ff99-3ec3-434f-b45b-3935b0847efc", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "District", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "Street", "TwoFactorEnabled", "UserName", "Ward" },
                values: new object[,]
                {
                    { "04936b57-13a8-4e8f-9ba9-52bc106f1814", 0, null, null, "ba70c274-869d-4d37-83ac-b8b06e659bfe", null, "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEOz8UCmycH306oxzu8XTGGb51tmIg4qJYuxwmh2ScP67EZAWpfZI0AwY2N2HaAulaQ==", null, false, "35439fb2-dee1-4ccf-a255-40a74d5ac49f", null, null, false, "user3@hotmail.com", null },
                    { "6bfb28d4-feb5-4bd5-bf1c-008770aa65da", 0, null, null, "42dba41f-e4a6-4fe2-ae88-a5a9693d9291", null, "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEFMxbZfyI9q8dj6esl4y1AAp0yZp+34dvQuXmwgvG/YzLgsOg2xIDmsErhkzAWyO3A==", null, false, "930c8b55-bbea-4d23-90f9-f726a445d303", null, null, false, "user2@hotmail.com", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "cd24ff99-3ec3-434f-b45b-3935b0847efc", "04936b57-13a8-4e8f-9ba9-52bc106f1814" },
                    { "6c3edabf-7062-44d9-9329-0625e1f9513b", "6bfb28d4-feb5-4bd5-bf1c-008770aa65da" }
                });
        }
    }
}
