
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Course_project.Services
{
    public class CommentHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            if (Context.User.Identity.Name != null)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, Context.User.Identity.Name);
                return base.OnConnectedAsync();
            }
            Groups.AddToGroupAsync(Context.ConnectionId, "default");
            return base.OnConnectedAsync();
        }


        public async Task SendMessage(string user, string message, int itemId)
        {
            //message send to all users
            await this.Clients.All.SendAsync("ReceiveMessage", user, message, itemId);
        }

        public Task SendMessageToGroup(string sender, string receiver, string message)
        {
            //message send to receiver only
            return Clients.Group(receiver).SendAsync("ReceiveMessage", sender, message);
        }
    }
}
