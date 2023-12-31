USE [Db_FlowerShop_Web]
GO
SET IDENTITY_INSERT [dbo].[Locations] ON 

INSERT [dbo].[Locations] ([ID_Locations], [Name_Locations]) VALUES (1, N'Ho Chi Minh')
INSERT [dbo].[Locations] ([ID_Locations], [Name_Locations]) VALUES (2, N'Ha Noi')
INSERT [dbo].[Locations] ([ID_Locations], [Name_Locations]) VALUES (3, N'Thua Thien Hue')
INSERT [dbo].[Locations] ([ID_Locations], [Name_Locations]) VALUES (4, N'Vung Tau')
SET IDENTITY_INSERT [dbo].[Locations] OFF
GO
SET IDENTITY_INSERT [dbo].[Shops] ON 

INSERT [dbo].[Shops] ([ID_Shop], [Name_Shop], [Address_Shop], [Phone_Shop], [ID_Locations]) VALUES (1, N'Chi nhánh 1', N'268 Đ Tô Hiến Thành, Phường 15, Quận 10, Thành phố Hồ Chí Minh, Vietnam', N'02838632990 ', 1)
INSERT [dbo].[Shops] ([ID_Shop], [Name_Shop], [Address_Shop], [Phone_Shop], [ID_Locations]) VALUES (2, N'Chi nhánh 2', N'Đ. TL 08, Thạnh Lộc, Quận 12, Thành phố Hồ Chí Minh 71515, Vietnam', N'0789893955  ', 1)
INSERT [dbo].[Shops] ([ID_Shop], [Name_Shop], [Address_Shop], [Phone_Shop], [ID_Locations]) VALUES (3, N'Chi nhánh 3', N'11 P. Lê Phụng Hiểu, French Quarter, Hoàn Kiếm, Hà Nội 100000, Vietnam', N'02439878888 ', 2)
SET IDENTITY_INSERT [dbo].[Shops] OFF
GO
SET IDENTITY_INSERT [dbo].[StockReceivedDockets] ON 

INSERT [dbo].[StockReceivedDockets] ([ID_StockReceivedDocket], [ID_Shop], [CreatedAt], [IsActive], [Received], [ReceivedAt]) VALUES (35, 1, CAST(N'2023-11-14T16:17:39.2590305' AS DateTime2), 1, 1, CAST(N'2023-11-14T16:17:41.1831069' AS DateTime2))
INSERT [dbo].[StockReceivedDockets] ([ID_StockReceivedDocket], [ID_Shop], [CreatedAt], [IsActive], [Received], [ReceivedAt]) VALUES (36, 1, CAST(N'2023-11-14T16:18:06.0678058' AS DateTime2), 1, 1, CAST(N'2023-11-14T16:18:07.2387556' AS DateTime2))
INSERT [dbo].[StockReceivedDockets] ([ID_StockReceivedDocket], [ID_Shop], [CreatedAt], [IsActive], [Received], [ReceivedAt]) VALUES (37, 1, CAST(N'2023-11-15T09:55:08.7259235' AS DateTime2), 1, 1, CAST(N'2023-11-15T09:55:10.6682648' AS DateTime2))
INSERT [dbo].[StockReceivedDockets] ([ID_StockReceivedDocket], [ID_Shop], [CreatedAt], [IsActive], [Received], [ReceivedAt]) VALUES (38, 1, NULL, 0, 0, NULL)
INSERT [dbo].[StockReceivedDockets] ([ID_StockReceivedDocket], [ID_Shop], [CreatedAt], [IsActive], [Received], [ReceivedAt]) VALUES (39, 1, CAST(N'2023-11-15T11:29:55.8673784' AS DateTime2), 1, 1, CAST(N'2023-11-15T11:30:16.8179984' AS DateTime2))
SET IDENTITY_INSERT [dbo].[StockReceivedDockets] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialTypes] ON 

