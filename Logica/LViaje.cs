using Entidades;
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

        //public static List<Viaje> ConsultarViajesActivos(ApplicationDbContext _context)
        //{
        //    return _context.Viajes.Where(x => x.Activo).ToList();
        //}

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

        public static async Task<List<Viaje>> ConsultarMisViajes(string idUsuario, ApplicationDbContext _context)
        {
            return await _context.Viajes
                .Include(x => x.Auxiliar)
                .Include(x => x.Conductor)
                .Include(x => x.Camion)
                .Include(x => x.SedeOrigen)
                .Include(x => x.SedeDestino)
                .Include(x => x.Cliente)
                .Include(x => x.Usuario)
                .Where(x => x.IdAuxiliar == idUsuario || x.IdConductor == idUsuario).ToListAsync();
        }        

        public static async Task GuardarViaje(Viaje Viaje, ApplicationDbContext _context)
        {
            //Viaje.Activo = true;
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

        //public static void EliminarViaje(int id, ApplicationDbContext _context)
        //{
        //    Viaje Viaje = ConsultarViajePorId(id, _context);
        //    Viaje.Activo = false;
        //    EditarViaje(Viaje, _context);
        //}
    }
}
