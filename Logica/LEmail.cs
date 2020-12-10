using Entidades;
using Entidades.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;

namespace Logica
{
    public class LEmail
    {
        public static async Task EnviarEmailAsignacionViaje(Viaje viaje, IEmailSender _emailSender)
        {
            var asunto = "Recordatorio Asignación Viaje #" + viaje.Id;  

            var mensaje = string.Format("Te recordamos los detalles del viaje: <br><br>" +
                            "Fecha Entrega: {0} <br>" +
                            "Cliente: {1} <br> " +
                            "Destino: {2} <br> " +
                            "Dirección: {3} <br>" +
                            "Conductor: {4} <br>" +
                            "Auxiliar: {5} <br>" +
                            "Placa: {6} <br>",
                            viaje.Fecha.ToString("dd/MM/yyyy HH:mm tt"), 
                            viaje.NombreCliente,
                            viaje.NombreDestino, 
                            viaje.SedeDestino.Direccion,
                            viaje.Conductor.InfoContacto,
                            viaje.Auxiliar?.InfoContacto ?? "",
                            viaje.Placa);
            await _emailSender.SendEmailAsync(viaje.Conductor.Email, asunto, mensaje).ConfigureAwait(false);

            if(viaje.Auxiliar != null)
                await _emailSender.SendEmailAsync(viaje.Auxiliar.Email, asunto, mensaje).ConfigureAwait(false);
        }

        public static async Task EditarEmailAsignacionViaje(Viaje viaje, IEmailSender _emailSender)
        {
            var asunto = "Recordatorio Asignación Viaje #" + viaje.Id;

            var mensaje = string.Format("Tu viaje ha sido modificado: <br><br>" +
                            "Fecha Entrega: {0} <br>" +
                            "Cliente: {1} <br> " +
                            "Destino: {2} <br> " +
                            "Dirección: {3} <br>" +
                            "Conductor: {4} <br>" +
                            "Auxiliar: {5} <br>" +
                            "Placa: {6} <br>",
                            viaje.Fecha.ToString("dd/MM/yyyy HH:mm tt"),
                            viaje.NombreCliente,
                            viaje.NombreDestino,
                            viaje.SedeDestino.Direccion,
                            viaje.Conductor.InfoContacto,
                            viaje.Auxiliar?.InfoContacto ?? "",
                            viaje.Placa);
            await _emailSender.SendEmailAsync(viaje.Conductor.Email, asunto, mensaje).ConfigureAwait(false);

            if (viaje.Auxiliar != null)
               await _emailSender.SendEmailAsync(viaje.Auxiliar.Email, asunto, mensaje).ConfigureAwait(false);
        }

        public static async Task EnviarEmailValorAnticipo(Viaje viaje, IConfiguration _configuration,  IEmailSender _emailSender)
        {

            var correos = _configuration.GetConnectionString("emailAnticipo")?.Split(";").ToList();

            var asunto = "Notificación Transferencia Anticipo #" + viaje.Id;

            var mensaje = string.Format("Se debe realizar la transferencia con los siguientes campos: <br><br>" +
                            "Valor Anticipo: {0} <br>" +
                            "Número de Cuenta: {1} <br> "+
                            "Cliente: {2} <br> " +
                            "Destino: {3} <br> " +
                            "Dirección: {4} <br>",
                            string.Format("{0:C2}", viaje.ValorAnticipo),
                            viaje.NumeroCuenta,
                            viaje.NombreCliente,
                            viaje.NombreDestino,
                            viaje.SedeDestino.Direccion);

            if (correos != null)  foreach (var correo in correos)
            {
                await _emailSender.SendEmailAsync(correo.Trim(), asunto, mensaje).ConfigureAwait(false);
            }
            

        }
    }
}
