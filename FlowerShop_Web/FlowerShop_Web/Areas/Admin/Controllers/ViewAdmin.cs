using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using FlowerShop_Web.Models.Flower_ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Net.WebSockets;
using System.Security.Policy;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ViewAdmin : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ViewAdmin(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // xem toàn bộ bill của các cửa hàng
        public async Task<IActionResult> allBill(string? searching, string? id)
        {
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");

            int? shopId = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;

            var query = _context.Bills.Select(model => new BillAdminVM
            {
                ID_Bill = model.ID_Bill,
                ID_Shop = model.ID_Shop,
                ID_Customer = model.ID_Customer,
                ID_Voucher = model.ID_Voucher,
                Total_Bill = model.Total_Bill,
                CreatedAt = model.CreatedAt,
                DeliveredAt = model.DeliveredAt,
                BillStatus = model.BillStatus,
                Name = model.Name,
                Phone = model.Phone,
                Message = model.Message,
                Address = model.Address,
            });

            if (shopId.HasValue && shopId != -1)
            {
                query = query.Where(b => b.ID_Shop == shopId);

                var shop = await _context.Shops.Where(x => x.ID_Shop == shopId).FirstOrDefaultAsync();

                ViewBag.SelectedShop = shop.Name_Shop;
            }
            else if(shopId.HasValue && shopId == -1)
            {
                ViewBag.SelectedShop = "All shop";
            }

            if (!string.IsNullOrEmpty(searching))
            {
                query = query.Where(b => b.Name.Contains(searching) || b.Address.Contains(searching));
            }

            var model = query.ToList();
            return View(model);
        }

        // xem toàn bộ phiếu nhập của các cửa hàng
        public async Task<IActionResult> allSRD(string? id)
        {
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");

            int? shopId = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;

            var query = _context.StockReceivedDockets.Select(model => new SRDAdminVM
            {
                ID_StockReceivedDocket = model.ID_StockReceivedDocket,
                ID_Shop = model.ID_Shop,
                CreatedAt = model.CreatedAt,
                IsActive = model.IsActive,
                Received = model.Received, 
                ReceivedAt = model.ReceivedAt
            });

            if (shopId.HasValue)
            {
                query = query.Where(b => b.ID_Shop == shopId);

                var shop = await _context.Shops.Where(x => x.ID_Shop == shopId).FirstOrDefaultAsync();

                ViewBag.SelectedShop = shop.Name_Shop;
            }
            else if (shopId.HasValue && shopId == -1)
            {
                ViewBag.SelectedShop = "All shop";
            }

            var model = query.ToList();
            return View(model);
        }

        // xem toàn bộ kho nguyên liệu của cửa hàng
        public async Task<IActionResult> allMaterialWH(string? id)
        {
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");

            int? shopId = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;

            var query = _context.MaterialWarehouses.Select(model => new MaterialWHAdminVM
            {
                ID = model.ID,
                ID_Shop = model.ID_Shop,
                ID_Material = model.ID_Material,
                InStock_Quantity = model.InStock_Quantity, 
                Sold_Quantity = model.Sold_Quantity
            });

            if (shopId.HasValue)
            {
                query = query.Where(b => b.ID_Shop == shopId);

                var shop = await _context.Shops.Where(x => x.ID_Shop == shopId).FirstOrDefaultAsync();

                ViewBag.SelectedShop = shop.Name_Shop;
            }
            else if (shopId.HasValue && shopId == -1)
            {
                ViewBag.SelectedShop = "All shop";
            }

            var model = query.ToList();
            return View(model);
        }

        // xem toàn bộ kho sản phẩm của cửa hàng
        public async Task<IActionResult> allProductWH(string? id)
        {
            ViewBag.Shop = new SelectList(_context.Shops, "ID_Shop", "Name_Shop");

            int? shopId = !string.IsNullOrEmpty(id) ? int.Parse(id) : (int?)null;

            var query = _context.ProductWarehouses.Select(model => new ProductWHAdminVM
            {
                ID = model.ID,
                ID_Shop = model.ID_Shop,
                ID_Product = model.ID_Product,
                InStock_Quantity = model.InStock_Quantity,
                Sold_Quantity = model.Sold_Quantity
            });

            if (shopId.HasValue)
            {
                query = query.Where(b => b.ID_Shop == shopId);

                var shop = await _context.Shops.Where(x => x.ID_Shop == shopId).FirstOrDefaultAsync();

                ViewBag.SelectedShop = shop.Name_Shop;
            }
            else if (shopId.HasValue && shopId == -1)
            {
                ViewBag.SelectedShop = "All shop";
            }

            var model = query.ToList();
            return View(model);
        }

        public async Task<IActionResult> detailBill1(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("allBill");
            }
            var item = await _context.Bills.Where(x=>x.ID_Bill == id).FirstOrDefaultAsync();
            if (item == null)
            {
                return RedirectToAction("allBill");
            }

            return View(item);
        }

        public async Task<IActionResult> detailBill(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("allBill");
            }

            var detail = await _context.BillDetails.Where(x => x.ID_Bill == id).ToListAsync();
            if (detail == null)
            {
                return RedirectToAction("allBill");
            }

            return View(detail);
        }

        public async Task<IActionResult> detailSRD(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("allSRD");
            }

            var detail = await _context.StockReceivedDocketDetails.Where(x => x.ID_StockReceivedDocket == id).ToListAsync();
            if (detail == null)
            {
                return RedirectToAction("allSRD");
            }

            return View(detail);
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

            return RedirectToAction("allSRD","ViewAdmin");
        }
    }
}
