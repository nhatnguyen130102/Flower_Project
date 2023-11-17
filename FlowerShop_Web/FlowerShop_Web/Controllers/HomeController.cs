using Flower_Models;
using Flower_Repository;
using FlowerShop_Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Flower_ViewModels;
using Microsoft.Identity.Client;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Net;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FlowerShop_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }


        public IActionResult Index(string? searching, string? id)
        {
            int? id_occation = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;
            var query = _context.Products.Select(model => new Product()
            {
                ID_Product = model.ID_Product,
                ID_Occasion = model.ID_Occasion,
                Name_Product = model.Name_Product,
                Price_Product = model.Price_Product,
                Img_Product = model.Img_Product,
                isAvailabled = model.isAvailabled,
                isDiscontinued = model.isDiscontinued,
                CreatedAt = model.CreatedAt,
                Rating = model.Rating,
                ViewCount = model.ViewCount,
                ID_FlashSale = model.ID_FlashSale,
                ID_ProductType = model.ID_ProductType,
                size = model.size,
            });



            //var list = _context.Products.Include(x => x.FlashSale).Include(x => x.ProductType).Include(x => x.Occasion).ToList();
            if (_context.Occasions.Any())
            {
                ViewBag.Occasion = new SelectList(_context.Occasions, "ID_Occasion", "Name_Occasion");
            }
            if (_context.ProductTypes.Any())
            {
                ViewBag.ProductType = new SelectList(_context.ProductTypes, "ID_ProductType", "Name_ProductType");
            }

            if (id_occation.HasValue)
            {
                query = query.Where(b => b.ID_Occasion == id_occation);
            }

            if (!string.IsNullOrEmpty(searching))
            {
                query = query.Where(b => b.Name_Product.Contains(searching));
            }
            var model = query.ToList();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Post()
        //{
        //    var item = _context.Posts.Include(x => x.Category).ToList();
        //    return View(item);
        //}
        //public async Task<IActionResult> PostDetail(int? id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var item = await _context.Posts.FindAsync(id);
        //        return View(item);
        //    }
        //    else
        //    {
        //        return NotFound();
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> ProductDetails(int? id)
        {

            ViewBag.Location = new SelectList(_context.Locations, "ID_Locations", "Name_Locations").ToList();

            var product = await _context.Products.FindAsync(id);

            if (ViewBag.ID_Location != null)
            {
                int id_Location = Convert.ToInt32(ViewBag.ID_Location);

                var wareHouse = _context.ProductWarehouses.Where(x => x.Shop.ID_Locations == id_Location);

                //var quantity = wareHouse.

            }
            if (product == null)
            {
                return NotFound();
            }
            var flashsale = await _context.FlashSales.FindAsync(product.ID_FlashSale);
            if (flashsale != null)
            {
                ViewBag.FlashSale = flashsale.Price_FlashSale;
            }
            else
            {
                ViewBag.FlashSale = null;
            }
            // đếm lượt xem sản phẩm
            product.ViewCount++;
            _context.SaveChanges();
            // Đếm lượt xem sản phẩm khi user login
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var MUP = _context.ManagerUserProducts.FirstOrDefault(x => x.ID_Customer == user.Id
                && x.ID_Product == id);
                if (MUP != null)
                {
                    MUP.ViewCount++;
                    _context.SaveChanges();
                }
            }
            return View(product);
        }

        //public async Task<IActionResult> CartView()
        //{
        //    // kiểm tra người dùng đã đăng nhập hay chưa
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user != null)
        //    {
        //        // kiểm tra thông tin giỏ hàng
        //        var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();
        //        if (cart == null)
        //        {
        //            var newcart = new Cart();
        //            newcart.ID_Customer = user.Id;

        //            await _context.Carts.AddAsync(newcart);
        //            await _context.SaveChangesAsync();

        //            var newcartdetails = new CartDetails();
        //            newcartdetails.ID_Cart = newcart.ID_Cart;

        //            await _context.SaveChangesAsync();

        //            var list = await _context.CartDetails.Where(x => x.ID_Cart == newcartdetails.ID_Cart).Include(x => x.Product).ToListAsync();

        //            ViewData["ID_Cart"] = newcart.ID_Cart;

        //            return View(list);
        //        }
        //        else
        //        {
        //            ViewData["ID_Cart"] = cart.ID_Cart;

        //            var cartdetails = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).Include(x => x.Product).FirstOrDefaultAsync();

        //            if (cartdetails != null)
        //            {
        //                var list = await _context.CartDetails.Where(x => x.ID_Cart == cartdetails.ID_Cart).ToListAsync();
        //                _context.SaveChanges();
        //                return View(list);
        //            }
        //            else
        //            {
        //                var newCartDetails = new CartDetails()
        //                {
        //                    ID_Cart = cart.ID_Cart,
        //                };

        //                await _context.CartDetails.AddAsync(newCartDetails);
        //                await _context.SaveChangesAsync();
        //                return View(newCartDetails);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index");
        //    }
        //}

        //public async Task<IActionResult> AddToCart(int? id)
        //{
        //    if (id == null) return BadRequest();

        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        var id_user = _userManager.GetUserId(User);
        //        var cart_user = await _context.Carts.Where(x => x.ID_Customer == id_user).FirstOrDefaultAsync();
        //        if (cart_user == null)
        //        {
        //            var newCart = new Cart()
        //            {
        //                ID_Customer = id_user,
        //            };
        //            await _context.Carts.AddAsync(newCart);
        //            await _context.SaveChangesAsync();

        //            var newCartDetail = new CartDetails()
        //            {
        //                ID_Cart = newCart.ID_Cart,
        //                ID_Product = (int)id,
        //                Product_Quantity = 1,
        //            };

        //            await _context.CartDetails.AddAsync(newCartDetail);
        //            await _context.SaveChangesAsync();

        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart_user.ID_Cart).FirstOrDefaultAsync();
        //            if (cartDetail == null)
        //            {
        //                var newCartDetail = new CartDetails()
        //                {
        //                    ID_Cart = cart_user.ID_Cart,
        //                    ID_Product = (int)id,
        //                    Product_Quantity = 1,
        //                };
        //                await _context.CartDetails.AddAsync(newCartDetail);
        //                _context.SaveChanges();

        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                var findNullCartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart_user.ID_Cart && x.ID_Product.ToString() == null).FirstOrDefaultAsync();
        //                if (findNullCartDetail == null)
        //                {
        //                    var findProduct = await _context.CartDetails.Where(x => x.ID_Product == (int)id).FirstOrDefaultAsync();
        //                    if (findProduct == null)
        //                    {
        //                        var item = new CartDetails()
        //                        {
        //                            ID_Cart = cartDetail.ID_Cart,
        //                            ID_Product = (int)id,
        //                            Product_Quantity = 1,
        //                        };
        //                        _context.CartDetails.Update(item);
        //                        _context.SaveChanges();

        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                    else
        //                    {
        //                        findProduct.Product_Quantity += 1;
        //                        _context.CartDetails.Update(findProduct);
        //                        _context.SaveChanges();

        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                }
        //                else
        //                {
        //                    findNullCartDetail.ID_Product = (int)id;
        //                    findNullCartDetail.Product_Quantity = 1;

        //                    _context.CartDetails.Update(findNullCartDetail);
        //                    await _context.SaveChangesAsync();

        //                    return RedirectToAction("Index", "Home");
        //                }

        //            }
        //        }
        //    }
        //    else
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //}

        public IActionResult test()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SaveSelectedDate(string selectedDate)
        {
            ViewBag.SelectedDate = selectedDate; // Lưu ngày đã chọn vào ViewBag
                                                 // Hoặc sử dụng ViewData: ViewData["SelectedDate"] = selectedDate;

            return Json(new { success = true }); // Trả về kết quả (nếu cần)
        }

        //public async Task<IActionResult> Success(int? id)
        //{
        //    if (id != null)
        //    {
        //        var confirm = await _context.BillDetails.Where(x => x.ID_Bill == id).ToListAsync();
        //        return View(confirm);
        //    }
        //    return NotFound();
        //}

        //public async Task<IActionResult> goToOrder(int? id)
        //{
        //    var listitem = await _context.CartDetails.Where(x => x.ID_Cart == id).ToListAsync();

        //    ViewBag.Location = new SelectList(_context.Locations, "ID_Locations", "Name_Locations");

        //    var list = new OrderVM()
        //    {
        //        CartDetails = listitem
        //    };

        //    return View(list);
        //}

        //public async Task<IActionResult> getOrder(OrderVM vm)
        //{
        //    var today = DateTime.Now;

        //    var user = await _userManager.GetUserAsync(User);

        //    var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();

        //    var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

        //    var total = 0.0;

        //    foreach (var item in cartDetail)
        //    {
        //        var pro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
        //        total += pro.Price_Product * item.Product_Quantity;
        //    }

        //    var getCsType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == user.ID_CustomerType).FirstOrDefaultAsync();

        //    if (getCsType != null)
        //    {
        //        total = total * (1 - getCsType.Discount/100);
        //    }
        //    var newbill = new Bill();
        //    var getVoucher = await _context.Vouchers.Where(x => x.Code == vm.Code).FirstOrDefaultAsync();
        //    if (getVoucher != null)
        //    {
        //        if (getVoucher.StartedAt <= today && getVoucher.EndedAt >= today && getVoucher.Voucher_Quantity > 0 && getVoucher.IsActive == true && getVoucher.MinimumAmount <= total)
        //        {
        //            if (getVoucher.Type == "0")
        //            {
        //                total = total * (1 - getVoucher.Discount/100);
        //                getVoucher.Voucher_Quantity -= 1;
        //                newbill.ID_Voucher = getVoucher.ID_Voucher;

        //                _context.Vouchers.Update(getVoucher);
        //                _context.SaveChanges();
        //            }
        //            else if (getVoucher.Type == "1")
        //            {
        //                total = total - getVoucher.Discount;
        //                getVoucher.Voucher_Quantity -= 1;
        //                newbill.ID_Voucher = getVoucher.ID_Voucher;

        //                _context.Vouchers.Update(getVoucher);
        //                _context.SaveChanges();
        //            }
        //        }
        //    }
        //    newbill.Total_Bill = total + 8.9;
        //    newbill.Subtotal = total;
        //    newbill.ID_Customer = user.Id;
        //    newbill.CreatedAt = today;
        //    newbill.DeliveredAt = vm.DeliveredAt;
        //    newbill.BillStatus = true;
        //    newbill.Name = vm.Name;
        //    newbill.Phone = vm.Phone;
        //    newbill.Name_Order = vm.Phone_Order;
        //    newbill.Phone_Order = vm.Phone_Order;
        //    newbill.ID_Shop = 1;
        //    newbill.City = vm.City;
        //    newbill.Address = vm.Address;
        //    //newbill.Message = vm.Message;
        //    newbill.Message = vm.Message;

        //    await _context.Bills.AddAsync(newbill);
        //    await _context.SaveChangesAsync();

        //    if (user.Spend != null)
        //    {
        //        user.Spend += newbill.Total_Bill;
        //    }
        //    else
        //    {
        //        user.Spend = newbill.Total_Bill;
        //    }

        //    var csType = await _context.CustomerTypes.ToListAsync();

        //    foreach (var item in csType)
        //    {
        //        if (user.Spend >= item.MinSpend && user.Spend < item.MaxSpend)
        //        {
        //            user.ID_CustomerType = item.ID_CustomerType;
        //        }
        //        await _userManager.UpdateAsync(user);
        //        await _context.SaveChangesAsync();
        //    }

        //    await _userManager.UpdateAsync(user);
        //    await _context.SaveChangesAsync();

        //    foreach (var item in cartDetail)
        //    {
        //        // lấy ra sản phẩm dựa trên giỏ hàng
        //        var pro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();

        //        // cập nhật billdetails
        //        var newBillDetail = new BillDetails()
        //        {
        //            ID_Bill = newbill.ID_Bill,
        //            ID_Product = Convert.ToInt32(item.ID_Product),
        //            Product_Quantity = item.Product_Quantity,
        //            Total = pro.Price_Product * item.Product_Quantity,
        //        };

        //        await _context.AddAsync(newBillDetail);
        //        await _context.SaveChangesAsync();

        //        //xoá các sản phẩm đã được đặt trong giỏ hàng của người dùng
        //        _context.CartDetails.Remove(item);
        //        _context.SaveChanges();
        //    }

        //    return RedirectToAction("Success", "Home", new { id = newbill.ID_Bill });
        //}

        public async Task<IActionResult> productToOrder(int? id)
        {
            var item = await _context.Products.Where(x => x.ID_Product == id).FirstOrDefaultAsync();

            ViewBag.Location = new SelectList(_context.Locations, "ID_Locations", "Name_Locations");

            return View(item);
        }




    }
}