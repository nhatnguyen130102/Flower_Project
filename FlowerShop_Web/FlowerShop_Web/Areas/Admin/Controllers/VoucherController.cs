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
    public class VoucherController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Import_ExportService _service;


        public VoucherController(ApplicationDbContext context, Import_ExportService service)
        {
            _context = context;
            _service = service;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.Vouchers.ToList();
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
        public async Task<IActionResult> Create(Voucher model)
        {
            if (ModelState.IsValid)
            {
                await _context.Vouchers.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.Vouchers.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Voucher model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Voucher()
                    {
                        ID_Voucher = model.ID_Voucher,
                        Code = model.Code,
                        Type = model.Type,
                        Discount = model.Discount,
                        StartedAt = model.StartedAt,
                        EndedAt = model.EndedAt,
                        Voucher_Quantity = model.Voucher_Quantity,
                        MinimumAmount = model.MinimumAmount,
                        IsActive = model.IsActive
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
            var item = await _context.Vouchers.FindAsync(id);
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
            var item = await _context.Vouchers.FindAsync(id);
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
            var item = await _context.Vouchers.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Vouchers.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Voucher> listModel = await _context.Vouchers.ToListAsync();

            // Tạo một danh sách mới từ VM
            VoucherVM model = new VoucherVM();

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
            var fileName = $"VoucherExport_{today:yyyyMMddHHmmss}.xlsx";

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

            List<Voucher> listModel = await _context.Vouchers.ToListAsync();

            // Tạo một danh sách mới từ VM
            VoucherVM model = new VoucherVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<Voucher> list = _service.ImportFromExcel<Voucher>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var item2 = new Voucher()
                {
                    Code = item.Code,
                    Type = item.Type,
                    Discount = item.Discount,
                    StartedAt = item.StartedAt,

                    EndedAt = item.EndedAt,
                    Voucher_Quantity = item.Voucher_Quantity,
                    MinimumAmount = item.MinimumAmount,
                    IsActive = item.IsActive,

                };

                _context.Vouchers.Add(item2);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

    }
}
