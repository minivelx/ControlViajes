﻿using System;
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
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            try
            {
                List<Cliente> lstClientes = LCliente.ConsultarClientes(_context);
                return Json(new { success = true, message = lstClientes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Clientes/activos
        [Route("Activos")]
        [HttpGet]
        public async Task<IActionResult> GetClientesActivos()
        {
            try
            {
                List<Cliente> lstClientes = LCliente.ConsultarClientesActivos(_context);
                return Json(new { success = true, message = lstClientes });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClienteById([FromRoute] int id)
        {
            try
            {
                Cliente Cliente = LCliente.ConsultarClientePorId(id, _context);

                if (Cliente == null)
                {
                    return Json(new { success = false, message = "Cliente no encontrado" });
                }

                return Json(new { success = true, message = Cliente });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente Cliente)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                LCliente.GuardarCliente(Cliente, _context);
                return Json(new { success = true, message = "Cliente guardado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // PUT: api/Clientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente([FromRoute] int id, [FromBody] Cliente Cliente)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {

                if (Cliente.Id != id)
                {
                    return Json(new { success = false, message = "No se pude editar el id del Cliente" });
                }

                LCliente.EditarCliente(Cliente, _context);
                return Json(new { success = true, message = "Cliente editado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] int id)
        {
            try
            {
                LCliente.EliminarCliente(id, _context);
                return Json(new { success = true, message = "Cliente eliminado correctamente" });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

    }
}