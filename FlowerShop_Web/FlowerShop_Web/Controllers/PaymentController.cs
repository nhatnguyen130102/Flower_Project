using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models;
using FlowerShop_Web.Models.Pattern;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace FlowerShop_Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentStrategy _paymentStrategy;
        private readonly CODPaymentStrategy _codPaymentStrategy;

        public PaymentController(OnlinePaymentStrategy paymentStrategy, CODPaymentStrategy codPaymentStrategy)
        {
            _paymentStrategy = paymentStrategy;
            _codPaymentStrategy = codPaymentStrategy;
        }

        public IActionResult Index()
        {
            // Your existing implementation for the Index action
            return View();
        }

        public async Task<ActionResult> PaymentWithPaypal()
        {
            try
            {
                // Call the PaymentWithPaypalAsync method from OnlinePaymentStrategy
                var approvalUrl = await _paymentStrategy.ProcessPaymentAsync(/* pass required parameters */);

                if (approvalUrl == "PaymentFailed")
                {
                    // Handle the case where payment failed
                    return View("PaymentFailed");
                }

                // Redirect to the PayPal approval URL
                return Redirect(approvalUrl);
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log the exception or perform additional error handling
                return View("PaymentFailed");
            }
        }
    
        public async Task<ActionResult> ExecutePayment(string payerId, string token)
        {
            try
            {
                // Call the ExecutePaymentAsync method from OnlinePaymentStrategy
                var result = await _paymentStrategy.ExecutePaymentAsync(payerId, token);

                // Check the type of the result
                if (result is ViewResult)
                {
                    // The payment failed, handle accordingly
                    return View("PaymentFailed");
                }
                else if (result is RedirectToActionResult)
                {
                    // The payment was successful, redirect to success page
                    return (RedirectToActionResult)result;
                }
                else
                {
                    // Handle other result types if needed
                    return View("PaymentFailed");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // Log the exception or perform additional error handling
                return View("PaymentFailed");
            }
        }

        /// Payment COD
        public async Task<ActionResult> SaveInfo()
        {
            try
            {
                // Gọi SaveInfoAsync từ _codPaymentStrategy
                var result = await _codPaymentStrategy.SaveInfoAsync();

                // Xử lý kết quả tùy thuộc vào loại kết quả
                if (result is ViewResult)
                {
                    // Xử lý trường hợp khi lưu thông tin thất bại
                    return View("SaveInfoFailed");
                }
                else if (result is JsonResult jsonResult && (string)jsonResult.Value == "PaymentFailed")
                {
                    // Xử lý trường hợp thanh toán thất bại
                    return View("PaymentFailed");
                }
                else if (result is RedirectToActionResult redirectResult)
                {
                    // Xử lý trường hợp khi lưu thông tin thành công và redirect
                    return redirectResult;
                }
                else
                {
                    // Xử lý các loại kết quả khác nếu cần
                    return View("SaveInfoFailed");
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ nếu cần
                return View("SaveInfoFailed");
            }
        }

    }


    //public class PaymentController : Controller
    //{
    //    private readonly ILogger<HomeController> _logger;
    //    private readonly ApplicationDbContext _context;
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly SignInManager<ApplicationUser> _signInManager;
    //    private readonly IMemoryCache _memoryCache;
    //    private readonly IHttpContextAccessor _httpContextAccessor;
    //    private readonly IConfiguration _configuration;

    //    public PaymentController(
    //        ILogger<HomeController> logger,
    //        ApplicationDbContext context,
    //        UserManager<ApplicationUser> userManager,
    //        SignInManager<ApplicationUser> signInManager,
    //        IMemoryCache memoryCache,
    //        IHttpContextAccessor httpContextAccessor,
    //        IConfiguration configuration)
    //    {
    //        _logger = logger;
    //        _context = context;
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //        _memoryCache = memoryCache;
    //        _httpContextAccessor = httpContextAccessor;
    //        _configuration = configuration;
    //    }

    //    public IActionResult Index()
    //    {
    //        return View();
    //    }


    //    public async Task<ActionResult> PaymentWithPaypalAsync()
    //    {
    //        try
    //        {
    //            // Replace these values with your actual PayPal credentials
    //            var clientId = "ARJHTqeKXz5m-3V7TgtAMuOqjhHjWmeUk9wVspQpFke4fP7kwVGmUlt4yhPEaP4d1Nb-e_teARoV89JV";
    //            var clientSecret = "EEDaWmbi4Ra2Sgpm3tqQh2XRVjzinHDIKiZqZ7wC3qigxWUF6Dy0TayQVB-IGh4q5yMOmo4Z7ywlkolx";
    //            var mode = "sandbox"; // or "live" for production


    //            APIContext apiContext = PaypalConfiguration.GetAPIContext(clientId, clientSecret, mode);

    //            string baseURI = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Payment/ExecutePayment?";
    //            var guid = Guid.NewGuid().ToString(); // Sử dụng Guid mới và không ngẫu nhiên
    //            var itemList = new ItemList()
    //            {
    //                items = new List<Item>()
    //            };


    //            var getVoucher = _memoryCache.Get<Voucher>("_tempVoucher");
    //            var getCartDetail = _memoryCache.Get<List<CartDetails>>("_tempCartDetail");


    //            double sub_total = 0;
    //            foreach (var item in getCartDetail)
    //            {
    //                var product = _context.Products.FirstOrDefault(x => x.ID_Product == item.ID_Product);
    //                if (product != null)
    //                {
    //                    sub_total += product.Price_Product * item.Product_Quantity;
    //                    itemList.items.Add(new Item()
    //                    {
    //                        name = product.Name_Product,
    //                        currency = "USD",
    //                        price = product.Price_Product.ToString(),
    //                        quantity = item.Product_Quantity.ToString(),
    //                        sku = "sku"
    //                    });
    //                }
    //            }
    //            double price_voucher = 0;
    //            if (_signInManager.IsSignedIn(User))
    //            {
    //                var getUser = await _userManager.GetUserAsync(User);
    //                if (getUser == null)
    //                {
    //                    return NotFound();
    //                }

    //                var getUserType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == getUser.ID_CustomerType).FirstOrDefaultAsync();
    //                if (getUserType != null)
    //                {
    //                    price_voucher = sub_total * (1 - getUserType.Discount / 100);

    //                }

    //            }
    //            if (getVoucher != null)
    //            {
    //                if (getVoucher.Type == "0")
    //                {
    //                    price_voucher += sub_total * (getVoucher.Discount / 100);
    //                }
    //                else if (getVoucher.Type == "1")
    //                {
    //                    price_voucher += getVoucher.Discount;
    //                }

    //            }

    //            var payer = new Payer { payment_method = "paypal" };
    //            var redirectUrls = new RedirectUrls
    //            {
    //                //cancel_url = Url.Action("PaymentWithPaypal", "Payment", null, Request.Scheme) + "&Cancel=true",
    //                cancel_url = Url.Action("Index", "Home", null, Request.Scheme) + "&Cancel=true",
    //                //return_url = Url.Action("PaymentWithPaypal", "Payment", null, Request.Scheme)
    //                return_url = baseURI + "guid=" + guid
    //            };
    //            double ship = 6.9;
    //            string formattedNumber = price_voucher.ToString("F2");
    //            price_voucher = double.Parse(formattedNumber);
    //            price_voucher = -price_voucher;
    //            String discount = (price_voucher).ToString();
    //            // get 2 number after , of subtotal
    //            formattedNumber = sub_total.ToString("F2");
    //            sub_total = double.Parse(formattedNumber);
    //            var detail = new Details
    //            {
    //                tax = "0",
    //                shipping = ship.ToString(),
    //                subtotal = sub_total.ToString(),
    //                handling_fee = "0",
    //                shipping_discount = discount
    //            };
    //            double totalAll = sub_total + price_voucher + ship;
    //            formattedNumber = totalAll.ToString("F2");
    //            totalAll = double.Parse(formattedNumber);
    //            var amount = new Amount
    //            {
    //                currency = "USD",
    //                total = totalAll.ToString(), // Replace with the actual total amount
    //                //details = new Details { tax = "0", shipping = "0", subtotal = sub_total.ToString()

    //                //, shipping_discount = discount
    //                //}
    //                details = detail,
    //            };

    //            var transactionList = new List<Transaction>
    //            {
    //                new Transaction
    //                {
    //                    description = "Transaction description",
    //                    invoice_number = Guid.NewGuid().ToString(),
    //                    amount = amount,
    //                    item_list = itemList
    //                }
    //            };

    //            var payment = new Payment
    //            {
    //                intent = "sale",
    //                payer = payer,
    //                transactions = transactionList,
    //                redirect_urls = redirectUrls
    //            };

    //            var createdPayment = payment.Create(apiContext);

    //            var approvalUrl = createdPayment.links.Find(x => x.rel.ToLower() == "approval_url");

    //            if (approvalUrl != null)
    //            {
    //                // Save the paymentId in the session for later use
    //                _httpContextAccessor.HttpContext.Session.SetString("paymentId", createdPayment.id);
    //                return Redirect(approvalUrl.href);
    //            }
    //            else
    //            {
    //                // Handle the case where the approval URL is not found
    //                return View("PaymentFailed");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Handle exceptions
    //            _logger.LogError(ex, "Error in PaymentWithPaypal");
    //            return View("PaymentFailed");
    //        }
    //    }

    //    public async Task<ActionResult> ExecutePaymentAsync(string PayerID, string token)
    //    {
    //        try
    //        {
    //            var clientId = "ARJHTqeKXz5m-3V7TgtAMuOqjhHjWmeUk9wVspQpFke4fP7kwVGmUlt4yhPEaP4d1Nb-e_teARoV89JV";
    //            var clientSecret = "EEDaWmbi4Ra2Sgpm3tqQh2XRVjzinHDIKiZqZ7wC3qigxWUF6Dy0TayQVB-IGh4q5yMOmo4Z7ywlkolx";
    //            var mode = "sandbox"; // or "live" for production

    //            APIContext apiContext = PaypalConfiguration.GetAPIContext(clientId, clientSecret, mode);

    //            var paymentId = _httpContextAccessor.HttpContext.Session.GetString("paymentId");

    //            if (string.IsNullOrEmpty(paymentId))
    //            {
    //                // Handle the case where paymentId is not found in the session
    //                return View("PaymentFailed");
    //            }

    //            var paymentExecution = new PaymentExecution { payer_id = PayerID };
    //            var payment = new Payment { id = paymentId };

    //            var executedPayment = payment.Execute(apiContext, paymentExecution);

    //            if (executedPayment.state.ToLower() == "approved")
    //            {
    //                // Payment was successful
    //                var getCartDetail = _memoryCache.Get<List<CartDetails>>("_tempCartDetail");

    //                var getBill = _memoryCache.Get<Bill>("_tempBill");

    //                var getVoucher = _memoryCache.Get<Voucher>("_tempVoucher");

    //                var total = getBill.Subtotal;

    //                var newBill = new Bill()
    //                {
    //                    ID_Customer = getBill.ID_Customer,
    //                    Total_Bill = total + 6.9,
    //                    Subtotal = total,
    //                    CreatedAt = getBill.CreatedAt,
    //                    DeliveredAt = getBill.DeliveredAt,
    //                    BillStatus = getBill.BillStatus,
    //                    DeliveredStatus = getBill.DeliveredStatus,
    //                    HandleStatus = getBill.HandleStatus,
    //                    Canceled = getBill.Canceled,
    //                    Name = getBill.Name,
    //                    Phone = getBill.Phone,
    //                    Name_Order = getBill.Name_Order,
    //                    Phone_Order = getBill.Phone_Order,
    //                    ID_Shop = getBill.ID_Shop,
    //                    City = getBill.City,
    //                    Address = getBill.Address,

    //                    Message = getBill.Message,
    //                };

    //                await _context.Bills.AddAsync(newBill);
    //                await _context.SaveChangesAsync();

    //                if (_signInManager.IsSignedIn(User))
    //                {
    //                    var getUser = await _userManager.GetUserAsync(User);
    //                    if (getUser == null)
    //                    {
    //                        return NotFound();
    //                    }

    //                    var getUserType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == getUser.ID_CustomerType).FirstOrDefaultAsync();
    //                    if (getUserType != null)
    //                    {
    //                        total = total * (1 - getUserType.Discount / 100);
    //                        newBill.Subtotal = total;
    //                        newBill.Total_Bill = total + 6.9;

    //                        _context.Bills.Update(newBill);
    //                        _context.SaveChanges();
    //                    }

    //                }

    //                if (getVoucher != null)
    //                {
    //                    if (getVoucher.Type == "0")
    //                    {
    //                        total = total * (1 - getVoucher.Discount / 100);
    //                    }
    //                    else if (getVoucher.Type == "1")
    //                    {
    //                        total = total - getVoucher.Discount;
    //                    }

    //                    var voucher = await _context.Vouchers.Where(x => x.ID_Voucher == getVoucher.ID_Voucher).FirstOrDefaultAsync();

    //                    voucher.Voucher_Quantity -= 1;

    //                    _context.Vouchers.Update(voucher);
    //                    _context.SaveChanges();

    //                    newBill.ID_Voucher = getVoucher.ID_Voucher;
    //                    newBill.Subtotal = total;
    //                    newBill.Total_Bill = total + 6.9;

    //                    _context.Bills.Update(newBill);
    //                    _context.SaveChanges();
    //                }

    //                if (_signInManager.IsSignedIn(User))
    //                {
    //                    var getUser = await _userManager.GetUserAsync(User);
    //                    if (getUser == null)
    //                    {
    //                        return NotFound();
    //                    }

    //                    getUser.Spend += newBill.Total_Bill;

    //                    await _userManager.UpdateAsync(getUser);
    //                    await _context.SaveChangesAsync();

    //                    var getUserType = await _context.CustomerTypes.ToListAsync();
    //                    foreach (var item in getUserType)
    //                    {
    //                        if ((getUser.Spend >= item.MinSpend && getUser.Spend < item.MaxSpend) || getUser.Spend >= item.MaxSpend)
    //                        {
    //                            getUser.ID_CustomerType = item.ID_CustomerType;
    //                        }

    //                        await _userManager.UpdateAsync(getUser);
    //                        await _context.SaveChangesAsync();
    //                    }
    //                }

    //                if (getCartDetail != null)
    //                {
    //                    foreach (var item in getCartDetail)
    //                    {
    //                        var getPro = await _context.Products.Where(x => x.ID_Product == item.ID_Product).FirstOrDefaultAsync();
    //                        var newBillDetail = new BillDetails()
    //                        {
    //                            ID_Bill = newBill.ID_Bill,
    //                            ID_Product = (int)item.ID_Product,
    //                            Product_Quantity = item.Product_Quantity,
    //                            Total = getPro.Price_Product * item.Product_Quantity
    //                        };

    //                        await _context.BillDetails.AddAsync(newBillDetail);
    //                        await _context.SaveChangesAsync();
    //                    }
    //                }

    //                if (_signInManager.IsSignedIn(User))
    //                {


    //                    var getUser = await _userManager.GetUserAsync(User);

    //                    if (getUser == null)
    //                    {
    //                        return NotFound();
    //                    }
    //                    // lấy  cart
    //                    var cart = await _context.Carts.Where(x => x.ID_Customer == getUser.Id).FirstOrDefaultAsync();

    //                    // lấy cartdetail
    //                    var cartDetail = await _context.CartDetails.Where(x => x.ID_Cart == cart.ID_Cart).ToListAsync();

    //                    foreach (var item in cartDetail)
    //                    {
    //                        _context.CartDetails.Remove(item);
    //                        _context.SaveChanges();
    //                    }
    //                }
    //                else
    //                {
    //                    _memoryCache.Remove("UserCart");
    //                }
    //                _memoryCache.Remove("_tempCartDetail");
    //                _memoryCache.Remove("_tempBill");
    //                _memoryCache.Remove("_tempVoucher");
    //                //return RedirectToAction("successView", "Order");
    //                //return View("PaymentSuccess");
    //                return RedirectToAction("Success", "Order", new { id = newBill.ID_Bill });
    //            }
    //            else
    //            {
    //                // Payment was not approved
    //                return View("PaymentFailed");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            // Handle exceptions
    //            _logger.LogError(ex, "Error in ExecutePayment");
    //            return View("PaymentFailed");
    //        }
    //    }

    //}


}
