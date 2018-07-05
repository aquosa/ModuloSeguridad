using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    public class vMonitorActividades : EntidadesBase
    {
        public vMonitorActividades()
        {
            objFiltro = new vMonitorFiltro();
        }

        public vMonitorFiltro objFiltro { get; set; }
    }

    public class itemActividad
    {
        public string usuario { get; set; }

        public string terminal { get; set; }
    }

    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    public class vMonitorFiltro : EntidadesBase
    {
        public string usuario { get; set; }

        public string terminal { get; set; }

        public string area { get; set; }

        public string nombre { get; set; }
    }
}