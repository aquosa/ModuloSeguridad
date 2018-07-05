using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion._UIHelpers;

namespace COA.WebCipol.Presentacion.Formularios
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14414
    /// </history>
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
                    datos.InnerText = "Usuario: " + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login + " || " + " v. " + System.Diagnostics.FileVersionInfo.GetVersionInfo(strPath).FileVersion;
                }
            }
            catch (Exception)
            {
                Response.Redirect("frmInicio.aspx");
            }
        }
    }
}