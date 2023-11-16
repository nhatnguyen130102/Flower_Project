using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Controllers
{
   
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public OrderController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

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

        public async Task<IActionResult> goToOrder(int? id)
        {
            var listitem = await _context.CartDetails.Where(x => x.ID_Cart == id).ToListAsync();

            ViewBag.Location = new SelectList(_context.Locations, "ID_Locations", "Name_Locations");

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
            var today = DateTime.Now;

            var user = await _userManager.GetUserAsync(User);

            var cart = await _context.Carts.Where(x => x.ID_Customer == user.Id).FirstOrDefaultAsync();

            var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

            var total = 0.0;

            foreach (var item in cartDetail)
            {
                var pro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
                total += pro.Price_Product * item.Product_Quantity;
            }

            var getCsType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == user.ID_CustomerType).FirstOrDefaultAsync();

            if (getCsType != null)
            {
                total = total * (1 - getCsType.Discount / 100);
            }
            var newbill = new Bill();
            var getVoucher = await _context.Vouchers.Where(x => x.Code == vm.Code).FirstOrDefaultAsync();
            if (getVoucher != null)
            {
                if (getVoucher.StartedAt <= today && getVoucher.EndedAt >= today && getVoucher.Voucher_Quantity > 0 && getVoucher.IsActive == true && getVoucher.MinimumAmount <= total)
                {
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
    }
}
