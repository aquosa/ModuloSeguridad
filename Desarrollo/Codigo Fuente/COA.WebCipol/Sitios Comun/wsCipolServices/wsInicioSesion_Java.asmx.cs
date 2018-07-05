using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using wsCipolServices.ConectorWSCipolNET;

namespace wsCipolServices
{
    /// <summary>
    /// Descripción breve de wsInicioSesion_Java
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsInicioSesion_Java : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string IniciarSesion(string pUsuario, string pTerminal, string pPassword, ref string pError, ref bool pTerminal_ActualizacionLAN)
        {
            try
            {
                ConectorwsInicioSesion_Java cnInicioSesion = new ConectorwsInicioSesion_Java();
                return cnInicioSesion.IniciarSesion(pUsuario, pTerminal, ref pError, pPassword, ref pTerminal_ActualizacionLAN);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
    }
}
