using Flower_Models;
using Flower_Repository;
using FlowerShop_Web.Chat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace FlowerShop_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChatAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<ChatHub> _hubContext;
        public ChatAdminController(IHubContext<ChatHub> hubContext, ApplicationDbContext context)
        {
            _hubContext = hubContext;
            _context = context;
            //_userManager = userManager;
        }



        public async Task<IActionResult> Index(string idRoom = null)
        {
            var listRoom = _context.Rooms.ToList();
            List<Room> noResponseMess = new List<Room>();
            foreach (var room in listRoom)
            {
                var lastMessage = await _context.Messages
                .Where(x => x.IdRoom == room.Id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefaultAsync();

                if (lastMessage != null && lastMessage.UserType == UserType.User)
                {
                    noResponseMess.Add(room);
                }
            }
            ViewBag.noResponseMess = noResponseMess;
            // Sắp xếp listRoom
            listRoom.Sort((room1, room2) =>
            {
                // Kiểm tra xem room1 có trong noResponseMess hay không
                bool isInNoResponseMess1 = noResponseMess.Contains(room1);

                // Kiểm tra xem room2 có trong noResponseMess hay không
                bool isInNoResponseMess2 = noResponseMess.Contains(room2);

                // Xử lý logic sắp xếp
                if (isInNoResponseMess1 && !isInNoResponseMess2)
                {
                    return -1; // room1 sẽ được đưa lên đầu
                }
                else if (!isInNoResponseMess1 && isInNoResponseMess2)
                {
                    return 1; // room2 sẽ được đưa lên đầu
                }
                else
                {
                    return 0; // Giữ nguyên thứ tự
                }
            });
            if (!string.IsNullOrEmpty(idRoom))
            {
                var listMess = _context.Messages.Where(x => x.IdRoom == idRoom).ToList();
                ViewBag.listMess = listMess;
                ViewBag.room = idRoom;
            }
            else
            {
                var listMess = _context.Messages.Where(x => x.IdRoom == listRoom[0].Id);
                ViewBag.listMess = listMess;
                ViewBag.room = listRoom[0].Id;
            }
            return View(listRoom);
        }
        //public IActionResult AddRoom(string roomName)
        //{
        //    // Thêm phòng vào cơ sở dữ liệu
        //    var newRoom = new Room { Name = roomName };
        //    _context.Rooms.Add(newRoom);
        //    _context.SaveChanges();

        //    // Thêm phòng vào SignalR
        //    _hubContext.Clients.All.SendAsync("AddRoom", roomName);

        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult> SendMessage(string user, string message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", user, message);
            return Ok();
        }

    }
}
