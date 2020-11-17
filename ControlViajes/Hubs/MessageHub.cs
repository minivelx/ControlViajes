using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ControlViajes
{
    public class MessageHub : Hub
    {
        public Task Send(string message)
        {
            return Clients.All.SendAsync("Send", message);
        }
    }
}
