using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ControlViajes.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    //[Produces("application/json")]
    public class MessageController : Controller
    {

        private IHubContext<MessageHub> _hubContext;

        public MessageController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost]
        public IActionResult Post()
        {
            _hubContext.Clients.All.SendAsync("send", "Hello from the server");
            return Ok();
        }
    }
}
