using Entidades;
using Entidades.Interfaces;

namespace Logica
{
    public class LEmail
    {
        public static void EnviarEmailAsignacionViaje(Viaje viaje, IEmailSender _emailSender)
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
            _emailSender.SendEmailAsync(viaje.Conductor.Email, asunto, mensaje).ConfigureAwait(false);

            if(viaje.Auxiliar != null)
                _emailSender.SendEmailAsync(viaje.Auxiliar.Email, asunto, mensaje).ConfigureAwait(false);
        }

        public static void EditarEmailAsignacionViaje(Viaje viaje, IEmailSender _emailSender)
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
            _emailSender.SendEmailAsync(viaje.Conductor.Email, asunto, mensaje).ConfigureAwait(false);

            if (viaje.Auxiliar != null)
                _emailSender.SendEmailAsync(viaje.Auxiliar.Email, asunto, mensaje).ConfigureAwait(false);
        }

        public static void EnviarEmailValorAnticipo(Viaje viaje, IEmailSender _emailSender)
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
            _emailSender.SendEmailAsync(viaje.Conductor.Email, asunto, mensaje).ConfigureAwait(false);

            if (viaje.Auxiliar != null)
                _emailSender.SendEmailAsync(viaje.Auxiliar.Email, asunto, mensaje).ConfigureAwait(false);
        }


    }
}
