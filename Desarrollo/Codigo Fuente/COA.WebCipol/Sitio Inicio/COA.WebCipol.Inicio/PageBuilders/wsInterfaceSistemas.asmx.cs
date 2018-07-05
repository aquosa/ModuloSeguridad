using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Net;
using System.IO;
using COA.Cipol.Inicio._UIHelpers;
using COA.WebCipol.Inicio.view;
using COA.WebCipol.Inicio.Controlers;
using WebAppControlDualLogin.Model;

namespace COA.WebCipol.Presentacion.PageBuilders
{
    /// <summary>
    /// Summary description for wsInterfaceSistemas
    /// </summary>
    /// <history>
    /// [LucianoP]          [jueves, 6 de abril de 2017]    Controla que la sesion no haya expirado
    /// </history>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsInterfaceSistemas : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSistemasPermitidos ObtenerObjetoCIPOL(string IDSistemaActual)
        {
            try
            {
                Byte[] bytCipol;
                System.IO.MemoryStream objFlujo = new MemoryStream();
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objSerializador = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //todo: Ver como se manejan estas variables.
                //.Key
                //.IV

                //Control de sesion activa
                if (ManejoSesion.DatosCIPOLSesion == null)
                {
                    cInicioSesion objIS = new cInicioSesion();
                    objIS.RegistrarExpiroSesion();
                    return new vSistemasPermitidos() { ResultadoEjecucion = false, MensajeError = "La sesión ha expirado" };
                }

                if (!DatosSesion.Control.VerificarId(Application["Sessions"] as List<mSession>, Session))
                {
                    cInicioSesion objIS = new cInicioSesion();
                    objIS.RegistrarExpiroSesion();
                    return new vSistemasPermitidos() { ResultadoEjecucion = false, MensajeError = "La sesión ha expirado" };
                }

                ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDSistemaActual = Convert.ToInt16(IDSistemaActual);
                objSerializador.Serialize(objFlujo, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente);
                bytCipol = objFlujo.ToArray();

                return new vSistemasPermitidos() { ResultadoEjecucion = true, strcipol = System.Convert.ToBase64String(bytCipol) };
            }
            catch (Exception ex)
            {
                return new vSistemasPermitidos() { ResultadoEjecucion = false, MensajeError = "Ha ocurrido un error al procesar la solicitud \r\n" + ex.Message };
            }
        }
    }
}
