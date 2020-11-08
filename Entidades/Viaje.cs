using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Viaje
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required, ForeignKey("Camion")]
        public int IdCamion { get; set; }        

        [Required, StringLength(450), ForeignKey("Conductor")]
        public String IdCoductor { get; set; }

        [Required, ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required, ForeignKey("SedeOrigen")]
        public int IdOrigen { get; set; }

        [Required, ForeignKey("SedeDestino")]
        public int IdDestino { get; set; }

        public DateTime ? InicioRuta { get; set; }

        public DateTime ? FinRuta { get; set; }

        public int ? Desfase { 
            get 
            {
                if (FinRuta == null)
                    return null;

                int diferencia = (FinRuta.Value - Fecha).Minutes;
                return diferencia < 0 ? 0 : diferencia;
            } 
        }

        public String Placa { get { return Camion?.Placa; } }

        public String NombreConductor { get { return Coductor?.Nombre; } }

        public String NombreCliente { get { return Cliente?.Nombre; } }

        public String NombreOrigen { get { return SedeOrigen?.Nombre; } }

        public String NombreDestino { get { return SedeDestino?.Nombre; } }

        public DateTime FechaRegistro { get; set; }

        [JsonIgnore]
        public Camion Camion { get; set; }
        [JsonIgnore]
        public ApplicationUser Coductor { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
        [JsonIgnore]
        public Sede SedeOrigen { get; set; }
        [JsonIgnore]
        public Sede SedeDestino { get; set; }

    }
}
