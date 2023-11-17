using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Manager")]
    public class ProductWarehouseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ProductWarehouseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

      
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            if (user.ID_Shop == null)
            {
                return NotFound();
            }
            var item = await _context.ProductWarehouses.Where(x => x.ID_Shop == user.ID_Shop).ToListAsync();
            return View(item);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductWarehouse model)
        {
            if (ModelState.IsValid)
            {
                await _context.ProductWarehouses.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.ProductWarehouses.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductWarehouse model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new ProductWarehouse()
                    {
                        ID = model.ID,
                        ID_Shop = model.ID_Shop,
                        ID_Product = model.ID_Product,
                        InStock_Quantity = model.InStock_Quantity,
                        Sold_Quantity = model.Sold_Quantity,
                    };
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var item = await _context.ProductWarehouses.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.ProductWarehouses.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var item = await _context.ProductWarehouses.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.ProductWarehouses.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
