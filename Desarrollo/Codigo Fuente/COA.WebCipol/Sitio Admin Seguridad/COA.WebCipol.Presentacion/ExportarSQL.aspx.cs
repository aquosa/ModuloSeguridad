using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using COA.WebCipol.Presentacion.view;
using System.Web.Services;

namespace COA.WebCipol.Presentacion.PageBuilders
{
    public partial class ExportarSQL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExportarArchivoSQL(objDatosStringSQL.stringSQL);
        }
        public void ExportarArchivoSQL(StringSQL obj)
        {
            //try
            //{
                var texto = new UTF8Encoding(true).GetBytes(obj.STRINGSQL);

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/octet-stream";

                Response.AddHeader("content-disposition", "filename=*.sql");
                Response.AddHeader("content-length", texto.Length.ToString());
                Response.BinaryWrite(texto);
                Response.Flush();
                //Response.End();
            //}
            //catch (Exception ex)
            //{
            //    throw (ex);
            //}

        }

        private vSessionStringSQL objDatosStringSQL
        {
            get
            {
                return (vSessionStringSQL)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.STRING_SQL];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.STRING_SQL] = value;
            }
        }
    }
}