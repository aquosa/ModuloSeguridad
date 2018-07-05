using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.DropDownList
{
    public partial class UIcboGenerico : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlForm Form = (HtmlForm)this.Controls[0];
            this.EnableViewState = false;
            UIcboGenericoUC objcbo = (UIcboGenericoUC)Form.Controls[0];
            //Bindea.
            objcbo.DataBindCombo();
        }
    }
}