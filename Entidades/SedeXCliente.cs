using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class SedeXCliente
    {
        [Key, Column(Order =1 ), ForeignKey("Sede")]
        public int IdSede { get; set; }

        [Key, Column(Order = 2), ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [JsonIgnore]
        public Sede Sede { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
    }
}
