using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using wsCipolServices.ConectorWSCipolNET;
using COA.WebCipol.Entidades;
using wsCipolServices.ReglasDeNegocio;

namespace wsCipolServices
{
    /// <summary>
    /// Descripción breve de wsInicioSesion
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsInicioSesion : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string IniciarSesion(string pUsuario, string pTerminal, ref string pError, string pPassword, ref bool pTerminal_ActualizacionLAN, System.Net.CookieContainer cookie)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.IniciarSesion(pUsuario, pTerminal, ref pError, pPassword, ref pTerminal_ActualizacionLAN, cookie);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public byte[] GetClavePublica(byte[] ClavePublicaCliente, System.Net.CookieContainer cookie)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.GetClavePublica(ClavePublicaCliente, cookie);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public void CerrarSesion()
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                cnInicioSesion.CerrarSesion();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool ValidarContrasenia(string Usuario, string Clave, ref string MensajeError)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.ValidarContraseña(Usuario, Clave, ref MensajeError);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool Auditar(string MensajeAuditoria)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.Auditar(MensajeAuditoria);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool CambiarContrasenia(Int32 CantidadContraseniasAlmacenadas, Int32 pIdUsuario, string Usuario, string MensajeAuditoria, string NuevaContrasenia, Int32 DuracionContrasenia, byte mbytObligatorio, string ContraseniaActual, ref string MensajeError, int TiempoEnDiasNoPermitirCambiarContrasenia)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.CambiarContrasenia(CantidadContraseniasAlmacenadas, pIdUsuario, Usuario, MensajeAuditoria, NuevaContrasenia, DuracionContrasenia, mbytObligatorio, ContraseniaActual, ref MensajeError, TiempoEnDiasNoPermitirCambiarContrasenia);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }

        }

        [WebMethod(EnableSession = true)]
        public bool SupervisarUsuario(string Usuario, string Clave, int IDTarea, ref string MensajeError)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.SupervisarUsuario(Usuario, Clave, IDTarea, ref MensajeError);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public string RecuperarNombreDominio(System.Net.CookieContainer cookie)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.RecuperarNombreDominio(cookie);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public string UsuariosXSistema(Int32 IDSistema)
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                return cnInicioSesion.UsuariosXSistema(IDSistema);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public string Recuperar_UsuariosXSistema(Int32 IDSistema, ref List<SE_USUARIOS> lstRetorno)
        {
            try
            {
                RNwsInicioSesion rnReglaSNegocio = new RNwsInicioSesion();
                return rnReglaSNegocio.Recuperar_UsuariosXSistema(IDSistema, lstRetorno);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public void RegistrarExpiroSesion()
        {
            try
            {
                ConectorwsIniciosesion cnInicioSesion = new ConectorwsIniciosesion();
                cnInicioSesion.RegistrarExpiroSesion();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
    }
}
