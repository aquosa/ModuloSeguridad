using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Inicio._UIHelpers;
using COA.WebCipol.Fachada;
using COA.WebCipol.Comun;
using COA.WebCipol.Inicio.Controlers;
using COA.WebCipol.Inicio.model;
using Microsoft.VisualBasic;
using WebAppControlDualLogin.Model;
using System.Configuration;

namespace COA.Cipol.Presentacion
{
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [viernes, 08 de noviembre de 2013]       Creado  GCP-Cambios 14582
    /// </history>
    public partial class frmLogin : System.Web.UI.Page
    {
        #region COMPORTAMIENTO
        /// <summary>
        /// Evento Load.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            cFormLogin objUILogin = new cFormLogin();
            if (!IsPostBack)
            {
                try
                {

                    //Recupera el Tipo de Seguridad y nombre de Dominio para Mostrar.
                    HttpContext.Current.Session["objCipol"] = new EntidadesEmpresariales.PadreCipolCliente();
                    this.lblNombreDominio.Text = objUILogin.RecuperarTipoSeguridadYNombreDeDominio(new System.Net.CookieContainer());

                    // [IvanSa] Se registra la funcion de Jquery para obtener el ip del cliente.
                    if (System.Configuration.ConfigurationManager.AppSettings["EnviarDireccionIP"] == "S" &&
                            System.Configuration.ConfigurationManager.AppSettings["ServicioPublicoIP"] == "S")
                    {
                        string script = string.Format("ObtenerIP();");
                        if (!ClientScript.IsClientScriptBlockRegistered("myScript"))
                        {
                            ClientScript.RegisterClientScriptBlock(typeof(frmLogin), "myScript", script, true);
                        }

                    }
                }
                catch (Exception)
                {
                    //TODO MOSTRAR ERROR
                    throw;
                }
            }

        }

