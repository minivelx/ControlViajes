using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace Logica
{
    public class LHttpRequest
    {
        public static Object POSTRequest(object body, string url)
        {
            //Cabecera
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.Method = "POST";
            httpRequest.ProtocolVersion = HttpVersion.Version11;
            httpRequest.ContentType = "application/json";
            httpRequest.Accept = "application/json";
            //Body
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(body);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            //Peticion
            WebResponse httpResponse = httpRequest.GetResponse();

            //Aqui empieza la logica de pasar un reporte de ASPNET a ASPNET CORE
            //Obtengo la ruta del archivo temporal que contiene el pdf deserialzando el Json De Respuesta

            Object resultMessage;
            var result = httpResponse.GetResponseStream();
            using (var sr = new StreamReader(result))
            {
                var contributorsAsJson = sr.ReadToEnd();
                resultMessage = JsonConvert.DeserializeObject<Object>(contributorsAsJson);
            }

            return resultMessage;
        }
    }
}
