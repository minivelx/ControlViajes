using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlViajes;
using Entidades;
using Entidades.Interfaces;
using Entidades.ViewModel;
using Logica;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Convidarte.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ViajesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private IHubContext<ContadorHub> _hubContext;

        public ViajesController(ApplicationDbContext context, IHubContext<ContadorHub> hubContext, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _hubContext = hubContext;
            _emailSender = emailSender;
            _configuration = configuration;
        }        

        [AllowAnonymous]
        [HttpGet("Prueba")]
        public async Task<IActionResult> Prueba()
        {
            var message = await LViaje.getDashboard(_context);
            await _hubContext.Clients.All.SendAsync("dashboard", new { success = true, message });

            //LNotificacionFirebase.consumirNotificacionPushViaje("b7a3edb2-ca49-490b-bac0-05e2d11cfa72", _configuration, _context);
            return Json(new { success = true, message = message });
        }

        // GET: api/Viajes        
        [HttpGet]
        public async Task<IActionResult> GetViajes()
        {
            try
            {
                List<Viaje> lstViajes = await LViaje.ConsultarViajesDia(_context);
                return Json(new { success = true, message = lstViajes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Viajes/MisViajes        
        [HttpGet("MisViajes")]
        public async Task<IActionResult> GetMisViajes()
        {
            try
            {
                string userId = User.getUserId();
                var roles = User.getRoles();

                if(roles.Any(x=> x.ToLower() == "administrador"))
                {
                    List<Viaje> lstResult = await LViaje.ConsultarViajesDia(_context);
                    return Json(new { success = true, message = lstResult.Where(x=> x.NumeroEstado != 3) });
                }

                if (roles.Any(x => x.ToLower() == "cliente"))
                {
                    var usuario = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
                    List<Viaje> lstResult = await LViaje.ConsultarViajesDiaCliente(usuario.IdCliente.Value, _context);
                    return Json(new { success = true, message = lstResult.Where(x => x.NumeroEstado != 3) });
                }

                List<Viaje> lstViajes = await LViaje.ConsultarMisViajes(userId, _context);
                return Json(new { success = true, message = lstViajes.Where(x => x.NumeroEstado != 3) });
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
                Viaje.FechaRegistro = DateTime.Now;
                Viaje.UsuarioRegistro = User?.getUserId();
                await LViaje.GuardarViaje(Viaje, _context);
                var message = await LViaje.getDashboard(_context);
                await _hubContext.Clients.All.SendAsync("dashboard", new { success = true, message });

                try
                {
                    Viaje = await LViaje.ConsultarViajePorId(Viaje.Id, _context);
                    await LEmail.EnviarEmailAsignacionViaje(Viaje, _emailSender);

                    if (Viaje.ValorAnticipo > 0)
                    {
                        await LEmail.EnviarEmailValorAnticipo(Viaje, _configuration, _emailSender);
                    }
                }
                catch
                {
                }               

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
                var message = await LViaje.getDashboard(_context);
                await _hubContext.Clients.All.SendAsync("dashboard", new { success = true, message });

                try
                {
                    Viaje = await LViaje.ConsultarViajePorId(Viaje.Id, _context);
                    await LEmail.EditarEmailAsignacionViaje(Viaje, _emailSender);

                    if (Viaje.ValorAnticipo > 0)
                    {
                        await LEmail.EnviarEmailValorAnticipo(Viaje, _configuration, _emailSender);
                    }
                }
                catch
                {
                }

                return Json(new { success = true, message = "Registro editado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Viajes/ActualizarEstado
        [HttpPost("ActualizarEstado")]
        public async Task<IActionResult> ActualizarEstadoViaje([FromBody] Viaje Viaje)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                await LViaje.ActualizarEstadoViaje(Viaje.Id, _context);
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

        // POST: api/Viajes/Historico
        [HttpPost("Historico")]
        public async Task<IActionResult> HistoricoViajes([FromBody] FiltroViewModel filtro)
        {
            try
            {
                var lstViajes = await LViaje.ConsultarHistoricoViajes(filtro, _context);
                return Json(new { success = true, message = lstViajes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Viajes/OcupacionDiaria
        [HttpPost("OcupacionDiaria")]
        public async Task<IActionResult> OcupacionDiaria([FromBody] FiltroViewModel filtro)
        {
            try
            {
                var lstViajes = await LViaje.ConsultarOcupacionDiaria(filtro, _context);
                return Json(new { success = true, message = lstViajes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }        

    }
}