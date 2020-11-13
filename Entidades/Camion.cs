using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(450), ForeignKey("Usuario")]
        public string UsuarioRegistro { get; set; }

        [JsonIgnore]
        public ApplicationUser Usuario { get; set; }
    }
}
