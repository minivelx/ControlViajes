using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logica
{
    public class LSede
    {
        public static async Task<List<Sede>> ConsultarSedes(ApplicationDbContext _context)
        {
            return await _context.Sedes.ToListAsync();
        }

        public static async Task<List<Sede>> ConsultarSedesActivos(ApplicationDbContext _context)
        {
            return await _context.Sedes.Where(x => x.Activo).ToListAsync();
        }

        public static async Task<List<Sede>> ConsultarSedesPorCliente(int idCliente, ApplicationDbContext _context)
        {
            return await _context.Sedes.Where(x => x.Activo && x.IdCliente == idCliente).ToListAsync();
        }
        

        public static async Task<Sede> ConsultarSedePorId(int id, ApplicationDbContext _context)
        {
            return await _context.Sedes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public static async Task GuardarSede(Sede Sede, ApplicationDbContext _context)
        {
            Sede.Activo = true;
            _context.Sedes.Add(Sede);
            await _context.SaveChangesAsync();
        }

        public static async Task EditarSede(Sede Sede, ApplicationDbContext _context)
        {
            _context.Entry(Sede).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public static async Task EliminarSede(int id, ApplicationDbContext _context)
        {
            Sede Sede = await ConsultarSedePorId(id, _context);
            Sede.Activo = false;
            await EditarSede(Sede, _context);
        }
    }
}
