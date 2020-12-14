using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logica
{
    public class LCamion
    {
        public static async Task<List<Camion>> ConsultarCamiones(ApplicationDbContext _context)
        {
            return await _context.Camiones.ToListAsync();
        }

        public static async Task<List<Camion>> ConsultarCamionesActivos(ApplicationDbContext _context)
        {
            return await _context.Camiones.Where(x => x.Activo && x.EstadoTaller == false).ToListAsync();
        }

        public static async Task<Camion> ConsultarCamionPorId(int id, ApplicationDbContext _context)
        {
            return await _context.Camiones.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public static async Task<Camion> ConsultarCamionPorPlaca(string placa, ApplicationDbContext _context)
        {
            return await _context.Camiones.Where(x => x.Placa == placa).FirstOrDefaultAsync();
        }

        public static async Task GuardarCamion(Camion Camion, ApplicationDbContext _context)
        {
            Camion.Activo = true;
            Camion.Placa = Camion.Placa.ToUpper();
            Camion.Remolque = Camion.Remolque.ToUpper();
            _context.Camiones.Add(Camion);
            await _context.SaveChangesAsync();
        }

        public static async Task EditarCamion(Camion Camion, ApplicationDbContext _context)
        {
            _context.Entry(Camion).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public static async Task EliminarCamion(int id, ApplicationDbContext _context)
        {
            Camion Camion = await ConsultarCamionPorId(id, _context);
            Camion.Activo = false;
            await EditarCamion(Camion, _context);
        }
    }
}
