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
        public string IdConductor { get; set; }

        [StringLength(450), ForeignKey("Auxiliar")]
        public string IdAuxiliar { get; set; }

        [Required, ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        [Required, ForeignKey("SedeOrigen")]
        public int IdOrigen { get; set; }

        [Required, ForeignKey("SedeDestino")]
        public int IdDestino { get; set; }

        [StringLength(20)]
        public string NumeroManifiesto { get; set; }

        [StringLength(20)]
        public string NumeroCuenta { get; set; }

        public decimal ? ValorAnticipo { get; set; }

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

        public string Placa { get { return Camion?.Placa; } }

        public string NombreConductor { get { return Conductor?.Nombre; } }

        public string NombreAuxiliar { get { return Auxiliar?.Nombre; } }

        public string NombreCliente { get { return Cliente?.Nombre; } }

        public string NombreOrigen { get { return SedeOrigen?.Nombre; } }

        public string NombreDestino { get { return SedeDestino?.Nombre; } }

        public DateTime FechaRegistro { get; set; }

        public string NombreUsuarioRegistro { get { return Usuario?.Nombre; } }

        public string Estado
        {
            get
            {
                if (FinRuta != null)
                    return "Finalizado";
                else if (InicioRuta != null)
                    return "En proceso";
                else
                    return "Sin Iniciar";                        
            }
        }

        [JsonIgnore, StringLength(450), ForeignKey("Usuario")]
        public string UsuarioRegistro { get; set; }


   
        public Camion Camion { get; set; }
       
        public ApplicationUser Conductor { get; set; }
        
        public ApplicationUser Auxiliar { get; set; }
        
        public Cliente Cliente { get; set; }
        
        public Sede SedeOrigen { get; set; }
        
        public Sede SedeDestino { get; set; }
        [JsonIgnore]
        public ApplicationUser Usuario { get; set; }

    }
}
