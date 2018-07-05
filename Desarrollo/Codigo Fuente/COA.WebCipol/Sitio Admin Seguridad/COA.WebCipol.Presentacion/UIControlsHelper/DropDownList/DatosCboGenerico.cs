using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.DropDownList
{
    public class DatosCboGenerico
    {
        public object DataSource { get; set; }
        public string Id { get; set; }
        public string DataTextField { get; set; }
        public string DataValueField { get; set; }
        public Unit Height { get; set; }
        public Unit Widht { get; set; }
        public ListItem itemTodos { get; set; }

    }
}