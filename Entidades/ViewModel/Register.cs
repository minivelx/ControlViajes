using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Entidades
{
    public class Register : IValidatableObject
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

        public int ? IdCliente { get; set; }

        public List<RolViewModel> Roles { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Roles.Any(x=> x.Nombre.ToLower() == "cliente" && x.Seleccionado) && IdCliente == null)
            {
                yield return new ValidationResult(errorMessage: "El rol 'Cliente' requiere que el usuario se asigne a un Cliente", memberNames: new[] { "IdCliente" });
            }
        }
    }
}
