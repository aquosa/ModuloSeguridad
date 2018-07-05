using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsCipolServices.Fachada.Seguridad;

namespace wsCipolServices.ConectorWSCipolNET
{
    public partial class ConectorwsSeguridad
    {
        private static Fachada.Seguridad.wsSeguridad wsobj
        {
            get
            {
                return (Fachada.Seguridad.wsSeguridad)HttpContext.Current.Session["wsSeguridad"];
            }
            set
            {
                HttpContext.Current.Session["wsSeguridad"] = value;
            }
        }
        private Fachada.Seguridad.wsSeguridad obj
        {
            get
            {
                //if (wsobj == null)
                // {
                wsobj = new Fachada.Seguridad.wsSeguridad();
                wsobj.CookieContainer = ((COA.WebCipol.Comun.DatosCIPOL)System.Web.HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.CIPOL]).DatosPadreCIPOLCliente.objColeccionDeCookies;
                wsobj.Timeout = System.Threading.Timeout.Infinite;
                //}
                return wsobj;
            }
        }

        #region "Seguridad"
        public string Ping(int Codigo)
        {
            return obj.Ping(Codigo);
        }

        public System.Data.DataSet RecuperarPoliticasGenerales()
        {
            return obj.RecuperarPoliticasGenerales();
        }

        public System.Data.DataSet RecuperarDatosParaEntornoCIPOLAdministrador()
        {
            return obj.RecuperarDatosParaEntornoCIPOLAdministrador();
        }

        public bool AdministrarPoliticasGenerales(System.Data.DataSet dtsDatos)
        {
            return obj.AdministrarPoliticasGenerales(dtsDatos);
        }

        public Fachada.Seguridad.dtsSucesosSeguridad RecuperarDatosParaVisorSucesosSeguridad()
        {
            return obj.RecuperarDatosParaVisorSucesosSeguridad();
        }

        public System.Data.DataSet RecuperarDatosParaMonitorActividades()
        {
            return obj.RecuperarDatosParaMonitorActividades();
        }

        public Int32 EliminarSesionActiva(System.Data.DataSet dtsCriterio)
        {
            return obj.EliminarSesionActiva(dtsCriterio);
        }

        public Fachada.Seguridad.dtsSucesosSeguridad RetornarLogAuditoria(DateTime fechadesde, DateTime fechahasta, string UsuarioActuante, string usuarioafectado, string CodigoEvento)
        {
            return obj.RetornarLogAuditoria(fechadesde, fechahasta, UsuarioActuante, usuarioafectado, CodigoEvento);
        }

        public bool ActualizarTipoSeguridad(string TipoSeguridad)
        {
            return obj.ActualizarTipoSeguridad(TipoSeguridad);
        }

        public Fachada.Seguridad.dtsSistHabilitados RecuperarSistemasHabilitados()
        {
            return obj.RecuperarSistemasHabilitados();
        }

        public int InsertarSistemaHabilitado(Fachada.Seguridad.dtsSistHabilitados dtsDatosSistHab)
        {
            return obj.InsertarSistemaHabilitado(dtsDatosSistHab);
        }

        public int ActualizarSistemaHabilitado(Fachada.Seguridad.dtsSistHabilitados dtsDatosSistHab)
        {
            return obj.ActualizarSistemaHabilitado(dtsDatosSistHab);
        }

        public int EliminarSistemaHabilitado(Fachada.Seguridad.dtsSistHabilitados dtsDatosSistHab)
        {
            return obj.EliminarSistemaHabilitado(dtsDatosSistHab);
        }

        public Fachada.Seguridad.dtsAuditoriaEventos RecuperarSIST_Habilitados()
        {
            return obj.RecuperarSIST_Habilitados();
        }

        public Fachada.Seguridad.dtsAuditoriaEventos RecuperarUsuarios()
        {
            return obj.RecuperarUsuarios();
        }

        public Fachada.Seguridad.dtsAuditoriaEventos RecuperaTablasDeSistemas()
        {
            return obj.RecuperaTablasDeSistemas();
        }

