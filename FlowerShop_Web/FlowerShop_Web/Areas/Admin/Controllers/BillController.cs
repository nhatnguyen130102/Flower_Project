using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Manager")]

    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public BillController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // Xem danh sách Bill của cửa hàng
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Home", new { area = "" });

            if (user.ID_Shop == null)
            {
                return NotFound();
            }
            var list = await _context.Bills.Where(x=>x.ID_Shop == user.ID_Shop).ToListAsync();
            return View(list);  
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bill model)
        {
            if (ModelState.IsValid)
            {
                await _context.Bills.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Bill model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Bill()
                    {
                        ID_Bill = model.ID_Bill,
                        ID_Customer = model.ID_Customer,
                        ID_Voucher = model.ID_Voucher,
                        Total_Bill = model.Total_Bill,
                        CreatedAt = model.CreatedAt,
                        DeliveredAt = model.DeliveredAt,
                        BillStatus = model.BillStatus,
                        Name = model.Name,
                        Phone = model.Phone,
                    };
                    _context.Bills.Update(item);
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
            var item = await _context.Bills.FindAsync(id);
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
            var item = await _context.Bills.FindAsync(id);
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
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> handleStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
                return NotFound();

            if (item.HandleStatus == true)
                item.HandleStatus = false;
            else
                item.HandleStatus = true;
            _context.Bills.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> deliveredStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
                return NotFound();
            if (item.DeliveredStatus == true)
            {
                item.DeliveredStatus = false;
            }
            else
            {
                item.DeliveredStatus = true;

            }
            _context.Bills.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> billStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
                return NotFound();
            if (item.BillStatus == true)
            {
                item.BillStatus = false;
            }
            else
            {
                item.BillStatus = true;

            }
            _context.Bills.Update(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
