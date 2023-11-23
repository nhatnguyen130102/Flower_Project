using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NuGet.Protocol;
using System.Runtime.CompilerServices;

namespace FlowerShop_Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _memoryCache;

        public CartController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache memoryCache)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
        }

        public IActionResult CartViewGuest()
        {
            // Lấy giỏ hàng từ cache (nếu có)
            var cart = _memoryCache.Get<Cart>("UserCart") ?? new Cart();

            return View(cart);
        }

        public async Task<IActionResult> CartView()
        {
            // kiểm tra người dùng đã đăng nhập hay chưa
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            // kiểm tra thông tin giỏ hàng
            var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();
            if (cart == null)
            {
                var newcart = new Cart();
                newcart.ID_Customer = user.Id;

                await _context.Carts.AddAsync(newcart);
                await _context.SaveChangesAsync();

                var newcartdetails = new CartDetails();
                newcartdetails.ID_Cart = newcart.ID_Cart;

                await _context.SaveChangesAsync();

                var list = await _context.CartDetails.Where(x => x.ID_Cart == newcartdetails.ID_Cart).Include(x => x.Product).ToListAsync();

                ViewData["ID_Cart"] = newcart.ID_Cart;

                return View(list);
            }
            else
            {
                ViewData["ID_Cart"] = cart.ID_Cart;

                var cartdetails = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

                if (cartdetails != null)
                {
                    return View(cartdetails);
                }
                else
                {
                    var newCartDetails = new CartDetails()
                    {
                        ID_Cart = cart.ID_Cart,
                    };

                    await _context.CartDetails.AddAsync(newCartDetails);
                    await _context.SaveChangesAsync();
                    return View(newCartDetails);
                }
            }

        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null) return BadRequest();

            if (_signInManager.IsSignedIn(User))
            {
                var id_user = _userManager.GetUserId(User);
                var cart_user = await _context.Carts.Where(x => x.ID_Customer == id_user).FirstOrDefaultAsync();
                if (cart_user == null)
                {
                    var newCart = new Cart()
                    {
                        ID_Customer = id_user,
                    };
                    await _context.Carts.AddAsync(newCart);
                    await _context.SaveChangesAsync();

                    var newCartDetail = new CartDetails()
                    {
                        ID_Cart = newCart.ID_Cart,
                        ID_Product = (int)id,
                        Product_Quantity = 1,
                    };

                    await _context.CartDetails.AddAsync(newCartDetail);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart_user.ID_Cart).FirstOrDefaultAsync();
                    if (cartDetail == null)
                    {
                        var newCartDetail = new CartDetails()
                        {
                            ID_Cart = cart_user.ID_Cart,
                            ID_Product = (int)id,
                            Product_Quantity = 1,
                        };
                        await _context.CartDetails.AddAsync(newCartDetail);
                        _context.SaveChanges();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        var findNullCartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart_user.ID_Cart && x.ID_Product.ToString() == null).FirstOrDefaultAsync();
                        if (findNullCartDetail == null)
                        {
                            var findProduct = await _context.CartDetails.Where(x => x.ID_Product == (int)id).FirstOrDefaultAsync();
                            if (findProduct == null)
                            {
                                var item = new CartDetails()
                                {
                                    ID_Cart = cartDetail.ID_Cart,
                                    ID_Product = (int)id,
                                    Product_Quantity = 1,
                                };
                                _context.CartDetails.Update(item);
                                _context.SaveChanges();

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                findProduct.Product_Quantity += 1;
                                _context.CartDetails.Update(findProduct);
                                _context.SaveChanges();

                                return RedirectToAction("Index", "Home");
                            }
                        }
                        else
                        {
                            findNullCartDetail.ID_Product = (int)id;
                            findNullCartDetail.Product_Quantity = 1;

                            _context.CartDetails.Update(findNullCartDetail);
                            await _context.SaveChangesAsync();

                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
            }
            else
            {
                // Lấy giỏ hàng từ cache (nếu có)
                var cart = _memoryCache.Get<Cart>("UserCart") ?? new Cart();

                // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
                var existingItem = cart.CartDetails.Find(item => item.ID_Product == id);

                if (existingItem != null)
                {
                    // Nếu đã tồn tại, cập nhật số lượng
                    existingItem.Product_Quantity += 1;
                }
                else
                {
                    var getPro = await _context.Products.Where(x => x.ID_Product == id).FirstOrDefaultAsync();
                    // Nếu chưa tồn tại, thêm sản phẩm vào giỏ hàng
                    cart.CartDetails.Add(new CartDetails
                    {
                        ID_Product = id,
                        Product_Quantity = 1,
                    });
                }

                // Cập nhật giỏ hàng trong cache
                _memoryCache.Set("UserCart", cart, TimeSpan.FromDays(1)); // Lưu trong 1 ngày, bạn có thể điều chỉnh thời gian lưu trữ

                // Chuyển hướng hoặc thực hiện các thao tác khác
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> IncreaseBtn(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }
                var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();
                if (cart == null)
                {
                    return NotFound();
                }
                if (id == null)
                {
                    return NotFound();
                }
                var item = await _context.CartDetails.Where(x => x.ID_Product == id && x.ID_Cart == cart.ID_Cart).FirstOrDefaultAsync();
                if (item == null)
                {
                    return NotFound();
                }
                item.Product_Quantity += 1;
                _context.CartDetails.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("CartView", "Cart");
            }
            else
            {
                // Lấy giỏ hàng từ cache hoặc từ nơi bạn lưu trữ giỏ hàng
                var cart = _memoryCache.GetOrCreate("UserCart", entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                    return new Cart();
                });

                var cartDetails = cart.CartDetails;

                var getPro = cart.CartDetails.Find(item => item.ID_Product == id);

                getPro.Product_Quantity += 1;

                _memoryCache.Set("UserCart", cart, TimeSpan.FromDays(1)); // Lưu trong 1 ngày, bạn có thể điều chỉnh thời gian lưu trữ

                return RedirectToAction("CartViewGuest", "Cart");
            }
            
        }

        public async Task<IActionResult> DecreaseBtn(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }
                var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();
                if (cart == null)
                {
                    return NotFound();
                }
                if (id == null)
                {
                    return NotFound();
                }
                var item = await _context.CartDetails.Where(x => x.ID_Product == id && x.ID_Cart == cart.ID_Cart).FirstOrDefaultAsync();
                if (item == null)
                {
                    return NotFound();
                }
                item.Product_Quantity -= 1;
                if (item.Product_Quantity < 1)
                {
                    item.Product_Quantity = 1;
                }
                _context.CartDetails.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("CartView", "Cart");
            }
            else
            {
                // Lấy giỏ hàng từ cache hoặc từ nơi bạn lưu trữ giỏ hàng
                var cart = _memoryCache.GetOrCreate("UserCart", entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                    return new Cart();
                });

                var cartDetails = cart.CartDetails;

                var getPro = cart.CartDetails.Find(item => item.ID_Product == id);

                getPro.Product_Quantity -= 1;

                if(getPro.Product_Quantity < 1)
                {
                    getPro.Product_Quantity = 1;
                }

                _memoryCache.Set("UserCart", cart, TimeSpan.FromDays(1)); // Lưu trong 1 ngày, bạn có thể điều chỉnh thời gian lưu trữ

                return RedirectToAction("CartViewGuest", "Cart");
            }

        }

        public async Task<IActionResult> RemoveItem(int? id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }
                var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();
                if (cart == null)
                {
                    return NotFound();
                }
                if (id == null)
                {
                    return NotFound();
                }
                var item = await _context.CartDetails.Where(x => x.ID_Product == id && x.ID_Cart == cart.ID_Cart).FirstOrDefaultAsync();
                if (item == null)
                {
                    return NotFound();
                }

                _context.CartDetails.Remove(item);
                await _context.SaveChangesAsync();

                var cartDetails = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

                if (cartDetails == null)
                {
                    var newCartDetail = new CartDetails()
                    {
                        ID_Cart = cart.ID_Cart,
                    };
                }
                return RedirectToAction("CartView", "Cart");
            }
            else
            {
                // Lấy giỏ hàng từ cache hoặc từ nơi bạn lưu trữ giỏ hàng
                var cart = _memoryCache.GetOrCreate("UserCart", entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                    return new Cart();
                });

                var cartDetails = cart.CartDetails;

                var getPro = cart.CartDetails.Find(item => item.ID_Product == id);

                if(getPro != null)
                {
                    cartDetails.Remove(getPro);
                }

                _memoryCache.Set("UserCart", cart, TimeSpan.FromDays(1)); // Lưu trong 1 ngày, bạn có thể điều chỉnh thời gian lưu trữ

                return RedirectToAction("CartViewGuest", "Cart");
            }
        }


    }
}
