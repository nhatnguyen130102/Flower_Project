
using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            var list = await _userManager.Users.ToListAsync();
            return View(list);
        }

        // Thêm một tài khoản mới, cấp quyền cho tài khoản mới và chỉ định chi nhánh cho shop
        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Role = new SelectList(_roleManager.Roles, "Id", "Name");
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(AdminCreateAccountVM model)
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

              

                if (model.NewPassword == null)
                {
                    return View("Create", model);
                }

                await _userManager.CreateAsync(user, model.NewPassword);

                if (model.ID_Role == null)
                {
                    return View("Create", model);
                }

                var role = await _roleManager.FindByIdAsync(model.ID_Role);

                if (role == null)
                {
                    return View("Create", model);
                }

                await _userManager.AddToRoleAsync(user, role.Name);

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

        // thêm role cho người dùng
        [HttpGet]
        public async Task<IActionResult> addRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            ViewBag.Role = new SelectList(_roleManager.Roles, "Name", "Name");
            //ViewBag.User = new SelectList(_userManager.Users, "Id", "Id");
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> addRole(string Id, string role)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            await _userManager.AddToRoleAsync(user, role);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> removeRole(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var oldRole = await _userManager.GetRolesAsync(user);
            if (oldRole != null)
            {
                foreach (var item in oldRole)
                {
                    await _userManager.RemoveFromRoleAsync(user, item);
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> removeOneRole(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            var oleRole = await _userManager.GetRolesAsync(user);
            if (oleRole != null)
            {
                ViewBag.RolesSelectList = new SelectList(oleRole);
            }
            else
            {
                return RedirectToAction("Index");
            }

            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> removeOneRole(string Id, string role)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            await _userManager.RemoveFromRoleAsync(user, role);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> addBranch(string Id)
        {

            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> addBranch(string Id, string Id_shop)
        {
            int id = Convert.ToInt32(Id_shop);
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null)
            {
                return RedirectToAction("Index");
            }

            if (user.ID_Shop != null)
            {
                user.ID_Shop = null;
                _context.ApplicationUsers.Update(user);
                _context.SaveChanges();
            }

            user.ID_Shop = id;

            _context.ApplicationUsers.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
