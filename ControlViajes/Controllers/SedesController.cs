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
    public class SedesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SedesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sedes
        [HttpGet]
        public async Task<IActionResult> GetSedes()
        {
            try
            {
                List<Sede> lstSedes = await LSede.ConsultarSedes(_context);
                return Json(new { success = true, message = lstSedes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Sedes/activos
        [Route("Activos")]
        [HttpGet]
        public async Task<IActionResult> GetSedesActivos()
        {
            try
            {
                List<Sede> lstSedes = await LSede.ConsultarSedesActivos(_context);
                return Json(new { success = true, message = lstSedes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Sedes/PorCliente/5
        [Route("PorCliente/{idCliente}")]
        [HttpGet]
        public async Task<IActionResult> GetSedesPorCliente([FromRoute] int idCliente)
        {
            try
            {
                List<Sede> lstSedes = await LSede.ConsultarSedesPorCliente(idCliente, _context);
                return Json(new { success = true, message = lstSedes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Sedes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSedeById([FromRoute] int id)
        {
            try
            {
                Sede Sede = await LSede.ConsultarSedePorId(id, _context);

                if (Sede == null)
                {
                    return Json(new { success = false, message = "Sede no encontrada" });
                }

                return Json(new { success = true, message = Sede });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Sedes
        [HttpPost]
        public async Task<IActionResult> PostSede([FromBody] Sede Sede)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                await LSede.GuardarSede(Sede, _context);
                return Json(new { success = true, message = "Sede guardada correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // PUT: api/Sedes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSede([FromRoute] int id, [FromBody] Sede Sede)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {

                if (Sede.Id != id)
                {
                    return Json(new { success = false, message = "No se pude editar el id de la Sede" });
                }

                await LSede.EditarSede(Sede, _context);
                return Json(new { success = true, message = "Sede editada correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // DELETE: api/Sedes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSede([FromRoute] int id)
        {
            try
            {
                await LSede.EliminarSede(id, _context);
                return Json(new { success = true, message = "Sede eliminada correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }
    }
}