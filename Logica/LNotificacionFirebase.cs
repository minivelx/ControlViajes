using Entidades.ViewModel;
using Microsoft.Extensions.Configuration;

namespace Logica
{
    public class LNotificacionFirebase
    {
        public static void consumirNotificacionPush(IConfiguration _configuration)
        {
            var url = _configuration.GetConnectionString("servidorFirebase").ToString();
            PushNotificationViewModel notificacion = new PushNotificationViewModel();

            //ResultViewModel resultMessage = LHttpRequest.POSTRequest(body2, url);
        }

    }
}
