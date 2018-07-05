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
    /// [MartinV]          [jueves, 18 de septiembre de 2014]       Modificado  GCP-Cambios 15581
    /// </history>
    public partial class VisualizadorPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            byte[] bytPDF = (byte[])Session["DatosPDF"];
            object objNombrePDF = Session["NombrePDF"];
            string NombrePDF = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_";
            NombrePDF += objNombrePDF != null ? objNombrePDF.ToString() : "Documento.pdf";

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "filename=" + NombrePDF);
            Response.AddHeader("content-length", bytPDF.Length.ToString());
            Response.BinaryWrite(bytPDF);
            Response.Flush();
            Response.End();
        }
    }
}