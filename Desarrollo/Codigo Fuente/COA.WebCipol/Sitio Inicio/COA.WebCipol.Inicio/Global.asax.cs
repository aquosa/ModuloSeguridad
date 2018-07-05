using COA.Cipol.Inicio._UIHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using WebAppControlDualLogin.Model;

namespace COA.WebCipol.Inicio
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application.Add("Sessions", new List<mSession>());
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
                //Response.Redirect("Gestion_Error.aspx");
                Response.Redirect("Gestion_Error.aspx?" + "Origen=" + strOrigen
                                                        + "&Mensaje=" + strMensaje
                                                        + "&Stack=" + strStack);
            }
        }

        protected void Session_End(object sender, EventArgs e)
        {
            mSession sesion = Session["Id"] as mSession;
            List<mSession> sesiones = Application["Sessions"] as List<mSession>;

            bool UsuarioCerroSesion = DatosSesion.Control.Eliminar(sesiones, sesion);

            //En caso de que haya algun problema al eliminar la sesion, es grabado en el registro de Log
            if (!UsuarioCerroSesion && sesion != null)
                COA.Logger.Logueador.Loggear("Se cerró la sesión del usuario " + sesion.User + " pero la misma no pudo ser eliminada del registro de la aplicación", 
                                             System.Diagnostics.EventLogEntryType.Information);
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}