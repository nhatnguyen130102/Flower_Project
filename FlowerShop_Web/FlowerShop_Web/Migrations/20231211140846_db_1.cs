using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class db_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "9b783a59-e175-4f0a-987c-3e9787bcac1a", "09409e55-c163-46a8-9566-d4fec30f30e6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2733f7ef-fca5-492b-a1a5-7982d4b35897", "9ec82931-6689-4d81-bcc8-fb85c6dfaad5" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2733f7ef-fca5-492b-a1a5-7982d4b35897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9b783a59-e175-4f0a-987c-3e9787bcac1a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "09409e55-c163-46a8-9566-d4fec30f30e6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ec82931-6689-4d81-bcc8-fb85c6dfaad5");

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c6015ea-44be-40d7-9a17-3f63d8f46af9", null, "Manager", "MANAGER" },
                    { "508fd932-05dc-4f9f-9411-8a166fdb9c93", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "03be5608-7a8a-4961-8f6a-363e8de81ea3", 0, null, null, "bc9cbe05-8425-4c6f-94ad-3e51f37d909f", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEBBKZ7luB7cx4af/5FUBiZTzU7sXT1+7AN+v+Bfz0hcSjtDJDMkjC/ORAS6ZaYo4FQ==", null, false, "94f104c3-09d4-47f4-8cf0-1892419f98d3", null, false, "user3@hotmail.com" },
                    { "ff739ad0-a48b-47ff-9c5f-2aa71c4f7db6", 0, null, null, "5eea95b2-f135-46e0-a9a1-61ddeeff6821", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEBSJMXM3tkIA2fDXHEv1mbNNtaIWzy2xrpTx+yCqPRYIOCPnL5aoyzUgk3dCtzv9Ig==", null, false, "ea904957-707f-49b1-8e13-980b90ab3a36", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "508fd932-05dc-4f9f-9411-8a166fdb9c93", "03be5608-7a8a-4961-8f6a-363e8de81ea3" },
                    { "3c6015ea-44be-40d7-9a17-3f63d8f46af9", "ff739ad0-a48b-47ff-9c5f-2aa71c4f7db6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ProductId",
                table: "Feedbacks",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "508fd932-05dc-4f9f-9411-8a166fdb9c93", "03be5608-7a8a-4961-8f6a-363e8de81ea3" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3c6015ea-44be-40d7-9a17-3f63d8f46af9", "ff739ad0-a48b-47ff-9c5f-2aa71c4f7db6" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6015ea-44be-40d7-9a17-3f63d8f46af9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "508fd932-05dc-4f9f-9411-8a166fdb9c93");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "03be5608-7a8a-4961-8f6a-363e8de81ea3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ff739ad0-a48b-47ff-9c5f-2aa71c4f7db6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2733f7ef-fca5-492b-a1a5-7982d4b35897", null, "Manager", "MANAGER" },
                    { "9b783a59-e175-4f0a-987c-3e9787bcac1a", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "City", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "09409e55-c163-46a8-9566-d4fec30f30e6", 0, null, null, "9453d34e-0fdd-441b-b83f-5e6d1c4944c9", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAENXDiYDRBskil30r5vqbW4JzqdiI9bxgpTgWezr/lSTlg4UiV+2xoQH2XQz62O0PKQ==", null, false, "7f95ca3f-fecc-4bbc-a33d-ae541048d4b9", null, false, "user3@hotmail.com" },
                    { "9ec82931-6689-4d81-bcc8-fb85c6dfaad5", 0, null, null, "63201776-e806-4072-8a4b-32ea52c706ad", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEGHb+Go8ZASPOQfF9FEr3eBL3Ii7ox1aY7HmstvsMzSQ2aXJizn5NTSgLyECIa66VA==", null, false, "59d828ba-09f1-4dd1-9343-ca3aefb81af3", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "9b783a59-e175-4f0a-987c-3e9787bcac1a", "09409e55-c163-46a8-9566-d4fec30f30e6" },
                    { "2733f7ef-fca5-492b-a1a5-7982d4b35897", "9ec82931-6689-4d81-bcc8-fb85c6dfaad5" }
                });
        }
    }
}
