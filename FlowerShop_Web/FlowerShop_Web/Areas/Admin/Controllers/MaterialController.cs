using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    
    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public MaterialController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.Materials.ToList();
            return View(item);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaterialType = new SelectList(_context.MaterialTypes, "ID_MaterialType", "Name_MaterialType");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material model, IFormFile? Img_Material)
        {

            if (ModelState.IsValid)
            {

                if (Img_Material != null)
                {
                    var webRootPath = _hostingEnvironment.WebRootPath;
                    var productsFolder = Path.Combine(webRootPath, "images", "materials");

                    if (!Directory.Exists(productsFolder))
                    {
                        Directory.CreateDirectory(productsFolder);
                    }

                    var materialName = model.Name_Material;
                    var productFolder = Path.Combine(productsFolder, materialName);

                    if (!Directory.Exists(productFolder))
                    {
                        Directory.CreateDirectory(productFolder);
                    }

                    string uniqueFileName = materialName + ".jpg";
                    var filePath = Path.Combine(productFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await Img_Material.CopyToAsync(stream);
                    }

                    model.Img_Material = Path.Combine("images", "materials", materialName, uniqueFileName);
                }
                await _context.Materials.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.MaterialType = new SelectList(_context.MaterialTypes, "ID_MaterialType", "Name_MaterialType");
            var item = await _context.Materials.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Material model, IFormFile? Img_Material)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Material();
                    item.ID_Material = model.ID_Material;
                    item.ID_MaterialType = model.ID_MaterialType;
                    item.Name_Material = model.Name_Material;
                    item.ImportAt = model.ImportAt;
                    item.EXP_Material = model.EXP_Material;
                    item.Price_Material = model.Price_Material;

                    if (Img_Material != null)
                    {
                        // lây đường dẫn đến wwwroot
                        var webRootPath = _hostingEnvironment.WebRootPath;

                        // lấy đường dẫn wwwroot/images/products
                        var productsFolder = Path.Combine(webRootPath, "images", "materials");

                        if (!Directory.Exists(productsFolder))
                        {
                            Directory.CreateDirectory(productsFolder);
                        }


                        var nameMaterial = model.Name_Material; // Lấy tên sản phẩm, ví dụ: "MyProduct"

                        // lấy đường dẫn wwwroot/images/products/{tên sản phẩm}
                        var productFolder = Path.Combine(productsFolder, nameMaterial);

                        // kiểm tra đường dẫn trên có tồn tại, nếu chưa tồn tại thì tạo mới
                        if (!Directory.Exists(productFolder))
                        {
                            Directory.CreateDirectory(productFolder);
                        }

                        // đặt lại tên cho hình ảnh 
                        //var uniqueFileName = model.Name_Product + ".jpg";
                        string uniqueFileName = nameMaterial + ".jpg";
                        // lấy đường dẫn wwwroot/images/products/{tên sản phẩm}/{tên hình ảnh.jpg}
                        var filePath = Path.Combine(productFolder, uniqueFileName);

                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }

                        // thực hiện thêm 1 hỉnh ảnh ở đường dẫn trên
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await Img_Material.CopyToAsync(stream);
                        }

                        //var getNewMaterial = await _context.Materials.Where(x => x.ID_Material == item.ID_Material).FirstOrDefaultAsync();

                        //getNewMaterial.Img_Material = Path.Combine("images", "materials", nameMaterial, uniqueFileName);

                        //_context.Materials.Update(getNewMaterial);
                        //_context.SaveChanges();
                        item.Img_Material = Path.Combine("images", "materials", nameMaterial, uniqueFileName);
                    }

                    _context.Update(item);
                    _context.SaveChanges();
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
            var item = await _context.Materials.FindAsync(id);
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
            var item = await _context.Materials.FindAsync(id);
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
            var item = await _context.Materials.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
