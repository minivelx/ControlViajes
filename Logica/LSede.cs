using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public class LSede
    {
        public static List<Sede> ConsultarSedes(ApplicationDbContext _context)
        {
            return _context.Sedes.ToList();
        }

        public static List<Sede> ConsultarSedesActivos(ApplicationDbContext _context)
        {
            return _context.Sedes.Where(x => x.Activo).ToList();
        }

        public static Sede ConsultarSedePorId(int id, ApplicationDbContext _context)
        {
            return _context.Sedes.Where(x => x.Id == id).FirstOrDefault();
        }

        public static void GuardarSede(Sede Sede, ApplicationDbContext _context)
        {
            Sede.Activo = true;
            _context.Sedes.Add(Sede);
            _context.SaveChanges();
        }

        public static void EditarSede(Sede Sede, ApplicationDbContext _context)
        {
            _context.Entry(Sede).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public static void EliminarSede(int id, ApplicationDbContext _context)
        {
            Sede Sede = ConsultarSedePorId(id, _context);
            Sede.Activo = false;
            EditarSede(Sede, _context);
        }
    }
}
