using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Entidades;

namespace COA.WebCipol.Fachada
{
    public class FInicioSesion : FPadreFachada
    {

        private wsCipolServices.wsInicioSesion mobjInicioSesion;
        private wsCipolServices.wsInicioSesion obj
        {
            get
            {
                if (mobjInicioSesion == null) mobjInicioSesion = new wsCipolServices.wsInicioSesion();

                return mobjInicioSesion;
            }
        }


        public string IniciarSesion(string pUsuario, string pTerminal, ref string pError, string pPassword, System.Net.CookieContainer cookie)//, ref bool pTerminal_ActualizacionLAN)
        {
            //this.SeguridadCP.AplicarSeguridadCP();
            bool pTerminal_ActualizacionLAN = false;
            string strRetorno = obj.IniciarSesion(pUsuario, pTerminal, ref pError, pPassword, ref pTerminal_ActualizacionLAN, cookie);
            //this.SeguridadCP.UndoAplicarSeguridadCP();
            return strRetorno;
        }

        public byte[] GetClavePublica(byte[] ClavePublicaCliente, System.Net.CookieContainer cookie)
        {
            //this.SeguridadCP.AplicarSeguridadCP();
            byte[] bytRetorno = obj.GetClavePublica(ClavePublicaCliente, cookie);
            //this.SeguridadCP.UndoAplicarSeguridadCP();
            return bytRetorno;
        }

        public void CerrarSesion()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            obj.CerrarSesion();
            this.SeguridadCP.UndoAplicarSeguridadCP();
        }

        public bool ValidarContraseña(string Usuario, string Clave, ref string MensajeError)
        {
            //this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.ValidarContrasenia(Usuario, Clave, ref MensajeError);
            //this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }

        public bool Auditar(string MensajeAuditoria)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.Auditar(MensajeAuditoria);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }

        public bool CambiarContrasenia(Int32 CantidadContraseniasAlmacenadas, Int32 pIdUsuario, string Usuario, string MensajeAuditoria, string NuevaContrasenia, Int32 DuracionContrasenia, byte mbytObligatorio, string ContraseñaActual, ref string MensajeError, int TiempoEnDiasNoPermitirCambiarContrasenia)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.CambiarContrasenia(CantidadContraseniasAlmacenadas, pIdUsuario, Usuario, MensajeAuditoria, NuevaContrasenia, DuracionContrasenia, mbytObligatorio, ContraseñaActual, ref MensajeError, TiempoEnDiasNoPermitirCambiarContrasenia);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }

        public bool SupervisarUsuario(string Usuario, string Clave, int IDTarea, ref string MensajeError)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.SupervisarUsuario(Usuario, Clave, IDTarea, ref MensajeError);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }

        public string RecuperarNombreDominio(System.Net.CookieContainer cookie)
        {
            //this.SeguridadCP.AplicarSeguridadCP();
            string strRetorno = obj.RecuperarNombreDominio(cookie);
            //this.SeguridadCP.UndoAplicarSeguridadCP();
            return strRetorno;
        }

        public string UsuariosXSistema(Int32 IDSistema)
        {
            //this.SeguridadCP.AplicarSeguridadCP();
            string strRetorno = obj.UsuariosXSistema(IDSistema);
            //this.SeguridadCP.UndoAplicarSeguridadCP();
            return strRetorno;
        }

        public string Recuperar_UsuariosXSistema(Int32 IDSistema, ref  List<COA.WebCipol.Entidades.SE_USUARIOS> dtsRetorno)
        {
            //this.SeguridadCP.AplicarSeguridadCP();
            string strRetorno = obj.Recuperar_UsuariosXSistema(IDSistema, ref dtsRetorno);
            //this.SeguridadCP.UndoAplicarSeguridadCP();
            return strRetorno;
        }

        public void RegistrarExpiroSesion()
        {
            obj.RegistrarExpiroSesion();
        }
    }
}
