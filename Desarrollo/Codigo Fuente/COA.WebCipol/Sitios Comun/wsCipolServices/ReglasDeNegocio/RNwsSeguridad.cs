using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsCipolServices.ConectorWSCipolNET;
using System.Data;
using COA.WebCipol.Entidades.ClasesWs;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaMonitorActividades;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarSesionActiva;
using COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados;
using COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado;
using COA.WebCipol.Entidades.Core;
using COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSIST_Habilitados;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperaTablasDeSistemas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperaTerminales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria;
using COA.WebCipol.Entidades.ClasesWs.dcRetornaStringSQL;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSupervisores;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas;
using COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas;
using COA.WebCipol.Entidades.ClasesWs.dcActualizarArea;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuariosFiltro;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPorSistemaYGrupo;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasSinAutorizacion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasQueRequierenAutorizacion;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaArbolDeGruposDeTareas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados;
using COA.WebCipol.Entidades.ClasesWs.dcInsertarTareaPrimitiva;
using COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarTareaPrimitiva;
using COA.WebCipol.Entidades.ClasesWs.dcInsertarSistBloqueados;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuario;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuarioDetalle;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXArea;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesComposicion;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteUsuariosXRoles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteRolesXUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarTerminalesParaReporte;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuarioSinAcceso;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarPoliticasGenerales;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarPoliticasGenerales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaEntornoCIPOLAdministrador;
using COA.WebCipol.Entidades;

namespace wsCipolServices.ReglasDeNegocio
{
    public partial class RNwsSeguridad
    {
        #region "Seguridad"
        public dcRecuperarPoliticasGenerales RecuperarPoliticasGenerales()
        {
            ConectorwsSeguridad objConector;
            DataSet dtsRetorno;
            dcRecuperarPoliticasGenerales dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarPoliticasGenerales();
                dtsRetorno = objConector.RecuperarPoliticasGenerales();

                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_PARAMETROS = EntityLoader.Load<COA.WebCipol.Entidades.SE_PARAMETROS>(dtsRetorno, "Sist_parametros");
                return dcRetorno;
            }
            catch (Exception)
            {
                //todo: dejar log de errores
                return null;
            }
        }

