using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas;
using COA.WebCipol.Fachada;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales;

namespace COA.WebCipol.Presentacion
{
    public partial class frmABMAreas : PaginaPadre
    {
        /// <summary>
        /// Retorna el id de tarea que permite acceder al formulario.
        /// </summary>
        /// <history>
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// </history>
        public override string IDTarea
        {
            //Administración de Areas
            get { return "1011"; }
        }

    }
}