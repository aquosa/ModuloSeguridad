using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion.UIControlsHelper.DropDownList
{
    public partial class UIcboGenericoUC : System.Web.UI.UserControl
    {

        private DatosCboGenerico _datos;
        public DatosCboGenerico datos
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
                cbo.DataSource = datos.DataSource;
                if (!string.IsNullOrEmpty(datos.Id))
                    cbo.ID = datos.Id;
                if (!string.IsNullOrEmpty(datos.DataTextField))
                    cbo.DataTextField = datos.DataTextField;
                if (!string.IsNullOrEmpty(datos.DataValueField))
                    cbo.DataValueField = datos.DataValueField;
                //if (datos.Height != null && !datos.Height.IsEmpty)
                //    cbo.Height = datos.Height;
                //else
                //   // cbo.Height = new System.Web.UI.WebControls.Unit(22);

                //if (datos.Widht != null && !datos.Widht.IsEmpty)
                //    cbo.Width = datos.Widht;
                //else
                    //cbo.Width = new System.Web.UI.WebControls.Unit(150);
            }
        }

        public void DataBindCombo()
        {
            cbo.DataBind();
            if (datos.itemTodos != null)
                cbo.Items.Insert(0, datos.itemTodos);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}