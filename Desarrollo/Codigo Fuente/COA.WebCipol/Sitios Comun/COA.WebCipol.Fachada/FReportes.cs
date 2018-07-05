using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesComposicion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuario;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTerminalesParaReporte;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXArea;
using COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteUsuariosXRoles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteRolesXUsuarios;


namespace COA.WebCipol.Fachada
{
    public class FReportes : FPadreFachada
    {
        private wsCipolServices.wsSeguridad wsobj;
        private wsCipolServices.wsSeguridad obj
        {
            get
            {
                if (wsobj == null) wsobj = new wsCipolServices.wsSeguridad();
                return wsobj;
            }
        }


        #region "Reportes"

        public List<SIST_KAREAS> RecuperarAreas()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<SIST_KAREAS> lstRetorno = obj.RecuperarAreas().lstKAreas;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }


        public List<RolesXUsuarios> RecuperarReporteRolesXUsuario(Int32 pIdUsuario)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<RolesXUsuarios> lstRetorno = obj.RecuperarReporteRolesXUsuario(pIdUsuario).lstRolesXUsuarios;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        //public dcRecuperarReporteRolesXUsuarioDetalle RecuperarReporteRolesXUsuarioDetalle(dcRecuperarReporteRolesXUsuarioDetalleIN datos)
        //{
        //    return obj.RecuperarReporteRolesXUsuarioDetalle(datos);
        //}

        //public dcRecuperarReporteUsuariosXRolDetalle RecuperarReporteUsuariosXRolDetalle(dcRecuperarReporteUsuariosXRolDetalleIN datos)
        //{
        //    return obj.RecuperarReporteUsuariosXRolDetalle(datos);
        //}

        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol.RolesXUsuarios> RecuperarReporteUsuariosXRol(Int32 pidRol)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol.RolesXUsuarios> lstRetorno = obj.RecuperarReporteUsuariosXRol(pidRol).lstRolesXUsuarios;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public List<UsuariosXAreas> RecuperarReporteUsuariosXArea(Int32 IdArea)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<UsuariosXAreas> lstRetorno = obj.RecuperarReporteUsuariosXArea(IdArea).lstUsuariosXAreas;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public List<Roles_Composicion> RecuperarReporteRolesComposicion(Int32 pIdrol)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<Roles_Composicion> lstRetorno = obj.RecuperarReporteRolesComposicion(pIdrol).lstRoles_Composicion;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public dcRecuperarDatosParaReporteUsuariosXRoles RecuperarDatosParaReporteUsuariosXRoles()
        {
            return obj.RecuperarDatosParaReporteUsuariosXRoles();
        }

        public dcRecuperarDatosParaReporteRolesXUsuarios RecuperarDatosParaReporteRolesXUsuarios()
        {
            return obj.RecuperarDatosParaReporteRolesXUsuarios();
        }

        public List<SE_TERMINALES> RecuperarTerminalesParaReporte(int IDArea, string Habilitadas, bool TodasLasAreas)
        {
            var objCIpol = System.Web.HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.CIPOL];
            this.SeguridadCP.AplicarSeguridadCP();
            List<SE_TERMINALES> lstRetorno = obj.RecuperarTerminalesParaReporte(IDArea, Habilitadas, TodasLasAreas).lstSE_TERMINALES;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.dcRecuperarReporteUsuario RecuperarReporteUsuario(Int32 pIdUsuario)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.dcRecuperarReporteUsuario lstRetorno = obj.RecuperarReporteUsuario(pIdUsuario);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Horarios_Usuario> RecuperarReporteUsuarioHorarios(Int32 pIdUsuario)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Horarios_Usuario> lstRetorno = obj.RecuperarReporteUsuario(pIdUsuario).lstSE_Horarios_Usuario;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Term_Usuario> RecuperarReporteUsuarioTerminales(Int32 pIdUsuario)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Term_Usuario> lstRetorno = obj.RecuperarReporteUsuario(pIdUsuario).lstSE_Term_Usuario;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuarioSinAcceso.Sist_Usuarios> RecuperarReporteUsuarioSinAcceso()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuarioSinAcceso.Sist_Usuarios> lsrRetorno = obj.RecuperarReporteUsuarioSinAcceso().lstSist_Usuarios;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lsrRetorno;
        }

        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad.Sist_Usuarios> RecuperarReporteControlInactividad(DateTime FechaUltimoUsoCta, string LapsoInactividad)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad.Sist_Usuarios> lstRetorno = obj.RecuperarReporteControlInactividad(FechaUltimoUsoCta, LapsoInactividad).lstSist_Usuarios;
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        public COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria.dcRetornarLogAuditoria RetornarLogAuditoria(dcRetornarLogAuditoriaIN datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria.dcRetornarLogAuditoria lstRetorno = obj.RetornarLogAuditoria(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return lstRetorno;
        }

        #endregion


    }
}