INSERT [dbo].[MaterialTypes] ([ID_MaterialType], [Name_MaterialType]) VALUES (3, N'Flower')
INSERT [dbo].[MaterialTypes] ([ID_MaterialType], [Name_MaterialType]) VALUES (4, N'Plant')
SET IDENTITY_INSERT [dbo].[MaterialTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (1, 3, N'White roses', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\White roses\White roses.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (2, 3, N'Yellow roses', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Yellow roses\Yellow roses.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (3, 3, N'Pink roses', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Pink roses\Pink roses.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (4, 3, N'Red roses', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Red roses\Red roses.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (5, 3, N'White lilies', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\White lilies\White lilies.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (6, 3, N'Calla lilies', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Calla lilies\Calla lilies.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (7, 3, N'Pink lilies', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Pink lilies\Pink lilies.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (8, 3, N'White chrysanthemums', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\White chrysanthemums\White chrysanthemums.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (9, 3, N'Pink chrysanthemums', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Pink chrysanthemums\Pink chrysanthemums.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (10, 3, N'Red chrysanthemums', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Red chrysanthemums\Red chrysanthemums.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (11, 3, N'Green chrysanthemums', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Green chrysanthemums\Green chrysanthemums.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (12, 3, N'White alstroemerias', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\White alstroemerias\White alstroemerias.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (13, 3, N'Purple alstroemerias', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Purple alstroemerias\Purple alstroemerias.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (14, 3, N'Pink germini', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Pink germini\Pink germini.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (15, 3, N'Orange germini', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Orange germini\Orange germini.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (16, 3, N'White statice', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\White statice\White statice.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (17, 3, N'Pink anastasias', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Pink anastasias\Pink anastasias.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (18, 3, N'Goldenrod', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Goldenrod\Goldenrod.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (19, 3, N'Gypsophila', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Gypsophila\Gypsophila.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (20, 3, N'Veronica', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Veronica\Veronica.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (21, 3, N'Waxflower', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Waxflower\Waxflower.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (22, 4, N'Aspidistra', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Aspidistra\Aspidistra.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (23, 4, N'Decorative greenery', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Decorative greenery\Decorative greenery.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (24, 4, N'Hypericum', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Hypericum\Hypericum.jpg')
INSERT [dbo].[Materials] ([ID_Material], [ID_MaterialType], [Name_Material], [ImportAt], [EXP_Material], [Price_Material], [Img_Material]) VALUES (25, 4, N'Salix', CAST(N'2023-11-11T11:30:03.5900000' AS DateTime2), CAST(N'2023-11-15T00:00:00.0000000' AS DateTime2), 2, N'images\materials\Salix\Salix.jpg')
SET IDENTITY_INSERT [dbo].[Materials] OFF
GO
SET IDENTITY_INSERT [dbo].[StockReceivedDocketDetails] ON 

INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (398, 35, 1, 100)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (399, 35, 2, 100)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (400, 35, 3, 100)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (401, 36, 1, 99)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (402, 37, 4, 100)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (403, 37, 18, 200)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (404, 38, 4, 2)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (405, 38, 5, 3)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (406, 39, 2, 2000)
INSERT [dbo].[StockReceivedDocketDetails] ([ID], [ID_StockReceivedDocket], [ID_Material], [StockReceived_Quantity]) VALUES (407, 39, 16, 2000)
SET IDENTITY_INSERT [dbo].[StockReceivedDocketDetails] OFF
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'355e7f1c-b691-4d2b-8ba5-95557d87add7', N'User', N'USER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'affc9d0e-3818-4dd5-af30-2eddc5134e08', N'Shop_2', N'SHOP_2', N'30dfb635-3721-4bdf-99e5-d0e963ca38d3')
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'c305469a-90ce-43a1-85dd-ae06635abbff', N'Admin', N'ADMIN', NULL)
GO
SET IDENTITY_INSERT [dbo].[CustomerTypes] ON 

INSERT [dbo].[CustomerTypes] ([ID_CustomerType], [Name_CustomerType], [MinSpend], [MaxSpend], [Discount]) VALUES (1, N'GOLD', 200, 400, 10)
INSERT [dbo].[CustomerTypes] ([ID_CustomerType], [Name_CustomerType], [MinSpend], [MaxSpend], [Discount]) VALUES (2, N'VIP', 500, 700, 15)
SET IDENTITY_INSERT [dbo].[CustomerTypes] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [LastName], [FirstName], [Address], [ID_CustomerType], [Spend], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [ID_Shop]) VALUES (N'2d3f7b27-0352-4776-a2b0-49613affd353', N'TestRole1234@gmail.com', N'TestRole1234@gmail.com', N'TestRole1234@gmail.com', NULL, 113.7, N'TestRole1234@gmail.com', N'TESTROLE1234@GMAIL.COM', N'TestRole1234@gmail.com', N'TESTROLE1234@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEFOtQY6eGGhizxLIUgZpokiiFf8BvbH88l0DoNp1ccIfwdv3+qaW09iPKLqjQKOSug==', N'GLSQD4DRW4G5LVFZEZJRZMEAOPX4TIB2', N'f9d9eb7a-b742-4937-9d5a-47f03ba6dbd6', NULL, 0, 0, NULL, 1, 0, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [LastName], [FirstName], [Address], [ID_CustomerType], [Spend], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [ID_Shop]) VALUES (N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, NULL, NULL, 2, 533.86, N'user2@hotmail.com', N'USER2@HOTMAIL.COM', N'user2@hotmail.com', N'USER2@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPGNIvfWbo6xOPCGHA1e1dI7HiYAHQsVU+KmDKvOsxUaavnwQV1/HAngU2rVoh5J1g==', N'10126690-8bfd-421f-84d9-a65087c641ef', N'752f6cf2-fe98-460b-88b5-0079d9e0df12', NULL, 0, 0, NULL, 0, 0, NULL)
INSERT [dbo].[AspNetUsers] ([Id], [LastName], [FirstName], [Address], [ID_CustomerType], [Spend], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [ID_Shop]) VALUES (N'b55dec88-c9df-4d6f-92c5-cdcbe61e1912', NULL, NULL, NULL, 1, 322.2, N'user3@hotmail.com', N'USER3@HOTMAIL.COM', N'user3@hotmail.com', N'USER3@HOTMAIL.COM', 1, N'AQAAAAIAAYagAAAAELBUxy66f0HLB1YZEnISxIgJ0J6AFrIXqXILjXZjnQ1D/XQXsym4cHbCOVDGWAwy9w==', N'8e7f20cc-424b-4a2a-900b-eea81d3ca502', N'd8deb1c9-a6ae-4155-a47b-b594fdadb329', NULL, 0, 0, NULL, 0, 0, 1)
INSERT [dbo].[AspNetUsers] ([Id], [LastName], [FirstName], [Address], [ID_CustomerType], [Spend], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [ID_Shop]) VALUES (N'df9d6e3e-f612-4f90-8d77-dce9ac3fd4c2', N'TestCreate1234@gmail.com', N'TestCreate1234@gmail.com', N'TestCreate1234@gmail.com', NULL, NULL, N'TestCreate1234@gmail.com', N'TESTCREATE1234@GMAIL.COM', N'TestCreate1234@gmail.com', N'TESTCREATE1234@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEB3tSVjRTGSlrYcteu2Ya+T7I/0XRadzr325t5IG1KrPmjdD2xHmUgq419fJMNv9DQ==', N'IW7Y26LY5HSU2LWVGMW5XYZKI4JN73L5', N'3f9da856-d80c-4689-8268-dc4ac9ac8c4a', NULL, 0, 0, NULL, 1, 0, NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', N'355e7f1c-b691-4d2b-8ba5-95557d87add7')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b55dec88-c9df-4d6f-92c5-cdcbe61e1912', N'c305469a-90ce-43a1-85dd-ae06635abbff')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (1, N'Flash Sale')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (2, N'Collection')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (3, N'Branding')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (4, N'Party')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (5, N'Gift')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (6, N'Preference')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (7, N'Event')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (8, N'Woman')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (9, N'Man')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (10, N'Couples')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (11, N'Daily')
INSERT [dbo].[Categories] ([ID_Category], [Name_Category]) VALUES (12, N'Alert')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Posts] ON 

INSERT [dbo].[Posts] ([ID_Post], [ID_Category], [Title], [Content], [CreatedAt], [CreatedBy], [ViewCount]) VALUES (1, 7, N'Don''t Miss Out on These Fall/Autumn Flash Sales That Can Save You a Bundle on Fresh Flowers and More', N'Don''t let the high expenses of back- to-school and holiday shopping ruin your budget. Fall is the season of savings at online retailers, offering seasonal deals on everything from fresh flowers and fall home décor to cozy sweaters and fall-themed treats. Embrace the beauty of fall while saving money with these four amazing fall-themed flash sales. Let''s explore the discounts and deals to upgrade your fall style and lifestyle. Happy shopping!', CAST(N'2023-10-24T00:00:00.0000000' AS DateTime2), N'Thao Tran Hien Thao', NULL)
INSERT [dbo].[Posts] ([ID_Post], [ID_Category], [Title], [Content], [CreatedAt], [CreatedBy], [ViewCount]) VALUES (2, 9, N'Valentine''s Day Bouquet at a Huge Discount', N'Valentine''s Day is the day of love and romance. It''s also a time to show your love for that special someone with a beautiful bouquet of flowers. But why pay full price for a bouquet that will only last a few days? That''s where online flower bouquet flash sales come in.

Online flower retailers often offer flash sales on their flower bouquets around Valentine''s Day, making it the perfect time to stock up on your favorite blooms at a discounted rate. Not only do flash sales offer a great deal on flowers, but they can also provide you with the opportunity to try out new bouquets and expand your floral horizon.', CAST(N'2023-10-24T00:00:00.0000000' AS DateTime2), N'Thao Tran Hien', NULL)
SET IDENTITY_INSERT [dbo].[Posts] OFF
GO
SET IDENTITY_INSERT [dbo].[Occasions] ON 

INSERT [dbo].[Occasions] ([ID_Occasion], [Name_Occasion]) VALUES (11, N'Christmas')
INSERT [dbo].[Occasions] ([ID_Occasion], [Name_Occasion]) VALUES (12, N'Birthday')
INSERT [dbo].[Occasions] ([ID_Occasion], [Name_Occasion]) VALUES (13, N'Love & Romance')
INSERT [dbo].[Occasions] ([ID_Occasion], [Name_Occasion]) VALUES (14, N'New Baby')
INSERT [dbo].[Occasions] ([ID_Occasion], [Name_Occasion]) VALUES (15, N'Thinking of you')
INSERT [dbo].[Occasions] ([ID_Occasion], [Name_Occasion]) VALUES (16, N'Congratulations')
SET IDENTITY_INSERT [dbo].[Occasions] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductTypes] ON 

INSERT [dbo].[ProductTypes] ([ID_ProductType], [Name_ProductType]) VALUES (4, N'Flower Basket')
INSERT [dbo].[ProductTypes] ([ID_ProductType], [Name_ProductType]) VALUES (5, N'Bouquet')
SET IDENTITY_INSERT [dbo].[ProductTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (29, 12, N'Subtle Freshness', 43.9, N'images\products\Subtle Freshness\Subtle Freshness.jpg', 1, 0, CAST(N'2023-11-10T19:19:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (32, 12, N'Classic Love', 54.9, N'images\products\Classic Love\Classic Love.jpg', 1, 0, CAST(N'2023-11-10T19:23:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (33, 12, N'Aurore', 49.9, N'images\products\Aurore\Aurore.jpg', 1, 0, CAST(N'2023-11-10T19:24:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (34, 13, N'Infinite Strength', 49.9, N'images\products\Infinite Strength\Infinite Strength.jpg', 1, 0, CAST(N'2023-11-10T19:27:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (35, 13, N'Dressed in Pink', 43.9, N'images\products\Dressed in Pink\Dressed in Pink.jpg', 1, 0, CAST(N'2023-11-10T19:31:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (36, 13, N'More Than Words', 38.9, N'images\products\More Than Words\More Than Words.jpg', 1, 0, CAST(N'2023-11-10T19:32:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (37, 14, N'Floral Energy', 49.9, N'images\products\Floral Energy\Floral Energy.jpg', 1, 0, CAST(N'2023-11-10T19:34:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'0')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (38, 14, N'Pink Bloom', 65.9, N'images\products\Pink Bloom\Pink Bloom.jpg', 1, 0, CAST(N'2023-11-11T10:20:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'Classic')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (39, 14, N'Ice Cream', 65.9, N'images\products\Ice Cream\Ice Cream.jpg', 1, 0, CAST(N'2023-11-11T10:23:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'Classic')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (40, 15, N'Always Near', 65.9, N'images\products\Always Near\Always Near.jpg', 1, 0, CAST(N'2023-11-11T10:27:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'Classic')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (41, 15, N'Gentle Harmony', 65.9, N'images\products\Gentle Harmony\Gentle Harmony.jpg', 1, 0, CAST(N'2023-11-11T10:29:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'Classic')
INSERT [dbo].[Products] ([ID_Product], [ID_Occasion], [Name_Product], [Price_Product], [Img_Product], [isAvailabled], [isDiscontinued], [CreatedAt], [Rating], [ViewCount], [ID_FlashSale], [ID_ProductType], [size]) VALUES (42, 15, N'Romantic Date', 43.9, N'images\products\Romantic Date\Romantic Date.jpg', 1, 0, CAST(N'2023-11-11T10:31:00.0000000' AS DateTime2), NULL, NULL, NULL, 4, N'Classic')
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Vouchers] ON 

INSERT [dbo].[Vouchers] ([ID_Voucher], [Code], [Type], [Discount], [StartedAt], [EndedAt], [Voucher_Quantity], [MinimumAmount], [IsActive]) VALUES (1, N'Halloween 2023', N'0', 10, CAST(N'2023-11-06T11:11:00.0000000' AS DateTime2), CAST(N'2023-11-30T11:11:00.0000000' AS DateTime2), 100, 100, 1)
INSERT [dbo].[Vouchers] ([ID_Voucher], [Code], [Type], [Discount], [StartedAt], [EndedAt], [Voucher_Quantity], [MinimumAmount], [IsActive]) VALUES (2, N'Halloween 2024', N'0', 10, CAST(N'2023-11-07T11:12:00.0000000' AS DateTime2), CAST(N'2023-11-09T11:12:00.0000000' AS DateTime2), 100, 100, 1)
INSERT [dbo].[Vouchers] ([ID_Voucher], [Code], [Type], [Discount], [StartedAt], [EndedAt], [Voucher_Quantity], [MinimumAmount], [IsActive]) VALUES (3, N'Halloween2025', N'0', 10, CAST(N'2023-11-06T11:11:00.0000000' AS DateTime2), CAST(N'2023-11-30T11:11:00.0000000' AS DateTime2), 100, 100, 1)
INSERT [dbo].[Vouchers] ([ID_Voucher], [Code], [Type], [Discount], [StartedAt], [EndedAt], [Voucher_Quantity], [MinimumAmount], [IsActive]) VALUES (4, N'Halloween2026', N'0', 10, CAST(N'2023-11-07T11:12:00.0000000' AS DateTime2), CAST(N'2023-11-09T11:12:00.0000000' AS DateTime2), 98, 100, 1)
SET IDENTITY_INSERT [dbo].[Vouchers] OFF
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (15, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 58.8, 49.9, CAST(N'2023-11-14T02:07:01.2974663' AS DateTime2), CAST(N'2023-11-16T02:06:00.0000000' AS DateTime2), 1, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'0936658881', 1, N'1', N'52/164', NULL, 0, 1, N'xyz', N'abc', N'bca')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (16, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 47.8, 38.9, CAST(N'2023-11-14T02:31:19.8546130' AS DateTime2), CAST(N'2023-11-17T02:31:00.0000000' AS DateTime2), 1, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'0936658881', 1, N'1', N'52/164', NULL, 0, 1, N'xyz', N'abc', N'bca')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (17, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 108.7, 99.8, CAST(N'2023-11-14T18:57:11.6803156' AS DateTime2), CAST(N'2023-11-16T18:56:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'0936658881', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (18, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 8.9, 0, CAST(N'2023-11-14T19:23:24.9669943' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'0936658881', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (19, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 202.04, 193.14, CAST(N'2023-11-15T09:49:58.4984747' AS DateTime2), CAST(N'2023-11-17T09:49:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'0936658881', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (20, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 53.809999999999995, 44.91, CAST(N'2023-11-15T09:50:50.9481590' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'0936658881', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (21, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd', NULL, 53.809999999999995, 44.91, CAST(N'2023-11-15T11:34:12.2893045' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'+84834565544', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (22, N'b55dec88-c9df-4d6f-92c5-cdcbe61e1912', NULL, 163.6, 154.7, CAST(N'2023-11-15T11:36:12.2711051' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'+84834565544', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
INSERT [dbo].[Bills] ([ID_Bill], [ID_Customer], [ID_Voucher], [Total_Bill], [Subtotal], [CreatedAt], [DeliveredAt], [BillStatus], [Name], [Phone], [Name_Order], [Phone_Order], [ID_Shop], [City], [Address], [Message], [DeliveredStatus], [HandleStatus], [District], [Street], [Ward]) VALUES (23, N'b55dec88-c9df-4d6f-92c5-cdcbe61e1912', NULL, 158.6, 149.7, CAST(N'2023-11-15T11:36:42.3112260' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), 0, N'NGUYEN QUANG NHAT', N'+84834565544', N'NGUYEN QUANG NHAT', N'+84834565544', 1, N'1', N'77/13/11', NULL, 0, 0, N'Bình Tân', N'Phạm Đăng Giảng', N'Bình Hưng Hoà')
SET IDENTITY_INSERT [dbo].[Bills] OFF
GO
SET IDENTITY_INSERT [dbo].[Carts] ON 

INSERT [dbo].[Carts] ([ID_Cart], [ID_Customer]) VALUES (6, N'6ecaeb25-5ac3-4614-90f6-2d836de08edd')
INSERT [dbo].[Carts] ([ID_Cart], [ID_Customer]) VALUES (7, N'b55dec88-c9df-4d6f-92c5-cdcbe61e1912')
SET IDENTITY_INSERT [dbo].[Carts] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialWarehouses] ON 

INSERT [dbo].[MaterialWarehouses] ([ID], [ID_Shop], [ID_Material], [InStock_Quantity], [Sold_Quantity]) VALUES (159, 1, 1, 199, 0)
INSERT [dbo].[MaterialWarehouses] ([ID], [ID_Shop], [ID_Material], [InStock_Quantity], [Sold_Quantity]) VALUES (160, 1, 2, 2100, 0)
INSERT [dbo].[MaterialWarehouses] ([ID], [ID_Shop], [ID_Material], [InStock_Quantity], [Sold_Quantity]) VALUES (161, 1, 3, 100, 0)
INSERT [dbo].[MaterialWarehouses] ([ID], [ID_Shop], [ID_Material], [InStock_Quantity], [Sold_Quantity]) VALUES (162, 1, 4, 100, 0)
INSERT [dbo].[MaterialWarehouses] ([ID], [ID_Shop], [ID_Material], [InStock_Quantity], [Sold_Quantity]) VALUES (163, 1, 18, 200, 0)
INSERT [dbo].[MaterialWarehouses] ([ID], [ID_Shop], [ID_Material], [InStock_Quantity], [Sold_Quantity]) VALUES (164, 1, 16, 2000, 0)
SET IDENTITY_INSERT [dbo].[MaterialWarehouses] OFF
GO
SET IDENTITY_INSERT [dbo].[Recipes] ON 

INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (24, 33, 2, 2)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (25, 33, 1, 2)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (26, 36, 4, 2)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (27, 36, 5, 3)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (32, 33, 10, 3)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (33, 33, 18, 1)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (44, 29, 16, 3)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (45, 29, 18, 2)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (46, 29, 17, 3)
INSERT [dbo].[Recipes] ([ID_Recipe], [ID_Product], [ID_Material], [Material_Quantity]) VALUES (47, 32, 4, 4)
SET IDENTITY_INSERT [dbo].[Recipes] OFF
GO
SET IDENTITY_INSERT [dbo].[CartDetails] ON 

INSERT [dbo].[CartDetails] ([ID], [ID_Cart], [ID_Product], [Product_Quantity]) VALUES (32, 6, 38, 4)
INSERT [dbo].[CartDetails] ([ID], [ID_Cart], [ID_Product], [Product_Quantity]) VALUES (35, 7, 33, 1)
SET IDENTITY_INSERT [dbo].[CartDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[BillDetails] ON 

INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (20, 15, 33, 1, 49.9)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (21, 16, 36, 1, 38.9)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (22, 17, 33, 2, 99.8)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (23, 19, 32, 3, 164.7)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (24, 19, 33, 1, 49.9)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (25, 20, 33, 1, 49.9)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (26, 21, 33, 1, 49.9)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (27, 22, 33, 2, 99.8)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (28, 22, 32, 1, 54.9)
INSERT [dbo].[BillDetails] ([ID], [ID_Bill], [ID_Product], [Product_Quantity], [Total]) VALUES (29, 23, 33, 3, 149.7)
SET IDENTITY_INSERT [dbo].[BillDetails] OFF
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231106073602_db_1', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231107075713_db_2', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231107193357_db_3', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231109100241_db_4', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231112061629_add-ID_Shop-ApplicationUser', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231112070119_updatepassword', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231113051345_update-address', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231113054529_update-SRD', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231113071044_updateSRD_Received', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231113071505_updateSRD_ReceivedAT', N'7.0.13')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231113182544_DeliveredAt_Not_Null', N'7.0.13')
GO
