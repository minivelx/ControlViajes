using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Camion
    {
        [Key]
        public int Id { get; set; }

        [StringLength(7), Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Placa { get; set; }

        [StringLength(7)]
        public string Remolque { get; set; }

        public bool EsPropio { get; set; }

        public bool Activo { get; set; }

        [StringLength(450)]
        public string UsuarioRegistro { get; set; }
    }
}
