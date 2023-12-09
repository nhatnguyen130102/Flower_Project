using DesignPattern;
using Elfie.Serialization;
using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Collections.Immutable;
using System.Net.WebSockets;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]


    public class BillController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Order _order;


        public BillController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, Order order)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _order = order;
        }

        // Xem danh sách Bill của cửa hàng
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Index", "Home", new { area = "" });
            if (!User.IsInRole("Manager"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (user.ID_Shop == null)
            {
                return NotFound();
            }
            var list = await _context.Bills.Where(x => x.ID_Shop == user.ID_Shop).ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bill model)
        {
            if (ModelState.IsValid)
            {
                await _context.Bills.AddAsync(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Bill model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Bill()
                    {
                        ID_Bill = model.ID_Bill,
                        ID_Customer = model.ID_Customer,
                        ID_Voucher = model.ID_Voucher,
                        Total_Bill = model.Total_Bill,
                        CreatedAt = model.CreatedAt,
                        DeliveredAt = model.DeliveredAt,
                        BillStatus = model.BillStatus,
                        Name = model.Name,
                        Phone = model.Phone,
                    };
                    _context.Bills.Update(item);
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
            var item = await _context.Bills.FindAsync(id);
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
            var item = await _context.Bills.FindAsync(id);
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
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> handleStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
                return NotFound();

            if (item.HandleStatus == true)
                item.HandleStatus = false;
            else
                item.HandleStatus = true;
            _context.Bills.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> deliveredStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
                return NotFound();
            if (item.DeliveredStatus == true)
            {
                item.DeliveredStatus = false;
            }
            else
            {
                item.DeliveredStatus = true;

            }
            _context.Bills.Update(item);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> billStatus(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var item = await _context.Bills.FindAsync(id);
            if (item == null)
                return NotFound();
            if (item.BillStatus == true)
            {
                item.BillStatus = false;
            }
            else
            {
                item.BillStatus = true;

            }
            _context.Bills.Update(item);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> listBill(string? list)
        {
            if (list == null)
            {
                return RedirectToAction("Index");
            }

            // cắt chuỗi nhận về ra thành các id và lưu và array
            string[] listID = list.Split(",");

            List<int> idMaterial = new List<int>();
            List<int> Quantity = new List<int>();

            // duyệt danh sách Bill
            foreach (var idBill in listID)
            {
                int id = int.Parse(idBill);

                // lấy ra từng bill
                var getBill = await _context.Bills.Where(x => x.ID_Bill == id).FirstOrDefaultAsync();
                if (getBill != null)
                {
                    getBill.BillStatus = true;


                    // lấy ra từng billdetail
                    var getBillDetail = await _context.BillDetails.Where(x => x.ID_Bill == getBill.ID_Bill).ToListAsync();

                    // duyệt danh sách các sản phẩm trong Bill
                    foreach (var a in getBillDetail)
                    {
                        if (a != null)
                        {
                            // lấy ra sản phẩm trong billdetail
                            var getPro = await _context.Products.Where(x => x.ID_Product == a.ID_Product).FirstOrDefaultAsync();
                            if (getPro != null)
                            {
                                // lấy công thức của sản phẩm
                                var getRecipe = await _context.Recipes.Where(x => x.ID_Product == getPro.ID_Product).ToListAsync();
                                if (getRecipe != null)
                                {
                                    // duyệt từng phần trong công thức
                                    foreach (var b in getRecipe)
                                    {
                                        // kiểm tra tồn tại
                                        if (!idMaterial.Contains(b.ID_Material))
                                        {
                                            // thêm nguyên liệu mới vào danh sách
                                            idMaterial.Add(b.ID_Material);

                                            // thêm số lượng vào danh sách
                                            Quantity.Add(b.Material_Quantity * a.Product_Quantity);
                                        }
                                        else
                                        {
                                            // thêm số lượng vào vị trí nguyên liệu đã tồn tại
                                            Quantity.Insert(idMaterial.IndexOf(b.ID_Material), b.Material_Quantity * a.Product_Quantity);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    _context.Bills.Update(getBill);
                    _context.SaveChanges();
                }
            }

            var getUser = await _userManager.GetUserAsync(User);
            if (getUser != null)
            {
                var getSRD = await _context.StockReceivedDockets.Where(x => x.CreatedAt == DateTime.Today.Date && x.IsActive == false).FirstOrDefaultAsync();
                if (getSRD == null) // tạo mới toàn bộ
                {
                    // tạo ra phiếu nhập
                    StockReceivedDocket newSRD = new StockReceivedDocket();

                    newSRD.ID_Shop = (int)getUser.ID_Shop;
                    newSRD.CreatedAt = DateTime.Today;
                    newSRD.IsActive = false;
                    newSRD.Received = false;
                    newSRD.ReceivedAt = null;

                    await _context.StockReceivedDockets.AddAsync(newSRD);
                    await _context.SaveChangesAsync();

                    // tạo ra chi tiết cả phiếu nhập
                    foreach (var item in idMaterial)
                    {
                        StockReceivedDocketDetails newSRDD = new StockReceivedDocketDetails();

                        newSRDD.ID_StockReceivedDocket = newSRD.ID_StockReceivedDocket;
                        newSRDD.ID_Material = item;
                        newSRDD.StockReceived_Quantity = Quantity[idMaterial.IndexOf(item)];

                        await _context.StockReceivedDocketDetails.AddAsync(newSRDD);
                        await _context.SaveChangesAsync();
                    }
                }
                else // cộng dồn vào cái đã tạo
                {
                    List<int> newIDMaterial = new List<int>();
                    List<int> newQuantity = new List<int>();

                    // lấy chi tiết phiếu nhập đã tồn tại
                    var getSRDD = await _context.StockReceivedDocketDetails.Where(x => x.ID_StockReceivedDocket == getSRD.ID_StockReceivedDocket).ToListAsync();

                    // lấy từng thành phần trong chi tiết phiếu nhập
                    foreach (var item in getSRDD)
                    {
                        // kiểm tra tồn tại trong danh sách nguyên liệu
                        if (idMaterial.Contains(item.ID_Material))
                        {
                            var updateSRDD = await _context.StockReceivedDocketDetails.Where(x => x.ID_Material == item.ID_Material && x.ID_StockReceivedDocket == item.ID_StockReceivedDocket).FirstOrDefaultAsync();
                            if (updateSRDD != null)
                            {
                                updateSRDD.StockReceived_Quantity += Quantity[idMaterial.IndexOf(item.ID_Material)];

                                _context.StockReceivedDocketDetails.Update(updateSRDD);
                                _context.SaveChanges();
                            }

                            Quantity[idMaterial.IndexOf(item.ID_Material)] = 0;
                        }
                    }

                    foreach (var item in idMaterial)
                    {
                        if (Quantity[idMaterial.IndexOf(item)] != 0)
                        {
                            StockReceivedDocketDetails newSRDD = new StockReceivedDocketDetails();

                            newSRDD.ID_StockReceivedDocket = getSRD.ID_StockReceivedDocket;
                            newSRDD.ID_Material = item;
                            newSRDD.StockReceived_Quantity = Quantity[idMaterial.IndexOf(item)];

                            await _context.StockReceivedDocketDetails.AddAsync(newSRDD);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> billDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var getBill = await _context.Bills.Where(x => x.ID_Bill == id).FirstOrDefaultAsync();
            var getBillDetail = await _context.BillDetails.Where(x => x.ID_Bill == id).ToListAsync();

            List<BillDetails> list = getBillDetail;
            var bill = new Bill()
            {
                ID_Bill = getBill.ID_Bill,
                ID_Customer = getBill.ID_Customer,
                ID_Voucher = getBill.ID_Voucher,
                Total_Bill = getBill.Total_Bill,
                Subtotal = getBill.Subtotal,
                CreatedAt = getBill.CreatedAt,
                DeliveredAt = getBill.DeliveredAt,
                BillStatus = getBill.BillStatus,
                DeliveredStatus = getBill.DeliveredStatus,
                HandleStatus = getBill.HandleStatus,
                Canceled = getBill.Canceled,
                Name = getBill.Name,
                Phone = getBill.Phone,
                Name_Order = getBill.Name_Order,
                Phone_Order = getBill.Phone_Order,
                ID_Shop = getBill.ID_Shop,
                City = getBill.City,
                Address = getBill.Address,
                Message = getBill.Message,
                BillDetails = getBillDetail,
            };

            return View(bill);
        }

        public IActionResult ConfirmOrder()
        {
            _order.ConfirmOrder();
            return View("Index", _order);
        }

        public IActionResult ShipOrder()
        {
            _order.ShipOrder();
            return View("Index", _order);
        }

        public IActionResult CompleteOrder()
        {
            _order.CompleteOrder();
            return View("Index", _order);
        }
    }
}