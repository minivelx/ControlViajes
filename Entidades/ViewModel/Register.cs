using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Register
    {
        public Register()
        {
            Roles = new List<RolViewModel>();
        }

        [Required(ErrorMessage = "Se requiere el campo de Identificación personal")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "Se requiere una contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Se requiere un Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Se requiere un Email")]
        public string Email { get; set; }

        public string Celular { get; set; }

        public List<RolViewModel> Roles { get; set; }
    }
}
