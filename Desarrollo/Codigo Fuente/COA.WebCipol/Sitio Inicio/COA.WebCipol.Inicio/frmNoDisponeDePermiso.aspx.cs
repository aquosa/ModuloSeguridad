using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Inicio
{
    public partial class frmNoDisponeDePermiso : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMENSAJE.Text = "No dispone de permiso para esta acción";
        }
    }
}