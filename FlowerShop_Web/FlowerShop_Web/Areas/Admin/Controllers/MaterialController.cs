using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlowerShop_Web.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class MaterialController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Import_ExportService _service;

        public MaterialController(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment, Import_ExportService service)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
            _service = service;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Index(string? filter)
        {
            var query = _context.Materials.Select(model => new Material()
            {
                ID_Material = model.ID_Material,
                ID_MaterialType = model.ID_MaterialType,
                Name_Material = model.Name_Material,
                ImportAt = model.ImportAt,
                EXP_Material = model.EXP_Material,
                Price_Material = model.Price_Material,
                Img_Material = model.Img_Material,
            });
            var model = query.ToList();
            if (!string.IsNullOrEmpty(filter))
            {
                query = query.Where(b => b.Name_Material.Contains(filter));
                model = query.ToList();
            }
            else
            {
                model = query.ToList();
            }
           
            return View(model);
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


        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Material> listModel = await _context.Materials.ToListAsync();

            // Tạo một danh sách mới từ VM
            MaterialVM model = new MaterialVM();

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
            var fileName = $"MaterialExport_{today:yyyyMMddHHmmss}.xlsx";

            // Xuất dữ liệu ra Excel với tên tệp duy nhất
            _service.ExportToExcel(listModel, colum, fileName);

            // Chuyển hướng đến trang Index của Recipe controller
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File not selected");
            }

            List<Material> listModel = await _context.Materials.ToListAsync();

            // Tạo một danh sách mới từ VM
            MaterialVM model = new MaterialVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<Material> list = _service.ImportFromExcel<Material>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var item2 = new Material()
                {
                    ID_MaterialType = item.ID_MaterialType,
                    Name_Material = item.Name_Material,
                    ImportAt = item.ImportAt,
                    EXP_Material = item.EXP_Material,
                    Price_Material = item.Price_Material,
                    Img_Material = item.Img_Material,

                };

                _context.Materials.Add(item2);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
