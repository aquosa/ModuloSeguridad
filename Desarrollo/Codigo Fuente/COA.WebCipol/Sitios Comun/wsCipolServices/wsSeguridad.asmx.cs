using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using wsCipolServices.ConectorWSCipolNET;
using wsCipolServices.ReglasDeNegocio;
using COA.WebCipol.Entidades;
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

namespace wsCipolServices
{
    /// <summary>
    /// Descripción breve de wsSeguridad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class wsSeguridad : System.Web.Services.WebService
    {
        #region "Seguridad"
        [WebMethod(EnableSession = true)]
        public string Ping(int Codigo)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.Ping(Codigo);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [viernes, 19 de julio de 2013]       Modificado  GCP-Cambios 
        /// </history>
        [WebMethod(EnableSession = true)]
        public dcRecuperarPoliticasGenerales RecuperarPoliticasGenerales()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RecuperarPoliticasGenerales();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaEntornoCIPOLAdministrador RecuperarDatosParaEntornoCIPOLAdministrador()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RecuperarDatosParaEntornoCIPOLAdministrador();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool AdministrarPoliticasGenerales(dcAdministrarPoliticasGenerales datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.AdministrarPoliticasGenerales(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaVisorSucesosSeguridad RecuperarDatosParaVisorSucesosSeguridad()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RecuperarDatosParaVisorSucesosSeguridad();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaMonitorActividades RecuperarDatosParaMonitorActividades()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RecuperarDatosParaMonitorActividades();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public Int32 EliminarSesionActiva(dcEliminarSesionActiva datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.EliminarSesionActiva(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRetornarLogAuditoria RetornarLogAuditoria(dcRetornarLogAuditoriaIN datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RetornarLogAuditoria(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool ActualizarTipoSeguridad(string TipoSeguridad)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.ActualizarTipoSeguridad(TipoSeguridad);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        #endregion

        #region "ABM Sistemas Habilitados"
        [WebMethod(EnableSession = true)]
        public dcRecuperarSistemasHabilitados RecuperarSistemasHabilitados()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RecuperarSistemasHabilitados();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public int InsertarSistemaHabilitado(dcInsertarSistemaHabilitado datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.InsertarSistemaHabilitado(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public int ActualizarSistemaHabilitado(dcActualizarSistemaHabilitado datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.ActualizarSistemaHabilitado(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public int EliminarSistemaHabilitado(dcEliminarSistemaHabilitado datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.EliminarSistemaHabilitado(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        #endregion

        #region "ANALISIS DE AUDITORIA"
        [WebMethod(EnableSession = true)]
        public dcRecuperarSIST_Habilitados RecuperarSIST_Habilitados()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RecuperarSIST_Habilitados();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarUsuarios RecuperarUsuarios()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarUsuarios());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperaTablasDeSistemas RecuperaTablasDeSistemas()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperaTablasDeSistemas());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }

        }

        [WebMethod(EnableSession = true)]
        public dcRecuperaTerminales RecuperaTerminales()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperaTerminales());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public DateTime RecuperarFechaMinima()
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.RecuperarFechaMinima();
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool ProbarStringSQL(string NewStringSQL)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.ProbarStringSQL(NewStringSQL);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarEventosAuditoria RecuperarEventosAuditoria(dcRecuperarEventosAuditoriaIN datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarEventosAuditoria(datos));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public string RetornaStringSQL(dcRetornaStringSQL datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.RetornaStringSQL(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarSupervisores RecuperarSupervisores()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarSupervisores());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        #endregion

        #region "Areas"
        [WebMethod(EnableSession = true)]
        public dcRecuperarAreas RecuperarAreas()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarAreas());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 AltaDeAreas(dcAltaDeAreas datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.AltaDeAreas(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 ActualizarArea(dcActualizarArea datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.ActualizarArea(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 EliminarArea(Int32 idArea)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.EliminarArea(idArea);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 AgregarArea(Int32 idArea)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.AgregarArea(idArea);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod()]
        public string AuditarIntentoInicioSesionConSesionActiva(string Login)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.AuditarIntentoInicioSesionConSesionActiva(Login);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        #endregion

        #region "Terminales"
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaABMTerminales RecuperarDatosParaABMTerminales()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaABMTerminales());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public short AdministrarTerminales(dcAdministrarTerminales datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.AdministrarTerminales(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public bool EliminarTerminal(short IDTerminal)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.EliminarTerminal(IDTerminal);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        #endregion

        #region "Usuario Sistema"
        [WebMethod(EnableSession = true)]
        public dcAdministrarAbmUsuarios AdministrarAbmUsuarios(dcAdministrarAbmUsuariosIN datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.AdministrarAbmUsuarios(datos));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 EliminarUsuario(Int32 pidUsuario, string strMensajeAuditoria)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.EliminarUsuario(pidUsuario, strMensajeAuditoria);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 ActivarUsuario(Int32 pidUsuario, string strMensajeAuditoria)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.ActivarUsuario(pidUsuario, strMensajeAuditoria);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaABMUsuarios RecuperarDatosParaABMUsuarios(Int32 pIdUsuario)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaABMUsuarios(pIdUsuario));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaGrillaABMUsuarios RecuperarDatosParaGrillaABMUsuarios(COA.WebCipol.Entidades.ClasesWs.Filtros.dcFiltrosUsuarios fil)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
                return (rnReglaSNegocio.RecuperarDatosParaGrillaABMUsuarios(fil));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaGrillaABMUsuariosFiltro RecuperarDatosParaGrillaABMUsuariosFiltro(string Filtro)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaGrillaABMUsuariosFiltro(Filtro));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        #endregion

        #region "Tarea"

        #region "Grupos de Tareas"
        [WebMethod(EnableSession = true)]
        public Int32 VerificaTareasAutorizantesEnUso(Int32 pidTarea)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.VerificaTareasAutorizantesEnUso(pidTarea);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public Int32 EliminarTareasAutorizantes(Int32 pidTarea)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.EliminarTareasAutorizantes(pidTarea);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaABMGrupos RecuperarDatosParaABMGrupos()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaABMGrupos());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosDelGrupo RecuperarDatosDelGrupo(short IDGrupo)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosDelGrupo(IDGrupo));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarTareasPorSistemaYGrupo RecuperarTareasPorSistemaYGrupo(short IDSistema, short IDGrupo)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarTareasPorSistemaYGrupo(IDSistema, IDGrupo));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public short AdministrarGrupo(dcAdministrarGrupo datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.AdministrarGrupo(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public bool EliminarGrupo(short IDGrupo)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.EliminarGrupo(IDGrupo);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }

        #endregion

        #region "Tareas Autorizantes"
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaABMTareasAutorizantes RecuperarDatosParaABMTareasAutorizantes()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaABMTareasAutorizantes());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarTareasSinAutorizacion RecuperarTareasSinAutorizacion(short IDSistema)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarTareasSinAutorizacion(IDSistema));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarTareasQueRequierenAutorizacion RecuperarTareasQueRequierenAutorizacion(int IDTareaAutorizante)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarTareasQueRequierenAutorizacion(IDTareaAutorizante));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int AdministrarTareasSupervisantes(dcAdministrarTareasSupervisantes datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.AdministrarTareasSupervisantes(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        #endregion

        #region "Roles"
        [WebMethod(EnableSession = true)]
        public Int32 EliminarRol(Int32 pidRol)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.EliminarRol(pidRol);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaArbolDeGruposDeTareas RecuperarDatosParaArbolDeGruposDeTareas()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaArbolDeGruposDeTareas());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int VerificarRolAsignadoAUsuarios(Int32 pidRol)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.VerificarRolAsignadoAUsuarios(pidRol);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaABMRoles RecuperarDatosParaABMRoles()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaABMRoles());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarComposicionRol RecuperarComposicionRol(Int32 pIdRol, bool SoloIdTareas)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarComposicionRol(pIdRol, SoloIdTareas));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcAdministrarRoles AdministrarRoles(dcAdministrarRolesIN datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.AdministrarRoles(datos));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        #endregion

        #region "Tareas"
        [WebMethod(EnableSession = true)]
        public dcRecuperarTareasPrimitivas RecuperarTareasPrimitivas()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarTareasPrimitivas());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int InsertarTareaPrimitiva(dcInsertarTareaPrimitiva datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.InsertarTareaPrimitiva(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int ActualizarTareaPrimitiva(dcActualizarTareaPrimitiva datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.ActualizarTareaPrimitiva(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int EliminarTareaPrimitiva(dcEliminarTareaPrimitiva datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.EliminarTareaPrimitiva(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int VerificarExistenciaIDTarea(int IDTarea)
        {
            try
            {
                ConectorwsSeguridad cnInicioSesion = new ConectorwsSeguridad();
                return cnInicioSesion.VerificarExistenciaIDTarea(IDTarea);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarSistBloqueados RecuperarSistBloqueados(short IDSistema, short IDUsuario)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarSistBloqueados(IDSistema, IDUsuario));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public int InsertarSistBloqueados(dcInsertarSistBloqueados datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return rnReglaSNegocio.InsertarSistBloqueados(datos);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        #endregion

        #endregion

        #region "Reportes"
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteRolesXUsuario RecuperarReporteRolesXUsuario(Int32 pIdUsuario)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteRolesXUsuario(pIdUsuario));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteRolesXUsuarioDetalle RecuperarReporteRolesXUsuarioDetalle(dcRecuperarReporteRolesXUsuarioDetalleIN datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteRolesXUsuarioDetalle(datos));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteUsuariosXRolDetalle RecuperarReporteUsuariosXRolDetalle(dcRecuperarReporteUsuariosXRolDetalleIN datos)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteUsuariosXRolDetalle(datos));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteUsuariosXRol RecuperarReporteUsuariosXRol(Int32 pidRol)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteUsuariosXRol(pidRol));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteUsuariosXArea RecuperarReporteUsuariosXArea(Int32 IdArea)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteUsuariosXArea(IdArea));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteRolesComposicion RecuperarReporteRolesComposicion(Int32 pIdrol)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteRolesComposicion(pIdrol));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaReporteUsuariosXRoles RecuperarDatosParaReporteUsuariosXRoles()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaReporteUsuariosXRoles());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarDatosParaReporteRolesXUsuarios RecuperarDatosParaReporteRolesXUsuarios()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarDatosParaReporteRolesXUsuarios());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarTerminalesParaReporte RecuperarTerminalesParaReporte(int IDArea, string Habilitadas, bool TodasLasAreas)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarTerminalesParaReporte(IDArea, Habilitadas, TodasLasAreas));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteUsuario RecuperarReporteUsuario(Int32 pIdUsuario)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteUsuario(pIdUsuario));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteUsuarioSinAcceso RecuperarReporteUsuarioSinAcceso()
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteUsuarioSinAcceso());
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        [WebMethod(EnableSession = true)]
        public dcRecuperarReporteControlInactividad RecuperarReporteControlInactividad(DateTime FechaUltimoUsoCta, string LapsoInactividad)
        {
            try
            {
                RNwsSeguridad rnReglaSNegocio = new RNwsSeguridad();
                return (rnReglaSNegocio.RecuperarReporteControlInactividad(FechaUltimoUsoCta, LapsoInactividad));
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                throw ex;
            }
        }
        #endregion
    }
}
