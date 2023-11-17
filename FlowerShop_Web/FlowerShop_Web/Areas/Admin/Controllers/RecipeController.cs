using Flower_Models;
using Flower_Repository;
using Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RecipeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecipeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles ="Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.Recipes.ToList();
            return View(item);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Product = new SelectList(_context.Products, "ID_Product", "Name_Product");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recipe model)
        {
            if (ModelState.IsValid)
            {
                await _context.Recipes.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> CreateNext(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            ViewBag.id = id;
            var recipe = await _context.Recipes.Where(x => x.ID_Product == id).ToListAsync();
            var getPro = await _context.Products.Where(x => x.ID_Product == id).FirstOrDefaultAsync();
            if(getPro.isAvailabled == true)
            {
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ViewBag.Material = new SelectList(_context.Materials, "ID_Material", "Name_Material");
                if (recipe != null)
                {
                    var list = new RecipeVM()
                    {
                        Recipes = recipe,
                    };
                    return View(list);
                }
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNext(RecipeVM model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem công thức đã có chưa, nếu có thì chỉ bổ sung thêm số lượng
                var getRecipe = await _context.Recipes.Where(x=>x.ID_Material == model.ID_Material && x.ID_Product == model.ID_Product).FirstOrDefaultAsync();  
                if(getRecipe == null)
                {
                    var recipe = new Recipe()
                    {
                        ID_Product = model.ID_Product,
                        ID_Material = model.ID_Material,
                        Material_Quantity = model.Material_Quantity,
                    };

                    _context.Recipes.Add(recipe);
                    _context.SaveChanges();
                }
                else
                {
                    getRecipe.Material_Quantity += model.Material_Quantity;

                    _context.Update(getRecipe);
                    _context.SaveChanges();

                }
            
                return RedirectToAction("CreateNext", "Recipe", new { id = model.ID_Product });
            }
            return View(model);
        }

       

        public async Task<IActionResult> deleteMaterial(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var getRecipe = await _context.Recipes.Where(x=>x.ID_Recipe == id).FirstOrDefaultAsync();
            var getPro = getRecipe.ID_Product;
            _context.Recipes.Remove(getRecipe);
            _context.SaveChanges();

            return RedirectToAction("CreateNext", "Recipe", new { id = getPro });
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.Recipes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Recipe model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Recipe()
                    {
                        ID_Recipe = model.ID_Recipe,
                        ID_Product = model.ID_Product,
                        ID_Material = model.ID_Material,
                        Material_Quantity = model.Material_Quantity,
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
            var item = await _context.Recipes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Recipes.FindAsync(id);
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
            var item = await _context.Recipes.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}

