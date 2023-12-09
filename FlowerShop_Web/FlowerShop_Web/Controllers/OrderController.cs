using DesignPattern;
using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FlowerShop_Web.Controllers
{

    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly Order _order;
        public OrderController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor, Order order)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
            _order = order;
        }
        public async Task<IActionResult> Success(int? id)
        {
            if (id != null)
            {
                var confirm = await _context.Bills.Where(x => x.ID_Bill == id).FirstOrDefaultAsync();
                return View(confirm);
            }
            return NotFound();
        }

   
        public async Task<IActionResult> getInfoView()
        {
            ViewData["PredefinedAddresses"] = await _context.Shops.Select(s => s.Address_Shop).ToListAsync();
            if (_signInManager.IsSignedIn(User))
            {
                var getUser = await _userManager.GetUserAsync(User);
                if (getUser == null)
                {
                    return NotFound();
                }

                var getCart = await _context.Carts.Where(x => x.ID_Customer == getUser.Id).FirstOrDefaultAsync();
                if (getCart == null)
                {
                    return NotFound();
                }

                var getCartDetail = await _context.CartDetails.Where(x => x.ID_Cart == getCart.ID_Cart).ToListAsync();
                var list = new OrderVM()
                {
                    CartDetails = getCartDetail,
                };

                // Lưu danh sách sản phẩm trong cart vào bộ nhớ cache trong 10p
                _memoryCache.Set("_tempCartDetail", getCartDetail, TimeSpan.FromMinutes(10));

                return View(list);
            }
            else
            {
                // Lấy giỏ hàng từ cache hoặc từ nơi bạn lưu trữ giỏ hàng
                var cart = _memoryCache.GetOrCreate("UserCart", entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                    return new Cart();
                });

                // Lấy danh sách sản phẩm từ giỏ hàng
                var cartDetails = cart.CartDetails;

                var list = new OrderVM()
                {
                    CartDetails = cartDetails,
                };

                _memoryCache.Set("_tempCartDetail", cartDetails, TimeSpan.FromMinutes(10));

                return View(list);
            }
        }

        public async Task<IActionResult> getInfo(OrderVM model, string Shop)
        {
            if (_signInManager.IsSignedIn(User))
            {
                int id_Shop = 3;

                if(Shop != null)
                {
                    var getShop = await _context.Shops.Where(x => x.Address_Shop == Shop).FirstOrDefaultAsync();

                    if (getShop != null)
                    {
                        id_Shop = getShop.ID_Shop;
                    }
                }

             

                // lấy thông tin user
                var getUser = await _userManager.GetUserAsync(User);
                if (getUser == null)
                {
                    return NotFound();
                }

                // tính tổng bill
                var getCart = await _context.Carts.Where(x => x.ID_Customer == getUser.Id).FirstOrDefaultAsync();

                var getCartDetail = await _context.CartDetails.Where(x => x.ID_Cart == getCart.ID_Cart).ToListAsync();

                var subTotal = 0.0d;

                foreach (var item in getCartDetail)
                {
                    var getPro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
                    if (getPro.ID_FlashSale != null && getPro.isDiscontinued == true)
                    {
                        var getFlashSale = await _context.FlashSales.Where(x => x.ID_FlashSale == getPro.ID_FlashSale).FirstOrDefaultAsync();
                        subTotal += getFlashSale.Price_FlashSale * item.Product_Quantity;
                    }
                    else
                    {
                        subTotal += getPro.Price_Product * item.Product_Quantity;
                    }
                }

                // tạo mới thông tin người dùng tcho bill
                var newBill = new Bill()
                {
                    ID_Customer = getUser.Id,
                    Total_Bill = subTotal + 6.9,
                    Subtotal = subTotal,
                    CreatedAt = DateTime.Now,
                    DeliveredAt = model.DeliveredAt,
                    BillStatus = false,
                    DeliveredStatus = false,
                    HandleStatus = false,
                    Canceled = false,
                    Name = model.Name,
                    Phone = model.Phone,
                    Name_Order = model.Name_Order,
                    Phone_Order = model.Phone_Order,
                    ID_Shop = id_Shop,
                    City = model.City,
                    Address = model.Address,
                 
                };

                _memoryCache.Set("_tempBill", newBill, TimeSpan.FromMinutes(10));

                return RedirectToAction("getVoucherView", "Order");
            }
            else
            {
                int id_Shop = 3;

                if (Shop != null)
                {
                    var getShop = await _context.Shops.Where(x => x.Address_Shop == Shop).FirstOrDefaultAsync();

                    if (getShop != null)
                    {
                        id_Shop = getShop.ID_Shop;
                    }
                }

                var cart = _memoryCache.GetOrCreate("UserCart", entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromMinutes(30);
                    return new Cart();
                });

                // Lấy danh sách sản phẩm từ giỏ hàng
                var cartDetails = cart.CartDetails;

                var subTotal = 0.0d;

                foreach (var item in cartDetails)
                {
                    var getPro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
                    if(getPro.ID_FlashSale != null && getPro.isDiscontinued == true)
                    {
                        var getFlashSale = await _context.FlashSales.Where(x => x.ID_FlashSale == getPro.ID_FlashSale).FirstOrDefaultAsync();
                        subTotal += getFlashSale.Price_FlashSale * item.Product_Quantity;
                    }
                    else
                    {
                        subTotal += getPro.Price_Product * item.Product_Quantity;
                    }
                }

                var newBill = new Bill()
                {
                    Total_Bill = subTotal + 6.9,
                    Subtotal = subTotal,
                    CreatedAt = DateTime.Now,
                    DeliveredAt = model.DeliveredAt,
                    BillStatus = false,
                    DeliveredStatus = false,
                    HandleStatus = false,
                    Canceled = false,
                    Name = model.Name,
                    Phone = model.Phone,
                    Name_Order = model.Name_Order,
                    Phone_Order = model.Phone_Order,
                    ID_Shop = id_Shop,
                    City = model.City,
                    Address = model.Address,
              
                };

                _memoryCache.Set("_tempBill", newBill, TimeSpan.FromMinutes(10));

                return RedirectToAction("confirmOrderView", "Order");
            }

        }

        public async Task<IActionResult> getVoucherView(int? code)
        {

            ViewBag.Voucher = new SelectList(_context.Vouchers.Where(x => x.StartedAt.Date <= DateTime.Now.Date && x.EndedAt.Date >= DateTime.Now.Date && x.IsActive == true), "ID_Voucher", "Code");

            var getUser = await _userManager.GetUserAsync(User);
            if (getUser == null)
            {
                return NotFound();
            }

            var getBill = _memoryCache.Get<Bill>("_tempBill");


            if (code != null)
            {
                var id_voucher = await getVoucher(code);
                if (id_voucher != -1)
                {
                    var getVoucher = await _context.Vouchers.Where(x => x.ID_Voucher == id_voucher).FirstOrDefaultAsync();
                    if(getBill.Subtotal >= getVoucher.MinimumAmount)
                    {
                        var discount = getBill.Subtotal * (1 - getVoucher.Discount / 100);

                        _memoryCache.Set("_tempVoucher", getVoucher, TimeSpan.FromMinutes(10));

                        ViewBag.Discount = discount;
                    }
                }
            }
            if (getBill != null)
            {
                return View(getBill);
            }

            return View();
        }

        public async Task<int> getVoucher(int? code)
        {
            var getBill = _memoryCache.Get<Bill>("_tempBill");

            //var getVoucher = await _context.Vouchers.Where(x => x.ID_Voucher == code && x.EndedAt.Date >= DateTime.Today.Date && x.StartedAt.Date <= DateTime.Today.Date && x.IsActive == true && x.MinimumAmount <= getBill.Subtotal).FirstOrDefaultAsync();
            var getVoucher = await _context.Vouchers.Where(x => x.ID_Voucher == code).FirstOrDefaultAsync();

            if (getVoucher != null)
            {
                return getVoucher.ID_Voucher;
            }
            return -1;
        }

        public IActionResult confirmOrderView()
        {
            var getCartDetail = _memoryCache.Get<List<CartDetails>>("_tempCartDetail");

            return View(getCartDetail);
        }

        public async Task<IActionResult> saveInfo()
        {
            

            var getCartDetail = _memoryCache.Get<List<CartDetails>>("_tempCartDetail");

            var getBill = _memoryCache.Get<Bill>("_tempBill");

            var getVoucher = _memoryCache.Get<Voucher>("_tempVoucher");

            var total = getBill.Subtotal;


            //// tạo đối tượng 
            //var _order = new Order()
            //{
            //    ID_Customer = getBill.ID_Customer,
            //    Total_Bill = total + 6.9,
            //    Subtotal = total,
            //    CreatedAt = getBill.CreatedAt,
            //    DeliveredAt = getBill.DeliveredAt,
            //    BillStatus = getBill.BillStatus,
            //    DeliveredStatus = getBill.DeliveredStatus,
            //    HandleStatus = getBill.HandleStatus,
            //    Canceled = getBill.Canceled,
            //    Name = getBill.Name,
            //    Phone = getBill.Phone,
            //    Name_Order = getBill.Name_Order,
            //    Phone_Order = getBill.Phone_Order,
            //    ID_Shop = getBill.ID_Shop,
            //    City = getBill.City,
            //    Address = getBill.Address,
            //    Message = getBill.Message,
            //};


            var newBill = new Bill()
            {
                ID_Customer = getBill.ID_Customer,
                Total_Bill = total + 6.9,
                Subtotal = total,
                CreatedAt = getBill.CreatedAt,
                DeliveredAt = getBill.DeliveredAt,
                BillStatus = getBill.BillStatus,
                DeliveredStatus = getBill.DeliveredStatus,
                HandleStatus = getBill.HandleStatus,
                Canceled = getBill.Canceled,
                Name = getBill.Name,
                Phone = getBill.Phone,
                Name_Order = getBill.Name_Order,
                Phone_Order = getBill.Phone_Order,
                ID_Shop = getBill.ID_Shop,
                City = getBill.City,
                Address = getBill.Address,
                Message = getBill.Message,
            };

            await _context.Bills.AddAsync(newBill);
            await _context.SaveChangesAsync();

            if (_signInManager.IsSignedIn(User))
            {
                var getUser = await _userManager.GetUserAsync(User);
                if (getUser == null)
                {
                    return NotFound();
                }

                var getUserType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == getUser.ID_CustomerType).FirstOrDefaultAsync();
                if (getUserType != null)
                {
                    total = total * (1 - getUserType.Discount / 100);
                    newBill.Subtotal = total;
                    newBill.Total_Bill = total + 6.9;

                    _context.Bills.Update(newBill);
                    _context.SaveChanges();
                }

            }

            if (getVoucher != null)
            {
                if (getVoucher.Type == "0")
                {
                    total = total * (1 - getVoucher.Discount / 100);
                }
                else if (getVoucher.Type == "1")
                {
                    total = total - getVoucher.Discount;
                }

                var voucher = await _context.Vouchers.Where(x => x.ID_Voucher == getVoucher.ID_Voucher).FirstOrDefaultAsync();

                voucher.Voucher_Quantity -= 1;

                _context.Vouchers.Update(voucher);
                _context.SaveChanges();

                newBill.ID_Voucher = getVoucher.ID_Voucher;
                newBill.Subtotal = total;
                newBill.Total_Bill = total + 6.9;

                _context.Bills.Update(newBill);
                _context.SaveChanges();
            }

            if (_signInManager.IsSignedIn(User))
            {
                var getUser = await _userManager.GetUserAsync(User);
                if(getUser == null)
                {
                    return NotFound();
                }

                getUser.Spend += newBill.Total_Bill;

                await _userManager.UpdateAsync(getUser);
                await _context.SaveChangesAsync();

                var getUserType = await _context.CustomerTypes.ToListAsync();
                foreach(var item in getUserType)
                {
                    if((getUser.Spend >= item.MinSpend && getUser.Spend < item.MaxSpend) || getUser.Spend >= item.MaxSpend)
                    {
                        getUser.ID_CustomerType = item.ID_CustomerType;
                    }

                    await _userManager.UpdateAsync(getUser);
                    await _context.SaveChangesAsync();
                }
            }

            if (getCartDetail != null)
            {
                foreach (var item in getCartDetail)
                {
                    var getPro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
                    var newBillDetail = new BillDetails()
                    {
                        ID_Bill = newBill.ID_Bill,
                        ID_Product = (int)item.ID_Product,
                        Product_Quantity = item.Product_Quantity,
                        Total = getPro.Price_Product * item.Product_Quantity
                    };

                    await _context.BillDetails.AddAsync(newBillDetail);
                    await _context.SaveChangesAsync();
                }
            }

            if (_signInManager.IsSignedIn(User))
            {


                var getUser = await _userManager.GetUserAsync(User);

                if (getUser == null)
                {
                    return NotFound();
                }
                // lấy  cart
                var cart = await _context.Carts.Where(x => x.ID_Customer == getUser.Id).FirstOrDefaultAsync();

                // lấy cartdetail
                var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

                foreach (var item in cartDetail)
                {
                    _context.CartDetails.Remove(item);
                    _context.SaveChanges();
                }
            }
            else
            {
                _memoryCache.Remove("UserCart");
            }
            _memoryCache.Remove("_tempCartDetail");
            _memoryCache.Remove("_tempBill");
            _memoryCache.Remove("_tempVoucher");

            return RedirectToAction("Success", "Order", new {id = newBill.ID_Bill });
        }

        public IActionResult successView()
        {
            return View();
        }
    }
}
