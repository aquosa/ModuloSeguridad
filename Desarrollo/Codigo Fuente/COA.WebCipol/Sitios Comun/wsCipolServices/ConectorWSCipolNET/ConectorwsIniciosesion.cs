using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wsCipolServices.ConectorWSCipolNET
{
    public class ConectorwsIniciosesion
    {
        private static Fachada.Seguridad.IniciarSesion.wsInicioSesion wsobj
        {
            get
            {
                return (Fachada.Seguridad.IniciarSesion.wsInicioSesion)HttpContext.Current.Session["wsInicioSesion"];
            }
            set
            {
                HttpContext.Current.Session["wsInicioSesion"] = value;
            }
        }
        private Fachada.Seguridad.IniciarSesion.wsInicioSesion obj
        {
            get
            {
                //if (wsobj == null)
                //{
                wsobj = new Fachada.Seguridad.IniciarSesion.wsInicioSesion();
                wsobj.CookieContainer = ((COA.WebCipol.Comun.DatosCIPOL)System.Web.HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.CIPOL]).DatosPadreCIPOLCliente.objColeccionDeCookies;
                wsobj.Timeout = System.Threading.Timeout.Infinite;
                //}
                return wsobj;
            }
        }

        private Fachada.Seguridad.IniciarSesion.wsInicioSesion objSinCookieContainer
        {
            get
            {
                wsobj = new Fachada.Seguridad.IniciarSesion.wsInicioSesion();
                wsobj.Timeout = System.Threading.Timeout.Infinite;
                return wsobj;
            }
        }

        public string IniciarSesion(string pUsuario, string pTerminal, ref string pError, string pPassword, ref bool pTerminal_ActualizacionLAN, System.Net.CookieContainer cookie)
        {
            Fachada.Seguridad.IniciarSesion.wsInicioSesion objAux = new Fachada.Seguridad.IniciarSesion.wsInicioSesion();
            objAux.CookieContainer = cookie;
            objAux.Timeout = System.Threading.Timeout.Infinite;
            return objAux.IniciarSesion(pUsuario, pTerminal, ref pError, pPassword, ref pTerminal_ActualizacionLAN);
        }

        public byte[] GetClavePublica(byte[] ClavePublicaCliente, System.Net.CookieContainer cookie)
        {
            Fachada.Seguridad.IniciarSesion.wsInicioSesion objAux = new Fachada.Seguridad.IniciarSesion.wsInicioSesion();
            objAux.CookieContainer = cookie;
            objAux.Timeout = System.Threading.Timeout.Infinite;
            return objAux.GetClavePublica(ClavePublicaCliente);
        }

        public void CerrarSesion()
        {
            obj.CerrarSesion();
        }

        public bool ValidarContraseña(string Usuario, string Clave, ref string MensajeError)
        {
            return obj.ValidarContraseña(Usuario, Clave, ref MensajeError);
        }

        public bool Auditar(string MensajeAuditoria)
        {
            return obj.Auditar(MensajeAuditoria);
        }

        public bool CambiarContrasenia(Int32 CantidadContraseniasAlmacenadas, Int32 pIdUsuario, string Usuario, string MensajeAuditoria, string NuevaContrasenia, Int32 DuracionContrasenia, byte mbytObligatorio, string ContraseñaActual, ref string MensajeError, int TiempoEnDiasNoPermitirCambiarContrasenia)
        {
            return obj.CambiarContrasenia(CantidadContraseniasAlmacenadas, pIdUsuario, Usuario, MensajeAuditoria, NuevaContrasenia, DuracionContrasenia, mbytObligatorio, ContraseñaActual, ref MensajeError, TiempoEnDiasNoPermitirCambiarContrasenia);
        }

        public bool SupervisarUsuario(string Usuario, string Clave, int IDTarea, ref string MensajeError)
        {
            return obj.SupervisarUsuario(Usuario, Clave, IDTarea, ref MensajeError);
        }

        public string RecuperarNombreDominio(System.Net.CookieContainer cookie)
        {
            Fachada.Seguridad.IniciarSesion.wsInicioSesion objAux = new Fachada.Seguridad.IniciarSesion.wsInicioSesion();
            objAux.CookieContainer = cookie;
            objAux.Timeout = System.Threading.Timeout.Infinite;
            return objAux.RecuperarNombreDominio();
        }

        public string UsuariosXSistema(Int32 IDSistema)
        {
            return obj.UsuariosXSistema(IDSistema);
        }

        public string Recuperar_UsuariosXSistema(Int32 IDSistema, ref  Fachada.Seguridad.IniciarSesion.dtsUsuarios dtsRetorno)
        {
            return obj.Recuperar_UsuariosXSistema(IDSistema, ref dtsRetorno);
        }

        public void RegistrarExpiroSesion()
        {
            objSinCookieContainer.RegistrarExpiroSesion();
        }
    }
}