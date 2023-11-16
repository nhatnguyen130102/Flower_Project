using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Controllers
{
    public class PostController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PostController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }
        public IActionResult Post()
        {
            var item = _context.Posts.Include(x => x.Category).ToList();
            return View(item);
        }
        public async Task<IActionResult> PostDetail(int? id)
        {
            if (ModelState.IsValid)
            {
                var item = await _context.Posts.FindAsync(id);
                return View(item);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
