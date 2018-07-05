using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsCipolServices.ConectorWSCipolNET
{
    public class ConectorwsInicioSesion_Java
    {
        private static Fachada.Seguridad.InicioSesion_Java.wsInicioSesion_Java wsobj
        {
            get
            {
                return (Fachada.Seguridad.InicioSesion_Java.wsInicioSesion_Java)HttpContext.Current.Session["wsInicioSesion_Java"];
            }
            set
            {
                HttpContext.Current.Session["wsInicioSesion_Java"] = value;
            }
        }
        private Fachada.Seguridad.InicioSesion_Java.wsInicioSesion_Java obj
        {
            get
            {
                if (wsobj == null)
                {
                    wsobj = new Fachada.Seguridad.InicioSesion_Java.wsInicioSesion_Java();
                    wsobj.CookieContainer = ((COA.WebCipol.Comun.DatosCIPOL)System.Web.HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.CIPOL]).DatosPadreCIPOLCliente.objColeccionDeCookies;
                    wsobj.Timeout = System.Threading.Timeout.Infinite;
                }
                return wsobj;
            }
        }

        public string IniciarSesion(string pUsuario, string pTerminal, ref string pError, string pPassword, ref bool pTerminal_ActualizacionLAN)
        {
            //string strRetorno = "";
            //try
            //{
            //    obj = new Fachada.Seguridad.InicioSesion_Java.wsInicioSesion_Java();
            //    strRetorno = obj.IniciarSesion(pUsuario, pTerminal, pPassword, ref pError, ref pTerminal_ActualizacionLAN);
            //}
            //catch (Exception Ex)
            //{
            //    //throw;
            //}
            //return strRetorno;
            return obj.IniciarSesion(pUsuario, pTerminal, pPassword, ref pError, ref pTerminal_ActualizacionLAN);
        }
    }
}