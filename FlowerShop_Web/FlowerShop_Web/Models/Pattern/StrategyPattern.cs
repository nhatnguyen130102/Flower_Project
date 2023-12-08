using Azure.Core;
using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PayPal.Api;
using System.Security.Claims;
using System.Security.Policy;

namespace FlowerShop_Web.Models.Pattern
{
    public class StrategyPattern
    {
    }
    public interface IPaymentStrategy 
    {
        void ProcessPayment();
    }

    public class OnlinePaymentStrategy : IPaymentStrategy
    {
        private readonly ILogger<OnlinePaymentStrategy> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public OnlinePaymentStrategy(
            ApplicationDbContext context,
            ILogger<OnlinePaymentStrategy> logger,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IMemoryCache memoryCache,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        public async Task<string> PaymentWithPaypalAsync()
        {
            try
            {
                var clientId = "ARJHTqeKXz5m-3V7TgtAMuOqjhHjWmeUk9wVspQpFke4fP7kwVGmUlt4yhPEaP4d1Nb-e_teARoV89JV";
                var clientSecret = "EEDaWmbi4Ra2Sgpm3tqQh2XRVjzinHDIKiZqZ7wC3qigxWUF6Dy0TayQVB-IGh4q5yMOmo4Z7ywlkolx";
                var mode = "sandbox";

                APIContext apiContext = PaypalConfiguration.GetAPIContext(clientId, clientSecret, mode);

                string baseURI = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/Payment/ExecutePayment?";
                var guid = Guid.NewGuid().ToString();
                var itemList = new ItemList()
                {
                    items = new List<Item>()
                };

                var getVoucher = _memoryCache.Get<Voucher>("_tempVoucher");
                var getCartDetail = _memoryCache.Get<List<CartDetails>>("_tempCartDetail");

                double sub_total = 0;
                foreach (var item in getCartDetail)
                {
                    var product = _context.Products.FirstOrDefault(x => x.ID_Product == item.ID_Product);
                    if (product != null)
                    {
                        sub_total += product.Price_Product * item.Product_Quantity;
                        itemList.items.Add(new Item()
                        {
                            name = product.Name_Product,
                            currency = "USD",
                            price = product.Price_Product.ToString(),
                            quantity = item.Product_Quantity.ToString(),
                            sku = "sku"
                        });
                    }
                }

                double price_voucher = 0;
                if (_signInManager.IsSignedIn(_httpContextAccessor.HttpContext.User))
                {
                    var getUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                    if (getUser == null)
                    {
                        return "NotFound";
                    }

                    var getUserType = await _context.CustomerTypes.Where(x => x.ID_CustomerType == getUser.ID_CustomerType).FirstOrDefaultAsync();
                    if (getUserType != null)
                    {
                        price_voucher = sub_total * (1 - getUserType.Discount / 100);
                    }
                }

                if (getVoucher != null)
                {
                    if (getVoucher.Type == "0")
                    {
                        price_voucher += sub_total * (getVoucher.Discount / 100);
                    }
                    else if (getVoucher.Type == "1")
                    {
                        price_voucher += getVoucher.Discount;
                    }
                }

                // Rest of your existing implementation for creating a PayPal payment
                // ...

                var payer = new Payer { payment_method = "paypal" };
                var redirectUrls = new RedirectUrls
                {
                    cancel_url = _httpContextAccessor.HttpContext.Request.Scheme + "://" + _httpContextAccessor.HttpContext.Request.Host + "/Home/Index" + "&Cancel=true",
                    return_url = baseURI + "guid=" + guid
                };
                double ship = 6.9;
                string formattedNumber = price_voucher.ToString("F2");
                price_voucher = double.Parse(formattedNumber);
                price_voucher = -price_voucher;
                String discount = (price_voucher).ToString();
                formattedNumber = sub_total.ToString("F2");
                sub_total = double.Parse(formattedNumber);
                var detail = new Details
                {
                    tax = "0",
                    shipping = ship.ToString(),
                    subtotal = sub_total.ToString(),
                    handling_fee = "0",
                    shipping_discount = discount
                };
                double totalAll = sub_total + price_voucher + ship;
                formattedNumber = totalAll.ToString("F2");
                totalAll = double.Parse(formattedNumber);
                var amount = new Amount
                {
                    currency = "USD",
                    total = totalAll.ToString(),
                    details = detail,
                };

                var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    description = "Transaction description",
                    invoice_number = Guid.NewGuid().ToString(),
                    amount = amount,
                    item_list = itemList
                }
            };

                var payment = new Payment
                {
                    intent = "sale",
                    payer = payer,
                    transactions = transactionList,
                    redirect_urls = redirectUrls
                };

                var createdPayment = payment.Create(apiContext);

                var approvalUrl = createdPayment.links.Find(x => x.rel.ToLower() == "approval_url");

                if (approvalUrl != null)
                {
                    _httpContextAccessor.HttpContext.Session.SetString("paymentId", createdPayment.id);
                    return approvalUrl.href;
                }
                else
                {
                    return "PaymentFailed";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in PaymentWithPaypal");
                return "PaymentFailed";
            }
        }
        public async Task<ActionResult> ExecutePaymentAsync(string payerId, string token)
        {
            try
            {
                // Your existing implementation for creating a PayPal payment
                var clientId = "ARJHTqeKXz5m-3V7TgtAMuOqjhHjWmeUk9wVspQpFke4fP7kwVGmUlt4yhPEaP4d1Nb-e_teARoV89JV";
                var clientSecret = "EEDaWmbi4Ra2Sgpm3tqQh2XRVjzinHDIKiZqZ7wC3qigxWUF6Dy0TayQVB-IGh4q5yMOmo4Z7ywlkolx";
                var mode = "sandbox"; // or "live" for production

                APIContext apiContext = PaypalConfiguration.GetAPIContext(clientId, clientSecret, mode);

                var paymentId = _httpContextAccessor.HttpContext.Session.GetString("paymentId");

                if (string.IsNullOrEmpty(paymentId))
                {
                    // Handle the case where paymentId is not found in the session
                    return new ViewResult { ViewName = "PaymentFailed" };
                }

                var paymentExecution = new PaymentExecution { payer_id = payerId };
                var payment = new Payment { id = paymentId };

                var executedPayment = payment.Execute(apiContext, paymentExecution);

                if (executedPayment.state.ToLower() == "approved")
                {
                    // Payment was successful
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

                    if (_signInManager.IsSignedIn(_httpContextAccessor.HttpContext.User))
                    {
                        var getUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                        if (getUser == null)
                        {
                            return new NotFoundResult();
                        }

                        var getUserType = await _context.CustomerTypes
                            .Where(x => x.ID_CustomerType == getUser.ID_CustomerType)
                            .FirstOrDefaultAsync();

                        if (getUserType != null)
                        {
                            total = total * (1 - getUserType.Discount / 100);
                            newBill.Subtotal = total;
                            newBill.Total_Bill = total + 6.9;

                            _context.Bills.Update(newBill);
                            await _context.SaveChangesAsync();
                        }
                    }

                    // Rest of your logic for vouchers, user updates, and clearing cache
                    // ...

                    return new RedirectToActionResult("Success", "Order", new { id = newBill.ID_Bill });
                }
                else
                {
                    // Payment was not approved
                    return new ViewResult { ViewName = "PaymentFailed" };
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                _logger.LogError(ex, "Error in ExecutePayment");
                return new ViewResult { ViewName = "PaymentFailed" };
            }
        }

        async void IPaymentStrategy.ProcessPayment()
        {
            await PaymentWithPaypalAsync();
        }
    }
}
