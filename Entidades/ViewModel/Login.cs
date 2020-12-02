using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Login
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Password { get; set; }

        public string tokenFirebase { get; set; }
    }
}
