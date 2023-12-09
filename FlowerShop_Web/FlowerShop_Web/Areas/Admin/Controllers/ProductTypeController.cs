using DesignPattern;
using Flower_Models;
using Flower_Repository;
using Flower_Services;
using Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlowerShop_Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Import_ExportService _service;


        public ProductTypeController(ApplicationDbContext context, Import_ExportService service)
        {
            _context = context;
            _service = service;
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.ProductTypes.ToList();
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType model)
        {
            if (ModelState.IsValid)
            {
                FlowerSingleton item = FlowerSingleton.GetInstance();
                item.AddProductType(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.ProductTypes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductType model)
        {
            if (ModelState.IsValid)
            {
                FlowerSingleton item = FlowerSingleton.GetInstance();
                item.EditProductType(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var item = await _context.ProductTypes.FindAsync(id);
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
            var item = await _context.ProductTypes.FindAsync(id);
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

            FlowerSingleton item = FlowerSingleton.GetInstance();
            item.RemoveProductType(id);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<ProductType> listModel = await _context.ProductTypes.ToListAsync();

            // Tạo một danh sách mới từ VM
            ProductTypeVM model = new ProductTypeVM();

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
            var fileName = $"ProductTypeExport_{today:yyyyMMddHHmmss}.xlsx";

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

            List<ProductType> listModel = await _context.ProductTypes.ToListAsync();

            // Tạo một danh sách mới từ VM
            ProductTypeVM model = new ProductTypeVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<ProductType> list = _service.ImportFromExcel<ProductType>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var item2 = new ProductType()
                {
                    Name_ProductType = item.Name_ProductType,
                };

                _context.ProductTypes.Add(item2);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
