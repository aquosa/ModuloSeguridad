using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntidadesEmpresariales;
using COA.Cipol.Presentacion._UIHelpers;
using COA.WebCipol.Presentacion.Controlers;
using Fachada;

namespace COA.WebCipol.Presentacion
{
    public partial class frmInicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strCipol = "";
            if (Request["stringcipol"] != null)
                strCipol = Request["stringcipol"].ToString();

            if (!string.IsNullOrEmpty(strCipol))
            {
                EntidadesEmpresariales.PadreCipolCliente objUsuarioCipol;
                //Dim objFlujo As System.IO.MemoryStream
                System.IO.MemoryStream objFlu;
                //Dim objDeserializador As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objDeser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //Dim objSerializar As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objSerializar = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                //objFlujo = New System.IO.MemoryStream(System.Convert.FromBase64CharArray(pStrCipol.ToCharArray, 0, pStrCipol.Length))
                objFlu = new System.IO.MemoryStream(System.Convert.FromBase64CharArray(strCipol.ToCharArray(), 0, strCipol.Length));

                //gobjUsuarioCipol = CType(objDeserializador.Deserialize(objFlujo), EntidadesEmpresariales.PadreCipolCliente)
                objUsuarioCipol = (EntidadesEmpresariales.PadreCipolCliente)objDeser.Deserialize(objFlu);

                //Proceso de autentificacion.
                ManejoSesion.DatosCIPOLSesion = new Comun.DatosCIPOL();
                ManejoSesion.DatosSistemaSesion = new Comun.DatosSistema();
                ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente = objUsuarioCipol;
                ManejoSesion.MensajeCerrar = "El Sistema se ha cerrado con éxito.";
              
                cFormLogin objUILogin = new cFormLogin();
                string Mensaje = "";
                //Carga los parámetros generales del Sistema en Sesión.
                if (!objUILogin.CargarParametros(ref Mensaje))
                {
                    //Si falló al cargar los parámetros generales.
                    mensajesession.Text = Mensaje;
                    return;
                }
                General objGeneral;
                objGeneral = new General(System.Reflection.Assembly.GetExecutingAssembly());
                objGeneral.AcercaDe_Descripcion = "Componente de Seguridad. Desarrollado por COA S.A.";
                objGeneral.AcercaDe_Detalle = "Configurador Interactivo de Políticas de seguridad de los sistemas. Resuelve las funciones operativas propias de la seguridad de sistemas (implementación de políticas, administración de usuarios,  roles, acceso a subsistemas).";
                objGeneral.AcercaDe_Cliente = objUsuarioCipol.NombreOrganizacion;
                objGeneral.UsuarioCIPOL = objUsuarioCipol.Login;

                objGeneral.Hoy = objUsuarioCipol.FechaServidor;
                ManejoSesion.DatosSistemaSesion.DatosGenerales = objGeneral;
                ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreOrganizacion;

                Response.Redirect("frmPrincipal.aspx");
            }
            else
            {
                cInicioSesion objIS = new cInicioSesion();
                objIS.RegistrarExpiroSesion();

                mensajesession.Text = "No se detecta sesión iniciada o la misma ha expirado";
            }

        }
    }
}