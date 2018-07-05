using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace COA.ConectorServicio
{
    public class NProtocolWS
    {
        public string sUrl { get; set; }
        public string sXml { get; set; }
        public string sSoapAction { get; set; }
        public string sContentTypeSoap { get; set; }

        public string userName { get; set; }
        public string userPassword { get; set; }
        public string userDomain { get; set; }
        public AuthenticateEnum authenticate { get; set; }

        public string Execute()
        {
            HttpWebRequest request = GetWRequest();

            // load the XML to be posted
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(this.sXml);

            // add our body to the request
            Stream stream = request.GetRequestStream();
            xmlDoc.Save(stream);
            stream.Close();

            // get the response back
            string xmlResult = "";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {

                StreamReader reader = new StreamReader(response.GetResponseStream());
                xmlResult = reader.ReadToEnd();

                // si se necesita procesar la respuesta antes de enviarla, va acá
                // ver si hay que reemplazar caracteres escapeados aqui dentro o no
            } // end using

            return xmlResult;
        }

        /// <summary>
        /// Crea el httprequest con los valores iniciales basicos
        /// </summary>
        /// <returns></returns>
        private HttpWebRequest GetWRequest()
        {
            // valida que se encuentre bien configurada la llamada
            if (string.IsNullOrEmpty(this.sUrl)) throw new ConfigurationException("No se suministró una URL válida para el WS");
            if (string.IsNullOrEmpty(this.sXml)) throw new ConfigurationException("No se suministró un XML válido para enviar al WS");
            //if (string.IsNullOrEmpty(this.sSoapAction)) throw new ConfigurationException("No se suministró el SOAP ACTION para el WS");

            // create the request to your URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.sUrl);
            request.Proxy = null;

            //Identifica el tipo de autenticación al servicio
            switch (authenticate)
            {
                // Network Credentials
                case AuthenticateEnum.DEFAULT:
                    request.UseDefaultCredentials = true;
                    break;
                //Basica
                case AuthenticateEnum.BASICA:
                    SetBasicAuthHeader(request, this.userName, this.userPassword);
                    request.PreAuthenticate = true;
                    break;
                //Active Directory (NTLM)
                case AuthenticateEnum.ACTIVE_DIRECTORY:
                    CredentialCache cc = new CredentialCache();
                    cc.Add(
                        new Uri(this.sUrl),
                        "NTLM",
                        new NetworkCredential(this.userName, this.userPassword, this.userDomain));
                    request.Credentials = cc;
                    break;
                default:
                    request.UseDefaultCredentials = true;
                    break;
            }

            // add the headers
            // the SOAPACtion determines what action the web service should use
            request.Headers.Add("SOAPAction", this.sSoapAction);

            // set the request type
            // we use utf-8 but set the content type here
            if (string.IsNullOrEmpty(sContentTypeSoap))
                request.ContentType = "text/xml;charset=\"utf-8\"";
            else
                request.ContentType = sContentTypeSoap;
            request.Method = "POST";

            return (HttpWebRequest)request;
        }

        private void SetBasicAuthHeader(WebRequest request, String userName, String userPassword)
        {
            string authInfo = userName + ":" + userPassword;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers["Authorization"] = "Basic " + authInfo;
        }
    }

    /// <summary>
    /// Clase que indica que no se ha configurado correctamente la llamada a servicio remoto
    /// </summary>
    public class ConfigurationException : Exception
    {
        public ConfigurationException() : base() { }
        public ConfigurationException(string message) : base(message) { }
        public ConfigurationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
