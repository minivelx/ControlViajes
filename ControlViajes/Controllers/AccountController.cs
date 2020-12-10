using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;

namespace ControlViajes.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _rolManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> rolManager,
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolManager = rolManager;
            _configuration = configuration;
            _context = context;
        }

        [Route("Users")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetUsers()
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                var Usuarios = _userManager.Users.ToList();
                var Clientes = _context.Clientes.ToList();
                Usuarios.ForEach(x => x.Cliente = x.IdCliente == null ? null : Clientes.Find(c => c.Id == x.IdCliente));
                List<Usuario> Users = Usuarios.Select(x => new Usuario
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Email = x.Email,
                    Cedula = x.Cedula,
                    Celular = x.PhoneNumber,
                    Activo = x.Activo,
                    IdCliente = x.IdCliente,
                    NombreCliente = x.NombreCliente,
                    Roles = ObtenerRoles(x)
                }).ToList();

                return Json(new { success = true, message = Users });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        [Route("Users/{id}")]
        [HttpGet]
        public IActionResult GetUser([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                var Usuario = _userManager.FindByIdAsync(id).Result;
                var totalRoles = _rolManager.Roles.ToList();
                var Roles = _userManager.GetRolesAsync(Usuario).Result.ToList();
                var user = new Usuario { Id = Usuario.Id, Email = Usuario.Email, Nombre = Usuario.Nombre, Cedula = Usuario.Cedula, Celular = Usuario.PhoneNumber, Activo = Usuario.Activo };
                var clienteUsuario = Usuario.IdCliente == null ? null : _context.Clientes.Where(x=> x.Id == Usuario.IdCliente).FirstOrDefault();
                user.NombreCliente = clienteUsuario?.Nombre;
                totalRoles.ForEach(x => user.Roles.Add(new RolViewModel { Nombre = x.Name, Seleccionado = Roles.Any(y => y == x.Name) ? true : false }));

                return Json(new { success = true, message = user });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Register model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                var user = new ApplicationUser { UserName = model.Cedula, Nombre = model.Nombre, Email = model.Email, PhoneNumber = model.Celular, Cedula = model.Cedula, Activo = true, IdCliente = model.IdCliente };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await AsignarRolAsync(_context, user, model.Roles);
                    _userManager.Dispose();
                    return Json(new { success = true, message = "Usuario creado correctamente." });
                }
                else
                {
                    string ErrorMsj = string.Concat(". ", result.Errors.Select(x=> x.Code));
                    return Json(new { success = false, message = ErrorMsj });
                }
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error!. " + ErrorMsg });
            }
        }

        private async Task AsignarRolAsync(ApplicationDbContext _context, ApplicationUser user, List<RolViewModel> listaRoles)
        {
            try
            {
                var Roles = _context.Roles.ToList();

                foreach (var rol in Roles)
                {
                    bool RolSeleccionado = listaRoles.Where(x => x.Nombre == rol.Name).Select(x => x.Seleccionado).FirstOrDefault();

                    if (RolSeleccionado)
                    {
                        bool isInRole = _userManager.IsInRoleAsync(user, rol.Name).Result;
                        if (!isInRole)
                            await _userManager.AddToRoleAsync(user, rol.Name);
                    }
                    else
                    {
                        bool isInRole = _userManager.IsInRoleAsync(user, rol.Name).Result;
                        if (isInRole)
                            await _userManager.RemoveFromRoleAsync(user, rol.Name);
                    }
                }

                await _context.SaveChangesAsync();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<RolViewModel> ObtenerRoles(ApplicationUser user)
        {
            List<RolViewModel> lstResult = new List<RolViewModel>();
            var Roles = _context.Roles.ToList();

            foreach (var rol in Roles)
            {
                bool isInRole = _userManager.IsInRoleAsync(user, rol.Name).Result;
                lstResult.Add(new RolViewModel { Nombre = rol.Name, Seleccionado = isInRole });
            }

            return lstResult;
        }

        [Route("ChangePassword")]
        [HttpPost]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                string Id = User.getUserId();
                var usuarioActual = _userManager.FindByIdAsync(Id).Result;

                if (usuarioActual != null)
                {
                    var result = await _userManager.ChangePasswordAsync(usuarioActual, model.OldPassword, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return Json(new { success = true, message = "Contraseña cambiada satisfactoriamente." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "La contraseña antigua es incorrecta." });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Usuario no identificado, intente loguearse nuevamente." });
                }
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = ErrorMsg });
            }
        }

        [Route("ForcePassword")]
        [HttpPost]
        public async Task<ActionResult> ForcePassword([FromBody] EditPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                var user = _userManager.FindByIdAsync(model.Id).Result;
                var Token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, Token, model.Password);

                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "La contraseña se ha cambiado satisfactoriamente." });
                }
                else
                {
                    string ErrorMsj = string.Concat(" - ", result.Errors.Select(x => x.Code));
                    return Json(new { success = false, message = ErrorMsj });
                }
            }
            catch (Exception excError)
            {
                return Json(new { success = false, message = excError.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            try
            {
                var usuario = model.Email.Contains('@') ? _userManager.FindByEmailAsync(model.Email).Result : _userManager.FindByNameAsync(model.Email).Result;

                if (usuario == null)
                    return Json(new { success = false, message = "Usuario o contraseña invalido." });

                var result = await _signInManager.PasswordSignInAsync(usuario, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Json(new { success = false, message = "Usuario o contraseña invalido." });
                }

                //token de dispositivo firebase
                usuario.TokenFirebase = model.TokenFirebase;
                await _context.SaveChangesAsync();

                return BuildToken(usuario);
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error! " + ErrorMsg });
            }
        }

        [Route("ListarPorRol/{NombreRol}")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ListarPorRol([FromRoute] string NombreRol)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                var Usuarios = _userManager.Users.ToList();
                var lstResult = new List<Usuario>();

                foreach (var Usuario in Usuarios)
                {
                    var Roles = _userManager.GetRolesAsync(Usuario).Result.ToList();
                    foreach (var Rol in Roles)
                    {
                        if (Rol.ToLower() == NombreRol.ToLower())
                        {
                            lstResult.Add(new Usuario { Nombre = Usuario.Nombre, Id = Usuario.Id, Email = Usuario.Email, Activo = Usuario.Activo });
                        }
                    }
                }

                return Json(new { success = true, message = lstResult });

            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error! " + ErrorMsg });
            }
        }

        [Route("EditUser/{id}")]
        [HttpPut]
        public async Task<IActionResult> EditUser([FromRoute] string id, [FromBody] Usuario model)
        {

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = ErrorModelValidation.ShowError(new SerializableError(ModelState).Values) });
            }

            try
            {
                var result = _userManager.FindByIdAsync(id).Result;

                if (result != null)
                {
                    var Usuario = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                    Usuario.Nombre = model.Nombre;
                    Usuario.Email = model.Email;
                    Usuario.Cedula = model.Cedula;
                    Usuario.IdCliente = model.IdCliente;
                    Usuario.PhoneNumber = model.Celular;
                    Usuario.Activo = model.Activo;
                    _context.SaveChanges();

                    await AsignarRolAsync(_context, Usuario, model.Roles);

                    if(!string.IsNullOrEmpty(model.Password))
                    {
                        var Token = await _userManager.GeneratePasswordResetTokenAsync(result);
                        var reseteo = await _userManager.ResetPasswordAsync(result, Token, model.Password);

                        if (!reseteo.Succeeded)
                        {
                            return Json(new { success = false, message = "Usuario actualizado, sin embargo no se pudo cambiar la contraseña." });
                        }
                    } 

                    return Json(new { success = true, message = "Registro editado correctamente." });
                }
                else
                {
                    return Json(new { success = false, message = "Ha ocurrido un error. No se ha encontrado {0} {1} con el código especificado." });
                }
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error! " + ErrorMsg });
            }

        }

        private IActionResult BuildToken(ApplicationUser Usuario)
        {
            try
            {               
                var roles = _userManager.GetRolesAsync(Usuario).Result;

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, Usuario.Email),
                    new Claim(ClaimTypes.NameIdentifier, Usuario.Id),
                    new Claim(ClaimTypes.GivenName, Usuario.Nombre),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
                claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetConnectionString("Llave_secreta").ToString()));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(1);
                var codigos = claimsIdentity.Claims;

                JwtSecurityToken token = new JwtSecurityToken(
                   issuer: _configuration.GetConnectionString("serverDomain"),
                   audience: _configuration.GetConnectionString("serverDomain"),
                   claims: claimsIdentity.Claims,
                   expires: expiration,
                   signingCredentials: creds);

                return Ok(new
                {
                    success = true,
                    message = new  {

                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = expiration,
                        nombre = Usuario.Nombre,
                        id = Usuario.Id,
                        roles = roles
                    }
                    
                });
            }
            catch (Exception exc)
            {
                string ErrorMsg = exc.GetBaseException().InnerException != null ? exc.GetBaseException().InnerException.Message : exc.GetBaseException().Message;
                return Json(new { success = false, message = "Error! " + ErrorMsg });
            }
        }

    }
}