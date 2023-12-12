using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Import_ExportService _service;

        public CustomerTypeController(ApplicationDbContext context, Import_ExportService service)
        {
            _context = context;
            _service = service;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.CustomerTypes.ToList();
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
        public async Task<IActionResult> Create(CustomerType model)
        {
            if (ModelState.IsValid)
            {
                await _context.CustomerTypes.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.CustomerTypes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerType model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new CustomerType()
                    {
                        ID_CustomerType = model.ID_CustomerType,
                        Name_CustomerType = model.Name_CustomerType,
                        MinSpend = model.MinSpend,
                        MaxSpend = model.MaxSpend,
                        Discount = model.Discount,

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

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var item = await _context.CustomerTypes.FindAsync(id);
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
            var item = await _context.CustomerTypes.FindAsync(id);
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
            var item = await _context.CustomerTypes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.CustomerTypes.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Category> listModel = await _context.Categories.ToListAsync();

            // Tạo một danh sách mới từ VM
            CategoryVM model = new CategoryVM();

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
            var fileName = $"CustomerTypeExport_{today:yyyyMMddHHmmss}.xlsx";

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

            List<CustomerType> listModel = await _context.CustomerTypes.ToListAsync();

            // Tạo một danh sách mới từ VM
            CustomerTypeVM model = new CustomerTypeVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<CustomerType> list = _service.ImportFromExcel<CustomerType>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var item2 = new CustomerType()
                {
                    Name_CustomerType = item.Name_CustomerType,
                    MinSpend = item.MinSpend,
                    MaxSpend = item.MaxSpend,
                    Discount = item.Discount,
                };

                _context.CustomerTypes.Add(item2);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
