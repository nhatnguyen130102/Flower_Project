using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowerShop_Web.Migrations
{
    /// <inheritdoc />
    public partial class add_db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID_Category = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID_Category);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                columns: table => new
                {
                    ID_CustomerType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_CustomerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinSpend = table.Column<int>(type: "int", nullable: false),
                    MaxSpend = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.ID_CustomerType);
                });

            migrationBuilder.CreateTable(
                name: "FlashSales",
                columns: table => new
                {
                    ID_FlashSale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price_FlashSale = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlashSales", x => x.ID_FlashSale);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID_Locations = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Locations = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID_Locations);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    ID_MaterialType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_MaterialType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.ID_MaterialType);
                });

            migrationBuilder.CreateTable(
                name: "Occasions",
                columns: table => new
                {
                    ID_Occasion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Occasion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occasions", x => x.ID_Occasion);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    ID_ProductType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.ID_ProductType);
                });

            migrationBuilder.CreateTable(
                name: "Vouchers",
                columns: table => new
                {
                    ID_Voucher = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Voucher_Quantity = table.Column<int>(type: "int", nullable: false),
                    MinimumAmount = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vouchers", x => x.ID_Voucher);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    ID_Post = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Category = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.ID_Post);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_ID_Category",
                        column: x => x.ID_Category,
                        principalTable: "Categories",
                        principalColumn: "ID_Category",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    ID_Shop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Shop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Shop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Shop = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Locations = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.ID_Shop);
                    table.ForeignKey(
                        name: "FK_Shops_Locations_ID_Locations",
                        column: x => x.ID_Locations,
                        principalTable: "Locations",
                        principalColumn: "ID_Locations",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID_Material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_MaterialType = table.Column<int>(type: "int", nullable: false),
                    Name_Material = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EXP_Material = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price_Material = table.Column<double>(type: "float", nullable: false),
                    Img_Material = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID_Material);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_ID_MaterialType",
                        column: x => x.ID_MaterialType,
                        principalTable: "MaterialTypes",
                        principalColumn: "ID_MaterialType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Occasion = table.Column<int>(type: "int", nullable: false),
                    Name_Product = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price_Product = table.Column<double>(type: "float", nullable: false),
                    Img_Product = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isAvailabled = table.Column<bool>(type: "bit", nullable: false),
                    isDiscontinued = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: true),
                    ID_FlashSale = table.Column<int>(type: "int", nullable: true),
                    ID_ProductType = table.Column<int>(type: "int", nullable: false),
                    size = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID_Product);
                    table.ForeignKey(
                        name: "FK_Products_FlashSales_ID_FlashSale",
                        column: x => x.ID_FlashSale,
                        principalTable: "FlashSales",
                        principalColumn: "ID_FlashSale");
                    table.ForeignKey(
                        name: "FK_Products_Occasions_ID_Occasion",
                        column: x => x.ID_Occasion,
                        principalTable: "Occasions",
                        principalColumn: "ID_Occasion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductTypes_ID_ProductType",
                        column: x => x.ID_ProductType,
                        principalTable: "ProductTypes",
                        principalColumn: "ID_ProductType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_CustomerType = table.Column<int>(type: "int", nullable: true),
                    ID_Shop = table.Column<int>(type: "int", nullable: true),
                    Spend = table.Column<double>(type: "float", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_CustomerTypes_ID_CustomerType",
                        column: x => x.ID_CustomerType,
                        principalTable: "CustomerTypes",
                        principalColumn: "ID_CustomerType");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Shops_ID_Shop",
                        column: x => x.ID_Shop,
                        principalTable: "Shops",
                        principalColumn: "ID_Shop");
                });

            migrationBuilder.CreateTable(
                name: "StockReceivedDockets",
                columns: table => new
                {
                    ID_StockReceivedDocket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Shop = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Received = table.Column<bool>(type: "bit", nullable: true),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceivedDockets", x => x.ID_StockReceivedDocket);
                    table.ForeignKey(
                        name: "FK_StockReceivedDockets_Shops_ID_Shop",
                        column: x => x.ID_Shop,
                        principalTable: "Shops",
                        principalColumn: "ID_Shop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialWarehouses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Shop = table.Column<int>(type: "int", nullable: false),
                    ID_Material = table.Column<int>(type: "int", nullable: false),
                    InStock_Quantity = table.Column<int>(type: "int", nullable: false),
                    Sold_Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MaterialWarehouses_Materials_ID_Material",
                        column: x => x.ID_Material,
                        principalTable: "Materials",
                        principalColumn: "ID_Material",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialWarehouses_Shops_ID_Shop",
                        column: x => x.ID_Shop,
                        principalTable: "Shops",
                        principalColumn: "ID_Shop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductWarehouses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Shop = table.Column<int>(type: "int", nullable: false),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    InStock_Quantity = table.Column<int>(type: "int", nullable: false),
                    Sold_Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductWarehouses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductWarehouses_Products_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductWarehouses_Shops_ID_Shop",
                        column: x => x.ID_Shop,
                        principalTable: "Shops",
                        principalColumn: "ID_Shop",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID_Recipe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    ID_Material = table.Column<int>(type: "int", nullable: false),
                    Material_Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID_Recipe);
                    table.ForeignKey(
                        name: "FK_Recipes_Materials_ID_Material",
                        column: x => x.ID_Material,
                        principalTable: "Materials",
                        principalColumn: "ID_Material",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipes_Products_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    ID_Bill = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Customer = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ID_Voucher = table.Column<int>(type: "int", nullable: true),
                    Total_Bill = table.Column<double>(type: "float", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BillStatus = table.Column<bool>(type: "bit", nullable: true),
                    DeliveredStatus = table.Column<bool>(type: "bit", nullable: true),
                    HandleStatus = table.Column<bool>(type: "bit", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_Order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Order = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_Shop = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID_Bill);
                    table.ForeignKey(
                        name: "FK_Bills_AspNetUsers_ID_Customer",
                        column: x => x.ID_Customer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bills_Shops_ID_Shop",
                        column: x => x.ID_Shop,
                        principalTable: "Shops",
                        principalColumn: "ID_Shop",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bills_Vouchers_ID_Voucher",
                        column: x => x.ID_Voucher,
                        principalTable: "Vouchers",
                        principalColumn: "ID_Voucher");
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    ID_Cart = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Customer = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.ID_Cart);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_ID_Customer",
                        column: x => x.ID_Customer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProducts",
                columns: table => new
                {
                    ID_FavoriteProduct = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Customer = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProducts", x => x.ID_FavoriteProduct);
                    table.ForeignKey(
                        name: "FK_FavoriteProducts_AspNetUsers_ID_Customer",
                        column: x => x.ID_Customer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManagerUserProducts",
                columns: table => new
                {
                    ID_MUP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Customer = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagerUserProducts", x => x.ID_MUP);
                    table.ForeignKey(
                        name: "FK_ManagerUserProducts_AspNetUsers_ID_Customer",
                        column: x => x.ID_Customer,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ManagerUserProducts_Products_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceivedDocketDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_StockReceivedDocket = table.Column<int>(type: "int", nullable: false),
                    ID_Material = table.Column<int>(type: "int", nullable: false),
                    StockReceived_Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceivedDocketDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockReceivedDocketDetails_Materials_ID_Material",
                        column: x => x.ID_Material,
                        principalTable: "Materials",
                        principalColumn: "ID_Material",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceivedDocketDetails_StockReceivedDockets_ID_StockReceivedDocket",
                        column: x => x.ID_StockReceivedDocket,
                        principalTable: "StockReceivedDockets",
                        principalColumn: "ID_StockReceivedDocket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Bill = table.Column<int>(type: "int", nullable: false),
                    ID_Product = table.Column<int>(type: "int", nullable: false),
                    Product_Quantity = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_ID_Bill",
                        column: x => x.ID_Bill,
                        principalTable: "Bills",
                        principalColumn: "ID_Bill",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_Cart = table.Column<int>(type: "int", nullable: false),
                    ID_Product = table.Column<int>(type: "int", nullable: true),
                    Product_Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CartDetails_Carts_ID_Cart",
                        column: x => x.ID_Cart,
                        principalTable: "Carts",
                        principalColumn: "ID_Cart",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetails_Products_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteProductDetails",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_FavoriteProduct = table.Column<int>(type: "int", nullable: false),
                    ID_Product = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteProductDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FavoriteProductDetails_FavoriteProducts_ID_FavoriteProduct",
                        column: x => x.ID_FavoriteProduct,
                        principalTable: "FavoriteProducts",
                        principalColumn: "ID_FavoriteProduct",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteProductDetails_Products_ID_Product",
                        column: x => x.ID_Product,
                        principalTable: "Products",
                        principalColumn: "ID_Product");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "718238ab-92a8-48b0-9e43-5ef0f43b218b", null, "Admin", "ADMIN" },
                    { "9939c409-25bc-459a-a91e-b00d2b153d23", null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "ID_CustomerType", "ID_Shop", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Spend", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "0f04181a-5b9d-4766-b503-041cf80209b9", 0, null, "0e65192a-21d1-4fb2-9b22-0df3c82e1abf", "user3@hotmail.com", true, null, null, null, null, false, null, "USER3@HOTMAIL.COM", "USER3@HOTMAIL.COM", "AQAAAAIAAYagAAAAEOQqFbLA6Yi85SyuYT73cYKidJFVfwwkb5bfDxyb3PhpaG1HSm80HPqxytOQ5M9yow==", null, false, "3fe5d61e-60c9-4c2d-88a8-a5f6ac79e7d3", null, false, "user3@hotmail.com" },
                    { "8b6df906-e206-4ab2-89b4-7b86be24dc5c", 0, null, "32a01024-ffe8-4ecb-8db6-e7d189267e2a", "user2@hotmail.com", true, null, null, null, null, false, null, "USER2@HOTMAIL.COM", "USER2@HOTMAIL.COM", "AQAAAAIAAYagAAAAEC6rmFrF+N8mja81314TANOsohrkh0mD13F7yrreq94HdsY4XuqOmkyoA0rMhrruSQ==", null, false, "db56a88d-7074-479d-b4ad-3b827bc823f6", null, false, "user2@hotmail.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "718238ab-92a8-48b0-9e43-5ef0f43b218b", "0f04181a-5b9d-4766-b503-041cf80209b9" },
                    { "9939c409-25bc-459a-a91e-b00d2b153d23", "8b6df906-e206-4ab2-89b4-7b86be24dc5c" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ID_CustomerType",
                table: "AspNetUsers",
                column: "ID_CustomerType");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ID_Shop",
                table: "AspNetUsers",
                column: "ID_Shop");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ID_Bill",
                table: "BillDetails",
                column: "ID_Bill");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ID_Product",
                table: "BillDetails",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ID_Customer",
                table: "Bills",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ID_Shop",
                table: "Bills",
                column: "ID_Shop");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_ID_Voucher",
                table: "Bills",
                column: "ID_Voucher");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ID_Cart",
                table: "CartDetails",
                column: "ID_Cart");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ID_Product",
                table: "CartDetails",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ID_Customer",
                table: "Carts",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProductDetails_ID_FavoriteProduct",
                table: "FavoriteProductDetails",
                column: "ID_FavoriteProduct");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProductDetails_ID_Product",
                table: "FavoriteProductDetails",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteProducts_ID_Customer",
                table: "FavoriteProducts",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerUserProducts_ID_Customer",
                table: "ManagerUserProducts",
                column: "ID_Customer");

            migrationBuilder.CreateIndex(
                name: "IX_ManagerUserProducts_ID_Product",
                table: "ManagerUserProducts",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ID_MaterialType",
                table: "Materials",
                column: "ID_MaterialType");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialWarehouses_ID_Material",
                table: "MaterialWarehouses",
                column: "ID_Material");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialWarehouses_ID_Shop",
                table: "MaterialWarehouses",
                column: "ID_Shop");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ID_Category",
                table: "Posts",
                column: "ID_Category");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID_FlashSale",
                table: "Products",
                column: "ID_FlashSale");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID_Occasion",
                table: "Products",
                column: "ID_Occasion");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID_ProductType",
                table: "Products",
                column: "ID_ProductType");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouses_ID_Product",
                table: "ProductWarehouses",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_ProductWarehouses_ID_Shop",
                table: "ProductWarehouses",
                column: "ID_Shop");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_Material",
                table: "Recipes",
                column: "ID_Material");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ID_Product",
                table: "Recipes",
                column: "ID_Product");

            migrationBuilder.CreateIndex(
                name: "IX_Shops_ID_Locations",
                table: "Shops",
                column: "ID_Locations");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedDocketDetails_ID_Material",
                table: "StockReceivedDocketDetails",
                column: "ID_Material");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedDocketDetails_ID_StockReceivedDocket",
                table: "StockReceivedDocketDetails",
                column: "ID_StockReceivedDocket");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceivedDockets_ID_Shop",
                table: "StockReceivedDockets",
                column: "ID_Shop");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "FavoriteProductDetails");

            migrationBuilder.DropTable(
                name: "ManagerUserProducts");

            migrationBuilder.DropTable(
                name: "MaterialWarehouses");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ProductWarehouses");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "StockReceivedDocketDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "FavoriteProducts");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "StockReceivedDockets");

            migrationBuilder.DropTable(
                name: "Vouchers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FlashSales");

            migrationBuilder.DropTable(
                name: "Occasions");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "CustomerTypes");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
