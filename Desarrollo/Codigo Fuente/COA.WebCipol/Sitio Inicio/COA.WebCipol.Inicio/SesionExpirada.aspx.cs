using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Inicio
{
    public partial class SesionExpirada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = "";
            if (Request.UrlReferrer != null)
                Label1.Text = Request.UrlReferrer.ToString();
        }
    }
}