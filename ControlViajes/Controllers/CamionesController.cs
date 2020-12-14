﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ControlViajes;
using Entidades;
using Logica;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Convidarte.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CamionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHubContext<ContadorHub> _hubContext;

        public CamionesController(ApplicationDbContext context, IHubContext<ContadorHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
                Camion.FechaRegistro = DateTime.Now;
                Camion.UsuarioRegistro = User?.getUserId();
                await LCamion.GuardarCamion(Camion, _context);

                if (Camion.EsPropio)
                {
                    var message = await LViaje.getDashboard(_context);
                    await _hubContext.Clients.All.SendAsync("dashboard", new { success = true, message });
                }

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

                if (Camion.EsPropio)
                {
                    var message = await LViaje.getDashboard(_context);
                    await _hubContext.Clients.All.SendAsync("dashboard", new { success = true, message });
                }

                return Json(new { success = true, message = "Camión editado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // PUT: api/Camiones/5
        [HttpPut("CambiarEstadoTaller/{id}")]
        public async Task<IActionResult> CambiarEstadoTaller([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {

                await LTaller.RealizarMovimientoTaller(id, _context);

                var message = await LViaje.getDashboard(_context);
                await _hubContext.Clients.All.SendAsync("dashboard", new { success = true, message });

                return Json(new { success = true, message = "Estado actualizado correctamente" });
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