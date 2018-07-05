using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion
{
    public partial class frmAnalisisDeAuditoria : PaginaPadre
    {
        /// <summary>
        /// Retorna el id de tarea que permite acceder al formulario.
        /// </summary>
        /// <history>
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// </history>
        public override string IDTarea
        {
            //Análisis de Auditoría
            get { return "1016"; }
        }

        /// <summary>
        /// Es la cantidad maxima de registros a recuperar siempre y cuando el usuario no indique lo contrario
        /// </summary>
        public string CantidadRegistrosDefault
        {
            get { return "5000"; }
        }
    }
}