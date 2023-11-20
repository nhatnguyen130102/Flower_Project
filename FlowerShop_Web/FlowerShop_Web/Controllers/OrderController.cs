using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.ObjectModelRemoting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text.Json.Nodes;

namespace FlowerShop_Web.Controllers
{

    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _memoryCache;

        public OrderController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMemoryCache memoryCache)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _memoryCache = memoryCache;

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

        //public async Task<IActionResult> goToOrder(int? id)
        //{
        //    var listitem = await _context.CartDetails.Where(x => x.ID_Cart == id).ToListAsync();

        //    ViewBag.Location = new SelectList(_context.Locations, "ID_Locations", "Name_Locations");

        //    var list = new OrderVM()
        //    {
        //        CartDetails = listitem
        //    };
        //    // đưa đia chỉ lên cookie
        //    var AddressShops = _context.Shops.Select(x => x.Address_Shop).ToList();
        //    string locationsString = string.Join("|", AddressShops);
        //    Response.Cookies.Append("Locations", locationsString);
        //    ViewBag.addressShops = AddressShops;
        //    return View(list);
        //}



        //test
        public async Task<IActionResult> goToOrder()
        {
            ViewBag.Voucher = new SelectList(_context.Vouchers, "Code", "Code");

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var getCart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();

            if (getCart == null)
            {
                return NotFound();
            }

            var listitem = await _context.CartDetails.Where(x => x.ID_Cart == getCart.ID_Cart).ToListAsync();

            ViewBag.Location = new SelectList(_context.Locations, "Name_Locations", "Name_Locations");

            var list = new OrderVM()
            {
                CartDetails = listitem
            };
            // đưa đia chỉ lên cookie
            var AddressShops = _context.Shops.Select(x => x.Address_Shop).ToList();
            string locationsString = string.Join("|", AddressShops);
            Response.Cookies.Append("Locations", locationsString);
            ViewBag.addressShops = AddressShops;
            return View(list);
        }

        public async Task<IActionResult> getOrder(OrderVM vm)
        {
            if (ModelState.IsValid)
            {
                var today = DateTime.Now;

                var user = await _userManager.GetUserAsync(User);

                // lấy  cart
                var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();

                // lấy cartdetail
                var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

                var total = 0.0;

                // lấy danh sách sản phẩm trong cart và tính tổng chi phí
                foreach (var item in cartDetail)
                {
                    var pro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
                    total += pro.Price_Product * item.Product_Quantity;
                }

                // lấy ra loại khách hàng
                var getCsType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == user.ID_CustomerType).FirstOrDefaultAsync();

                if (getCsType != null)
                {
                    total = total * (1 - getCsType.Discount / 100);
                }

                var newbill = new Bill();

                // kiểm tra voucher của khách hàng
                if (vm.Code != null)
                {
                    var getVoucher = await _context.Vouchers.Where(x => x.Code == vm.Code).FirstOrDefaultAsync();
                    if (getVoucher != null)
                    {
                        // kiểm tra voucher có đạt điều kiện
                        if (getVoucher.StartedAt <= today && getVoucher.EndedAt >= today && getVoucher.Voucher_Quantity > 0 && getVoucher.IsActive == true && getVoucher.MinimumAmount <= total)
                        {
                            var getBill = await _context.Bills.Where(x => x.ID_Customer == user.Id).ToListAsync();

                            // kiểm tra người dùng đã sử dụng voucher chưa
                            if (!getBill.Any(x => x.ID_Voucher == getVoucher.ID_Voucher))
                            {
                                // kiểm tra loại voucher
                                if (getVoucher.Type == "0")
                                {
                                    total = total * (1 - getVoucher.Discount / 100);
                                    getVoucher.Voucher_Quantity -= 1;
                                    newbill.ID_Voucher = getVoucher.ID_Voucher;

                                    _context.Vouchers.Update(getVoucher);
                                    _context.SaveChanges();
                                }
                                else if (getVoucher.Type == "1")
                                {
                                    total = total - getVoucher.Discount;
                                    getVoucher.Voucher_Quantity -= 1;
                                    newbill.ID_Voucher = getVoucher.ID_Voucher;

                                    _context.Vouchers.Update(getVoucher);
                                    _context.SaveChanges();
                                }
                            }
                        }
                    }

                }

                newbill.Total_Bill = total + 8.9;
                newbill.Subtotal = total;
                newbill.ID_Customer = user.Id;
                newbill.CreatedAt = today;
                newbill.DeliveredAt = vm.DeliveredAt;
                newbill.BillStatus = false;
                newbill.Name = vm.Name;
                newbill.Phone = vm.Phone;
                newbill.Name_Order = vm.Name_Order;
                newbill.Phone_Order = vm.Phone_Order;
                newbill.ID_Shop = 1;
                newbill.City = vm.City;
                newbill.Address = vm.Address;
                newbill.DeliveredStatus = false;
                newbill.HandleStatus = false;
                newbill.Message = vm.Message;
                newbill.Ward = vm.Ward;
                newbill.District = vm.District;
                newbill.Street = vm.Street;


                await _context.Bills.AddAsync(newbill);
                await _context.SaveChangesAsync();

                if (user.Spend != null)
                {
                    user.Spend += newbill.Total_Bill;
                }
                else
                {
                    user.Spend = newbill.Total_Bill;
                }

                var csType = await _context.CustomerTypes.ToListAsync();

                foreach (var item in csType)
                {
                    if (user.Spend >= item.MinSpend && user.Spend < item.MaxSpend)
                    {
                        user.ID_CustomerType = item.ID_CustomerType;
                    }
                    await _userManager.UpdateAsync(user);
                    await _context.SaveChangesAsync();
                }

                await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();

                foreach (var item in cartDetail)
                {
                    // lấy ra sản phẩm dựa trên giỏ hàng
                    var pro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();

                    // cập nhật billdetails
                    var newBillDetail = new BillDetails()
                    {
                        ID_Bill = newbill.ID_Bill,
                        ID_Product = Convert.ToInt32(item.ID_Product),
                        Product_Quantity = item.Product_Quantity,
                        Total = pro.Price_Product * item.Product_Quantity,
                    };

                    await _context.AddAsync(newBillDetail);
                    await _context.SaveChangesAsync();

                    //xoá các sản phẩm đã được đặt trong giỏ hàng của người dùng
                    _context.CartDetails.Remove(item);
                    _context.SaveChanges();
                }
                return RedirectToAction("Success", "Order", new { id = newbill.ID_Bill });
            }
            return View(vm);
        }

        public async Task<IActionResult> getInfoView()
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

        public async Task<IActionResult> getInfo(OrderVM model)
        {
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

                subTotal += getPro.Price_Product * item.Product_Quantity;
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
                Name = model.Name,
                Phone = model.Phone,
                Name_Order = model.Name_Order,
                Phone_Order = model.Phone_Order,
                ID_Shop = 1,
                City = model.City,
                Address = model.Address,
                Street = model.Street,
                District = model.District,
                Ward = model.Ward,
            };

            _memoryCache.Set("_tempBill", newBill, TimeSpan.FromMinutes(10));

            return RedirectToAction("getVoucherView", "Order");

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

          
            if(code != null)
            {
                var id_voucher = await getVoucher(code);
                if(id_voucher != -1)
                {
                    var getVoucher = await _context.Vouchers.Where(x=>x.ID_Voucher == id_voucher).FirstOrDefaultAsync();
                    var discount = getBill.Subtotal * (1 - getVoucher.Discount / 100);

                    _memoryCache.Set("_tempVoucher", getVoucher, TimeSpan.FromMinutes(10));

                    ViewBag.Discount = discount;
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

            if(getVoucher != null)
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
                Name = getBill.Name,
                Phone = getBill.Phone,
                Name_Order = getBill.Name_Order,
                Phone_Order = getBill.Phone_Order,
                ID_Shop = getBill.ID_Shop,
                City = getBill.City,
                Address = getBill.Address,
                Street = getBill.Street,
                District = getBill.District,
                Ward = getBill.Ward,
                Message = getBill.Message,
            };

            await _context.Bills.AddAsync(newBill);
            await _context.SaveChangesAsync();

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

            var getUser = await _userManager.GetUserAsync(User);

            // lấy  cart
            var cart = await _context.Carts.Where(x => x.ID_Customer == getUser.Id).FirstOrDefaultAsync();

            // lấy cartdetail
            var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

            foreach (var item in cartDetail)
            {
                _context.CartDetails.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("successView", "Order");
        }
        public IActionResult successView()
        {
            return View();
        }
    }
}
