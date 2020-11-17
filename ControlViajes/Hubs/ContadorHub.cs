using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ControlViajes
{
    public class ContadorHub: Hub
    {
        public async Task GetContador()
        {
            await Clients.Caller.SendAsync("clave", 1);
        }
    }
}