        public Fachada.Seguridad.dtsAuditoriaEventos RecuperaTerminales()
        {
            return obj.RecuperaTerminales();
        }

        public DateTime RecuperarFechaMinima()
        {
            return obj.RecuperarFechaMinima();
        }

        public bool ProbarStringSQL(string NewStringSQL)
        {
            return obj.ProbarStringSQL(NewStringSQL);
        }

        public Fachada.Seguridad.dtsAuditoriaEventos RecuperarEventosAuditoria(Fachada.Seguridad.dtsAuditoriaEventos dtsFiltros)
        {
            return obj.RecuperarEventosAuditoria(dtsFiltros);
        }

        public string RetornaStringSQL(Fachada.Seguridad.dtsAuditoriaEventos dtsFiltros)
        {
            return obj.RetornaStringSQL(dtsFiltros);
        }

        public Fachada.Seguridad.dtsAuditoriaEventos RecuperarSupervisores()
        {
            return obj.RecuperarSupervisores();
        }

        public string AuditarIntentoInicioSesionConSesionActiva(string Login)
        {
            Fachada.Seguridad.wsSeguridad objWS = new Fachada.Seguridad.wsSeguridad();
            objWS.Timeout = System.Threading.Timeout.Infinite;

            return objWS.AuditarIntentoInicioSesionConSesionActiva(Login);
        }

        #endregion

        #region "Areas"
        public Fachada.Seguridad.dtsKArea RecuperarAreas()
        {
            return obj.RecuperarAreas();
        }
        public Int32 AltaDeAreas(Fachada.Seguridad.dtsKArea dtsDatos)
        {
            return obj.AltaDeAreas(dtsDatos);
        }
        public Int32 ActualizarArea(Fachada.Seguridad.dtsKArea dtsDatos)
        {
            return obj.ActualizarArea(dtsDatos);
        }
        public Int32 EliminarArea(Int32 idArea)
        {
            return obj.EliminarArea(idArea);
        }
        public Int32 AgregarArea(Int32 idArea)
        {
            return obj.AgregarArea(idArea);
        }
        #endregion

        #region "Terminales"
        public Fachada.Seguridad.dtsTerminales RecuperarDatosParaABMTerminales()
        {
            return obj.RecuperarDatosParaABMTerminales();
        }

        public short AdministrarTerminales(Fachada.Seguridad.dtsTerminales dtsTerminal)
        {
            return obj.AdministrarTerminales(dtsTerminal);
        }

        public bool EliminarTerminal(short IDTerminal)
        {
            return obj.EliminarTerminal(IDTerminal);
        }
        #endregion

        #region "Usuario Sistema"
        public Int32 AdministrarAbmUsuarios(Fachada.Seguridad.dtsUsuariosABM pdtsDatos, ref string pstrError, string MensajeAuditoria, Int32 CantidadMaximaHistorialContraseniaa)
        {
            return obj.AdministrarAbmUsuarios(pdtsDatos, ref pstrError, MensajeAuditoria, CantidadMaximaHistorialContraseniaa);
        }

        public Int32 EliminarUsuario(Int32 pidUsuario, string strMensajeAuditoria)
        {
            return obj.EliminarUsuario(pidUsuario, strMensajeAuditoria);
        }

        public Int32 ActivarUsuario(Int32 pidUsuario, string strMensajeAuditoria)
        {
            return obj.ActivarUsuario(pidUsuario, strMensajeAuditoria);
        }


        public Fachada.Seguridad.dtsUsuariosABM RecuperarDatosParaABMUsuarios(Int32 pIdUsuario)
        {
            return obj.RecuperarDatosParaABMUsuarios(pIdUsuario);
        }

        //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
        public Fachada.Seguridad.dtsUsuarios RecuperarDatosParaGrillaABMUsuarios(COA.WebCipol.Entidades.ClasesWs.Filtros.dcFiltrosUsuarios fil)
        {
            return obj.RecuperarDatosParaGrillaABMUsuariosConParametros(fil.filtro, fil.chkArea, fil.chkNombre, fil.chkSubCadenas, fil.chkUsu, fil.filtrobaja);
        }

