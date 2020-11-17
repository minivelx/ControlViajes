using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ControlViajes.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    //[Produces("application/json")]
    public class MessageController : Controller
    {

        private IHubContext<ContadorHub> _hubContext;

        public MessageController(IHubContext<ContadorHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _hubContext.Clients.All.SendAsync("clave", "Hello from the server");
            return Ok();
        }
    }
}
