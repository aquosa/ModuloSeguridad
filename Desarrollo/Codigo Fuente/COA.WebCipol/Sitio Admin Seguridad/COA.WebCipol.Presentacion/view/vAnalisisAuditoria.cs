using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vAnalisisAuditoria : EntidadesBase
    {

        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        public vAnalisisAuditoria()
        {
            objFiltro = new vAuditoriaFiltro();
        }

        public vAuditoriaFiltro objFiltro { get; set; }

        public string jsoncbotablas { get; set; }
        public string jsoncbousuarios { get; set; }
        public string jsoncbosistemas { get; set; }
        public string jsoncbonombrepc { get; set; }
        public string jsoncbosupervisores { get; set; }
            
    }

    public class StringSQL
    {
        public string STRINGSQL { get; set; }
    }


    [Serializable]
    public class vSessionStringSQL
    {
        public StringSQL stringSQL { get; set; }
    }

    public class vEventosAuditoria:EntidadesBase
    {
        public string jsonevenetosauditoria { get; set; }
        public string cantidadRegistros { get; set; }
    }

    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    public class vAuditoriaFiltro : EntidadesBase
    { 
        public string fechaDesde { get; set; }
        public string fechaHasta { get; set; }
        public string tabla { get; set; }
        public string usuario { get; set; }
        public string operacion { get; set; }
        public string supervisor { get; set; }
        public string nombrePC { get; set; }
        public string sistema { get; set; }
        public string textoBusqueda { get; set; }
        public string CantidadRegistrosDefault { get; set; }
    }
}