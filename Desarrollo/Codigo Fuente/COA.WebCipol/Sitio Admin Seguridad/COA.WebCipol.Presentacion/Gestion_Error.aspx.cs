using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COA.WebCipol.Presentacion
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Creado  GCP-Cambios 14665
    /// </history>
    public partial class Gestion_Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblTituloCuerpo.Text = "SE HA PRODUCIDO UNA EXCEPCIÓN EN LA APLICACIÓN";

            string strMensaje = Request.QueryString["Mensaje"];
            string strStack = Request.QueryString["Stack"];
            string strOrigen = Request.QueryString["Origen"];

            strMensaje = HttpUtility.UrlDecode(strMensaje);
            strStack = HttpUtility.UrlDecode(strStack);
            strOrigen = HttpUtility.UrlDecode(strOrigen);


            txtOrigen.Text = strOrigen;
            txtMensaje.Text= strMensaje;
            txtStackTrace.Text = strStack;

        }
        protected void cmdContinuar_Click(object sender, EventArgs e)
        {
           
        }
    }
}