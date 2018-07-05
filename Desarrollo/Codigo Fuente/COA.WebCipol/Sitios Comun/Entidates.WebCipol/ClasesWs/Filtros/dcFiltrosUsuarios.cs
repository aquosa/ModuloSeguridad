using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.Filtros
{
    //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
    public class dcFiltrosUsuarios 
    {
        public string filtro { get; set; }
        public string filtrobaja { get; set; }
        public bool chkUsu { get; set; }
        public bool chkNombre { get; set; }
        public bool chkArea { get; set; }
        public bool chkSubCadenas { get; set; }
    }
}
