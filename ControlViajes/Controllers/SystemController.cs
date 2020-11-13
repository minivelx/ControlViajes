using System;
using Microsoft.AspNetCore.Mvc;

namespace ControlViajes.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SystemController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "System Works, current Date: " +  DateTime.Now.ToString();
        }
        
    }
}
