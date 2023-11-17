using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Controllers
{
    public class FavoriteProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public FavoriteProductController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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

                if(getFavorite == null)
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
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> addFavorite(int? id)
        {
            // kiểm tra sản phẩm có tồn tại
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            // kiểm tra người dùng đã đăng nhập
            if (_signInManager.IsSignedIn(User))
            {
                // kiểm tra người dùng có tồn tại
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                // kiểm tra người dùng đã có danh sách favorite chưa
                var getFavorite = await _context.FavoriteProducts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();

                if (getFavorite == null)
                {
                    // thêm danh sách favorite cho người dùng
                    var newFavorite = new FavoriteProduct()
                    {
                        ID_Customer = user.Id,
                    };

                    await _context.FavoriteProducts.AddAsync(newFavorite);
                    await _context.SaveChangesAsync();

                    // thêm sản phẩm favorite cho người dùng
                    var newFavoriteDetail = new FavoriteProductDetails()
                    {
                        ID_FavoriteProduct = newFavorite.ID_FavoriteProduct,
                        ID_Product = id,
                    };

                    await _context.AddAsync(newFavoriteDetail);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // kiểm tra đã tồn tại danh sách favorite 
                    var getFavoriteDetail = await _context.FavoriteProductDetails.Where(x => x.ID_FavoriteProduct == getFavorite.ID_FavoriteProduct).ToListAsync();
                    if (getFavoriteDetail == null)
                    {
                        // tạo danh sách và thêm sản phẩm mới
                        var newFavoriteDetail = new FavoriteProductDetails()
                        {
                            ID_FavoriteProduct = getFavorite.ID_FavoriteProduct,
                            ID_Product = id,
                        };

                        await _context.AddAsync(newFavoriteDetail);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // kiểm tra sản phẩm đã tồn tại trong danh sách favorite chưa
                        if (getFavoriteDetail.Any(x => x.ID_Product == id))
                        {
                            var remove = await _context.FavoriteProductDetails.Where(x => x.ID_Product == id && x.ID_FavoriteProduct == getFavorite.ID_FavoriteProduct).FirstOrDefaultAsync();
                            _context.FavoriteProductDetails.Remove(remove);
                            _context.SaveChanges();

                            return RedirectToAction("Index", "Home");
                        }

                        // thêm sản phẩm mới cho danh sách 
                        var newFavoriteDetail = new FavoriteProductDetails()
                        {
                            ID_FavoriteProduct = getFavorite.ID_FavoriteProduct,
                            ID_Product = id,
                        };

                        await _context.AddAsync(newFavoriteDetail);
                        await _context.SaveChangesAsync();

                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
