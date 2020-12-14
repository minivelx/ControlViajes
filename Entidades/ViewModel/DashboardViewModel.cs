using System.Collections.Generic;

namespace Entidades.ViewModel
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {            
            lstVehiculosPropios = new List<VehiculoViewModel>();
            lstVehiculosTerceros = new List<VehiculoViewModel>();
            lstIndicadores = new List<IndicadorViewModel>();
            lstDatosTorta = new List<decimal>();

        }

        public List<decimal> lstDatosTorta { get; set; }
        public List<VehiculoViewModel> lstVehiculosPropios { get; set; }
        public List<VehiculoViewModel> lstVehiculosTerceros { get; set; }
        public List<IndicadorViewModel> lstIndicadores { get; set; }
        public decimal SumaAnticiposDia { get; set; }
        public decimal AcumuladoMes { get; set; }

    }

    public class VehiculoViewModel
    {
        public string Placa { get; set; }
        public string Cliente { get; set; }
        public string Estado {
            get 
            {
                if (NumeroEstado == 0)
                    return "Taller";

                else if (NumeroEstado == 1)                
                    return "Inicio";

                else if(NumeroEstado == 2)                
                    return "Transito";
                
                else                
                    return "Finalizado";                
            }
        }

        public int NumeroEstado { get; set; }
    }

    public class IndicadorViewModel
    {
        public string Cliente { get; set; }
        public int NumeroViajes { get; set; }
    }
}
