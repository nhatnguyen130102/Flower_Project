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
    [Authorize(Roles = "Admin")]
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Import_ExportService _service;


        public ShopController(ApplicationDbContext context, Import_ExportService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.Shops.ToList();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Shop model)
        {
            if (ModelState.IsValid)
            {
                await _context.Shops.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.Shops.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Shop model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Shop()
                    {
                        ID_Shop = model.ID_Shop,
                        Name_Shop = model.Name_Shop,
                        Address_Shop = model.Address_Shop,
                        Phone_Shop = model.Phone_Shop,
                        ID_Locations = model.ID_Locations,
                    };
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var item = await _context.Shops.FindAsync(id);
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
            var item = await _context.Shops.FindAsync(id);
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
            var item = await _context.Shops.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Shops.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }




        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Shop> listModel = await _context.Shops.ToListAsync();

            // Tạo một danh sách mới từ VM
            ShopVM model = new ShopVM();

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
            var fileName = $"ShopExport_{today:yyyyMMddHHmmss}.xlsx";

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

            List<Shop> listModel = await _context.Shops.ToListAsync();

            // Tạo một danh sách mới từ VM
            ShopVM model = new ShopVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<Shop> list = _service.ImportFromExcel<Shop>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var item2 = new Shop()
                {
                    Name_Shop = item.Name_Shop,
                    Address_Shop = item.Address_Shop,
                    Phone_Shop = item.Phone_Shop,
                    ID_Locations = item.ID_Locations,

                };

                _context.Shops.Add(item2);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
