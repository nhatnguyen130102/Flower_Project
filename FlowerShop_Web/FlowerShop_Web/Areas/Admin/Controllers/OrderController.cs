using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
   
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
