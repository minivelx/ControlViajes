using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public class LCamion
    {

        public static List<Camion> ConsultarCamionesActivos(ApplicationDbContext _context)
        {
            return _context.Camiones.Where(x => x.Activo).ToList();
        }

        public static Camion ConsultarCamionPorId(int id, ApplicationDbContext _context)
        {
            return _context.Camiones.Where(x => x.Id == id).FirstOrDefault();
        }

        public static Camion ConsultarCamionPorPlaca(string placa, ApplicationDbContext _context)
        {
            return _context.Camiones.Where(x => x.Placa == placa).FirstOrDefault();
        }

        public static void GuardarCamion(Camion Camion, ApplicationDbContext _context)
        {
            Camion.Activo = true;
            _context.Camiones.Add(Camion);
            _context.SaveChanges();
        }

        public static void EditarCamion(Camion Camion, ApplicationDbContext _context)
        {
            _context.Entry(Camion).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public static void EliminarCamion(int id, ApplicationDbContext _context)
        {
            Camion Camion = ConsultarCamionPorId(id, _context);
            Camion.Activo = false;
            EditarCamion(Camion, _context);
        }
    }
}
