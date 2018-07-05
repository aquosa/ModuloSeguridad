using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion._UIHelpers;

namespace COA.WebCipol.Presentacion
{
    public partial class frmAcercaDe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblDescripcion.Text = ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Descripcion;
            lblDetalle.Text = ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Detalle;
            lblCliente.Text = ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente;
        }
    }
}