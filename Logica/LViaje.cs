using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Logica
{
    public class LViaje
    {
        public static List<Viaje> ConsultarViajes(ApplicationDbContext _context)
        {
            return _context.Viajes.ToList();
        }

        //public static List<Viaje> ConsultarViajesActivos(ApplicationDbContext _context)
        //{
        //    return _context.Viajes.Where(x => x.Activo).ToList();
        //}

        public static Viaje ConsultarViajePorId(int id, ApplicationDbContext _context)
        {
            return _context.Viajes.Where(x => x.Id == id).FirstOrDefault();
        }

        public static void GuardarViaje(Viaje Viaje, ApplicationDbContext _context)
        {
            //Viaje.Activo = true;
            _context.Viajes.Add(Viaje);
            _context.SaveChanges();
        }

        public static void EditarViaje(Viaje Viaje, ApplicationDbContext _context)
        {
            _context.Entry(Viaje).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //public static void EliminarViaje(int id, ApplicationDbContext _context)
        //{
        //    Viaje Viaje = ConsultarViajePorId(id, _context);
        //    Viaje.Activo = false;
        //    EditarViaje(Viaje, _context);
        //}
    }
}
