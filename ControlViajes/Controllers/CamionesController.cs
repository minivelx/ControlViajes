using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlViajes;
using Entidades;
using Logica;
using Microsoft.AspNetCore.Mvc;

namespace Convidarte.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
    [Route("api/[controller]")]
    [Produces("application/json")]
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
                List<Camion> lstCamiones = await LCamion.ConsultarCamiones(_context);
                return Json(new { success = true, message = lstCamiones });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Camiones/activos
        [Route("Activos")]
        [HttpGet]
        public async Task<IActionResult> GetCamionesActivos()
        {
            try
            {
                List<Camion> lstCamiones = await LCamion.ConsultarCamionesActivos(_context);
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
        public async Task<IActionResult> GetCamionById([FromRoute] int id)
        {
            try
            {
                Camion Camion = await LCamion.ConsultarCamionPorId(id, _context);

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

        // POST: api/Camiones
        [HttpPost]
        public async Task<IActionResult> PostCamion([FromBody] Camion Camion)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                await LCamion.GuardarCamion(Camion, _context);
                return Json(new { success = true, message = "Camión guardado correctamente" });
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

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {

                if (Camion.Id != id)
                {                    
                    return Json(new { success = false, message = "No se pude editar el id del Camión" });
                }

                await LCamion.EditarCamion(Camion, _context);
                return Json(new { success = true, message = "Camión editado correctamente" });
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
                await LCamion.EliminarCamion(id, _context);
                return Json(new { success = true, message = "Camión eliminado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }
 
    }
}