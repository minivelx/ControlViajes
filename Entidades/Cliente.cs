using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    public class Cliente
    {
        public Cliente()
        {
            lstSedes = new List<Sede>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(100), Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [StringLength(450)]
        public string UsuarioRegistro { get; set; }

        public bool Activo { get; set; }

        public List<Sede> lstSedes { get; set; }
    }
}
