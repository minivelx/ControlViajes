using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class EditPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser menor a {2}.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "La contraseña y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
