using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using COA.WebCipol.Fachada;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados;
using System.DirectoryServices;
using COA.WebCipol.Comun;
using Microsoft.VisualBasic;
using COA.CifrarDatos;
using COA.Cipol.Inicio._UIHelpers;
using System.Web.Security;



namespace COA.WebCipol.Presentacion.PageBuilders
{
    /// <summary>
    /// Summary description for wsAjaxwsSeguridad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAjaxwsSeguridad : System.Web.Services.WebService
    {

        #region "Mantener Session"
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string MantenerSession()
        {
            Session["MantenerSession"] = true;
         
            return "MantenerSession";
        }

        #endregion

        #region "Salir"

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string SalirSistema()
        {
            try
            {
                Fachada.FInicioSesion objInicioSesion = new FInicioSesion();
                //cierra la sessión.
                objInicioSesion.CerrarSesion();

                string mensaje = ManejoSesion.MensajeCerrar;
                HttpContext.Current.Session.Abandon();
                return mensaje;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

    }
}
