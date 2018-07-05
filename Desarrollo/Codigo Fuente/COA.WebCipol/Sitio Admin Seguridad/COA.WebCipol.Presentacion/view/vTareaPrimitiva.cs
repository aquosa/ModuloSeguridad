using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vTareaPrimitiva : EntidadesBase
    {
        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        public vTareaPrimitiva()
        {
            objFiltro = new vTareasPrimitivasFiltros();
        }

        public vTareasPrimitivasFiltros objFiltro { get; set; }

        public bool update { get; set; }

        public int IDTAREA { get; set; }

        public string CODIGOTAREA { get; set; }

        public string DESCRIPCIONTAREA { get; set; }

        public int IDSISTEMA { get; set; }

        public string DESCSISTEMA { get; set; }

        public string REQUIEREAUDITORIA { get; set; }

    }


    public class TareasPrimitivas : EntidadesBase
    {
        public List<Entidades.ClasesWs.dcRecuperarTareasPrimitivas.TAREAS> jsontareasprimitivas { get; set; }

        public int cantidadRegistros { get; set; }
    }

    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    public class vTareasPrimitivasFiltros : EntidadesBase
    {
        public string filtroCODIGOTAREA { get; set; }

        public string filtroDESCRIPCIONTAREA { get; set; }

        public string filtroIDAUTORIZACION { get; set; }

        public int filtroIDTAREA { get; set; }

        public int filtroSistema { get; set; }
    }
}