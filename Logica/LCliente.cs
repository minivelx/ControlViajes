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
            return _context.Clientes.ToList();
        }

        public static List<Cliente> ConsultarClientesActivos(ApplicationDbContext _context)
        {
            return _context.Clientes.Where(x => x.Activo).ToList();
        }

        public static Cliente ConsultarClientePorId(int id, ApplicationDbContext _context)
        {
            return _context.Clientes.Where(x => x.Id == id).FirstOrDefault();
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
            _context.SaveChanges();
        }

        public static void EliminarCliente(int id, ApplicationDbContext _context)
        {
            Cliente Cliente = ConsultarClientePorId(id, _context);
            Cliente.Activo = false;
            EditarCliente(Cliente, _context);
        }
    }
}
