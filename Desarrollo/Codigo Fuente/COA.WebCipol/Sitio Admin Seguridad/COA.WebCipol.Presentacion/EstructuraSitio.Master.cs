using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion._UIHelpers;
using EntidadesEmpresariales;

namespace COA.WebCipol.Presentacion
{
    public partial class EstructuraSitio : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (ManejoSesion.DatosCIPOLSesion == null)
                    Response.Redirect("frmInicio.aspx");
                if (!this.IsPostBack)
                {
                    string strPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                    lblUsuario.Text = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login;
                    lblVersion.Text = "CIPOL Versión " + System.Diagnostics.FileVersionInfo.GetVersionInfo(strPath).FileVersion;

                    if (ValidarLoginSSO())
                        mnuCambiarContrasenia1.Visible = false;
                }
            }
            catch (Exception)
            {
                Response.Redirect("frmInicio.aspx");
            }
        }

        private bool ValidarLoginSSO()
        {
            string strURL_SSO = System.Configuration.ConfigurationManager.AppSettings["ServicioValidacionToken"];
            if (strURL_SSO == null)
            {
                return false;
            }
            if (!String.IsNullOrWhiteSpace(strURL_SSO.Trim()))
                return true;
            else
                return false;
        }
    }
}