using Entidades;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Logica
{
    public class LTaller
    {
        public static async Task RealizarMovimientoTaller(int IdCamion,ApplicationDbContext _context)
        {
            var camion = await LCamion.ConsultarCamionPorId(IdCamion, _context);
            var historicoTaller = ConsultarUltimoMovimientoTaller(IdCamion, _context);
            var dateNow = DateTime.Now;

            if (historicoTaller == null)
            {
                historicoTaller = new HistoricoTaller { IdCamion = IdCamion, Placa = camion.Placa, InicioTaller = dateNow };
                camion.EstadoTaller = true;
                camion.InicioTaller = dateNow;
                _context.HistoricoTaller.Add(historicoTaller);
            } 
            else if(historicoTaller.FinTaller == null)
            {
                historicoTaller.FinTaller = dateNow;
                camion.EstadoTaller = false;
                camion.FinTaller = dateNow;
            }
            else
            {
                historicoTaller = new HistoricoTaller { IdCamion = IdCamion, Placa = camion.Placa, InicioTaller = dateNow };
                camion.EstadoTaller = true;
                camion.InicioTaller = dateNow;
                _context.HistoricoTaller.Add(historicoTaller);
            }

            await _context.SaveChangesAsync();
        }

        public static HistoricoTaller ConsultarUltimoMovimientoTaller(int IdCamion, ApplicationDbContext _context)
        {
            return _context.HistoricoTaller.Where(x => x.IdCamion == IdCamion).LastOrDefault();
        }
    }
}
