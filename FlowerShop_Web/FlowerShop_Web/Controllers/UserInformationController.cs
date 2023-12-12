using FlowerShop_Web.Models.DesignPattern;
using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_ViewModels;
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
        private FlowerShop flowerShop;


        public UserInformationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, FlowerShop flowerShop)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            this.flowerShop = flowerShop;
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
                    var getItem = _context.Products.Where(x => x.isDiscontinued == true && x.ID_FlashSale != null).ToList();

                    var getFavoriteDetail = await _context.FavoriteProductDetails.Where(x => x.ID_FavoriteProduct == getFavorite.ID_FavoriteProduct).ToListAsync();
                    //______________________________________________________________________

                    //var getFlashSale = await _context.Products.Where(x => x.ID_FlashSale != null && x.isDiscontinued == true).ToListAsync();

                   
                    //if(getFlashSale != null)
                    //{
                    //    string getPro = "";
                    //    foreach (var item2 in getFlashSale)
                    //    {
                    //        foreach (var item3 in getFavoriteDetail)
                    //        {
                    //            if (item2.ID_Product == item3.ID_Product)
                    //            {
                    //                getPro += item2.Name_Product + " | ";
                    //            }
                    //        }
                    //    }

                    //    Customer customer2 = new Customer();
                    //    customer2.id = user.Id;
                    //    customer2.lastName = user.LastName;
                    //    customer2.firstName = user.FirstName;
                    //    customer2.messages = getPro;

                    //    flowerShop.RegisterObserver(customer2);

                    //    flowerShop.NotifyObservers($"Xin chào {customer2.lastName} {customer2.firstName} có vài sản phẩm trong mục yêu thích đang được giảm giá: {customer2.messages} Bạn có muốn xem thử");

                    //    string item = flowerShop.getMessages();

                    //    TempData["Notification"] = item;

                    //}

                   

                    return View(getFavoriteDetail);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> myOrder()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var order = await _context.Bills.Where(x => x.ID_Customer == user.Id).ToListAsync();

            return View(order);
        }


        public async Task<IActionResult> cancelOrder(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("myOrder");
            }
            var getBill = await _context.Bills.Where(x => x.ID_Bill == id).FirstOrDefaultAsync();
            if (getBill == null)
            {
                return RedirectToAction("myOrder");
            }
            if (getBill.Canceled == true || getBill.Canceled != false || getBill.BillStatus == true || getBill.HandleStatus == true)
            {
                return RedirectToAction("myOrder");
            }
            getBill.Canceled = true;

            _context.Bills.Update(getBill);
            _context.SaveChanges();

            return RedirectToAction("myOrder");
        }


        public async Task<IActionResult> billDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("myOrder");
            }

            var getBillDetail = await _context.BillDetails.Where(x=>x.ID_Bill == id).ToListAsync();
            if (getBillDetail == null)
            {
                return RedirectToAction("myOrder");
            }

            return View(getBillDetail);
        }

       
    }
}
