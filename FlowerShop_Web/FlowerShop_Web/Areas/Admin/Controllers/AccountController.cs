using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AccountController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // xem tất cả tài khoản
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _userManager.Users.ToListAsync();
            return View(list);
        }

        // Thêm một tài khoản mới, cấp quyền cho tài khoản mới và chỉ định chi nhánh cho shop
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Role = new SelectList(_roleManager.Roles, "Id", "Name");
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create(AccoutManagerVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.Email,
                    Address = model.Address,
                    ID_Shop = model.ID_Shop,
                    EmailConfirmed = true,
                };

                var result = await _userManager.CreateAsync(user, model.NewPassword);

                var role = await _roleManager.FindByIdAsync(model.ID_Role);

                await _userManager.AddToRoleAsync(user, role.Name);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    // Xử lý khi có lỗi xảy ra
                    ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");

                    return View("Create", model);
                }
            }
            return View(model);
        }

        // Chỉnh sửa thông tin cá nhân
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            var getUser = new AccoutManagerVM()
            {
                ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                Phone = user.PhoneNumber,
            };
            if (getUser == null)
            {
                return NotFound();
            }
            return View(getUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AccoutManagerVM? model)
        {
            if (ModelState.IsValid)
            {
                var newUser = await _userManager.FindByIdAsync(model.ID);
                if (newUser == null)
                {
                    // Xử lý trường hợp không tìm thấy người dùng
                    return NotFound();
                }
                newUser.FirstName = model.FirstName;
                newUser.LastName = model.LastName;
                newUser.Address = model.Address;
                newUser.PhoneNumber = model.Phone;

                await _userManager.UpdateAsync(newUser);

                if (model.NewPassword != null)
                {
                    var result = await _userManager.ChangePasswordAsync(newUser, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, isPersistent: false);

                        // Xử lý khi thay đổi mật khẩu thành công
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        // Xử lý khi có lỗi xảy ra
                        ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");

                        return View("Edit", model); // Trả về view có chứa form để người dùng nhập lại thông tin
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // Thêm role mới 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addRole(string? id)
        {
            ViewBag.Role = new SelectList(_roleManager.Roles, "roleId", "roleName");
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> addRole(ApplicationUser model, string role)
        {
            if (role == null)
            {
                return BadRequest();
            }
            var getRole = await _roleManager.FindByIdAsync(role);
            if (getRole == null)
            {
                return NotFound();
            }
            await _userManager.AddToRoleAsync(model, getRole.Name);

            return RedirectToAction("Index");
        }

        //[HttpGet]
        //public async Task<IActionResult> EditAccount(string? id)
        //{
        //    if(id == null)
        //    {
        //        return NotFound();
        //    }
        //    var getUser = await _userManager.FindByIdAsync(id);
        //    if (getUser == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(getUser);
        //}

        //public async Task<IActionResult> EditAccout()
    }
}
