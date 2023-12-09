using Flower_Models;
using Flower_Repository;
using FlowerShop_Web.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FlowerShop_Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ApplicationDbContext _context;

        public ChatController(IHubContext<ChatHub> hubContext
        , UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _hubContext = hubContext;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = "/Chat/Index" });
            }

            string nameUser = user.Id.ToString();
            ViewBag.Id = nameUser;
            ViewBag.nameUser = user.UserName;

            var room = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (room == null)
            {
                Room newRoom = new Room()
                {
                    Id = user.Id,
                };
                _context.Rooms.Add(newRoom);
                _context.SaveChanges();
            }

            var listMess = await _context.Messages.Where(x => x.IdRoom == room.Id).ToListAsync();

            return View(listMess);
        }
        public async Task<IActionResult> SendMessage(string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok();
        }
        //public async Task<IActionResult> SendMessageToRoom(string user, string message)
        //{
        //    await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
        //    return Ok();
        //}
    }
}
