using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarSesionActiva;
using COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria;
using COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria;
using COA.WebCipol.Entidades.ClasesWs.dcRetornaStringSQL;
using COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas;
using COA.WebCipol.Entidades.ClasesWs.dcActualizarArea;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuarioDetalle;
using COA.WebCipol.Entidades.ClasesWs.dcInsertarSistBloqueados;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarTareaPrimitiva;
using COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva;
using COA.WebCipol.Entidades.ClasesWs.dcInsertarTareaPrimitiva;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo;
using COA.WebCipol.Entidades.ClasesWs;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarPoliticasGenerales;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarPoliticasGenerales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaMonitorActividades;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSIST_Habilitados;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperaTablasDeSistemas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperaTerminales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSupervisores;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuariosFiltro;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPorSistemaYGrupo;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasSinAutorizacion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasQueRequierenAutorizacion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaArbolDeGruposDeTareas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuario;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXArea;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesComposicion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteUsuariosXRoles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteRolesXUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTerminalesParaReporte;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuarioSinAcceso;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaEntornoCIPOLAdministrador;
using wsCipolServices.Fachada.Seguridad;

namespace COA.WebCipol.Fachada
{
    public class FSeguridad : FPadreFachada
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

        #region "Seguridad"
        public string Ping(int Codigo)
        {
            return obj.Ping(Codigo);
        }

        public dcRecuperarPoliticasGenerales RecuperarPoliticasGenerales()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarPoliticasGenerales objRetorno = obj.RecuperarPoliticasGenerales();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarDatosParaEntornoCIPOLAdministrador RecuperarDatosParaEntornoCIPOLAdministrador()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaEntornoCIPOLAdministrador objRetorno = obj.RecuperarDatosParaEntornoCIPOLAdministrador();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public bool AdministrarPoliticasGenerales(dcAdministrarPoliticasGenerales datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.AdministrarPoliticasGenerales(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }

        public dcRecuperarDatosParaVisorSucesosSeguridad RecuperarDatosParaVisorSucesosSeguridad()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaVisorSucesosSeguridad objRetorno = obj.RecuperarDatosParaVisorSucesosSeguridad();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarDatosParaMonitorActividades RecuperarDatosParaMonitorActividades()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaMonitorActividades objRetorno = obj.RecuperarDatosParaMonitorActividades();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public Int32 EliminarSesionActiva(dcEliminarSesionActiva datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarSesionActiva(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRetornarLogAuditoria RetornarLogAuditoria(dcRetornarLogAuditoriaIN datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRetornarLogAuditoria objRetorno = obj.RetornarLogAuditoria(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public bool ActualizarTipoSeguridad(string TipoSeguridad)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetono = obj.ActualizarTipoSeguridad(TipoSeguridad);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetono;
        }

        public dcRecuperarSistemasHabilitados RecuperarSistemasHabilitados()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarSistemasHabilitados objRetorno = obj.RecuperarSistemasHabilitados();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public int InsertarSistemaHabilitado(dcInsertarSistemaHabilitado dtsDatosSistHab)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.InsertarSistemaHabilitado(dtsDatosSistHab);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;

        }

        public int ActualizarSistemaHabilitado(dcActualizarSistemaHabilitado dtsDatosSistHab)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.ActualizarSistemaHabilitado(dtsDatosSistHab);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public int EliminarSistemaHabilitado(dcEliminarSistemaHabilitado dtsDatosSistHab)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarSistemaHabilitado(dtsDatosSistHab);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRecuperarSIST_Habilitados RecuperarSIST_Habilitados()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarSIST_Habilitados objRetorno = obj.RecuperarSIST_Habilitados();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarUsuarios RecuperarUsuarios()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarUsuarios objRetorno = obj.RecuperarUsuarios();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperaTablasDeSistemas RecuperaTablasDeSistemas()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperaTablasDeSistemas objRetorno = obj.RecuperaTablasDeSistemas();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperaTerminales RecuperaTerminales()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperaTerminales objRetorno = obj.RecuperaTerminales();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public DateTime RecuperarFechaMinima()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            DateTime dtRetorno = obj.RecuperarFechaMinima();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return dtRetorno;
        }

        public bool ProbarStringSQL(string NewStringSQL)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.ProbarStringSQL(NewStringSQL);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;

        }

        public dcRecuperarEventosAuditoria RecuperarEventosAuditoria(dcRecuperarEventosAuditoriaIN dtsFiltros)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarEventosAuditoria objRetorno = obj.RecuperarEventosAuditoria(dtsFiltros);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public string RetornaStringSQL(dcRetornaStringSQL dtsFiltros)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            string strRetorno = obj.RetornaStringSQL(dtsFiltros);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return strRetorno;

        }

