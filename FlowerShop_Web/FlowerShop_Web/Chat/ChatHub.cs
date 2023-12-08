using Flower_Models;
using Flower_Repository;
using Microsoft.AspNetCore.SignalR;

namespace FlowerShop_Web.Chat
{

    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }
        //public override Task OnConnectedAsync()
        //{
        //    Groups.AddToGroupAsync(Context.ConnectionId, "user2");
        //    return base.OnConnectedAsync();
        //    //HttpContext httpContext = Context.GetHttpContext();

        //    //string receiver = httpContext.Request.Query["userid"];
        //    //string sender = Context.User.Claims.FirstOrDefault().Value;

        //    //Groups.AddToGroupAsync(Context.ConnectionId, sender);
        //    //if (!string.IsNullOrEmpty(receiver))
        //    //{
        //    //    Groups.AddToGroupAsync(Context.ConnectionId, receiver);
        //    //}


        //}
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageToRoom(string roomName, string message, string name)
        {
            if (name == "Admin")
            {
                Message newMess = new Message()
                {
                    IdRoom = roomName,
                    SentAt = DateTime.Now,
                    Content = message,
                    UserType = UserType.Admin
                };
                _context.Messages.Add(newMess);
                _context.SaveChanges();
            }
            else
            {
                Message newMess = new Message()
                {
                    IdRoom = roomName,
                    SentAt = DateTime.Now,
                    Content = message,
                    UserType = UserType.User
                };
                _context.Messages.Add(newMess);
                _context.SaveChanges();
            }
            //await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId}: {message}", message);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", name, message);


        }

        public async Task JoinRoom(string roomName)
        {

            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            //await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} joined {roomName}");
        }
    }
}
