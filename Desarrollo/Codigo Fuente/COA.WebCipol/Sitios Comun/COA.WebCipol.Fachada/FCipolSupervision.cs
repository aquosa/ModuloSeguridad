using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Entidades.ClasesWs;

namespace COA.WebCipol.Fachada
{
    public class FCipolSupervision : FPadreFachada
    {
        private wsCipolServices.wsCipolSupervision wsobj;
        private wsCipolServices.wsCipolSupervision obj
        {
            get
            {
                if (wsobj == null) wsobj = new wsCipolServices.wsCipolSupervision();
                return wsobj;
            }
        }

        public bool ValidarSupervisor(dcValidarSupervisor datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool objRetorno = obj.ValidarSupervisor(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public bool ValidarSupervisorConAuditoria(dcValidarSupervisor datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool objRetorno = obj.ValidarSupervisorConAuditoria(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }
    }
}
