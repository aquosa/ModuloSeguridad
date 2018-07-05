using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.DropDownList
{
    public partial class UIdgcboAreas : System.Web.UI.UserControl
    {

        public object DataSource
        {
            get { return cboAreas.DataSource; }
            set { cboAreas.DataSource = value; }
        }

        public void DataBindCombo()
        {
            cboAreas.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}