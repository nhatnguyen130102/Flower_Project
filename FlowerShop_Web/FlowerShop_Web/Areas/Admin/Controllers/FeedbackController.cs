using FlowerShop_Web.Models.Flower_Models;
using FloweShop_Web.Models.Flower_Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbackController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public FeedbackController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var listFeedback = _context.Feedbacks.ToList();
            return View(listFeedback);
        }
        public IActionResult Delete(int? id)
        {
            var feedback = _context.Feedbacks.FirstOrDefault(x => x.FeedbackId == id);  
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
