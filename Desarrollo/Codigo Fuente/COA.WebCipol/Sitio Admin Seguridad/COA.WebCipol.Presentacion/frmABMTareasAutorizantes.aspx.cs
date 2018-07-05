using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion
{
    public partial class frmABMTareasAutorizantes : PaginaPadre
    {
        /// <summary>
        /// 
        /// </summary>
        /// <history>
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// </history>
        public override string IDTarea
        {
            //Definición de Políticas generales
            get { return "1006"; }
        }
    }
}