using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Reflection;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExcelServiceController : Controller
    {
        private readonly Import_ExportService _service;
        private readonly ApplicationDbContext _context;

        public ExcelServiceController(Import_ExportService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return View(recipes);
        }


        //[HttpPost]
        //public IActionResult ExportToExcel()
        //{
        //    // Lấy dữ liệu từ service để xuất ra Excel
        //    List<Recipe> recipes = _context.Recipes.ToList();
        //    _recipeService.ExportToExcel(recipes);

        //    return RedirectToAction("Index","Recipe");
        //}

        //[HttpPost]
        //public async Task<IActionResult> ExportToExcel()
        //{
        //    // Lấy dữ liệu từ service để xuất ra Excel
        //    List<Recipe> recipes = await _context.Recipes.ToListAsync();

        //    // Tạo một danh sách mới từ VM
        //    RecipeServiceVM modelRecipe = new RecipeServiceVM();

        //    // Lấy ra các thuộc tính có trong VM
        //    PropertyInfo[] properties = modelRecipe.GetType().GetProperties();

        //    // Gán các thuộc tính vào mảng string
        //    string[] colum = new string[properties.Length];
        //    for(int i = 0; i< properties.Length; i++)
        //    {
        //        colum[i] = properties[i].Name;
        //    }

        //    // Tạo một tên tệp duy nhất dựa trên thời gian để tránh việc ghi đè tệp
        //    DateTime today = DateTime.Now;
        //    var fileName = $"RecipeExport_{today:yyyyMMddHHmmss}.xlsx";

        //    // Xuất dữ liệu ra Excel với tên tệp duy nhất
        //    _service.ExportToExcel(recipes, colum, fileName);

        //    // Chuyển hướng đến trang Index của Recipe controller
        //    return RedirectToAction("Index", "Recipe");
        //}



        //[HttpPost]
        //public IActionResult ImportFromExcel()
        //{
        //    // Nhập dữ liệu từ file Excel và lưu vào database hoặc thực hiện các xử lý khác
        //    List<Recipe> importedRecipes = _service.ImportFromExcel("Recipes.xlsx");

        //    // Thực hiện xử lý với dữ liệu nhập khẩu (ví dụ: lưu vào database)
        //    foreach (var recipe in importedRecipes)
        //    {
        //        _service.AddRecipe(recipe);
        //    }

        //    return RedirectToAction("Index","Recipe");
        //}


    }
}
