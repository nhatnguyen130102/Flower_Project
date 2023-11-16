using Flower_Repository;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.userCount = UserCount();

            DateTime today = DateTime.Today;
            double TotalRevenueToday = 0;

            TotalRevenueToday = _context.Bills.Where(x => x.CreatedAt.Date == today).Sum(x => x.Total_Bill);
            ViewBag.TotalRevenueToday = TotalRevenueToday;

            // Lấy doanh thu của tháng

            DateTime currentDate = DateTime.Now;
            ViewBag.date = currentDate.ToString();
            DateTime firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            double revenueMonth = _context.Bills
            .Where(bill => bill.CreatedAt >= firstDayOfMonth && bill.CreatedAt <= lastDayOfMonth)
                .Sum(bill => bill.Total_Bill);

            ViewBag.RevenueMonth = revenueMonth;

            // Số lượng orders của 1 shop // bill thiếu id_shop
            int quantity = _context.Bills.Count();
            ViewBag.Quantity = quantity;

            // Thống kê viewCount product sort 
            var listViewCout = _context.Products.OrderByDescending(x => x.ViewCount).ToList();
            ViewBag.listViewCout = listViewCout;


            // thống kê revenue của từng tháng 
            // Lấy tất cả hóa đơn từ cơ sở dữ liệu
            var allBills = _context.Bills.ToList();

            // Group hóa đơn theo tháng và năm
            var monthlyRevenue = allBills
                .GroupBy(bill => new { Month = bill.CreatedAt.Month, Year = bill.CreatedAt.Year })
                .Select(group => new
                {
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    TotalRevenue = group.Sum(bill => bill.Total_Bill)
                })
                .OrderBy(group => group.Year)
                .ThenBy(group => group.Month)
                .ToList();

            // Tạo danh sách doanh thu cho từng tháng

            var monthlyRevenueList = monthlyRevenue
                .Select(group => new { Month = new DateTime(group.Year, group.Month, 1), Revenue = group.TotalRevenue })
                .ToList();

            ViewBag.monthlyRevenueList = monthlyRevenueList;

            // thống kê revenue từng shop
            var shopRevenue = allBills.GroupBy(bill => bill.Shop).Select(group => new
            {
                Name = group.FirstOrDefault()?.Name,
                TotalRevenue = group.Sum(bill => bill.Total_Bill)
            }).ToList();

            var shopRevenueList = shopRevenue.Select(group => new
            {
                Name = group.Name,
                Revenue = group.TotalRevenue
            }).ToList();

            ViewBag.shopRevenueList = shopRevenueList;

            // đưa đia chỉ lên cookie
            var AddressShops = _context.Shops.Select(x => x.Address_Shop).ToList();
            string locationsString = string.Join("|", AddressShops);
            Response.Cookies.Append("Locations", locationsString);
            ViewBag.addressShops = AddressShops;
            return View();
        }
        public String UserCount()
        {
            if (HttpContext.Request.Cookies.ContainsKey("AccessCount"))
            {
                var accessCount = HttpContext.Request.Cookies["AccessCount"];
                return accessCount.ToString();
            }

            return "0";

        }
        // get data chart
        [HttpPost]
        public List<object> GetList()
        {
            List<object> data = new List<object>();
            // Lấy ngày đầu tháng và ngày cuối tháng
            DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // Tạo danh sách labels cho tất cả các ngày trong tháng
            List<string> labels = new List<string>();
            List<double> tong = new List<double>();

            // Khởi tạo biến ngày hiện tại
            DateTime currentDate = firstDayOfMonth;

            while (currentDate <= lastDayOfMonth)
            {
                string label = currentDate.ToString("dd/MM");
                labels.Add(label);

                // Lấy tổng doanh thu tương ứng cho ngày hiện tại
                double total = _context.Bills
                    .Where(bill => bill.CreatedAt.Date == currentDate)
                    .Sum(bill => (double?)bill.Total_Bill) ?? 0.0;
                tong.Add(total);

                // Tăng ngày hiện tại lên 1 ngày
                currentDate = currentDate.AddDays(1);
            }


            labels.Insert(0, "");
            tong.Insert(0, 0);

            data.Add(labels);
            data.Add(tong);
            //data.Add(total);
            return data;
        }

     
    }
}

