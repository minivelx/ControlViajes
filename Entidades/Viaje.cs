using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entidades
{
    public class Viaje : IValidatableObject
    {
        private static int ESTADO_TALLER = 0;
        private static int ESTADO_ASIGNADO = 1;
        private static int ESTADO_INICIADO = 2;
        private static int ESTADO_FINALIZADO = 3;       

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
                if (Camion!= null && Camion.EstadoTaller && FinRuta == null)
                    return "Taller";
                else if (InicioRuta == null && FinRuta == null)
                    return "Sin Iniciar";
                else if (InicioRuta != null && FinRuta == null)
                    return "En Ruta";
                else
                    return "Finalizado";                        
            }
        }

        public int NumeroEstado
        {
            get
            {
                if (Camion != null && Camion.EstadoTaller && FinRuta == null)
                    return ESTADO_TALLER;
                else if (InicioRuta == null && FinRuta == null)
                    return ESTADO_ASIGNADO;
                else if (InicioRuta != null && FinRuta == null)
                    return ESTADO_INICIADO;
                else 
                    return ESTADO_FINALIZADO;
            }
        }

        [JsonIgnore, StringLength(450), ForeignKey("Usuario")]
        public string UsuarioRegistro { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IdDestino == IdOrigen)
            {
                yield return new ValidationResult(errorMessage: "El origen y destino no pueden ser iguales", memberNames: new[] { "Destino" });
            }

            //if (Fecha < DateTime.Now && InicioRuta == null)
            //{
            //    yield return new ValidationResult(errorMessage: "La fecha no puede ser menor a la fecha actual", memberNames: new[] { "Fecha" });
            //}
        }

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
