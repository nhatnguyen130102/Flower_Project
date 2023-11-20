using Flower_Models;
using Flower_Repository;
using FlowerShop_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Flower_ViewModels;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Net;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FlowerShop_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public IActionResult Index(string? searching, string? id)
        {
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
            product.ViewCount++;
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
                    _context.SaveChanges();
                }
            }
            return View(product);
        }
    }
}