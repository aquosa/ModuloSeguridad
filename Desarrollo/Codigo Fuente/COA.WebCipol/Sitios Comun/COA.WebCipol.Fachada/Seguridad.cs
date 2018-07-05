using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using EntidadesEmpresariales;
using System.Web;
using COA.WebCipol.Comun;

namespace COA.WebCipol.Fachada
{
    public sealed class Seguridad
    {
        //private IPrincipal objSeguridadOrigen;
        //private bool mblnAplicoSeguridad;

        public void AplicarSeguridadCP()
        {
            //mblnAplicoSeguridad = false;
            ////En caso de que la seguridad ya este aplicada no realiza ninguna accion
            //if (System.Threading.Thread.CurrentPrincipal is EntidadesEmpresariales.PadreCipolCliente)
            //    return;

            //EntidadesEmpresariales.PadreCipolCliente objCipol;
            //objCipol = (PadreCipolCliente)((DatosCIPOL)HttpContext.Current.Session[Constantes.VariablesDeSesion.CIPOL]).DatosPadreCIPOLCliente;
            //objSeguridadOrigen = System.Threading.Thread.CurrentPrincipal;
            //System.Threading.Thread.CurrentPrincipal = objCipol;
            //mblnAplicoSeguridad = true;
        }
                
        public void UndoAplicarSeguridadCP()
        {
            //if (mblnAplicoSeguridad)
            //    System.Threading.Thread.CurrentPrincipal = objSeguridadOrigen;
            //mblnAplicoSeguridad = false;
        }


    }
}
