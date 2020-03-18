using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
namespace Restoran.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string service)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message,service);
            
        }

       
        public Task SendPrivateMessage(string user, string message,string service)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message,service);
        }
        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);


        }

    }
}
