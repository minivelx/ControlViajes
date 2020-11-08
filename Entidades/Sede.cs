using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Sede
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100), Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [StringLength(200), Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Direccion { get; set; }

        public bool Activo { get; set; }
    }
}
