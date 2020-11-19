using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public class LCliente
    {
        public static List<Cliente> ConsultarClientes(ApplicationDbContext _context)
        {
            return _context.Clientes.Include(x=> x.lstSedes).Include(x=> x.Usuario).ToList();
        }

        public static List<Cliente> ConsultarClientesActivos(ApplicationDbContext _context)
        {
            return _context.Clientes.Where(x => x.Activo).Include(x => x.lstSedes).Include(x => x.Usuario).ToList();
        }

        public static Cliente ConsultarClientePorId(int id, ApplicationDbContext _context)
        {
            return _context.Clientes.Where(x => x.Id == id).Include(x => x.lstSedes).Include(x => x.Usuario).FirstOrDefault();
        }

        public static void GuardarCliente(Cliente Cliente, ApplicationDbContext _context)
        {
            Cliente.Activo = true;
            _context.Clientes.Add(Cliente);
            _context.SaveChanges();
        }

        public static void EditarCliente(Cliente Cliente, ApplicationDbContext _context)
        {
            _context.Entry(Cliente).State = EntityState.Modified;
            EditarListaSedes(Cliente.Id, Cliente.lstSedes, _context);
            _context.SaveChanges();
        }

        public static void EliminarCliente(int id, ApplicationDbContext _context)
        {
            Cliente Cliente = ConsultarClientePorId(id, _context);
            Cliente.Activo = false;
            EditarCliente(Cliente, _context);
        }

        public static void EditarListaSedes(int idCliente, List<Sede> lstSedes, ApplicationDbContext _context)
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
            lstInter.ForEach(x => LSede.EditarSede(x, _context));

        }
    }
}
