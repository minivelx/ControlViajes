using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entidades;
using Logica;
using Microsoft.AspNetCore.Mvc;



namespace Convidarte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CamionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CamionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Camiones
        [HttpGet]
        public async Task<IActionResult> GetCamiones()
        {
            try
            {
                List<Camion> lstCamiones = LCamion.ConsultarCamionesActivos(_context);
                return Json(new { success = true, message = lstCamiones });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Camiones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCamion([FromRoute] int id)
        {
            try
            {
                Camion Camion = LCamion.ConsultarCamionPorId(id, _context);

                if(Camion == null)
                {
                    return Json(new { success = false, message = "Camion no encontrado" });
                }

                return Json(new { success = true, message = Camion });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // PUT: api/Camiones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamion([FromRoute] int id, [FromBody] Camion Camion)
        {
            try
            {

                if (Camion.Id != id)
                {
                    return Json(new { success = false, message = "No se pude editar el id del Camion" });
                }

                return Json(new { success = true, message = "Camion editado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Camiones
        [HttpPost]
        public async Task<IActionResult> PostCamion([FromBody] Camion Camion)
        {
            try
            {
                LCamion.GuardarCamion(Camion, _context);
                return Json(new { success = true, message = "Camion guardado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // DELETE: api/Camiones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamion([FromRoute] int id)
        {
            try
            {
                LCamion.EliminarCamion(id, _context);
                return Json(new { success = true, message = "Camion eliminado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }
 
    }
}