        public dcRecuperarSupervisores RecuperarSupervisores()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarSupervisores objRetorno = obj.RecuperarSupervisores();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public string AuditarIntentoInicioSesionConSesionActiva(string Login)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            string objRetorno = obj.AuditarIntentoInicioSesionConSesionActiva(Login);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        #endregion

        #region "Areas"
        public dcRecuperarAreas RecuperarAreas()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarAreas objRetorno = obj.RecuperarAreas();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;

        }
        public Int32 AltaDeAreas(dcAltaDeAreas dtsDatos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.AltaDeAreas(dtsDatos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }
        public Int32 ActualizarArea(dcActualizarArea dtsDatos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.ActualizarArea(dtsDatos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }
        public Int32 EliminarArea(Int32 idArea)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarArea(idArea);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }
        public Int32 AgregarArea(Int32 idArea)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.AgregarArea(idArea);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }
        #endregion

        #region "Terminales"
        public dcRecuperarDatosParaABMTerminales RecuperarDatosParaABMTerminales()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaABMTerminales objRetorno = obj.RecuperarDatosParaABMTerminales();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public short AdministrarTerminales(dcAdministrarTerminales dtsTerminal)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            short shtRetorno = obj.AdministrarTerminales(dtsTerminal);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return shtRetorno;
        }

        public bool EliminarTerminal(short IDTerminal)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.EliminarTerminal(IDTerminal);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }
        #endregion

        #region "Usuario Sistema"
        public dcAdministrarAbmUsuarios AdministrarAbmUsuarios(dcAdministrarAbmUsuariosIN datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcAdministrarAbmUsuarios objRetorno = obj.AdministrarAbmUsuarios(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public Int32 EliminarUsuario(Int32 pidUsuario, string strMensajeAuditoria)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarUsuario(pidUsuario, strMensajeAuditoria);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public Int32 ActivarUsuario(Int32 pidUsuario, string strMensajeAuditoria)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.ActivarUsuario(pidUsuario, strMensajeAuditoria);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRecuperarDatosParaABMUsuarios RecuperarDatosParaABMUsuarios(Int32 pIdUsuario)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaABMUsuarios objRetorno = obj.RecuperarDatosParaABMUsuarios(pIdUsuario);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        
        public dcRecuperarDatosParaGrillaABMUsuarios RecuperarDatosParaGrillaABMUsuarios(COA.WebCipol.Entidades.ClasesWs.Filtros.dcFiltrosUsuarios fil)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
            dcRecuperarDatosParaGrillaABMUsuarios objRetorno = obj.RecuperarDatosParaGrillaABMUsuarios(fil);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarDatosParaGrillaABMUsuariosFiltro RecuperarDatosParaGrillaABMUsuariosFiltro(string Filtro)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaGrillaABMUsuariosFiltro objRetorno = obj.RecuperarDatosParaGrillaABMUsuariosFiltro(Filtro);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        #endregion

        #region "Tarea"

        #region "Grupos de Tareas"

        public Int32 VerificaTareasAutorizantesEnUso(Int32 pidTarea)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.VerificaTareasAutorizantesEnUso(pidTarea);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public Int32 EliminarTareasAutorizantes(Int32 pidTarea)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarTareasAutorizantes(pidTarea);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRecuperarDatosParaABMGrupos RecuperarDatosParaABMGrupos()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaABMGrupos objRetorno = obj.RecuperarDatosParaABMGrupos();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarDatosDelGrupo RecuperarDatosDelGrupo(short IDGrupo)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosDelGrupo objRetorno = obj.RecuperarDatosDelGrupo(IDGrupo);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;

        }

        public dcRecuperarTareasPorSistemaYGrupo RecuperarTareasPorSistemaYGrupo(short IDSistema, short IDGrupo)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarTareasPorSistemaYGrupo objRetorno = obj.RecuperarTareasPorSistemaYGrupo(IDSistema, IDGrupo);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public short AdministrarGrupo(dcAdministrarGrupo dtsDatos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            short shtRetorno = obj.AdministrarGrupo(dtsDatos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return shtRetorno;
        }

        public bool EliminarGrupo(short IDGrupo)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            bool blnRetorno = obj.EliminarGrupo(IDGrupo);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return blnRetorno;
        }

        #endregion

        #region "Tareas Autorizantes"

        public dcRecuperarDatosParaABMTareasAutorizantes RecuperarDatosParaABMTareasAutorizantes()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaABMTareasAutorizantes objRetorno = obj.RecuperarDatosParaABMTareasAutorizantes();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarTareasSinAutorizacion RecuperarTareasSinAutorizacion(short IDSistema)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarTareasSinAutorizacion objRetorno = obj.RecuperarTareasSinAutorizacion(IDSistema);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarTareasQueRequierenAutorizacion RecuperarTareasQueRequierenAutorizacion(int IDTareaAutorizante)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarTareasQueRequierenAutorizacion objRetorno = obj.RecuperarTareasQueRequierenAutorizacion(IDTareaAutorizante);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public int AdministrarTareasSupervisantes(dcAdministrarTareasSupervisantes dtsDatos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.AdministrarTareasSupervisantes(dtsDatos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;

        }
        #endregion

        #region "Roles"

        public Int32 EliminarRol(Int32 pidRol)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarRol(pidRol);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRecuperarDatosParaArbolDeGruposDeTareas RecuperarDatosParaArbolDeGruposDeTareas()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaArbolDeGruposDeTareas objRetorno = obj.RecuperarDatosParaArbolDeGruposDeTareas();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public int VerificarRolAsignadoAUsuarios(Int32 pidRol)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.VerificarRolAsignadoAUsuarios(pidRol);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRecuperarDatosParaABMRoles RecuperarDatosParaABMRoles()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarDatosParaABMRoles objRetorno = obj.RecuperarDatosParaABMRoles();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcRecuperarComposicionRol RecuperarComposicionRol(Int32 pIdRol, bool SoloIdTareas)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarComposicionRol objRetorno = obj.RecuperarComposicionRol(pIdRol, SoloIdTareas);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public dcAdministrarRoles AdministrarRoles(dcAdministrarRolesIN datos)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcAdministrarRoles objRetorno = obj.AdministrarRoles(datos);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }
        #endregion

        #region "Tareas"

        public dcRecuperarTareasPrimitivas RecuperarTareasPrimitivas()
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarTareasPrimitivas objRetorno =obj.RecuperarTareasPrimitivas();
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public int InsertarTareaPrimitiva(dcInsertarTareaPrimitiva dtsTareas)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.InsertarTareaPrimitiva(dtsTareas);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public int ActualizarTareaPrimitiva(dcActualizarTareaPrimitiva dtsDatosTareas)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.ActualizarTareaPrimitiva(dtsDatosTareas);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public int EliminarTareaPrimitiva(dcEliminarTareaPrimitiva dtsTareas)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.EliminarTareaPrimitiva(dtsTareas);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public int VerificarExistenciaIDTarea(int IDTarea)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.VerificarExistenciaIDTarea(IDTarea);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;
        }

        public dcRecuperarSistBloqueados RecuperarSistBloqueados(short IDSistema, short IDUsuario)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            dcRecuperarSistBloqueados objRetorno = obj.RecuperarSistBloqueados(IDSistema, IDUsuario);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return objRetorno;
        }

        public int InsertarSistBloqueados(dcInsertarSistBloqueados dtsDatosSistBloq)
        {
            this.SeguridadCP.AplicarSeguridadCP();
            int intRetorno = obj.InsertarSistBloqueados(dtsDatosSistBloq);
            this.SeguridadCP.UndoAplicarSeguridadCP();
            return intRetorno;

        }
        #endregion

        #endregion
              
       
    }
}