        /// <summary>
        /// Autenticación de control de usuario de Login.
        /// </summary>
        protected void ucLogin_Authenticate(Object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
        {
            List<mSession> Sesiones = Application["Sessions"] as List<mSession>;
            mSession SesionUsuario = null;

            try
            {
                string strMensaje = "";

                //Validaciones básicas.
                if (!ValidarDatos(ref strMensaje))
                {
                    //Fallaron validaciones básicas.
                    ucLogin.FailureText = strMensaje;

                    e.Authenticated = false;
                    return;
                }

                cFormLogin objFormLogin = new cFormLogin();

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //Control de login de usuarios
                bool blnValidarSesion = DatosSesion.Control.Verificar(ucLogin.UserName, Sesiones, out strMensaje);

                if (!blnValidarSesion)
                {
                    ucLogin.FailureText = strMensaje;
                    e.Authenticated = false;
                    return;
                }

                DatosSesion.Control.Guardar(ucLogin.UserName, Sesiones, Session, ref SesionUsuario);
                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                //Autentica el usuario.
                string ip = hdnIP.Value.ToString();

                //[GonzaloP]          [miércoles, 22 de febrero de 2017]       Work-Item: 9131
                bool LoginSSO = false;

                //Si el usuario no es "master" entonces se valida si está habilitado el logueo por SSO.
                if (ucLogin.UserName != "master")
                {
                    string strURL_SSO = System.Configuration.ConfigurationManager.AppSettings["ServicioValidacionToken"];

                    if (!String.IsNullOrWhiteSpace(strURL_SSO.Trim()))
                        LoginSSO = true;
                }

                //Sólo el usuario "master" puede acceder directamente a CIPOL cuando está habilitado el logueo por SSO.
                if (!LoginSSO)
                {

                    if (!objFormLogin.AutenticarUsuario(ref strMensaje, ucLogin.UserName, ucLogin.Password, new System.Net.CookieContainer(), ip))
                    {
                        //Falló autenticación.
                        ucLogin.FailureText = strMensaje;
                        e.Authenticated = false;
                        DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
                        return;
                    }

                    //Validar Dominio
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario.Equals(0))
                    {
                        if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio.Equals("X"))
                        {
                            //Abrir Administrar Tipo de Seguridad.
                            Response.Redirect("frmTipoSeguridad.aspx?CambiarDominio=false");
                            e.Authenticated = false;
                            DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
                            return;
                        }
                    }
                    else
                    {
                        if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio == null || ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio == Constantes.SeguridadNODefinida)
                        {
                            ucLogin.FailureText = "No existe un tipo de seguridad establecido, imposible iniciar la aplicación.";
                            e.Authenticated = false;
                            DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
                            return;
                        }
                    }

                    //Redirecciona a la página que obliga a cambiar clave
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave") == "1")
                    {
                        try
                        {
                            if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave.SeDebePreguntar") == "1")
                            {
                                ///     1 No obligatorio
                                ManejoSesion.ModoCambioClave = 1;
                                e.Authenticated = true;
                                DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
                                return;
                            }
                        }
                        catch (Exception)
                        {
                        }
                        ///     3 Obligatorio debido a que se debe forzar el cambio de la contraseña
                        ManejoSesion.ModoCambioClave = 3;
                        //[GonzaloP]          [viernes, 22 de julio de 2016]       Work-Item: 7289 - Se agrega el parámetro "true" para terminar la ejecución de la página actual.
                        Response.Redirect("ChangedPassword\\frmCambiarContrasenia.aspx?url=../frmLogin.aspx", true);
                        e.Authenticated = false;
                        DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
                        return;
                    }

                    e.Authenticated = true;
                }
                else
                {
                    ucLogin.FailureText = "Se encuentra habilitado el logueo por SSO. No se permite el acceso directo a CIPOL.";
                    e.Authenticated = false;
                    DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
                    return;
                }
            }
            catch (Exception ex)
            {
                ucLogin.FailureText = ex.Message;
                e.Authenticated = false;
                DatosSesion.Control.Eliminar(Sesiones, SesionUsuario);
            }
        }

        /// <summary>
        /// Evento que se dispara al realizar el Login exitosamente.
        /// </summary>
        /// <history>
        /// [MartinV]          [martes, 05 de noviembre de 2013]       GCP-Cambios 14460
        /// [LeandroF]         [miércoles, 26 de agosto de 2015]       TFS-Work Item ID: 4750
        /// </history>
        protected void ucLogin_LoggedIn(object sender, EventArgs e)
        {
            /////////////////////////////////////////////////////////////////////////////////////////////
            //Al usar el control asp:Login y seguridad de formularios, esta linea
            //debe ir aca sino puede dar el siguiente error:
            /////////////////////////////////////////////////////////////////////////////////////////////
            //      The ThreadAbortException is thrown when you make a call to Response.Redirect(url) 
            //      because the system aborts processing of the current web page thread after it sends 
            //      the redirect to the response stream. Response.Redirect(url) actually makes 
            //      a call to Response.End() internally, and it's Response.End() that calls 
            //      Thread.Abort() which bubbles up the stack to end the thread.

            ManejoSesion.MensajeCerrar = "El Sistema se ha cerrado con éxito.";

            string urlPost = "";
            if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario == Constantes.IDUsuarioMaster)
            {
                List<Se_SistemasHabilitados> objListaSistemas = ManejoSesion.DatosCIPOLSesion.ObtenerListaSistemas();
                if (objListaSistemas.Count == 1 && objListaSistemas[0].IDSistema == 1)
                {
                    //TFS-Work Item ID: 4750
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
                try
                {
                    //todo: martinv -> ver si conviene pasar antes por cambiar contraseña o frmlogueado?
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave.SeDebePreguntar") == "1")
                    {
                        urlPost = "ChangedPassword\\frmCambiarContrasenia.aspx?url=../frmLogin.aspx&urlPost=../" + urlPost + "&preguntar=" + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("ForzarCambioClave.Mensaje");
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            Response.Redirect(urlPost, true);
        }

        /// <summary>
        /// Realiza las mismas validaciones básicas de Login, 
        /// que se hacen en el cliente, del lado del servidor.
        /// </summary>
        /// <param name="MsgError">Mensaje de error</param>
        /// <returns></returns>
        /// <history>
        /// [LucianoP]          [jueves, 15 de diciembre de 2011]       Creado
        /// </history>
        //TODO: HACER ESTA VALIDACION CON JAVASCRIPT Y ELIMIANR DE ACA YA QUE LA REALIZA POSTERIORMENTE
        private bool ValidarDatos(ref string MsgError)
        {
            if (string.IsNullOrEmpty(ucLogin.UserName))
            {
                MsgError = "Usuario o contraseña incorrectos.";
                return false;
            }
            if (string.IsNullOrEmpty(ucLogin.Password))
            {
                MsgError = "Usuario o contraseña incorrectos.";
                return false;
            }

            return true;
        }

        #endregion
    }
}
