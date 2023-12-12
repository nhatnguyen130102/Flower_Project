using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_Services;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DiaSymReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    
    public class StockReceivedDocketController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Import_ExportService _service;

        public StockReceivedDocketController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, Import_ExportService service)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Index()    
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
                return NotFound();
            }
            if(user.ID_Shop == null)
            {
                return NotFound();
            }

            var item = await _context.StockReceivedDockets.Where(x => x.ID_Shop == user.ID_Shop).ToListAsync();
            return View(item);

        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewBag.Shop = new SelectList(_context.Shops,"ID_Shop","Name_Shop");
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StockReceivedDocket model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {

                var newSRD = new StockReceivedDocket()
                {
                    ID_Shop = (int)user.ID_Shop,
                    IsActive = false,
                    Received = false,
                };
                await _context.StockReceivedDockets.AddAsync(newSRD);
                await _context.SaveChangesAsync();
                return RedirectToAction("CreateNext", "StockReceivedDocket", new { id = newSRD.ID_StockReceivedDocket });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.StockReceivedDockets.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(StockReceivedDocket model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new StockReceivedDocket()
                    {
                        ID_StockReceivedDocket = model.ID_StockReceivedDocket,
                        ID_Shop = model.ID_Shop,
                        CreatedAt = DateTime.Today
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
            var item = await _context.StockReceivedDockets.FindAsync(id);
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
            var item = await _context.StockReceivedDockets.FindAsync(id);
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
            var item = await _context.StockReceivedDockets.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.StockReceivedDockets.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> CreateSRD()
        {

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound("1");
            }
            var today = DateTime.Now;
            var thenextday = today.AddDays(2);
            // khi nào có user mới thì mở ra
            var listBillThenextday = await _context.Bills.Where(x => x.DeliveredAt.Date == thenextday.Date && x.ID_Shop == user.ID_Shop && x.HandleStatus == true && x.BillStatus == false).ToListAsync();
            if (listBillThenextday is null)
            {
                return NotFound();
                //return RedirectToAction("Index");
            }
            var newSRD = new StockReceivedDocket()
            {
                ID_Shop = (int)user.ID_Shop,
                IsActive = false,
                Received = false,
            };

            await _context.StockReceivedDockets.AddAsync(newSRD);
            await _context.SaveChangesAsync();


            //var listBillTomorrow = await _context.Bills.Where(x => x.DeliveredAt == thenextday && x.ID_Shop == 1).ToListAsync();

            // danh sách nguyên liệu
            List<int> list_ID = new List<int>();

            // danh sách số lượng của nguyên liệu
            List<int> list_Quantity = new List<int>();

            // thực hiện vòng lập để lấy ra danh sách nguyên liệu và danh sách số lượng nguyên liệu
            foreach (var bill in listBillThenextday)
            {
                bill.BillStatus = true;

                _context.Bills.Update(bill);
                _context.SaveChanges();

                // lấy ra từng billdetail để chuẩn bị cho việc tạo danh sách 
                var getBillDetail = await _context.BillDetails.Where(x => x.ID_Bill == bill.ID_Bill).ToListAsync();

                foreach (var billdetail in getBillDetail)
                {
                    // lấy ra sản phẩm từ billdetails
                    var getPro = await _context.Products.Where(x => x.ID_Product == billdetail.ID_Product).FirstOrDefaultAsync();

                    // lấy ra công thức của sản phẩm 
                    var getRecipe = await _context.Recipes.Where(x => x.ID_Product == getPro.ID_Product).ToListAsync();

                    foreach (var recipe in getRecipe)
                    {
                        // kiểm tra nguyên liệu đã có trong danh sách chưa
                        if (!list_ID.Contains(recipe.ID_Material))
                        {
                            // thực hiện thêm nguyên liệu mới vào danh sách
                            list_ID.Add(recipe.ID_Material);

                            // thực hiện thêm số lượng vào danh sách
                            list_Quantity.Add(recipe.Material_Quantity * billdetail.Product_Quantity);
                        }
                        else
                        {
                            // thực hiện cập nhật lại số lượng nguyên liệu tại ví trị trí của nguyên liệu
                            list_Quantity[list_ID.IndexOf(recipe.ID_Material)] += recipe.Material_Quantity * billdetail.Product_Quantity;
                        }
                    }
                }
            }
            //var listMaterial = await _context.Materials.ToListAsync();

            //foreach (var item in listMaterial)
            //{
            //    if (!list_ID.Contains(item.ID_Material))
            //    {
            //        list_ID.Add(item.ID_Material);
            //        list_Quantity.Add(0);
            //    }
            //}

            for (int i = 0; i < list_ID.Count(); i++)
            {
                var newSRDD = new StockReceivedDocketDetails()
                {
                    ID_StockReceivedDocket = newSRD.ID_StockReceivedDocket,
                    ID_Material = list_ID[i],
                    StockReceived_Quantity = list_Quantity[i],
                };
                await _context.StockReceivedDocketDetails.AddAsync(newSRDD);
                await _context.SaveChangesAsync();
            }

            var getSRD = await _context.StockReceivedDockets.Where(x => x.ID_StockReceivedDocket == newSRD.ID_StockReceivedDocket).FirstOrDefaultAsync();
            return RedirectToAction("detailsSRDD", "StockReceivedDocket", new { id = getSRD.ID_StockReceivedDocket });
        }

        public async Task<IActionResult> detailsSRDD(int? id)
        {
            var list = await _context.StockReceivedDocketDetails.Where(x => x.ID_StockReceivedDocket == id).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> CreateNext(int? id)
        {
            // lấy id SRD
            ViewBag.id = id;
            var SRDD = await _context.StockReceivedDocketDetails.Where(x => x.ID_StockReceivedDocket == id).ToListAsync();
            ViewBag.Material = new SelectList(_context.Materials, "ID_Material", "Name_Material");
            if (SRDD != null)
            {
                var list = new SRDDVM()
                {
                    StockReceived_Details = SRDD,
                };
                return View(list);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNext(SRDDVM model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem phiếu nhập đã tồn tại, nếu đã tồn tại thì chỉ cập nhật thêm
                var getSRDD = await _context.StockReceivedDocketDetails.Where(x => x.ID_Material == model.ID_Material && x.ID_StockReceivedDocket == model.ID_StockReceivedDocket).FirstOrDefaultAsync();
                if (getSRDD == null)
                {
                    var newSRDD = new StockReceivedDocketDetails()
                    {
                        ID_Material = model.ID_Material,
                        ID_StockReceivedDocket = model.ID_StockReceivedDocket,
                        StockReceived_Quantity = model.StockReceived_Quantity,
                    };

                    await _context.StockReceivedDocketDetails.AddAsync(newSRDD);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    getSRDD.StockReceived_Quantity += model.StockReceived_Quantity;

                    _context.Update(getSRDD);
                    _context.SaveChanges();
                }

                return RedirectToAction("CreateNext", new { id = model.ID_StockReceivedDocket });
            }

            return View(model);
        }

        public async Task<IActionResult> isActive(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getSRD = await _context.StockReceivedDockets.Where(x => x.ID_StockReceivedDocket == id).FirstOrDefaultAsync();
            if (getSRD == null)
            {
                return NotFound();
            }

            if (getSRD.IsActive == false)
            {
                getSRD.IsActive = true;
                getSRD.CreatedAt = DateTime.Now;
            }
            else
            {
                getSRD.IsActive = false;
                getSRD.CreatedAt = null;
            }

            _context.StockReceivedDockets.Update(getSRD);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Received(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var getSRD = await _context.StockReceivedDockets.Where(x => x.ID_StockReceivedDocket == id).FirstOrDefaultAsync();
            if (getSRD == null)
            {
                return NotFound();
            }

            if (getSRD.Received == false)
            {
                getSRD.Received = true;
                getSRD.ReceivedAt = DateTime.Now;

                var getSRDD = await _context.StockReceivedDocketDetails.Where(x => x.ID_StockReceivedDocket == getSRD.ID_StockReceivedDocket).ToListAsync();

                foreach (var item in getSRDD)
                {
                    var getMWH = await _context.MaterialWarehouses.Where(x => x.ID_Material == item.ID_Material && x.ID_Shop == getSRD.ID_Shop).FirstOrDefaultAsync();
                    if (getMWH == null)
                    {
                        var newMWH = new MaterialWarehouse()
                        {
                            ID_Shop = getSRD.ID_Shop,
                            ID_Material = item.ID_Material,
                            InStock_Quantity = item.StockReceived_Quantity,
                            Sold_Quantity = 0,
                        };
                        await _context.MaterialWarehouses.AddAsync(newMWH);
                        await _context.SaveChangesAsync();

                    }
                    else
                    {
                        getMWH.InStock_Quantity += item.StockReceived_Quantity;

                        _context.MaterialWarehouses.Update(getMWH);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            _context.StockReceivedDockets.Update(getSRD);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }



        public async Task<IActionResult> DetailsSRD(int? id)
        {
            var list = await _context.StockReceivedDocketDetails.Where(x => x.ID_StockReceivedDocket == id).ToListAsync();

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> ExportToExcel(int? id)
        {
            // Lấy dữ liệu từ service để xuất ra Excel
            List<StockReceivedDocketDetails> listModel = await _context.StockReceivedDocketDetails.Where(x=>x.ID_StockReceivedDocket == id).ToListAsync();

            // Tạo một danh sách mới từ VM
            SRDDServiceVM model = new SRDDServiceVM();

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
            var fileName = $"SRDDExport_{today:yyyyMMddHHmmss}.xlsx";

            // Xuất dữ liệu ra Excel với tên tệp duy nhất
            _service.ExportToExcel(listModel, colum, fileName);

            // Chuyển hướng đến trang Index của Recipe controller
            return RedirectToAction("Index", "StockReceivedDocket");
        }


        [HttpPost]
        public async Task<IActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File not selected");
            }

            List<StockReceivedDocketDetails> listModel = await _context.StockReceivedDocketDetails.ToListAsync();

            // Tạo một danh sách mới từ VM
            SRDDServiceVM model = new SRDDServiceVM();

            // Lấy ra các thuộc tính có trong VM
            PropertyInfo[] properties = model.GetType().GetProperties();

            string[] columnHeaders = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                columnHeaders[i] = properties[i].Name;
            }

            List<StockReceivedDocketDetails> list = _service.ImportFromExcel<StockReceivedDocketDetails>(file.OpenReadStream(), columnHeaders);

            // Xử lý dữ liệu (lưu vào cơ sở dữ liệu, xử lý logic khác, ...)
            foreach (var item in list)
            {
                var item2 = new StockReceivedDocketDetails()
                {
                    ID_StockReceivedDocket = item.ID_StockReceivedDocket,
                    ID_Material = item.ID_Material,
                    StockReceived_Quantity = item.StockReceived_Quantity,
                 

                };

                _context.StockReceivedDocketDetails.Add(item2);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
