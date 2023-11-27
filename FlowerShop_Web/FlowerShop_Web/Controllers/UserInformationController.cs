using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Controllers
{
    public class UserInformationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserInformationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var getUser = await _userManager.GetUserAsync(User);
                if (getUser == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var item = new AccoutManagerVM()
                {
                    ID = getUser.Id,
                    FirstName = getUser.FirstName,
                    LastName = getUser.LastName,
                    Address = getUser.Address,
                
                    City = getUser.City,
                   
                    Phone = getUser.PhoneNumber,
                    Email = getUser.Email,
                    Spend = getUser.Spend,
                    ID_Shop = getUser.ID_Shop,
                    ID_CustomerType = getUser.ID_CustomerType,
                };

                return View(item);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AccoutManagerVM model)
        {
            if (ModelState.IsValid)
            {
                if (_signInManager.IsSignedIn(User))
                {
                    var getUser = await _userManager.GetUserAsync(User);
                    if (getUser == null)
                    {
                        return RedirectToAction("Index", "UserInformation");
                    }

                    
                    getUser.FirstName = model.FirstName;
                    getUser.LastName = model.LastName;
                    getUser.PhoneNumber = model.Phone;
                    getUser.Address = model.Address;
                 
                    getUser.City = model.City;
                   

                    _context.ApplicationUsers.Update(getUser);
                    _context.SaveChanges();


                    if (model.NewPassword != null && model.OldPassword != null)
                    {
                        var result = await _userManager.ChangePasswordAsync(getUser, model.OldPassword, model.NewPassword);

                        if (result.Succeeded)
                        {
                            await _signInManager.SignInAsync(getUser, isPersistent: false);

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
                }
            }
            return View(model);
        }
        public async Task<IActionResult> favoriteView()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var getFavorite = await _context.FavoriteProducts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();

                if (getFavorite == null)
                {
                    var newFavorite = new FavoriteProduct()
                    {
                        ID_Customer = user.Id,
                    };

                    return View();
                }
                else
                {
                    var getFavoriteDetail = await _context.FavoriteProductDetails.Where(x => x.ID_FavoriteProduct == getFavorite.ID_FavoriteProduct).ToListAsync();

                    return View(getFavoriteDetail);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> myOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var order = await _context.Bills.Where(x=>x.ID_Customer == user.Id).ToListAsync();

            return View(order);
        }


     
    }
}
