using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsCipolServices.ConectorWSCipolNET
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [lunes, 06 de octubre de 2014]       Modificado  GCP-Cambios 15588
    /// </history>
    public class ConectorwsCipolSupervision
    {
        private static Fachada.Seguridad.CipolSupervision.wsCipolSupervision wsobj
        {
            get
            {
                return (Fachada.Seguridad.CipolSupervision.wsCipolSupervision)HttpContext.Current.Session["wsCipolSupervision"];
            }
            set
            {
                HttpContext.Current.Session["wsCipolSupervision"] = value;
            }
        }

        private Fachada.Seguridad.CipolSupervision.wsCipolSupervision obj
        {
            get
            {
                if (wsobj == null)
                {
                    wsobj = new Fachada.Seguridad.CipolSupervision.wsCipolSupervision();
                    wsobj.CookieContainer = ((COA.WebCipol.Comun.DatosCIPOL)System.Web.HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.CIPOL]).DatosPadreCIPOLCliente.objColeccionDeCookies;
                    wsobj.Timeout = System.Threading.Timeout.Infinite;
                }
                return wsobj;
            }
        }

        public bool ValidarSupervisor(string Usuario, string Clave)
        {
            return obj.ValidarSupervisor(Usuario,Clave);
        }

        public bool ValidarSupervisorConAuditoria(string Usuario, string Clave, int IDUsuarioSupervisor, int IDUsuario, int IDTareaSupervisar, string Terminal)
        {
            return obj.ValidarSupervisorConAuditoria(Usuario, Clave, IDUsuarioSupervisor, IDUsuario, IDTareaSupervisar, Terminal);
        }
    }
}