using System;
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

            //// mock data VehiculosPropios
            //lstVehiculosPropios.Add(new VehiculoViewModel {Placa = "OMP888" ,Cliente = "Femsa", NumeroEstado = 1 });
            //lstVehiculosPropios.Add(new VehiculoViewModel { Placa = "ABC123", Cliente = "Plasticos S.A", NumeroEstado = 1 });
            //lstVehiculosPropios.Add(new VehiculoViewModel { Placa = "TMZ543", Cliente = "Femsa", NumeroEstado = 3 });

            //// mock data Vehiculosterceros
            //lstVehiculosTerceros.Add(new VehiculoViewModel { Placa = "ZXS633", Cliente = "Colcafe", NumeroEstado = 1 });
            //lstVehiculosTerceros.Add(new VehiculoViewModel { Placa = "MPu452", Cliente = "Plasticos S.A", NumeroEstado = 3 });

            //// mock data Indicadores
            //lstIndicadores.Add(new IndicadorViewModel { Cliente = "Colcafe", NumeroViajes = 1 });
            //lstIndicadores.Add(new IndicadorViewModel { Cliente = "Plasticos S.A", NumeroViajes = 3 });
            //lstIndicadores.Add(new IndicadorViewModel { Cliente = "Femsa", NumeroViajes = 3 });

            //SumaAnticipos = 750000M;
            //AcumuladoMes = 3800000M;

            //Random rnd = new Random();
            //int random = rnd.Next(100);
            //lstDatosTorta.Add(random);
            //lstDatosTorta.Add(100-random);

        }

        public List<decimal> lstDatosTorta { get; set; }
        public List<VehiculoViewModel> lstVehiculosPropios { get; set; }
        public List<VehiculoViewModel> lstVehiculosTerceros { get; set; }
        public List<IndicadorViewModel> lstIndicadores { get; set; }
        public decimal SumaAnticipos { get; set; }
        public decimal AcumuladoMes { get; set; }

    }

    public class VehiculoViewModel
    {
        public string Placa { get; set; }
        public string Cliente { get; set; }
        public string Estado {
            get 
            { 
                if(NumeroEstado == 1)                
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
