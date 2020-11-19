using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logica
{
    public class LCliente
    {
        public static async Task<List<Cliente>> ConsultarClientes(ApplicationDbContext _context)
        {
            return await _context.Clientes.Include(x=> x.lstSedes).Include(x=> x.Usuario).ToListAsync();
        }

        public static async Task<List<Cliente>> ConsultarClientesActivos(ApplicationDbContext _context)
        {
            var lstClientes = await _context.Clientes.Where(x => x.Activo).Include(x => x.lstSedes).Include(x => x.Usuario).ToListAsync();
            lstClientes.ForEach(x => x.lstSedes = x.lstSedes.Where(y => y.Activo).ToList());
            return lstClientes;
        }

        public static async Task<Cliente> ConsultarClientePorId(int id, ApplicationDbContext _context)
        {
            return await _context.Clientes.Where(x => x.Id == id).Include(x => x.lstSedes).Include(x => x.Usuario).FirstOrDefaultAsync();
        }

        public static async Task GuardarCliente(Cliente Cliente, ApplicationDbContext _context)
        {
            Cliente.Activo = true;
            _context.Clientes.Add(Cliente);
            await _context.SaveChangesAsync();
        }

        public static async Task EditarCliente(Cliente Cliente, ApplicationDbContext _context)
        {
            _context.Entry(Cliente).State = EntityState.Modified;
            await EditarListaSedes(Cliente.Id, Cliente.lstSedes, _context);
            await _context.SaveChangesAsync();
        }

        public static async Task EliminarCliente(int id, ApplicationDbContext _context)
        {
            Cliente Cliente = await ConsultarClientePorId(id, _context);
            Cliente.Activo = false;
            await EditarCliente(Cliente, _context);
        }

        public static async Task EditarListaSedes(int idCliente, List<Sede> lstSedes, ApplicationDbContext _context)
        {
            var lstOld = _context.Sedes.AsNoTracking().Where(x => x.IdCliente == idCliente).ToList();
            // Elimino las sedes que ya no se requieran 
            var lstRemove = lstOld.Except(lstSedes).ToList();
            if (lstRemove != null)
                lstRemove.ForEach(x => x.Activo = false);
            // Agrego las nuevas
            var lstNew = lstSedes.Except(lstOld).ToList();
            _context.Sedes.AddRange(lstNew);
            // Edito las comunes
            var lstInter = lstSedes.Intersect(lstOld).ToList();
            foreach (var item in lstInter)
            {
                await LSede.EditarSede(item, _context);
            }
        }
    }
}
