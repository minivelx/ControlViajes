using System;

namespace Entidades.ViewModel
{
    public class OcupacionDiaria
    {
        public DateTime Fecha { get; set; }

        public int NumeroViajes { get; set; }

        public int NumeroCamionesUtilizados { get; set; }

        public decimal PorcentajeOcupacion { get; set; }
    }
}