        public Fachada.Seguridad.dtsUsuarios RecuperarDatosParaGrillaABMUsuariosFiltro(string Filtro)
        {
            return obj.RecuperarDatosParaGrillaABMUsuariosFiltro(Filtro);
        }

        #endregion

        #region "Tarea"

        #region "Grupos de Tareas"

        public Int32 VerificaTareasAutorizantesEnUso(Int32 pidTarea)
        {
            return obj.VerificaTareasAutorizantesEnUso(pidTarea);
        }

        public Int32 EliminarTareasAutorizantes(Int32 pidTarea)
        {
            return obj.EliminarTareasAutorizantes(pidTarea);
        }

        public Fachada.Seguridad.dtsGrupos RecuperarDatosParaABMGrupos()
        {
            return obj.RecuperarDatosParaABMGrupos();
        }

        public Fachada.Seguridad.dtsGrupos RecuperarDatosDelGrupo(short IDGrupo)
        {
            return obj.RecuperarDatosDelGrupo(IDGrupo);
        }

        public Fachada.Seguridad.dtsGrupos RecuperarTareasPorSistemaYGrupo(short IDSistema, short IDGrupo)
        {
            return obj.RecuperarTareasPorSistemaYGrupo(IDSistema, IDGrupo);
        }

        public short AdministrarGrupo(Fachada.Seguridad.dtsGrupos dtsDatos)
        {
            return obj.AdministrarGrupo(dtsDatos);
        }

        public bool EliminarGrupo(short IDGrupo)
        {
            return obj.EliminarGrupo(IDGrupo);
        }

        #endregion

        #region "Tareas Autorizantes"

        public Fachada.Seguridad.dtsTareas RecuperarDatosParaABMTareasAutorizantes()
        {
            return obj.RecuperarDatosParaABMTareasAutorizantes();
        }

        public Fachada.Seguridad.dtsTareas RecuperarTareasSinAutorizacion(short IDSistema)
        {
            return obj.RecuperarTareasSinAutorizacion(IDSistema);
        }

        public Fachada.Seguridad.dtsTareas RecuperarTareasQueRequierenAutorizacion(int IDTareaAutorizante)
        {
            return obj.RecuperarTareasQueRequierenAutorizacion(IDTareaAutorizante);
        }

        public int AdministrarTareasSupervisantes(Fachada.Seguridad.dtsTareas dtsDatos)
        {
            return obj.AdministrarTareasSupervisantes(dtsDatos);
        }
        #endregion

        #region "Roles"

        public Int32 EliminarRol(Int32 pidRol)
        {
            return obj.EliminarRol(pidRol);
        }

        public Fachada.Seguridad.dtsRoles RecuperarDatosParaArbolDeGruposDeTareas()
        {
            return obj.RecuperarDatosParaArbolDeGruposDeTareas();
        }

        public int VerificarRolAsignadoAUsuarios(Int32 pidRol)
        {
            return obj.VerificarRolAsignadoAUsuarios(pidRol);
        }

        public Fachada.Seguridad.dtsRoles RecuperarDatosParaABMRoles()
        {
            return obj.RecuperarDatosParaABMRoles();
        }

        public Fachada.Seguridad.dtsRoles RecuperarComposicionRol(Int32 pIdRol, bool SoloIdTareas)
        {
            return obj.RecuperarComposicionRol(pIdRol, SoloIdTareas);
        }

        public Int32 AdministrarRoles(ref Fachada.Seguridad.dtsRoles pdtsDatos, string pStrElementosEliminados)
        {
            return obj.AdministrarRoles(ref pdtsDatos, pStrElementosEliminados);
        }
        #endregion

        #region "Tareas"

        public Fachada.Seguridad.dtsTareas RecuperarTareasPrimitivas()
        {
            return obj.RecuperarTareasPrimitivas();
        }

