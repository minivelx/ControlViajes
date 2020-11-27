using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlViajes;
using Entidades;
using Logica;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Convidarte.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ViajesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ViajesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Viajes
        [HttpGet]
        public async Task<IActionResult> GetViajes()
        {
            try
            {
                List<Viaje> lstViajes = await LViaje.ConsultarViajes(_context);
                return Json(new { success = true, message = lstViajes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Viajes/misViajes        
        [HttpGet("misViajes")]
        public async Task<IActionResult> GetMisViajes()
        {
            try
            {
                string userId = User.getUserId();
                List<Viaje> lstViajes = await LViaje.ConsultarMisViajes(userId, _context);
                return Json(new { success = true, message = lstViajes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Viajes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetViajeById([FromRoute] int id)
        {
            try
            {
                Viaje Viaje = await LViaje.ConsultarViajePorId(id, _context);

                if (Viaje == null)
                {
                    return Json(new { success = false, message = "Viaje no encontrado" });
                }

                return Json(new { success = true, message = Viaje });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Viajes
        [HttpPost]
        public async Task<IActionResult> PostViaje([FromBody] Viaje Viaje)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                await LViaje.GuardarViaje(Viaje, _context);
                return Json(new { success = true, message = "Registro guardado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Viajes/actualizarEstado
        [HttpPost("actualizarEstado")]
        public async Task<IActionResult> ActualizarEstadoViaje([FromBody] Viaje Viaje)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                await LViaje.ActualizarEstadoViaje(Viaje.Id, _context);
                return Json(new { success = true, message = "Registro guardado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // PUT: api/Viajes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutViaje([FromRoute] int id, [FromBody] Viaje Viaje)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                if (Viaje.Id != id)
                {
                    return Json(new { success = false, message = "No se pude editar el id del Registro" });
                }

                await LViaje.EditarViaje(Viaje, _context);
                return Json(new { success = true, message = "Registro editado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

    }
}