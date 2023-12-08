using DesignPattern;
using Flower_Models;
using Flower_Repository;
using Flower_Services;
using Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography.Pkcs;


namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Import_ExportService _service;
        private readonly IWebHostEnvironment _hostingEnvironment;
        //private readonly IFlowerService _flowerService;

        //public ProductController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, Import_ExportService service, IFlowerService flowerService )
        //{
        //    _context = context;
        //    _hostingEnvironment = hostingEnvironment;
        //    _service = service;
        //    _flowerService = flowerService;
        //}

        public ProductController(ApplicationDbContext context, Import_ExportService service, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _service = service;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.Products.ToList();
            return View(item);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "ID_ProductType", "Name_ProductType");
            ViewBag.FlashSales = new SelectList(_context.FlashSales, "ID_FlashSale");
            ViewBag.Occasions = new SelectList(_context.Occasions, "ID_Occasion", "Name_Occasion");
            ViewBag.Size = new List<SelectListItem>
            {
                new SelectListItem { Value = "Classic", Text = "Classic" },
                new SelectListItem { Value = "Premium", Text = "Premium" },
                new SelectListItem { Value = "Deluxe", Text = "Deluxe" }

            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product model, IFormFile? Img_Product)
        {
            if (ModelState.IsValid)
            {
                if (Img_Product != null )
                {

                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var productsFolder = Path.Combine(webRootPath, "images", "products");

                    if (!Directory.Exists(productsFolder))
                    {
                        Directory.CreateDirectory(productsFolder);
                    }

                    var productName = model.Name_Product; // Lấy tên sản phẩm, ví dụ: "MyProduct"
                    var productFolder = Path.Combine(productsFolder, productName);

                    if (!Directory.Exists(productFolder))
                    {
                        Directory.CreateDirectory(productFolder);
                    }

                    //var uniqueFileName = model.Name_Product + ".jpg";
                    string uniqueFileName = productName + ".jpg";
                    var filePath = Path.Combine(productFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Img_Product.CopyToAsync(stream);
                    }

                    model.Img_Product = Path.Combine("images", "products", productName, uniqueFileName);
                }
                model.isAvailabled = false;
                model.isDiscontinued = false;
                await _context.Products.AddAsync(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ProductTypes = new SelectList(_context.ProductTypes, "ID_ProductType", "Name_ProductType");
            ViewBag.FlashSales = new SelectList(_context.FlashSales, "ID_FlashSale");
            ViewBag.Occasions = new SelectList(_context.Occasions, "ID_Occasion", "Name_Occasion");
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product model, IFormFile? Img_Product)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var newPro = new Product();

                    newPro.ID_Product = model.ID_Product;
                    newPro.ID_Occasion = model.ID_Occasion;
                    newPro.Name_Product = model.Name_Product;
                    newPro.Price_Product = model.Price_Product;
                    newPro.isAvailabled = model.isAvailabled;
                    newPro.isDiscontinued = model.isDiscontinued;
                    newPro.CreatedAt = model.CreatedAt;
                    newPro.Rating = model.Rating;
                    newPro.ViewCount = model.ViewCount;
                    newPro.ID_FlashSale = model.ID_FlashSale;
                    newPro.Img_Product = model.Img_Product;
                    newPro.ID_ProductType = model.ID_ProductType;
                    newPro.size = model.size;

                    _context.Update(newPro);
                    _context.SaveChanges();

                    if (Img_Product != null)
                    {
                        // lây đường dẫn đến wwwroot
                        var webRootPath = _hostingEnvironment.WebRootPath;

                        // lấy đường dẫn wwwroot/images/products
                        var productsFolder = Path.Combine(webRootPath, "images", "products");

                        if (!Directory.Exists(productsFolder))
                        {
                            Directory.CreateDirectory(productsFolder);
                        }


                        var productName = model.Name_Product; // Lấy tên sản phẩm, ví dụ: "MyProduct"

                        // lấy đường dẫn wwwroot/images/products/{tên sản phẩm}
                        var productFolder = Path.Combine(productsFolder, productName);

                        // kiểm tra đường dẫn trên có tồn tại, nếu chưa tồn tại thì tạo mới
                        if (!Directory.Exists(productFolder))
                        {
                            Directory.CreateDirectory(productFolder);
                        }

                        // đặt lại tên cho hình ảnh 
                        //var uniqueFileName = model.Name_Product + ".jpg";
                        string uniqueFileName = productName + ".jpg";
                        // lấy đường dẫn wwwroot/images/products/{tên sản phẩm}/{tên hình ảnh.jpg}
                        var filePath = Path.Combine(productFolder, uniqueFileName);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // thực hiện thêm 1 hỉnh ảnh ở đường dẫn trên
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Img_Product.CopyToAsync(stream);
                        }

                        //newPro.Img_Product = Path.Combine("images", "products", productName, uniqueFileName);
                        var getNewPro = await _context.Products.Where(x => x.ID_Product == newPro.ID_Product).FirstOrDefaultAsync();

                        getNewPro.Img_Product = Path.Combine("images", "products", productName, uniqueFileName);

                        _context.Products.Update(getNewPro);
                        _context.SaveChanges();
                    }

                    //var item = await _context.Products.Where(x => x.ID_Product == newPro.ID_Product).FirstOrDefaultAsync();
                    //if(item.Img_Product == null)
                    //{
                    //    item.Img_Product = model.Img_Product;
                    //    _context.Products.Update(item);
                    //    _context.SaveChanges();
                    //}

                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> isActive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var getPro = await _context.Products.FindAsync(id);
    
            if (getPro.isAvailabled == false)
            {
                getPro.isAvailabled = true;

                _context.Update(getPro);
                _context.SaveChanges();
            }
            else
            {
                getPro.isAvailabled = false;

                _context.Update(getPro);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Product> listModel = await _context.Products.ToListAsync();

            // Tạo một danh sách mới từ VM
            ProductServiceVM model = new ProductServiceVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            // Gán các thuộc tính vào mảng string
            string[] colum = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                colum[i] = properties[i].Name;
            }

            // Tạo một tên tệp duy nhất dựa trên thời gian để tránh việc ghi đè tệp
            DateTime today = DateTime.Now;
            var fileName = $"ProductExport_{today:yyyyMMddHHmmss}.xlsx";

            // Xuất dữ liệu ra Excel với tên tệp duy nhất
            _service.ExportToExcel(listModel, colum, fileName);

            // Chuyển hướng đến trang Index của Recipe controller
            return RedirectToAction("Index", "Product");
        }

      
        //public async Task<IActionResult> addVoucer(int id)
        //{
        //    ViewBag.Voucher = new SelectList(_context.Vouchers,"ID_Voucher", "Code");
        //    var item = _flowerService.GetFlowerById(id);
        //    return View(item);
        //}

       
        //public async Task<IActionResult> addVoucer2(int id)
        //{

        //    var flower = _flowerService.GetFlowerById(id);
        //    // Hiển thị chi tiết hoa trong view
        //    return View(flower);

        //}
    }
}
