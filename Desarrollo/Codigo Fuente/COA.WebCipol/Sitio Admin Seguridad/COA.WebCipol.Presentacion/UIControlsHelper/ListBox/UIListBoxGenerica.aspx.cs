using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.ListBox
{
    public partial class UIListBoxGenerica : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlForm Form = (HtmlForm)this.Controls[0];
            this.EnableViewState = false;
            UIListBoxGenericaUC objListBox = (UIListBoxGenericaUC)Form.Controls[0];
            objListBox.DataBind();
        }
    }
}