using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public bool Activo { get; set; }

        [JsonIgnore, StringLength(450), ForeignKey("Usuario")]
        public string UsuarioRegistro { get; set; }

        public string NombreUsuarioRegistro { get { return Usuario?.Nombre; } }

        public List<Sede> lstSedes { get; set; }

        [JsonIgnore]
        public ApplicationUser Usuario { get; set; }
    }
}
