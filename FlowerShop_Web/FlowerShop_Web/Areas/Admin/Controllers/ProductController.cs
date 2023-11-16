using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Security.Cryptography.Pkcs;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
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
                if (Img_Product != null)
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
    }
}
