using Flower_Models;
using Flower_Repository;
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

    public class BillAminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BillAminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");
            var list = await _context.Bills.ToListAsync();  
            return View(list);
        }

        public async Task<IActionResult> filter(int? id)
        {
            var list = await _context.Bills.Where(x=>x.ID_Shop ==id).ToListAsync();
            return RedirectToAction("Index", "BillAdmin", list);
        }
    }
}