        public dcRecuperarDatosParaEntornoCIPOLAdministrador RecuperarDatosParaEntornoCIPOLAdministrador()
        {
            ConectorwsSeguridad objConector;
            DataSet dtsRetorno;
            dcRecuperarDatosParaEntornoCIPOLAdministrador dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarDatosParaEntornoCIPOLAdministrador();

                dcRetorno = new dcRecuperarDatosParaEntornoCIPOLAdministrador();
                //Retorn las listas correspondiente a la tabla.
                dcRetorno.lstParametros = EntityLoader.Load<COA.WebCipol.Entidades.SE_PARAMETROS>(dtsRetorno, "SIST_PARAMETROS");
                dcRetorno.lstCodAuditoria = EntityLoader.Load<COA.WebCipol.Entidades.SE_CODAUDITORIA>(dtsRetorno, "SE_CODAUDITORIA");
                dcRetorno.lstMensajes = EntityLoader.Load<SE_MENSAJES>(dtsRetorno, "GRECMENSAJES");

                return dcRetorno;

            }
            catch (Exception)
            {
                //todo: dejar log de errores
                return null;
            }
        }

        public bool AdministrarPoliticasGenerales(dcAdministrarPoliticasGenerales datos)
        {
            ConectorwsSeguridad objConector;
            DataSet dtsDatos;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsDatos = new DataSet();
                //Pasa de lista a DataSet.
                dtsDatos = ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarPoliticasGenerales.SE_PARAMETROS>(datos.lstSE_PARAMETROS, "SE_PARAMETROS");

                if (!datos.lstSE_PARAMETROS[0].Added)
                    foreach (DataRow item in dtsDatos.Tables[0].Rows)
                    {
                        item.AcceptChanges();
                        item.SetModified();
                    }
                //todo: ver como manipular los rowstate.
                return objConector.AdministrarPoliticasGenerales(dtsDatos);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaVisorSucesosSeguridad RecuperarDatosParaVisorSucesosSeguridad()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSucesosSeguridad dtsRetorno;
            dcRecuperarDatosParaVisorSucesosSeguridad dcRetorno;
            try
            {

                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarDatosParaVisorSucesosSeguridad();

                dcRetorno = new dcRecuperarDatosParaVisorSucesosSeguridad();
                //todo: probar.
                dcRetorno.lstCodAuditoria = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad.SE_CODAUDITORIA>(dtsRetorno, "SE_CodAuditoria");
                dcRetorno.lstSist_UsuariosConTareasCipol = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad.Sist_UsuariosConTareasCipol>(dtsRetorno, "Sist_UsuariosConTareasCipol");
                dcRetorno.lstSistUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                //Retorn la lista correspondiente a la tabla.
                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaMonitorActividades RecuperarDatosParaMonitorActividades()
        {
            ConectorwsSeguridad objConector;
            DataSet dtsRetorno;
            dcRecuperarDatosParaMonitorActividades dcRetorno;
            try
            {

                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarDatosParaMonitorActividades();

                //Retorn la lista correspondiente a la tabla.
                dcRetorno = new dcRecuperarDatosParaMonitorActividades();
                dcRetorno.lstSist_sesionesactivas = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaMonitorActividades.Sist_sesionesactivas>(dtsRetorno, "SIST_SESIONESACTIVAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Int32 EliminarSesionActiva(dcEliminarSesionActiva dcCriterio)
        {
            ConectorwsSeguridad objConector;
            DataSet dtsRetorno = new DataSet();
            try
            {
                objConector = new ConectorwsSeguridad();
               
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcEliminarSesionActiva.Sist_sesionesactivas>(dcCriterio.lstSist_sesionesactivas, "SIST_SESIONESACTIVAS", dtsRetorno);

                return objConector.EliminarSesionActiva(dtsRetorno);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRetornarLogAuditoria RetornarLogAuditoria(dcRetornarLogAuditoriaIN datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSucesosSeguridad dtsRetorno;
            dcRetornarLogAuditoria dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RetornarLogAuditoria(datos.fechadesde, datos.fechahasta, datos.UsuarioActuante, datos.usuarioafectado, datos.CodigoEvento);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno = new dcRetornarLogAuditoria();
                dcRetorno.lstAuditoria = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria.SE_AUDITORIA>(dtsRetorno, "SE_AUDITORIA");
                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarSistemasHabilitados RecuperarSistemasHabilitados()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSistHabilitados dtsRetorno;
            dcRecuperarSistemasHabilitados dcRetorno;
            try
            {

                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarSistemasHabilitados();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno = new dcRecuperarSistemasHabilitados();
                dcRetorno.lstdtsSistHabilitadosSE_SIST_HABILITADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados.SE_SIST_HABILITADOS>(dtsRetorno, "SE_SIST_HABILITADOS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertarSistemaHabilitado(dcInsertarSistemaHabilitado datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSistHabilitados dtsSistHabilitados;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsSistHabilitados = new Fachada.Seguridad.dtsSistHabilitados();
                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado.SE_SIST_HABILITADOS>(datos.lstSE_SIST_HABILITADOS, "SE_SIST_HABILITADOS", dtsSistHabilitados);
                intRetorno = objConector.InsertarSistemaHabilitado(dtsSistHabilitados);
                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ActualizarSistemaHabilitado(dcActualizarSistemaHabilitado datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSistHabilitados dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsSistHabilitados();

                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado.SE_SIST_HABILITADOS>(datos.lstSE_SIST_HABILITADOS, "SE_SIST_HABILITADOS", dtsRetorno);

                intRetorno = objConector.ActualizarSistemaHabilitado(dtsRetorno);
                //Retorn la lista correspondiente a la tabla.
                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EliminarSistemaHabilitado(dcEliminarSistemaHabilitado datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSistHabilitados dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsSistHabilitados();

                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado.SE_SIST_HABILITADOS>(datos.lstSE_SIST_HABILITADOS, "SE_SIST_HABILITADOS", dtsRetorno);

                intRetorno = objConector.EliminarSistemaHabilitado(dtsRetorno);
                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarSIST_Habilitados RecuperarSIST_Habilitados()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            dcRecuperarSIST_Habilitados dcRetorno;
            try
            {

                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarSIST_Habilitados();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno = new dcRecuperarSIST_Habilitados();
                dcRetorno.lstSE_SIST_HABILITADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSIST_Habilitados.SE_SIST_HABILITADOS>(dtsRetorno, "SE_SIST_HABILITADOS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarUsuarios RecuperarUsuarios()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            dcRecuperarUsuarios dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarUsuarios();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno = new dcRecuperarUsuarios();
                dcRetorno.lstUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarUsuarios.SE_USUARIOS>(dtsRetorno, "SE_USUARIOS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperaTablasDeSistemas RecuperaTablasDeSistemas()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            dcRecuperaTablasDeSistemas dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperaTablasDeSistemas();

                dtsRetorno = objConector.RecuperaTablasDeSistemas();
                dcRetorno.lstTablasDeSistema = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperaTablasDeSistemas.TablasDeSistema>(dtsRetorno, "TablasDeSistema");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperaTerminales RecuperaTerminales()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            dcRecuperaTerminales dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperaTerminales();
                dtsRetorno = objConector.RecuperaTerminales();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstTerminales = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperaTerminales.SE_TERMINALES>(dtsRetorno, "SE_TERMINALES");
                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public dcRecuperarEventosAuditoria RecuperarEventosAuditoria(dcRecuperarEventosAuditoriaIN dtsFiltros)
        {

            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            dcRecuperarEventosAuditoria dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsAuditoriaEventos();
                dcRetorno = new dcRecuperarEventosAuditoria();

                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria.DtFiltros>(dtsFiltros.lstFiltros, "DtFiltros", dtsRetorno);
                dtsRetorno = objConector.RecuperarEventosAuditoria(dtsRetorno);
                
                dcRetorno.Sist_Eventos = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria.SIST_EVENTOS>(dtsRetorno, "SIST_EVENTOS");
                dcRetorno.CantRegistrosTotal = dtsRetorno.ResultadoConsulta[0].CantidadRegistros;

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string RetornaStringSQL(dcRetornaStringSQL dtsFiltros)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            string strRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsAuditoriaEventos();

                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcRetornaStringSQL.DtFiltros>(dtsFiltros.lstFiltros, "DtFiltros", dtsRetorno);
                strRetorno = objConector.RetornaStringSQL(dtsRetorno);
                //Retorn la lista correspondiente a la tabla.
                return strRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarSupervisores RecuperarSupervisores()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsAuditoriaEventos dtsRetorno;
            dcRecuperarSupervisores dcRetorno;
            try
            {

                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarSupervisores();

                dtsRetorno = objConector.RecuperarSupervisores();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstDtSupervisores = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSupervisores.DtSupervisores>(dtsRetorno, "DtSupervisores");
                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Areas"
        public dcRecuperarAreas RecuperarAreas()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsKArea dtsRetorno;
            dcRecuperarAreas dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = objConector.RecuperarAreas();

                dcRetorno = new dcRecuperarAreas();
                //Retorn las listas correspondiente a la tabla.
                dcRetorno.lstKAreas = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas.SIST_KAREAS>(dtsRetorno, "SIST_KAREAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                //todo: dejar log de errores
                return null;
            }
        }
        public Int32 AltaDeAreas(dcAltaDeAreas datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsKArea dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsKArea();
                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.SIST_KAREAS>(datos.lstKAreas, "SIST_KAREAS", dtsRetorno);
                intRetorno = objConector.AltaDeAreas(dtsRetorno);
                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Int32 ActualizarArea(dcActualizarArea datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsKArea dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsKArea();
                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.SIST_KAREAS>(datos.lstKAreas, "SIST_KAREAS", dtsRetorno);
                intRetorno = objConector.ActualizarArea(dtsRetorno);
                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Terminal"
        public dcRecuperarDatosParaABMTerminales RecuperarDatosParaABMTerminales()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTerminales dtsRetorno;
            dcRecuperarDatosParaABMTerminales dcRetorno;
            try
            {

                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaABMTerminales();

                dtsRetorno = objConector.RecuperarDatosParaABMTerminales();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstKAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.KAREAS>(dtsRetorno, "KAREAS");
                dcRetorno.lstSE_teminales = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.SE_TERMINALES>(dtsRetorno, "SE_TERMINALES");
                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public short AdministrarTerminales(dcAdministrarTerminales datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTerminales dtsRetorno;
            short intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsTerminales();
                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES>(datos.lstSE_TERMINALES, "SE_TERMINALES", dtsRetorno);

                foreach (Fachada.Seguridad.dtsTerminales.SE_TERMINALESRow item in dtsRetorno.SE_TERMINALES)
                    if (item.IDTERMINAL > 0)
                    {
                        item.AcceptChanges();
                        item.SetModified();
                    }

                intRetorno = objConector.AdministrarTerminales(dtsRetorno);
                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Usuario Sistema"
        public dcAdministrarAbmUsuarios AdministrarAbmUsuarios(dcAdministrarAbmUsuariosIN datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuariosABM dtsRetorno;
            dcAdministrarAbmUsuarios dcRetorno;
            int intRetorno;
            string pstrError = "";
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsUsuariosABM();
                dcRetorno = new dcAdministrarAbmUsuarios();

                pstrError = datos.pstrError;
                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM>(datos.lstParametrosDeABM, "ParametrosDeABM", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.Roles_Composicion>(datos.lstRoles_Composicion, "Roles_Composicion", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_Horarios_Usuario>(datos.lstSE_Horarios_Usuario, "SE_Horarios_Usuario", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_Term_Usuario>(datos.lstSE_Term_Usuario, "SE_Term_Usuario", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.Sist_Usuarios>(datos.lstSist_Usuarios, "Sist_Usuarios", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_HISTORIAL_USUARIO>(datos.lstSE_HISTORIAL_USUARIO, "SE_HISTORIAL_USUARIO", dtsRetorno);
                //todo: DataRowState.Added
                intRetorno = objConector.AdministrarAbmUsuarios(dtsRetorno, ref pstrError, datos.MensajeAuditoria, datos.CantidadMaximaHistorialContraseniaa);

                dcRetorno.pstrError = pstrError;
                dcRetorno.intRetorno = intRetorno;

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaABMUsuarios RecuperarDatosParaABMUsuarios(Int32 pIdUsuario)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuariosABM dtsRetorno;
            dcRecuperarDatosParaABMUsuarios dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsUsuariosABM();
                dcRetorno = new dcRecuperarDatosParaABMUsuarios();
                //Carga el DataSet con los datos de la lista.
                //todo: DataRowState.Added
                dtsRetorno = objConector.RecuperarDatosParaABMUsuarios(pIdUsuario);

                dcRetorno.lstRoles_Composicion = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion>(dtsRetorno, "Roles_Composicion");
                dcRetorno.lstSE_HISTORIAL_USUARIO = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.SE_HISTORIAL_USUARIO>(dtsRetorno, "SE_HISTORIAL_USUARIO");
                dcRetorno.lstSE_Horarios_Usuario = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.SE_Horarios_Usuario>(dtsRetorno, "SE_Horarios_Usuario");
                dcRetorno.lstSE_Term_Usuario = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.SE_Term_Usuario>(dtsRetorno, "SE_Term_Usuario");
                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
        public dcRecuperarDatosParaGrillaABMUsuarios RecuperarDatosParaGrillaABMUsuarios(COA.WebCipol.Entidades.ClasesWs.Filtros.dcFiltrosUsuarios fil)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuarios dtsRetorno;
            dcRecuperarDatosParaGrillaABMUsuarios dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsUsuarios();
                dcRetorno = new dcRecuperarDatosParaGrillaABMUsuarios();
                //Carga el DataSet con los datos de la lista.
                //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
                dtsRetorno = objConector.RecuperarDatosParaGrillaABMUsuarios(fil);

                List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRolesAuxiliar> lstAuxiliar;
                lstAuxiliar = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRolesAuxiliar>(dtsRetorno, "ComposicionDeRoles");


                dcRetorno.lstComposicionDeRoles.AddRange(from item in lstAuxiliar
                                                         select new ComposicionDeRoles() { IdRol = (int)item.IdRol,
                                                                                           IdGrupo = (int)item.IdGrupo,
                                                                                           IdSistema = (int)item.IdSistema,
                                                                                           IdTarea = (int)item.IdTarea,
                                                                                           DescGrupo = item.DescGrupo,
                                                                                           DescripcionPerfil = item.DescripcionPerfil,
                                                                                           DescripcionTarea = item.DescripcionTarea,
                                                                                           DescSistema = item.DescSistema
                                                         });

                dcRetorno.lstKAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.KAREAS>(dtsRetorno, "KAREAS");
                dcRetorno.lstKDocumentos = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.KDocumentos>(dtsRetorno, "KDocumentos");
                dcRetorno.lstSE_GRUPO_EXCLUSION = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_GRUPO_EXCLUSION>(dtsRetorno, "SE_GRUPO_EXCLUSION");
                dcRetorno.lstSE_Horarios_Usuario = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_Horarios_Usuario>(dtsRetorno, "SE_Horarios_Usuario");
                dcRetorno.lstSE_PARAMETROS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_PARAMETROS>(dtsRetorno, "recParam");
                dcRetorno.lstSE_TERMINALES = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_TERMINALES>(dtsRetorno, "SE_TERMINALES");
                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaGrillaABMUsuariosFiltro RecuperarDatosParaGrillaABMUsuariosFiltro(string Filtro)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuarios dtsRetorno;
            dcRecuperarDatosParaGrillaABMUsuariosFiltro dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsUsuarios();
                dcRetorno = new dcRecuperarDatosParaGrillaABMUsuariosFiltro();
                //Carga el DataSet con los datos de la lista.
                dtsRetorno = objConector.RecuperarDatosParaGrillaABMUsuariosFiltro(Filtro);

                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuariosFiltro.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Tarea"

        #region "Grupos de Tareas"

        public dcRecuperarDatosParaABMGrupos RecuperarDatosParaABMGrupos()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsGrupos dtsRetorno;
            dcRecuperarDatosParaABMGrupos dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaABMGrupos();

                dtsRetorno = objConector.RecuperarDatosParaABMGrupos();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstGRUPO_EXCLUYENTE = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.GRUPO_EXCLUYENTE>(dtsRetorno, "GRUPO_EXCLUYENTE");
                dcRetorno.lstGRUPO_NO_EXCLUYENTE = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.GRUPO_NO_EXCLUYENTE>(dtsRetorno, "GRUPO_NO_EXCLUYENTE");
                dcRetorno.lstSE_GRUPO_TAREA = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.SE_GRUPO_TAREA>(dtsRetorno, "SE_GRUPO_TAREA");
                dcRetorno.lstSE_SIST_HABILITADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.SE_SIST_HABILITADOS>(dtsRetorno, "SE_SIST_HABILITADOS");
                dcRetorno.lstSE_TAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.SE_TAREAS>(dtsRetorno, "SE_TAREAS");
                dcRetorno.lstSE_TAREAS_ASIGNADAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.SE_TAREAS_ASIGNADAS>(dtsRetorno, "SE_TAREAS_ASIGNADAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosDelGrupo RecuperarDatosDelGrupo(short IDGrupo)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsGrupos dtsRetorno;
            dcRecuperarDatosDelGrupo dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosDelGrupo();

                dtsRetorno = objConector.RecuperarDatosDelGrupo(IDGrupo);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstGRUPO_EXCLUYENTE = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo.GRUPO_EXCLUYENTE>(dtsRetorno, "GRUPO_EXCLUYENTE");
                dcRetorno.lstGRUPO_NO_EXCLUYENTE = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo.GRUPO_NO_EXCLUYENTE>(dtsRetorno, "GRUPO_NO_EXCLUYENTE");
                dcRetorno.lstSE_TAREAS_ASIGNADAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo.SE_TAREAS_ASIGNADAS>(dtsRetorno, "SE_TAREAS_ASIGNADAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarTareasPorSistemaYGrupo RecuperarTareasPorSistemaYGrupo(short IDSistema, short IDGrupo)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsGrupos dtsRetorno;
            dcRecuperarTareasPorSistemaYGrupo dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarTareasPorSistemaYGrupo();

                dtsRetorno = objConector.RecuperarTareasPorSistemaYGrupo(IDSistema, IDGrupo);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_TAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPorSistemaYGrupo.SE_TAREAS>(dtsRetorno, "SE_TAREAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public short AdministrarGrupo(dcAdministrarGrupo datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsGrupos dtsRetorno;
            short shtRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsGrupos();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.Auditoria>(datos.lstAuditoria, "Auditoria", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.GRUPO_EXCLUYENTE>(datos.lstGRUPO_EXCLUYENTE, "GRUPO_EXCLUYENTE", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.SE_GRUPO_TAREA>(datos.lstSE_GRUPO_TAREA, "SE_GRUPO_TAREA", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.SE_TAREAS_ASIGNADAS>(datos.lstSE_TAREAS_ASIGNADAS, "SE_TAREAS_ASIGNADAS", dtsRetorno);
                //todo: DataRowState.Added
                shtRetorno = objConector.AdministrarGrupo(dtsRetorno);

                return shtRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region "Tareas Autorizantes"

        public dcRecuperarDatosParaABMTareasAutorizantes RecuperarDatosParaABMTareasAutorizantes()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            dcRecuperarDatosParaABMTareasAutorizantes dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaABMTareasAutorizantes();

                dtsRetorno = objConector.RecuperarDatosParaABMTareasAutorizantes();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_SIST_HABILITADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.SE_SIST_HABILITADOS>(dtsRetorno, "SE_SIST_HABILITADOS");
                dcRetorno.lstSE_TAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.SE_TAREAS>(dtsRetorno, "SE_TAREAS");
                dcRetorno.lstTAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.TAREAS>(dtsRetorno, "TAREAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarTareasSinAutorizacion RecuperarTareasSinAutorizacion(short IDSistema)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            dcRecuperarTareasSinAutorizacion dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarTareasSinAutorizacion();

                dtsRetorno = objConector.RecuperarTareasSinAutorizacion(IDSistema);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_TAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasSinAutorizacion.SE_TAREAS>(dtsRetorno, "SE_TAREAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarTareasQueRequierenAutorizacion RecuperarTareasQueRequierenAutorizacion(int IDTareaAutorizante)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            dcRecuperarTareasQueRequierenAutorizacion dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarTareasQueRequierenAutorizacion();

                dtsRetorno = objConector.RecuperarTareasQueRequierenAutorizacion(IDTareaAutorizante);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_TAREASAUTORIZADAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasQueRequierenAutorizacion.SE_TAREASAUTORIZADAS>(dtsRetorno, "SE_TAREASAUTORIZADAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int AdministrarTareasSupervisantes(dcAdministrarTareasSupervisantes datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsTareas();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.SE_TAREASAUTORIZADAS>(datos.lstSE_TAREASAUTORIZADAS, "SE_TAREASAUTORIZADAS", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS>(datos.lstTAREAS, "TAREAS", dtsRetorno);

                intRetorno = objConector.AdministrarTareasSupervisantes(dtsRetorno);

                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Roles"

        public dcRecuperarDatosParaArbolDeGruposDeTareas RecuperarDatosParaArbolDeGruposDeTareas()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRoles dtsRetorno;
            dcRecuperarDatosParaArbolDeGruposDeTareas dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaArbolDeGruposDeTareas();

                dtsRetorno = objConector.RecuperarDatosParaArbolDeGruposDeTareas();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstArbolGrupo = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaArbolDeGruposDeTareas.ArbolGrupo>(dtsRetorno, "ArbolGrupo");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaABMRoles RecuperarDatosParaABMRoles()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRoles dtsRetorno;
            dcRecuperarDatosParaABMRoles dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaABMRoles();

                dtsRetorno = objConector.RecuperarDatosParaABMRoles();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstArbolGrupo = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo>(dtsRetorno, "ArbolGrupo");
                dcRetorno.lstRoles_Composicion = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.Roles_Composicion>(dtsRetorno, "Roles_Composicion");
                dcRetorno.lstSE_GRUPO_EXCLUSION = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.SE_GRUPO_EXCLUSION>(dtsRetorno, "SE_GRUPO_EXCLUSION");
                dcRetorno.lstSE_ROLES = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.SE_ROLES>(dtsRetorno, "SE_ROLES");
                dcRetorno.lsttblUsuariosXRoles = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.tblUsuariosXRoles>(dtsRetorno, "tblUsuariosXRoles");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarComposicionRol RecuperarComposicionRol(Int32 pIdRol, bool SoloIdTareas)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRoles dtsRetorno;
            dcRecuperarComposicionRol dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarComposicionRol();

                dtsRetorno = objConector.RecuperarComposicionRol(pIdRol, SoloIdTareas);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstRoles_Composicion = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion>(dtsRetorno, "Roles_Composicion");
                dcRetorno.lsttblUsuariosXRoles = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.tblUsuariosXRoles>(dtsRetorno, "tblUsuariosXRoles");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcAdministrarRoles AdministrarRoles(dcAdministrarRolesIN datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRoles dtsRetorno;
            dcAdministrarRoles dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsRoles();
                dcRetorno = new dcAdministrarRoles();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.ParametrosDeABM>(datos.lstParametrosDeABM, "ParametrosDeABM", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.SE_ROLES>(datos.lstSE_ROLES, "SE_ROLES", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.TareasAsignadas>(datos.lstTareasAsignadas, "TareasAsignadas", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.TareasNoAsignadas>(datos.lstTareasNoAsignadas, "TareasNoAsignadas", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.tblUsuariosXRoles>(datos.lsttblUsuariosXRoles, "tblUsuariosXRoles", dtsRetorno);

                dcRetorno.intRetorno = objConector.AdministrarRoles(ref dtsRetorno, datos.pStrElementosEliminados);

                dcRetorno.lstParametrosDeABM = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.ParametrosDeABM>(dtsRetorno, "ParametrosDeABM");
                dcRetorno.lstSE_ROLES = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.SE_ROLES>(dtsRetorno, "SE_ROLES");
                dcRetorno.lstTareasAsignadas = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.TareasAsignadas>(dtsRetorno, "TareasAsignadas");
                dcRetorno.lstTareasNoAsignadas = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.TareasNoAsignadas>(dtsRetorno, "TareasNoAsignadas");
                dcRetorno.lsttblUsuariosXRoles = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.tblUsuariosXRoles>(dtsRetorno, "tblUsuariosXRoles");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region "Tareas"

        public dcRecuperarTareasPrimitivas RecuperarTareasPrimitivas()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            dcRecuperarTareasPrimitivas dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarTareasPrimitivas();

                dtsRetorno = objConector.RecuperarTareasPrimitivas();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_SIST_HABILITADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.SE_SIST_HABILITADOS>(dtsRetorno, "SE_SIST_HABILITADOS");
                dcRetorno.lstTAREAS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.TAREAS>(dtsRetorno, "TAREAS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertarTareaPrimitiva(dcInsertarTareaPrimitiva datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsTareas();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcInsertarTareaPrimitiva.TAREAS>(datos.lstTAREAS, "TAREAS", dtsRetorno);

                intRetorno = objConector.InsertarTareaPrimitiva(dtsRetorno);

                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ActualizarTareaPrimitiva(dcActualizarTareaPrimitiva datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsTareas();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva.TAREAS>(datos.lstTAREAS, "TAREAS", dtsRetorno);

                intRetorno = objConector.ActualizarTareaPrimitiva(dtsRetorno);

                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int EliminarTareaPrimitiva(dcEliminarTareaPrimitiva datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsTareas dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsTareas();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcEliminarTareaPrimitiva.TAREAS>(datos.lstTAREAS, "TAREAS", dtsRetorno);

                intRetorno = objConector.EliminarTareaPrimitiva(dtsRetorno);

                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarSistBloqueados RecuperarSistBloqueados(short IDSistema, short IDUsuario)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSistBloqueados dtsRetorno;
            dcRecuperarSistBloqueados dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarSistBloqueados();

                dtsRetorno = objConector.RecuperarSistBloqueados(IDSistema, IDUsuario);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_SIST_BLOQUEADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS>(dtsRetorno, "SE_SIST_BLOQUEADOS");
                dcRetorno.lstSE_SIST_HABILITADOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_HABILITADOS>(dtsRetorno, "SE_SIST_HABILITADOS");
                dcRetorno.lstSE_USUARIOS = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS>(dtsRetorno, "SE_USUARIOS");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertarSistBloqueados(dcInsertarSistBloqueados datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsSistBloqueados dtsRetorno;
            int intRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsSistBloqueados();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcInsertarSistBloqueados.SE_SIST_BLOQUEADOS>(datos.lstSE_SIST_BLOQUEADOS, "SE_SIST_BLOQUEADOS", dtsRetorno);

                intRetorno = objConector.InsertarSistBloqueados(dtsRetorno);

                return intRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #endregion

        #region "Reportes"
        public dcRecuperarReporteRolesXUsuario RecuperarReporteRolesXUsuario(Int32 pIdUsuario)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRolesXUsuarios dtsRetorno;
            dcRecuperarReporteRolesXUsuario dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteRolesXUsuario();

                dtsRetorno = objConector.RecuperarReporteRolesXUsuario(pIdUsuario);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstRolesXUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuario.RolesXUsuarios>(dtsRetorno, "RolesXUsuarios");
                dcRetorno.lstSE_TAREAS_USUARIO = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuario.SE_TAREAS_USUARIO>(dtsRetorno, "SE_TAREAS_USUARIO");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteRolesXUsuarioDetalle RecuperarReporteRolesXUsuarioDetalle(dcRecuperarReporteRolesXUsuarioDetalleIN datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRolesXUsuarios dtsRetorno;
            dcRecuperarReporteRolesXUsuarioDetalle dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsRolesXUsuarios();
                dcRetorno = new dcRecuperarReporteRolesXUsuarioDetalle();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuarioDetalle.RolesXUsuarios>(datos.lstRolesXUsuarios, "RolesXUsuarios", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuarioDetalle.RolesXUsuarioDetalle>(datos.lstRolesXUsuarioDetalle, "RolesXUsuarioDetalle", dtsRetorno);


                dcRetorno.intRetorno = objConector.RecuperarReporteRolesXUsuarioDetalle(datos.pIdUsuario, ref dtsRetorno);

                dcRetorno.lstRolesXUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuarioDetalle.RolesXUsuarios>(dtsRetorno, "RolesXUsuarios");
                dcRetorno.lstRolesXUsuarioDetalle = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesXUsuarioDetalle.RolesXUsuarioDetalle>(dtsRetorno, "RolesXUsuarioDetalle");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteUsuariosXRolDetalle RecuperarReporteUsuariosXRolDetalle(dcRecuperarReporteUsuariosXRolDetalleIN datos)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRolesXUsuarios dtsRetorno;
            dcRecuperarReporteUsuariosXRolDetalle dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dtsRetorno = new Fachada.Seguridad.dtsRolesXUsuarios();
                dcRetorno = new dcRecuperarReporteUsuariosXRolDetalle();

                //Carga el DataSet con los datos de la lista.
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle.RolesXUsuarios>(datos.lstRolesXUsuarios, "RolesXUsuarios", dtsRetorno);
                ListToDataSet.ToDataSet<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle.UsuariosXRolDetalle>(datos.lstUsuariosXRolDetalle, "UsuariosXRolDetalle", dtsRetorno);


                dcRetorno.intRetorno = objConector.RecuperarReporteUsuariosXRolDetalle(datos.pIdRol, ref dtsRetorno);

                dcRetorno.lstRolesXUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle.RolesXUsuarios>(dtsRetorno, "RolesXUsuarios");
                dcRetorno.lstUsuariosXRolDetalle = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle.UsuariosXRolDetalle>(dtsRetorno, "UsuariosXRolDetalle");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteUsuariosXRol RecuperarReporteUsuariosXRol(Int32 pidRol)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRolesXUsuarios dtsRetorno;
            dcRecuperarReporteUsuariosXRol dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteUsuariosXRol();

                dtsRetorno = objConector.RecuperarReporteUsuariosXRol(pidRol);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstRolesXUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol.RolesXUsuarios>(dtsRetorno, "RolesXUsuarios");
                dcRetorno.lstSE_TAREAS_USUARIO = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol.SE_TAREAS_USUARIO>(dtsRetorno, "SE_TAREAS_USUARIO");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteUsuariosXArea RecuperarReporteUsuariosXArea(Int32 IdArea)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuariosXAreas dtsRetorno;
            dcRecuperarReporteUsuariosXArea dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteUsuariosXArea();

                dtsRetorno = objConector.RecuperarReporteUsuariosXArea(IdArea);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstUsuariosXAreas = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXArea.UsuariosXAreas>(dtsRetorno, "UsuariosXAreas");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteRolesComposicion RecuperarReporteRolesComposicion(Int32 pIdrol)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRoles dtsRetorno;
            dcRecuperarReporteRolesComposicion dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteRolesComposicion();

                dtsRetorno = objConector.RecuperarReporteRolesComposicion(pIdrol);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstRoles_Composicion = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesComposicion.Roles_Composicion>(dtsRetorno, "Roles_Composicion");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaReporteUsuariosXRoles RecuperarDatosParaReporteUsuariosXRoles()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsRoles dtsRetorno;
            dcRecuperarDatosParaReporteUsuariosXRoles dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaReporteUsuariosXRoles();

                dtsRetorno = objConector.RecuperarDatosParaReporteUsuariosXRoles();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_ROLES = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteUsuariosXRoles.SE_ROLES>(dtsRetorno, "SE_ROLES");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarDatosParaReporteRolesXUsuarios RecuperarDatosParaReporteRolesXUsuarios()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuarios dtsRetorno;
            dcRecuperarDatosParaReporteRolesXUsuarios dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarDatosParaReporteRolesXUsuarios();

                dtsRetorno = objConector.RecuperarDatosParaReporteRolesXUsuarios();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteRolesXUsuarios.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarTerminalesParaReporte RecuperarTerminalesParaReporte(int IDArea, string Habilitadas, bool TodasLasAreas)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsReportes dtsRetorno;
            dcRecuperarTerminalesParaReporte dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarTerminalesParaReporte();

                dtsRetorno = objConector.RecuperarTerminalesParaReporte(IDArea, Habilitadas, TodasLasAreas);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSE_TERMINALES = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTerminalesParaReporte.SE_TERMINALES>(dtsRetorno, "SE_TERMINALES");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteUsuario RecuperarReporteUsuario(Int32 pIdUsuario)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuarios dtsRetorno;
            dcRecuperarReporteUsuario dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteUsuario();

                dtsRetorno = objConector.RecuperarReporteUsuario(pIdUsuario);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstRolesXUsuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.RolesXUsuarios>(dtsRetorno, "RolesXUsuarios");
                dcRetorno.lstSE_Horarios_Usuario = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Horarios_Usuario>(dtsRetorno, "SE_Horarios_Usuario");
                dcRetorno.lstSE_TAREAS_USUARIO = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_TAREAS_USUARIO>(dtsRetorno, "SE_TAREAS_USUARIO");
                dcRetorno.lstSE_Term_Usuario = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Term_Usuario>(dtsRetorno, "SE_Term_Usuario");
                dcRetorno.lstSE_TERMINALES = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_TERMINALES>(dtsRetorno, "SE_TERMINALES");
                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteUsuarioSinAcceso RecuperarReporteUsuarioSinAcceso()
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsUsuarios dtsRetorno;
            dcRecuperarReporteUsuarioSinAcceso dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteUsuarioSinAcceso();

                dtsRetorno = objConector.RecuperarReporteUsuarioSinAcceso();
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuarioSinAcceso.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public dcRecuperarReporteControlInactividad RecuperarReporteControlInactividad(DateTime FechaUltimoUsoCta, string LapsoInactividad)
        {
            ConectorwsSeguridad objConector;
            Fachada.Seguridad.dtsCtrlInactividad dtsRetorno;
            dcRecuperarReporteControlInactividad dcRetorno;
            try
            {
                objConector = new ConectorwsSeguridad();
                dcRetorno = new dcRecuperarReporteControlInactividad();

                dtsRetorno = objConector.RecuperarReporteControlInactividad(FechaUltimoUsoCta, LapsoInactividad);
                //Retorn la lista correspondiente a la tabla.
                dcRetorno.lstSist_Usuarios = EntityLoader.Load<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad.Sist_Usuarios>(dtsRetorno, "Sist_Usuarios");

                return dcRetorno;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
