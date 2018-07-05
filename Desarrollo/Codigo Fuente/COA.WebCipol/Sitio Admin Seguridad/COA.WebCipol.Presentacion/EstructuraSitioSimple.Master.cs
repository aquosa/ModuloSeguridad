using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion._UIHelpers;

namespace COA.WebCipol.Presentacion.Formularios
{
    public partial class EstructuraSitioSimple : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ManejoSesion.DatosCIPOLSesion == null)
                    Response.Redirect("frmInicio.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("frmInicio.aspx");
            }
        }
    }
}