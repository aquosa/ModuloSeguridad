using COA.Cipol.Inicio._UIHelpers;
using COA.ConectorServicio;
using COA.WebCipol.Comun;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace COA.WebCipol.Inicio
{
    public partial class frmLoginSSO : System.Web.UI.Page
    {
        private string _ID { get; set; }
        private string _Pass { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string strMensaje = "";
                    //Validación del token SSO
                    if (!ValidacionToken())
                    {
                        Response.Redirect("Gestion_Error.aspx?Mensaje=El token es inválido");
                        return;
                    }

                    string IP = System.Configuration.ConfigurationManager.AppSettings["MachineNameSSO"];
                    COA.WebCipol.Inicio.Controlers.cFormLogin objFormLogin = new COA.WebCipol.Inicio.Controlers.cFormLogin();

                    //Autentica el usuario.
                    if (!objFormLogin.AutenticarUsuario(ref strMensaje, _ID, _Pass, new System.Net.CookieContainer(), IP))
                    {
                        //Falló autenticación.
                        //REDIRIGIR A UNA PANTALLA DE ERROR con strMensaje
                        //Server.Execute("Gestion_Error.aspx?Mensaje=" + strMensaje, true);
                        Response.Redirect("Gestion_Error.aspx?Mensaje=" + strMensaje);
                        return;
                    }

                    //Validar Dominio
                    //if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario.Equals(0))
                    //{
                    //    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio.Equals("X"))
                    //    {
                    //        //Abrir Administrar Tipo de Seguridad.
                    //        Response.Redirect("frmTipoSeguridad.aspx?CambiarDominio=false");
                    //        e.Authenticated = false;
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio == null || ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio == Constantes.SeguridadNODefinida)
                    //    {
                    //        ucLogin.FailureText = "No existe un tipo de seguridad establecido, imposible iniciar la aplicación.";
                    //        e.Authenticated = false;
                    //        return;
                    //    }
                    //}


                    //Redirecciona a la página que obliga a cambiar clave
                    //if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave") == "1")
                    //{
                    //    try
                    //    {
                    //        if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave.SeDebePreguntar") == "1")
                    //        {
                    //            ///     1 No obligatorio
                    //            ManejoSesion.ModoCambioClave = 1;
                    //            e.Authenticated = true;
                    //            return;
                    //        }
                    //    }
                    //    catch (Exception)
                    //    {
                    //    }
                    //    ///     3 Obligatorio debido a que se debe forzar el cambio de la contraseña
                    //    ManejoSesion.ModoCambioClave = 3;
                    //    //[GonzaloP]          [viernes, 22 de julio de 2016]       Work-Item: 7289 - Se agrega el parámetro "true" para terminar la ejecución de la página actual.
                    //    Response.Redirect("ChangedPassword\\frmCambiarContrasenia.aspx?url=../frmLogin.aspx", true);
                    //    e.Authenticated = false;
                    //    return;
                    //}

                    //e.Authenticated = true;
                    ManejoSesion.MensajeCerrar = "El Sistema se ha cerrado con éxito.";

                    string urlPost = "";
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario == Constantes.IDUsuarioMaster)
                    {
                        List<Se_SistemasHabilitados> objListaSistemas = ManejoSesion.DatosCIPOLSesion.ObtenerListaSistemas();
                        if (objListaSistemas.Count == 1 && objListaSistemas[0].IDSistema == 1)
                        {
                            urlPost = "frmLogueado.aspx?urlPost=" + objListaSistemas[0].PaginaPorDefecto + "&target=" + objListaSistemas[0].IDSistema;
                        }
                    }
                    else
                    {
                        List<Se_SistemasHabilitados> objListaSistemas = ManejoSesion.DatosCIPOLSesion.ObtenerListaSistemas();
                        if (objListaSistemas.Count == 1 && objListaSistemas[0].IDSistema == 1)
                        {
                            //TFS-Work Item ID: 4750
                            urlPost = "frmLogueado.aspx?urlPost=" + objListaSistemas[0].PaginaPorDefecto + "&target=" + objListaSistemas[0].IDSistema;
                        }
                        else
                        {
                            urlPost = "frmSistemasPermitidos.aspx";
                        }
                        //try
                        //{
                        //    //todo: martinv -> ver si conviene pasar antes por cambiar contraseña o frmlogueado?
                        //    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave.SeDebePreguntar") == "1")
                        //    {
                        //        urlPost = "ChangedPassword\\frmCambiarContrasenia.aspx?url=../frmLogin.aspx&urlPost=../" + urlPost + "&preguntar=" + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave.Mensaje");
                        //    }
                        //}
                        //catch (Exception)
                        //{
                        //    throw;
                        //}
                    }

                    //Server.Execute(urlPost, true);
                    Response.Redirect(urlPost);

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
        }
        /// <summary>
        /// //INVOCAR WS DE AUTENTICACION con el TOKEN y OBTENER ID y PASS 
        /// </summary>
        /// <returns></returns>
        private bool ValidacionToken()
        {
            try
            {
                //Recuperación del token del request.
                string Token = Request["Token"].ToString();

                NProtocolWS _protocolWS = new NProtocolWS();
                _protocolWS.sUrl = System.Configuration.ConfigurationManager.AppSettings["ServicioValidacionToken"];
                _protocolWS.sSoapAction = System.Configuration.ConfigurationManager.AppSettings["ServicioValidacionTokenAction"];
                _protocolWS.authenticate = AuthenticateEnum.DEFAULT;

                string sXsl = File.ReadAllText(Server.MapPath("~/Template/AuthenticateToken.xslt"));
                string sXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?><root><inToken>" + Token + "</inToken></root>";
                string sXmlResult = UtilXML.TransformXML(sXml, sXsl);
                XElement doc = XElement.Parse(sXmlResult);
                _protocolWS.sXml = doc.ToString(SaveOptions.DisableFormatting);

                //todo: martinv. Quitar luego de terminar con las pruebas de conexión en el cliente
                Logger.Logueador.Loggear("xml IN :" + _protocolWS.sXml, System.Diagnostics.EventLogEntryType.Information);

                string respuesta = _protocolWS.Execute();

                //todo: martinv. Quitar luego de terminar con las pruebas de conexión en el cliente
                Logger.Logueador.Loggear("xml OUT :" + respuesta, System.Diagnostics.EventLogEntryType.Information);
                string _ID_APP = UtilXML.GetTagValue(respuesta, "ID_APP", false);
                //Si el Id de app no es retornado no es válido el token
                if (string.IsNullOrWhiteSpace(_ID_APP) || !_ID_APP.Equals(System.Configuration.ConfigurationManager.AppSettings["ID_APP"]))
                    return false;

                //_ID = UtilXML.GetTagValue(respuesta, "ID_USUARIO", false);
                _ID = UtilXML.GetTagValue(respuesta, "LOGIN", false);
                _Pass = "noaplica";
                //_Pass = UtilXML.GetTagValue(respuesta, "PASSWORD", false);

                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}