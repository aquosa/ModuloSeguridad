using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using COA.WebCipol.Fachada;

namespace COA.WebCipol.Presentacion.UIControlsHelper.ListBox
{
    public partial class UIListBoxTareaPrimitiva : System.Web.UI.Page
    {
        public short IdSistema { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlForm Form = (HtmlForm)this.Controls[0];
            this.EnableViewState = false;

            UIListBoxTareaPrimitivaDG objListBox = (UIListBoxTareaPrimitivaDG)Form.Controls[0];
            FSeguridad objFachada = new FSeguridad();
            COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasSinAutorizacion.dcRecuperarTareasSinAutorizacion objResp = objFachada.RecuperarTareasSinAutorizacion(IdSistema);

            objListBox.DataSource = objResp.lstSE_TAREAS;
            objListBox.DataBindCombo();
        }
    }
}