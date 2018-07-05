using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.ListBox
{
    public partial class UIListBoxGenericaUC : System.Web.UI.UserControl
    {
        private DatosListBoxGenerico _datos;
        public DatosListBoxGenerico datos
        {
            get { return _datos; }
            set
            {
                _datos = value;
                SetDatos();
            }
        }

        private void SetDatos()
        {
            if (datos == null) return;

            if (datos.DataSource != null)
            {
                ListBox.DataSource = datos.DataSource;
                if (!string.IsNullOrEmpty(datos.Id))
                    ListBox.ID = datos.Id;
                if (!string.IsNullOrEmpty(datos.DataTextField))
                    ListBox.DataTextField = datos.DataTextField;
                if (!string.IsNullOrEmpty(datos.DataValueField))
                    ListBox.DataValueField = datos.DataValueField;
                //if (datos.Height != null)
                //    ListBox.Height = datos.Height;
                //if (datos.Width != null)
                //    ListBox.Width = datos.Width;
            }
        }


        public void DataBind()
        {
            ListBox.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}