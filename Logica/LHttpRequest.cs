using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;


namespace Logica
{
    public class LHttpRequest
    {
        public static void POSTRequestFirebase(object body, string url, string key)
        {
            //Cabecera
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            httpRequest.Method = "POST";
            httpRequest.ProtocolVersion = HttpVersion.Version11;
            httpRequest.ContentType = "application/json";
            httpRequest.Accept = "application/json";
            httpRequest.Headers.Add("Authorization", string.Concat("key=", key));
            //Body
            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(body);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            //Peticion
            var response = (HttpWebResponse)httpRequest.GetResponse();

        }
    }
}
