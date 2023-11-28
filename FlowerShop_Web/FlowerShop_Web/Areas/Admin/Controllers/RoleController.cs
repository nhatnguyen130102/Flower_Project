using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _roleManager.Roles.ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string role)
        {
            if (role != null)
            {
                _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                return RedirectToAction("Index", "Role");
            }
            return View(role);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _roleManager.FindByIdAsync(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Id = model.Id,
                    Name = model.Name,
                };
                await _roleManager.UpdateAsync(role);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role != null)
                {
                    var result = await _roleManager.DeleteAsync(role);

                    if (result.Succeeded)
                    {
                        // Xóa thành công, thực hiện các hành động khác nếu cần
                        return RedirectToAction("Index", "Role");
                    }
                    else
                    {
                        // Xử lý lỗi xóa vai trò
                        ModelState.AddModelError(string.Empty, "Error deleting role.");
                    }
                }
                else
                {
                    // Vai trò không tồn tại
                    ModelState.AddModelError(string.Empty, "Role not found.");
                }
            }

            // Nếu có lỗi, hiển thị lại danh sách vai trò
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }
    }
}
