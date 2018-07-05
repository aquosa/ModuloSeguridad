using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Inicio._UIHelpers;
using COA.WebCipol.Fachada;
using COA.WebCipol.Inicio.Controlers;
using COA.WebCipol.Inicio.model;
using COA.WebCipol.Comun;
using Microsoft.VisualBasic;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion.ChangedPassword
{
    public partial class frmCambiarContrasenia : PaginaPadre
    {
        /// <history>
        /// [MartinV]          [martes, 05 de noviembre de 2013]       Modificado  GCP-Cambios 14460
        /// [MartinV]          [miércoles, 24 de septiembre de 2014]       Modificado  GCP-Cambios 15584
        /// </history>
        protected override void Evento_load(object sender, EventArgs e)
        {
            cFormLogin objUILogin = new cFormLogin();
            if (!this.IsPostBack)
            {
                //this.lblNombreDominio.Text = objUILogin.RecuperarTipoSeguridadYNombreDeDominio(ManejoSesion.CookieMaster);
                this.lblUsuario.Text = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login;
                //Controla si debe preguntar por el cambio de contraseña.
                if (Request.QueryString["preguntar"] != null)
                    ScriptManager.RegisterStartupScript(this, GetType(), "", "page.PreguntarCambioContrasenia('" + Request.QueryString["preguntar"].ToString() + "','" + Request.QueryString["urlPost"].ToString() + "');", true);
            }
        }

        #region "Cambiar Contraseña"
        private string AdministrarContrasenia()
        {
            try
            {

                string strMensaje = string.Empty;
                string strClave;
                System.Security.Cryptography.RSACryptoServiceProvider objRSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                EntidadesEmpresariales.PadreCipolCliente objCipol = (EntidadesEmpresariales.PadreCipolCliente)ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente;
                FSeguridad objFachada = new FSeguridad();
                FInicioSesion objInicioSesion = new FInicioSesion();

                objRSA.ImportCspBlob(objCipol.gobjRSAServ);
                strClave = CurrentPassword.Text.Trim();
                strClave = System.Convert.ToBase64String(objRSA.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(strClave), false));

                if (CurrentPassword.Text.Trim().Equals(NewPassword.Text.Trim()))
                    return COA.Cipol.Presentacion.PaginaPadre.RetornaMensajeUsuario(50);

                if (NewPassword.Text.Trim().Length < ManejoSesion.gudParam.LongitudContraseña)
                    if (objInicioSesion.Auditar(COA.WebCipol.Inicio.Utiles.cPrincipal.MensajeAuditoria(500, objCipol.Login, "", "")))
                        return "La contraseña especificada debe tener una longitud mínima de " + ManejoSesion.gudParam.LongitudContraseña + " caracteres.";
                    else
                        return "No se ha podido realizar el proceso solicitado.";

                if (!NewPassword.Text.Trim().Equals(ConfirmNewPassword.Text.Trim()))
                    if (objInicioSesion.Auditar(COA.WebCipol.Inicio.Utiles.cPrincipal.MensajeAuditoria(190, objCipol.Login, "", "")))
                        return COA.Cipol.Presentacion.PaginaPadre.RetornaMensajeUsuario(55);
                    else
                        return "No se ha podido realizar el proceso solicitado.";

                string strNuevaContraseña;
                strNuevaContraseña = NewPassword.Text.Trim();
                string strTemp = ValidarSeguridadContrasenia(ref strNuevaContraseña);

                if (!string.IsNullOrEmpty(strTemp.Trim()))
                    if (objInicioSesion.Auditar(COA.WebCipol.Inicio.Utiles.cPrincipal.MensajeAuditoria(205, objCipol.Login, "", "")))
                        return strTemp;
                    else
                        return "No se ha podido realizar el proceso solicitado.";

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.dcRecuperarDatosParaABMUsuarios objResp = objFachada.RecuperarDatosParaABMUsuarios(objCipol.IDUsuario);


                strNuevaContraseña = System.Convert.ToBase64String(objRSA.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(strNuevaContraseña), false));
                if (!objInicioSesion.CambiarContrasenia(ManejoSesion.gudParam.CantidadContraseñasAlmacenadas,
                                                    objCipol.IDUsuario,
                                                    objCipol.Login,
                                                    COA.WebCipol.Inicio.Utiles.cPrincipal.MensajeAuditoria(210, objCipol.Login, "", ""),
                                                    strNuevaContraseña,
                                                    ManejoSesion.gudParam.DuracionContraseña,
                                                    ManejoSesion.ModoCambioClave,
                                                    strClave,
                                                    ref strMensaje,
                                                    ManejoSesion.gudParam.TiempoEnDiasNoPermitirCambiarContrasenia))
                {
                    if (strMensaje.Trim().ToUpper().Equals("OCURRIO ERROR"))
                        return "No se ha podido realizar el proceso solicitado.";
                    else
                        if (string.IsNullOrEmpty(strMensaje))
                            return COA.Cipol.Presentacion.PaginaPadre.RetornaMensajeUsuario(50);
                        else
                            return strMensaje;
                }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private string ValidarSeguridadContrasenia(ref string pstrContrasenia)
        {
            string strMensajeRet = null;
            bool blnTieneLetras = false;
            bool blnTieneNumeros = false;
            bool blnTieneCaracteresEspeciales = false;
            Int32 intTemp = default(Int32);
            Int32 intI = default(Int32);

            //seteamos los flags
            blnTieneLetras = false;
            blnTieneNumeros = false;
            blnTieneCaracteresEspeciales = false;
            intTemp = 0;

            //Verificamos si la seguridad está integrada al dominio, en caso
            //que lo esté (o sea, gstrDominio <> "") salimos pues derivamos la
            //responsabilidad de la seguridad a Windows.
            if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio) //ver el usuario, si tiene datos..
            {
                strMensajeRet = "";
                return strMensajeRet;
            }

            //Si la seguridad no está integrada al dominio,
            //verificamos la contraseña de acuerdo al nivel de
            //seguridad indicado.
            //Recorremos la contraseña verificando si tiene caracteres alfabéticos, numéricos y especiales.
            for (intI = 1; intI <= pstrContrasenia.Length; intI++)
            {
                intTemp = Strings.InStr(1, "1234567890ABCDEFGHIJKLMNÑOPQRSTUVWXYZ", Strings.Mid(pstrContrasenia.ToUpper(), intI, 1)); //TODO[CIPOLWEB]

                //si hay algún caracter que no sea letra ni número, seteamos el flag
                //de caracteres especiales
                if (intTemp == 0)
                    blnTieneCaracteresEspeciales = true;
                //verificamos que inttemp sea < 11 para ver si hubo numeros
                if (intTemp < 11 & intTemp > 0)
                    blnTieneNumeros = true;
                //verficamos que haya habido caracteres solo alfabeticos
                if (intTemp >= 11)
                    blnTieneLetras = true;
            }

            //Ahora, de acuerdo al nivel de seguridad, verificamos
            //la contraseña.
            switch (ManejoSesion.gudParam.NivelSeguridadContraseña) //ver si tiene datos gudParam!
            {
                case Constantes.genuNivelSeguridad.Sin_requerimiento_específico:
                    //sin seguridad, la contraseña siempre es válida
                    strMensajeRet = "";
                    return strMensajeRet;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_numeros:
                    //sólo por números, verificamos que la contraseña
                    //solo tenga números, no tenga caracteres especiales y no tenga letras
                    if (blnTieneNumeros & (!blnTieneCaracteresEspeciales) & (!blnTieneLetras))
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        strMensajeRet = (Information.IsNumeric(pstrContrasenia) ? "" : "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por números.").ToString();
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_letras:
                    //contraseña compuesta solo por letras, verificamos
                    //que así sea
                    if (blnTieneLetras & (!blnTieneCaracteresEspeciales) & (!blnTieneNumeros))
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        //si se encontró un caracter que no es alfabético, rechazamos la contraseña
                        strMensajeRet = "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por letras.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_y_numeros:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & (!blnTieneCaracteresEspeciales))
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        strMensajeRet = "El nivel de seguridad parametrizado requiere que la contraseña posea solo letras y números.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & blnTieneCaracteresEspeciales)
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        strMensajeRet = "El nivel de seguridad parametrizado requiere que la contraseña posea letras, números y caracteres especiales.";
                    }
                    break;
                default:
                    throw new Exception("Nivel de seguridad no soportado.");
            }

            return strMensajeRet;
        }

        protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
        {
            string strMensaje = "";
            cFormLogin objFormLogin = new cFormLogin();
            //ivansa
            if (!objFormLogin.AutenticarUsuario(ref strMensaje, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, CurrentPassword.Text.Trim(), ManejoSesion.CookieMaster,""))
            {
                //Falló autenticación.
                FailureText.Text = strMensaje;
            }
            else
            {
                strMensaje = AdministrarContrasenia();
                if (!string.IsNullOrEmpty(strMensaje))
                    FailureText.Text = strMensaje;
                else
                    ChangedPassword();
            }
        }

        protected void ChangedPassword()
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
       
            List<Se_SistemasHabilitados> objListaSistemas = ManejoSesion.DatosCIPOLSesion.ObtenerListaSistemas();
            if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario == Constantes.IDUsuarioMaster)
            {
                Response.Redirect("../frmLogueado.aspx?urlPost=" + objListaSistemas[0].PaginaPorDefecto);
            }
            else
            {
                if (objListaSistemas.Count == 1 && objListaSistemas[0].IDSistema == 1)
                    Response.Redirect("../frmLogueado.aspx?urlPost=" + objListaSistemas[0].PaginaPorDefecto);
                else
                    Response.Redirect("../frmSistemasPermitidos.aspx?Mensaje=1");
            }
        }

        protected void CancelPushButton_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["url"] != null)
                Response.Redirect(Request.QueryString["url"].ToString());
        }
        #endregion
    }
}