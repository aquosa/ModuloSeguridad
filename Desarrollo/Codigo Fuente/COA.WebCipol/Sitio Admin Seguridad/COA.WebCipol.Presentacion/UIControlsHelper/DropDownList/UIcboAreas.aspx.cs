using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using COA.WebCipol.Fachada;

namespace COA.WebCipol.Presentacion.UIControlsHelper.DropDownList
{
    public partial class UIcboAreas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlForm Form = (HtmlForm)this.Controls[0];
            this.EnableViewState = false;

            UIdgcboAreas objGrilla = (UIdgcboAreas)Form.Controls[0];
            FSeguridad objFachada = new FSeguridad();
            COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.dcRecuperarDatosParaABMTerminales objResp = objFachada.RecuperarDatosParaABMTerminales();

            objGrilla.DataSource = objResp.lstKAREAS;
            objGrilla.DataBindCombo();
        }
    }
}