        public int InsertarTareaPrimitiva(Fachada.Seguridad.dtsTareas dtsTareas)
        {
            return obj.InsertarTareaPrimitiva(dtsTareas);
        }

        public int ActualizarTareaPrimitiva(Fachada.Seguridad.dtsTareas dtsDatosTareas)
        {
            return obj.ActualizarTareaPrimitiva(dtsDatosTareas);
        }

        public int EliminarTareaPrimitiva(Fachada.Seguridad.dtsTareas dtsTareas)
        {
            return obj.EliminarTareaPrimitiva(dtsTareas);
        }

        public int VerificarExistenciaIDTarea(int IDTarea)
        {
            return obj.VerificarExistenciaIDTarea(IDTarea);
        }

        public Fachada.Seguridad.dtsSistBloqueados RecuperarSistBloqueados(short IDSistema, short IDUsuario)
        {
            return obj.RecuperarSistBloqueados(IDSistema, IDUsuario);
        }

        public int InsertarSistBloqueados(Fachada.Seguridad.dtsSistBloqueados dtsDatosSistBloq)
        {
            return obj.InsertarSistBloqueados(dtsDatosSistBloq);
        }
        #endregion

        #endregion

        #region "Reportes"
        public Fachada.Seguridad.dtsRolesXUsuarios RecuperarReporteRolesXUsuario(Int32 pIdUsuario)
        {
            return obj.RecuperarReporteRolesXUsuario(pIdUsuario);
        }

        public Int32 RecuperarReporteRolesXUsuarioDetalle(Int32 pIdUsuario, ref Fachada.Seguridad.dtsRolesXUsuarios dtsRetorno)
        {
            return obj.RecuperarReporteRolesXUsuarioDetalle(pIdUsuario, ref dtsRetorno);
        }

        public Int32 RecuperarReporteUsuariosXRolDetalle(Int32 pIdRol, ref Fachada.Seguridad.dtsRolesXUsuarios dtsRetorno)
        {
            return obj.RecuperarReporteUsuariosXRolDetalle(pIdRol, ref dtsRetorno);
        }

        public Fachada.Seguridad.dtsRolesXUsuarios RecuperarReporteUsuariosXRol(Int32 pidRol)
        {
            return obj.RecuperarReporteUsuariosXRol(pidRol);
        }

        public Fachada.Seguridad.dtsUsuariosXAreas RecuperarReporteUsuariosXArea(Int32 IdArea)
        {
            return obj.RecuperarReporteUsuariosXArea(IdArea);
        }

        public Fachada.Seguridad.dtsRoles RecuperarReporteRolesComposicion(Int32 pIdrol)
        {
            return obj.RecuperarReporteRolesComposicion(pIdrol);
        }

        public Fachada.Seguridad.dtsRoles RecuperarDatosParaReporteUsuariosXRoles()
        {
            return obj.RecuperarDatosParaReporteUsuariosXRoles();
        }

        public Fachada.Seguridad.dtsUsuarios RecuperarDatosParaReporteRolesXUsuarios()
        {
            return obj.RecuperarDatosParaReporteRolesXUsuarios();
        }

        public Fachada.Seguridad.dtsReportes RecuperarTerminalesParaReporte(int IDArea, string Habilitadas, bool TodasLasAreas)
        {
            return obj.RecuperarTerminalesParaReporte(IDArea, Habilitadas, TodasLasAreas);
        }

        public Fachada.Seguridad.dtsUsuarios RecuperarReporteUsuario(Int32 pIdUsuario)
        {
            return obj.RecuperarReporteUsuario(pIdUsuario);
        }

        public Fachada.Seguridad.dtsUsuarios RecuperarReporteUsuarioSinAcceso()
        {
            return obj.RecuperarReporteUsuarioSinAcceso();
        }

        public Fachada.Seguridad.dtsCtrlInactividad RecuperarReporteControlInactividad(DateTime FechaUltimoUsoCta, string LapsoInactividad)
        {
            return obj.RecuperarReporteControlInactividad(FechaUltimoUsoCta, LapsoInactividad);
        }
        #endregion
    }
}