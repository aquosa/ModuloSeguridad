using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesEmpresariales;
using COA.Cipol.Presentacion._UIHelpers;

namespace COA.WebCipol.Presentacion.CIPOL
{
    public partial class CipolSupervision : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Me.lblNombreTarea.Text = CType(Request.QueryString("Nombre"), String).TrimEnd
                RecuperarSupervisores();
            }
        }

        private void RecuperarSupervisores()
        {
            PadreCipolCliente objCipol = (PadreCipolCliente)ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente;
            System.Data.DataSet dtsSup;

            dtsSup = objCipol.RecuperarSupervisores((int)Session["IDTareaSupervisar"]);
            cboSupervisores.DataSource = dtsSup;
            cboSupervisores.DataTextField = "Nombre";

            cboSupervisores.DataBind();

        }
    }
}