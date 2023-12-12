using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Net;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;
using FlowerShop_Web.Models.DesignPattern;

namespace FlowerShop_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly FlowerShop flowerShop;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, FlowerShop flowerShop)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            this.flowerShop = flowerShop;
        }


        public async Task<IActionResult> Index(string? searching, string? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                TempData["UserId"] = user.Id;
                if (user != null)
                {
                    var getFavorite = await _context.FavoriteProducts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();
                    if (getFavorite != null)
                    {
                        var getItem = _context.Products.Where(x => x.isDiscontinued == true && x.ID_FlashSale != null).ToList();

                        var getFavoriteDetail = await _context.FavoriteProductDetails.Where(x => x.ID_FavoriteProduct == getFavorite.ID_FavoriteProduct).ToListAsync();
                        //______________________________________________________________________

                        var getFlashSale2 = await _context.Products.Where(x => x.ID_FlashSale != null && x.isDiscontinued == true).FirstOrDefaultAsync();
                        var getFlashSale = await _context.Products.Where(x => x.ID_FlashSale != null && x.isDiscontinued == true).ToListAsync();


                        if (getFlashSale2 != null)
                        {
                            string getPro = "";
                            foreach (var item2 in getFlashSale)
                            {
                                foreach (var item3 in getFavoriteDetail)
                                {
                                    if (item2.ID_Product == item3.ID_Product)
                                    {
                                        getPro += item2.Name_Product + " | ";
                                    }
                                }
                            }

                            Customer customer2 = new Customer();
                            customer2.id = user.Id;
                            customer2.lastName = user.LastName;
                            customer2.firstName = user.FirstName;
                            customer2.messages = getPro;

                            flowerShop.RegisterObserver(customer2);

                            flowerShop.NotifyObservers($"Xin chào {customer2.lastName} {customer2.firstName} có vài sản phẩm trong mục yêu thích đang được giảm giá: {customer2.messages} Bạn có muốn xem thử");

                            string item = flowerShop.getMessages();

                            if(getPro != null && getPro != "")
                            {
                                TempData["Notification"] = item;
                            }

                            
                        }
                    }
                }
            }

            //_______________________________________________
            int? id_occation = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;
            var query = _context.Products.Select(model => new Product()
            {
                ID_Product = model.ID_Product,
                ID_Occasion = model.ID_Occasion,
                Name_Product = model.Name_Product,
                Price_Product = model.Price_Product,
                Img_Product = model.Img_Product,
                isAvailabled = model.isAvailabled,
                isDiscontinued = model.isDiscontinued,
                CreatedAt = model.CreatedAt,
                Rating = model.Rating,
                ViewCount = model.ViewCount,
                ID_FlashSale = model.ID_FlashSale,
                ID_ProductType = model.ID_ProductType,
                size = model.size,
            });



            //var list = _context.Products.Include(x => x.FlashSale).Include(x => x.ProductType).Include(x => x.Occasion).ToList();
            if (_context.Occasions.Any())
            {
                ViewBag.Occasion = new SelectList(_context.Occasions, "ID_Occasion", "Name_Occasion");
            }
            if (_context.ProductTypes.Any())
            {
                ViewBag.ProductType = new SelectList(_context.ProductTypes, "ID_ProductType", "Name_ProductType");
            }

            if (id_occation.HasValue)
            {
                query = query.Where(b => b.ID_Occasion == id_occation);
            }

            if (!string.IsNullOrEmpty(searching))
            {
                query = query.Where(b => b.Name_Product.Contains(searching));
            }
            var model = query.ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpGet]
        public async Task<IActionResult> ProductDetails(int? id)
        {

            ViewBag.Location = new SelectList(_context.Locations, "ID_Locations", "Name_Locations").ToList();

            var product = await _context.Products.FindAsync(id);

            if (ViewBag.ID_Location != null)
            {
                int id_Location = Convert.ToInt32(ViewBag.ID_Location);

                var wareHouse = _context.ProductWarehouses.Where(x => x.Shop.ID_Locations == id_Location);

                //var quantity = wareHouse.

            }
            if (product == null)
            {
                return NotFound();
            }
            TempData["ProductId"] = id;
            var flashsale = await _context.FlashSales.FindAsync(product.ID_FlashSale);
            if (flashsale != null)
            {
                ViewBag.FlashSale = flashsale.Price_FlashSale;
            }
            else
            {
                ViewBag.FlashSale = null;
            }
            // đếm lượt xem sản phẩm
            if (product.ViewCount == null)
            {
                product.ViewCount = 1;
            }
            else 
                product.ViewCount++;
            _context.Products.Update(product);
            _context.SaveChanges();
            // Đếm lượt xem sản phẩm khi user login
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var MUP = _context.ManagerUserProducts.FirstOrDefault(x => x.ID_Customer == user.Id
                && x.ID_Product == id);
                if (MUP != null)
                {
                    MUP.ViewCount++;
                    _context.ManagerUserProducts.Update(MUP);
                    _context.SaveChanges();
                } else
                {
                    ManagerUserProduct newMUP = new ManagerUserProduct()
                    {
                        ID_Customer = user.Id,
                        ID_Product = (int)id,
                        ViewCount = 1,
                    };
                    _context.ManagerUserProducts.Add(newMUP);
                    _context.SaveChanges();
                }
            }
            // recommend Product 
            var currentProduct = _context.Products.Find(id);

            var recommendedProducts = _context.Products
            .Where(p =>
                p.ID_ProductType == currentProduct.ID_ProductType &&
                p.ID_Occasion == currentProduct.ID_Occasion &&
                p.size == currentProduct.size &&
                p.ID_Product != id && // Loại trừ sản phẩm hiện tại
                p.isAvailabled &&
                !p.isDiscontinued)
            .OrderByDescending(p => p.Rating) // Sắp xếp theo Rating giảm dần
            .Take(5) // Giới hạn số lượng sản phẩm được đề xuất
            .ToList();
            // Lấy danh sách 2 sản phẩm được bán chạy nhất trong 30 ngày gần nhất
            var topSellingProducts = _context.BillDetails
                .Where(bd => bd.Bill.CreatedAt >= DateTime.Now.AddDays(-30))
                .GroupBy(bd => bd.ID_Product)
                .OrderByDescending(group => group.Sum(bd => bd.Product_Quantity))
                .Take(5)
                .Select(group => group.Key)
                .ToList();

            // Lấy thông tin chi tiết của các sản phẩm từ bảng Product
            var topSellproduct = _context.Products
                .Where(p => topSellingProducts.Contains(p.ID_Product))
                .ToList();
            // Hợp nhất danh sách sản phẩm được đề xuất và danh sách sản phẩm bán chạy nhất
            recommendedProducts = recommendedProducts.Union(topSellproduct).Take(4).ToList();
            ViewBag.recommendProduct = recommendedProducts;
            return View(product);
        }

        [HttpPost]
        public IActionResult SubmitFeedback(Feedback feedback)
        {
            if (TempData.TryGetValue("ProductId", out var productId) && TempData.TryGetValue("UserId", out var idUser))
            {
                Feedback newFeedback = new Feedback()
                {
                    FeedbackId = feedback.FeedbackId,
                    Content = feedback.Content,
                    CreatedAt = DateTime.Now,
                    UserId = idUser.ToString(),
                    ProductId = int.Parse(productId.ToString()),
                    Rate = feedback.Rate,

                };
                _context.Feedbacks.Add(newFeedback);
                _context.SaveChanges();
                TempData.Remove("ProductId");
                return RedirectToAction("ProductDetails", new { id = productId });
            }
                /// Lưu DB
                return RedirectToAction("Index");
        }
    }
}