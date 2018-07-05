using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using wsCipolServices.ReglasDeNegocio;

namespace wsCipolServices
{
    /// <summary>
    /// Descripción breve de wsCOA_ABMBase
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCOA_ABMBase : System.Web.Services.WebService
    {
        [WebMethod(EnableSession = true)]
        public bool EstablecerConexionActiva(string IDConexion)
        {
            ConectorwsCOA_ABMBase objConector;
            try
            {
                objConector = new ConectorwsCOA_ABMBase();
                return objConector.EstablecerConexionActiva(IDConexion);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                return false;
            }
        }

        [WebMethod(EnableSession = true)]
        //public DataSet Recuperar(string NombreTabla)
        public object Recuperar(string NombreTabla)
        {
            try
            {
                RNwsCOA_ABMBase rnReglaSNegocio = new RNwsCOA_ABMBase();
                return rnReglaSNegocio.Recuperar(NombreTabla);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public Int32 Grabar(Object Dataset, string NombreTabla, Fachada.Seguridad.ABM.TipoProceso TipoProceso)
        {
            try
            {
                RNwsCOA_ABMBase rnReglaSNegocio = new RNwsCOA_ABMBase();
                return rnReglaSNegocio.Grabar(Dataset, NombreTabla, TipoProceso);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public DateTime FechaServidor()
        {
            ConectorwsCOA_ABMBase objConector;
            try
            {
                objConector = new ConectorwsCOA_ABMBase();
                return objConector.FechaServidor();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw (ex);
            }
        }
    }
}
