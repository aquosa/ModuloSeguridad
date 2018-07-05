using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace wsCipolServices
{
    public class ConectorwsCOA_ABMBase
    {
        private static Fachada.Seguridad.ABM.wsCOA_ABMBase wsobj
        {
            get
            {
                return (Fachada.Seguridad.ABM.wsCOA_ABMBase)HttpContext.Current.Session["wsCOA_ABMBase"];
            }
            set
            {
                HttpContext.Current.Session["wsCOA_ABMBase"] = value;
            }
        }
        private Fachada.Seguridad.ABM.wsCOA_ABMBase obj
        {
            get
            {
                if (wsobj == null)
                {
                    wsobj = new Fachada.Seguridad.ABM.wsCOA_ABMBase();
                    wsobj.CookieContainer = ((COA.WebCipol.Comun.DatosCIPOL)System.Web.HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.CIPOL]).DatosPadreCIPOLCliente.objColeccionDeCookies;
                    wsobj.Timeout = System.Threading.Timeout.Infinite;
                }
                return wsobj;
            }
        }

        public bool EstablecerConexionActiva(string IDConexion)
        {
            return obj.EstablecerConexionActiva(IDConexion);
        }
        public DataSet Recuperar(string NombreTabla)
        {
            return obj.Recuperar(NombreTabla);
        }
        public Int32 Grabar(DataSet Dataset, string NombreTabla, Fachada.Seguridad.ABM.TipoProceso TipoProceso)
        {
            return obj.Grabar(Dataset, NombreTabla, TipoProceso);
        }
        public DateTime FechaServidor()
        {
            return obj.FechaServidor();
        }
    }
}