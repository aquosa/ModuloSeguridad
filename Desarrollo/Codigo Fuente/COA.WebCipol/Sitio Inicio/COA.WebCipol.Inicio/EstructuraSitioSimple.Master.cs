using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Inicio._UIHelpers;

namespace COA.WebCipol.Presentacion.Formularios
{
    public partial class EstructuraSitioSimple : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ManejoSesion.DatosCIPOLSesion == null)
                    Response.Redirect("frmLogin.aspx");
                if (!this.IsPostBack)
                {
                    string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    lblUsuario.Text = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login;
                    lblVersion.Text = "CIPOL Versión " + System.Diagnostics.FileVersionInfo.GetVersionInfo(strPath).FileVersion;

                }
            }
            catch (Exception)
            {
                Response.Redirect("frmLogin.aspx");
            }
        }
    }
}