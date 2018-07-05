using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesEmpresariales;

namespace COA.WebCipol.Presentacion
{
    public partial class frmPrincipal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                
            }
            //string strCipol = "";
            //if (Request["stringcipol"] != null)
            //    strCipol = Request["stringcipol"].ToString();

            //if (!string.IsNullOrEmpty(strCipol))
            //{
            //    PadreCipolCliente objUsuarioCipol;
            //    //Dim objFlujo As System.IO.MemoryStream
            //    System.IO.MemoryStream objFlu;
            //    //Dim objDeserializador As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            //    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objDeser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    //Dim objSerializar As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
            //    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objSerializar = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

            //    //objFlujo = New System.IO.MemoryStream(System.Convert.FromBase64CharArray(pStrCipol.ToCharArray, 0, pStrCipol.Length))
            //    objFlu = new System.IO.MemoryStream(System.Convert.FromBase64CharArray(strCipol.ToCharArray(), 0, strCipol.Length));

            //    //gobjUsuarioCipol = CType(objDeserializador.Deserialize(objFlujo), EntidadesEmpresariales.PadreCipolCliente)
            //    objUsuarioCipol = (PadreCipolCliente)objDeser.Deserialize(objFlu);

            //}

        }
    }
}