﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion
{
    public partial class frmABMTerminales : PaginaPadre
    {
        /// <summary>
        /// Retorna el id de tarea que permite acceder al formulario.
        /// </summary>
        /// <history>
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// </history>
        public override string IDTarea
        {
            //Habilitación de Terminales operativas
            get { return "1003"; }
        }
    }
}