using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.TreeView
{
    public class dcTreeView
    {
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo> list { get; set; }
        public string Id { get; set; }
        public Unit Height { get; set; }
        public Unit Width { get; set; }
    }
}