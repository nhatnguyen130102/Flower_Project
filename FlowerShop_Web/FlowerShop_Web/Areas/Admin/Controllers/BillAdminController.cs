using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class BillAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillAdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string? searching, string? id)
        {
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");

            int? shopId = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;

            var query = _context.Bills.Select(model => new BillAdminVM
            {
                ID_Bill = model.ID_Bill,
                ID_Shop = model.ID_Shop,
                ID_Customer = model.ID_Customer,
                ID_Voucher = model.ID_Voucher,
                Total_Bill = model.Total_Bill,
                CreatedAt = model.CreatedAt,
                DeliveredAt = model.DeliveredAt,
                BillStatus = model.BillStatus,
                Name = model.Name,
                Phone = model.Phone,
                Message = model.Message,
                Address = model.Address,
            });

          

            if (shopId.HasValue)
            {
                query = query.Where(b => b.ID_Shop == shopId);

                var shop = await _context.Shops.Where(x => x.ID_Shop == shopId).FirstOrDefaultAsync();

                ViewBag.SelectedShop = shop.Name_Shop;
            }

            if (!string.IsNullOrEmpty(searching))
            {
                query = query.Where(b => b.Name.Contains(searching) || b.Address.Contains(searching));
            }

            var model = query.ToList();
            return View(model);
        }
    }
}
