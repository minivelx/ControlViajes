using Entidades;
using Entidades.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logica
{
    public class LViaje
    {
        public static async Task<List<Viaje>> ConsultarViajes(ApplicationDbContext _context)
        {
            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .ToListAsync();
        }

        public static async Task<List<Viaje>> ConsultarViajesDia(ApplicationDbContext _context)
        {
            var dateNow = DateTime.Now.Date;
            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Where(x=> x.Fecha.Date >= dateNow || x.FechaRegistro == dateNow)
                .OrderByDescending(x=> x.Id).ToListAsync();
        }

        public static async Task<List<Viaje>> ConsultarViajesDiaCliente(int idCliente, ApplicationDbContext _context)
        {
            var dateNow = DateTime.Now.Date;
            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Where(x => (x.Fecha.Date >= dateNow || x.FechaRegistro == dateNow) && x.IdCliente == idCliente)
                .OrderByDescending(x => x.Id).ToListAsync();
        }

        public static async Task<Viaje> ConsultarViajePorId(int id, ApplicationDbContext _context)
        {
            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<List<Viaje>> ConsultarHistoricoViajes(FiltroViewModel filtro, ApplicationDbContext _context)
        {
            if(filtro == null)
            {
                return await _context.Viajes
                    .Include(x => x.Auxiliar)
                    .Include(x => x.Conductor)
                    .Include(x => x.Camion)
                    .Include(x => x.SedeOrigen)
                    .Include(x => x.SedeDestino)
                    .Include(x => x.Cliente)
                    .Include(x => x.Usuario)
                    .ToListAsync();
            }

            DateTime? fechaInicio = filtro.FechaInicio?.Date ?? null;
            DateTime? fechaFin = filtro.FechaFin?.Date ?? null;

            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Where(x => x.Fecha >= (fechaInicio != null ? fechaInicio : x.Fecha) &&
                            x.Fecha.Date <= (fechaFin != null ? fechaFin : x.Fecha)).ToListAsync();
        }

        public static async Task<List<OcupacionDiaria>> ConsultarOcupacionDiaria(FiltroViewModel filtro, ApplicationDbContext _context)
        {
            DateTime? fechaInicio = filtro?.FechaInicio?.Date ?? null;
            DateTime? fechaFin = filtro?.FechaFin?.Date ?? null;

            var lstViajes = await _context.Viajes                
                .Include(x => x.Camion)
                .Include(x => x.Cliente)
                .Where(x => x.Fecha >= (fechaInicio != null ? fechaInicio : x.Fecha) &&
                            x.Fecha.Date <= (fechaFin != null ? fechaFin : x.Fecha)).ToListAsync();
            //hacer logica

            var lstOcupacion = new List<OcupacionDiaria>();
            lstOcupacion.Add(new OcupacionDiaria { Fecha = new DateTime(2020, 11, 24), NumeroCamionesUtilizados = 1, PorcentajeOcupacion = 0, NumeroViajes = 1 });
            lstOcupacion.Add(new OcupacionDiaria { Fecha = new DateTime(2020, 11, 26), NumeroCamionesUtilizados = 1, PorcentajeOcupacion = 25, NumeroViajes = 1 });
            lstOcupacion.Add(new OcupacionDiaria { Fecha = new DateTime(2020, 11, 27), NumeroCamionesUtilizados = 1, PorcentajeOcupacion = 25, NumeroViajes = 1 });
            return lstOcupacion;
        }

        public static async Task<List<Viaje>> ConsultarMisViajes(string idUsuario, ApplicationDbContext _context)
        {
            var dateNow = DateTime.Now.Date;
            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Where(x => (x.IdAuxiliar == idUsuario || x.IdConductor == idUsuario) &&
                            (x.Fecha.Date == dateNow) ).ToListAsync();
        }

        public static async Task<DashboardViewModel> getDashboard(ApplicationDbContext _context)
        {
            var dashboard = new DashboardViewModel();
            var dateNow = DateTime.Now.Date;
            var lstViajes = await _context.Viajes.Where(x=> x.Fecha.Date == dateNow).Include(x=> x.Camion).Include(x=> x.Cliente).ToListAsync();
            var acumuladoMes = _context.Viajes.Where(x => x.Fecha.Month == dateNow.Month && x.Fecha.Year == dateNow.Year).Sum(x=> x.ValorAnticipo);

            foreach (var item in lstViajes)
            {
                if (item.Camion.EsPropio)                
                    dashboard.lstVehiculosPropios.Add(new VehiculoViewModel { Placa = item.Placa, Cliente = item.NombreCliente, NumeroEstado = item.NumeroEstado});
                else
                    dashboard.lstVehiculosTerceros.Add(new VehiculoViewModel { Placa = item.Placa, Cliente = item.NombreCliente, NumeroEstado = item.NumeroEstado });
            }

            var lstGroupViajes = lstViajes.GroupBy(x => new { x.IdCliente, x.NombreCliente } ).ToList();

            foreach(var agrupacion in lstGroupViajes)
            {
                var nombreCliente = agrupacion.FirstOrDefault().NombreCliente;
                var suma = agrupacion.ToList().Count;
                dashboard.lstIndicadores.Add(new IndicadorViewModel { Cliente = nombreCliente, NumeroViajes = suma });
            }

            int totalPropios = _context.Camiones.Where(x => x.Activo == true && x.EsPropio).Count();
            int totalUsados = dashboard.lstVehiculosPropios.GroupBy(x => x.Placa).Count();
            dashboard.lstDatosTorta.Add(totalUsados);
            dashboard.lstDatosTorta.Add(totalPropios -totalUsados);


            dashboard.SumaAnticiposDia = (decimal) lstViajes.Sum(x => x.ValorAnticipo);
            dashboard.AcumuladoMes = (decimal) acumuladoMes;

            return dashboard;
        }
        

        public static async Task GuardarViaje(Viaje Viaje, ApplicationDbContext _context)
        {
            _context.Viajes.Add(Viaje);
            await _context.SaveChangesAsync();
        }

        public static async Task ActualizarEstadoViaje(int idViaje, ApplicationDbContext _context)
        {
            var Viaje = await ConsultarViajePorId(idViaje, _context);
            
            if(Viaje.InicioRuta == null)            
                Viaje.InicioRuta = DateTime.Now;            
            else            
                Viaje.FinRuta = DateTime.Now;            

            _context.Entry(Viaje).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public static async Task EditarViaje(Viaje Viaje, ApplicationDbContext _context)
        {
            _context.Entry(Viaje).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
