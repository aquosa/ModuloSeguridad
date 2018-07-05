using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Fachada;
using COA.WebCipol.Entidades.ClasesWs;
using wsCipolServices.ReglasDeNegocio;

namespace wsCipolServices
{
    /// <summary>
    /// Descripción breve de wsCipolSupervision
    /// </summary>
    /// <history>
    /// [MartinV]          [lunes, 06 de octubre de 2014]       Modificado  GCP-Cambios 15588
    /// </history>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsCipolSupervision : System.Web.Services.WebService
    {


        [WebMethod(EnableSession = true)]
        public bool ValidarSupervisor(dcValidarSupervisor obj)
        {
            try
            {
                try
                {
                    RNwsCipolSupervision rnReglaSNegocio = new RNwsCipolSupervision();
                    return rnReglaSNegocio.ValidarSupervisor(obj);
                }
                catch (Exception ex)
                {
                    COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool ValidarSupervisorConAuditoria(dcValidarSupervisor obj)
        {
            try
            {
                try
                {
                    RNwsCipolSupervision rnReglaSNegocio = new RNwsCipolSupervision();
                    return rnReglaSNegocio.ValidarSupervisorConAuditoria(obj);
                }
                catch (Exception ex)
                {
                    COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
    }
}
