using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class HistoricoTaller
    {

        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Camion")]
        public int IdCamion { get; set; }

        [StringLength(7), Required]
        public string Placa { get; set; }

        public DateTime InicioTaller { get; set; }

        public DateTime? FinTaller { get; set; }

        [JsonIgnore]
        public Camion Camion { get; set; }
    }
}
