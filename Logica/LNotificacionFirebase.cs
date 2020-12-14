using Entidades;
using Entidades.ViewModel;
using Microsoft.Extensions.Configuration;

namespace Logica
{
    public class LNotificacionFirebase
    {
        public static void consumirNotificacionPushViaje(string userId, IConfiguration _configuration, ApplicationDbContext _context)
        {
            var url = _configuration.GetConnectionString("servidorFirebase").ToString();
            var llaveFirebase = _configuration.GetConnectionString("Llave_firebase").ToString();
            var user = _context.Users.Find(userId);

            if (!string.IsNullOrEmpty(user.TokenFirebase))
            {
                var notification = new Notification { Title = "Nuevo viaje asignado!", Body = "Ingresa para conocer el detalle del viaje" };
                var PushNotificacion = new PushNotificationViewModel { To = user.TokenFirebase, Notification = notification };
                LHttpRequest.POSTRequestFirebase(PushNotificacion, url, llaveFirebase);
            }
        }
    }
}
