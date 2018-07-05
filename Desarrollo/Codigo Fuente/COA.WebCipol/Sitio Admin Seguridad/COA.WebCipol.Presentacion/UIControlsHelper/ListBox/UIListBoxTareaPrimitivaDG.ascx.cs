using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.ListBox
{
    public partial class UIListBoxTareaPrimitivaDG : System.Web.UI.UserControl
    {

        public object DataSource
        {
            get { return lbTareasPrimitivas.DataSource; }
            set { lbTareasPrimitivas.DataSource = value; }
        }

        public void DataBindCombo()
        {
            lbTareasPrimitivas.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}