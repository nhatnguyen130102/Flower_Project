using FlowerShop_Web.Models.DesignPattern;
using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly Import_ExportService _service;
        private readonly ApplicationDbContext _context;
        private readonly IMemoryCache _memoryCache;
        private readonly MementoPattern _careTaker;

        public PostController(ApplicationDbContext context, Import_ExportService service, IMemoryCache memoryCache , MementoPattern careTacker )
        {
            _context = context;
            _service = service;
            _memoryCache = memoryCache;
            _careTaker = careTacker;
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public IActionResult Index()
        {
            var item = _context.Posts.Include(x => x.Category).ToList();
            return View(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = new SelectList(_context.Categories, "ID_Category", "Name_Category");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post model)
        {
            if (ModelState.IsValid)
            {
                await _context.Posts.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Category = new SelectList(_context.Categories,"ID_Category","Name_Category");
            var item = await _context.Posts.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Post model, string submit)
        {
            if (ModelState.IsValid)
            {
                var item = new Post()
                {
                    ID_Post = model.ID_Post,
                    Title = model.Title,
                    ID_Category = model.ID_Category,
                    CreatedAt = model.CreatedAt,
                    CreatedBy = model.CreatedBy,
                    ViewCount = model.ViewCount,
                    Content = model.Content
                };


                if (submit.ToString() == "Save")
                {
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                if (submit.ToString() == "Redo")
                {
                    var memento = _memoryCache.Get<Memento>("Memento");
                    _careTaker.StorePost = memento;

                    item.RestorePost(_careTaker.StorePost);

                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                if(submit.ToString() == "SaveTage")
                {
                    Post post = new Post();
                    post = item.ShallowCopy();
                    post = item.DeepCopy();
                    Post itemOlder = _context.Posts.Where(x=>x.ID_Post == post.ID_Post).FirstOrDefault();
                    _careTaker.StorePost = itemOlder.CreateStored(itemOlder);
                    _careTaker.BackUpToCache(_careTaker.StorePost);
                }

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            var item = await _context.Posts.FindAsync(id);
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
            var item = await _context.Posts.FindAsync(id);
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
            var item = await _context.Posts.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel()
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<Post> listModel = await _context.Posts.ToListAsync();

            // Tạo một danh sách mới từ VM
            PostServiceVM model = new PostServiceVM();

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
            var fileName = $"PostExport_{today:yyyyMMddHHmmss}.xlsx";

            // Xuất dữ liệu ra Excel với tên tệp duy nhất
            _service.ExportToExcel(listModel, colum, fileName);

            // Chuyển hướng đến trang Index của Recipe controller
            return RedirectToAction("Index", "Post");
        }

        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File not selected");
            }

            List<Post> listModel = await _context.Posts.ToListAsync();

            // Tạo một danh sách mới từ VM
            PostServiceVM model = new PostServiceVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<Post> list = _service.ImportFromExcel<Post>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var post = new Post()
                {
                    ID_Category = item.ID_Category,
                    Title = item.Title,
                    Content = item.Content,
                    CreatedAt = item.CreatedAt,
                    CreatedBy = item.CreatedBy,
                    ViewCount = item.ViewCount,
                };

                _context.Posts.Add(post);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Post");
        }

    }
}
