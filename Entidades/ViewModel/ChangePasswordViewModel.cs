using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "La contraseña nueva y la contraseña de confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}
