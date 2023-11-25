using Flower_Models;
using Flower_Repository;
using Flower_Services;
using Flower_ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExcelServiceController : Controller
    {
        private readonly RecipeService _recipeService;
        private readonly ApplicationDbContext _context;

        public ExcelServiceController(RecipeService recipeService, ApplicationDbContext context)
        {
            _recipeService = recipeService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return View(recipes);
        }


        [HttpPost]
        public IActionResult ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Recipe> recipes = _recipeService.GetRecipes();
            _recipeService.ExportToExcel(recipes, "Recipes.xlsx");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ImportFromExcel()
        {
            // Nhập dữ liệu từ file Excel và lưu vào database hoặc thực hiện các xử lý khác
            List<Recipe> importedRecipes = _recipeService.ImportFromExcel("Recipes.xlsx");

            // Thực hiện xử lý với dữ liệu nhập khẩu (ví dụ: lưu vào database)
            foreach (var recipe in importedRecipes)
            {
                _recipeService.AddRecipe(recipe);
            }

            return RedirectToAction("Index");
        }


    }
}
