using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace COA.WebCipol.Presentacion
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Creado  GCP-Cambios 14665
    /// </history>
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string strMensaje = "";
            string strStack = "";
            string strOrigen = "";

            //todo:Ver pérdida de sessión.    
            //try
            //{
            //    Session["Algo"] = "pepe";
            //}
            //catch (Exception ex)
            //{
            //    //Si el erro vino por pérdida de sesion.
            //    Response.Redirect("Login.aspx");
            //}

            // Code that runs when an unhandled error occurs
            if ((System.Configuration.ConfigurationManager.AppSettings["ModoDebug"] == "NO"))
            {
                if (Server != null && Server.GetLastError() != null)
                {
                    strMensaje = HttpUtility.UrlEncode(
                        ((Server.GetLastError().InnerException != null) ?
                        Server.GetLastError().InnerException.Message
                        : (Server.GetLastError().Message)));

                    strStack = HttpUtility.UrlEncode(
                        (Server.GetLastError().InnerException != null) ?
                        Server.GetLastError().InnerException.StackTrace
                        : Server.GetLastError().StackTrace);

                    strOrigen = HttpUtility.UrlEncode(
                        (Server.GetLastError().InnerException != null) ?
                        Server.GetLastError().InnerException.Source
                        : Server.GetLastError().Source);
                }

                Response.Redirect("Gestion_Error.aspx?" + "Origen=" + strOrigen
                                                        + "&Mensaje=" + strMensaje
                                                        + "&Stack=" + strStack);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}