using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Runtime.Serialization.Json;
using COA.WebCipol.Presentacion.UIControlsHelper;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;
using COA.WebCipol.Fachada;
using COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados;
using COA.WebCipol.Presentacion.view;
using COA.Cipol.Presentacion._UIHelpers;
using System.DirectoryServices;
using COA.WebCipol.Presentacion.UIControlsHelper.DropDownList;
using COA.WebCipol.Comun;
using Microsoft.VisualBasic;
using COA.CifrarDatos;
using COA.WebCipol.Presentacion.UIControlsHelper.ListBox;
using COA.WebCipol.Presentacion.UIControlsHelper.TreeView;
using COA.WebCipol.Presentacion.Utiles;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.Interfaces;
using COA.WebCipol.Entidades.ClasesWs;
using EntidadesEmpresariales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria;


namespace COA.WebCipol.Presentacion.PageBuilders
{
    /// <summary>
    /// Summary description for wsAjaxwsSeguridad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class wsAjaxwsSeguridad : System.Web.Services.WebService
    {
        #region "Autorización de tareas"
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioCipol RecuperarElementoUsuarioCipolBase()
        {
            return new vUsuarioCipol();
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioCipol ValidarSupervisor(vUsuarioCipol obj)
        {
            try
            {
                FCipolSupervision objFachada = new FCipolSupervision();
                dcValidarSupervisor objDC = new dcValidarSupervisor();

                string valor = obj.usuario.Substring(obj.usuario.IndexOf("(") + 1, obj.usuario.IndexOf(")") - obj.usuario.IndexOf("(") - 1);

                objDC.usuario = valor;
                objDC.clave = obj.clave;

                obj.ResultadoEjecucion = objFachada.ValidarSupervisor(objDC);
                ManejoSesion.IdAutorizacion = 0;
                if (!obj.ResultadoEjecucion)
                    obj.MensajeError = "Clave incorrecta. Verifique.";
                return obj;
            }
            catch (Exception ex)
            {
                throw (ex);// throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioCipol ValidarSupervisorConAuditoria(vUsuarioCipol obj)
        {
            try
            {
                try
                {
                    FCipolSupervision objFachada = new FCipolSupervision();
                    dcValidarSupervisor objDC = new dcValidarSupervisor();

                    //objDC.idusuario = obj.idusuario;
                    objDC.clave = obj.clave;
                    //objDC.idtareasupervisor = obj.idtareasupervisor;
                    //objDC.terminal = obj.terminal;
                    objDC.usuario = obj.usuario;

                    obj.ResultadoEjecucion = objFachada.ValidarSupervisorConAuditoria(objDC);
                    if (!obj.ResultadoEjecucion)
                        obj.MensajeError = "Clave incorrecta. Verifique.";
                    return obj;
                }
                catch (Exception ex)
                {
                    throw (ex);// throw (ex);
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarSupervisores()
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                PadreCipolCliente objCipol = (PadreCipolCliente)ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente;
                System.Data.DataSet dtsRetorno;

                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");

                dtsRetorno = objCipol.RecuperarSupervisores((int)Session["IDTareaSupervisar"]);
                //cboSupervisores.DataSource = dtsSup;
                //cboSupervisores.DataTextField = "Nombre";
                //cboSupervisores.DataBind();

                List<Supervisores> lst = new List<Supervisores>();

                if (dtsRetorno != null && dtsRetorno.Tables.Contains("Supervisores") && dtsRetorno.Tables["Supervisores"].Rows.Count > 0)
                    for (int i = 0; i < dtsRetorno.Tables["Supervisores"].Rows.Count; i++)
                        lst.Add(new Supervisores() { Nombre = dtsRetorno.Tables["Supervisores"].Rows[i]["Nombre"].ToString() });

                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = lst,
                    DataTextField = "Nombre",
                    DataValueField = "Nombre",
                    //Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(300),
                    Id = "cboSupervisores"
                };
                ctl.Attributes.Add("class", "col-md-8");
                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }

        }
        #endregion

        #region "Sistemas Habilitados"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados.SE_SIST_HABILITADOS> RecuperarSistemasHabilitados()
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                dcRecuperarSistemasHabilitados objResp = objFachada.RecuperarSistemasHabilitados();

                //Remover el idsistema 1.
                objResp.lstdtsSistHabilitadosSE_SIST_HABILITADOS.Remove((from item in objResp.lstdtsSistHabilitadosSE_SIST_HABILITADOS
                                                                         where item.IDSISTEMA == 1
                                                                         select item).ElementAt(0));

                return objResp.lstdtsSistHabilitadosSE_SIST_HABILITADOS;
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string EliminarSistemaHabilitado(int Id)
        {
            try
            {

                FSeguridad objFachada = new FSeguridad();
                dcEliminarSistemaHabilitado objDC = new dcEliminarSistemaHabilitado();
                COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado.SE_SIST_HABILITADOS objSistema = new COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado.SE_SIST_HABILITADOS();
                objSistema.IDSISTEMA = Id;
                //HAY QUE AGREGAR ESTOS DATOS PORQUE EL CONVERSOR A DATA SET NO ADMITE NULOS EN COLUMNA QUE NO LO SEAN
                objSistema.CODSISTEMA = "";
                objSistema.DESCSISTEMA = "";

                objDC.lstSE_SIST_HABILITADOS = new List<COA.WebCipol.Entidades.ClasesWs.dcEliminarSistemaHabilitado.SE_SIST_HABILITADOS>();
                objDC.lstSE_SIST_HABILITADOS.Add(objSistema);
                if (objFachada.EliminarSistemaHabilitado(objDC) == -1)
                {
                    return "No se puede eliminar el sistema porque tiene tareas primitivas asociadas";
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSistemas RecuperarElementoSistema(decimal Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados.dcRecuperarSistemasHabilitados objResp = objFachada.RecuperarSistemasHabilitados();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados.SE_SIST_HABILITADOS obj = null;


                var query = from item in objResp.lstdtsSistHabilitadosSE_SIST_HABILITADOS
                            where item.IDSISTEMA == Id
                            select item;

                if (query != null && query.Count() > 0)
                    obj = query.ElementAt(0);

                if (obj != null)
                {
                    return new vSistemas()
                    {
                        IDSISTEMA = obj.IDSISTEMA,
                        NOMBREEXEC = obj.NOMBREEXEC,
                        PAGINAPORDEFECTO = obj.PAGINAPORDEFECTO,
                        SISTEMAHABILITADO = obj.SISTEMAHABILITADO,
                        CODSISTEMA = obj.CODSISTEMA,
                        DESCSISTEMA = obj.DESCSISTEMA,
                        ICONO = obj.ICONO
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSistemas RecuperarElementoSistemaBase()
        {
            try
            {
                return new vSistemas();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string ActualizarSistemaHabilitado(vSistemas obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                if (!obj.update)
                {
                    //Validaciones.
                    if (!(obj.IDSISTEMA > 0))
                        return "El <strong>ID SISTEMA</strong> es un dato obligatorio.";
                    if ((obj.IDSISTEMA.Equals(1)))
                        return "El <strong>ID SISTEMA</strong> no puede ser utilizado.";
                    if (obj.IDSISTEMA > byte.MaxValue)
                        return "El <strong>ID SISTEMA</strong> no puede ser mayor a tres digitos.";

                    //Valida la existencia del IDSISTEMA.

                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados.dcRecuperarSistemasHabilitados objResp = objFachada.RecuperarSistemasHabilitados();

                    var query = from item in objResp.lstdtsSistHabilitadosSE_SIST_HABILITADOS
                                where item.IDSISTEMA == obj.IDSISTEMA
                                select item;

                    if (query != null && query.Count() > 0)
                        return "El <strong>ID SISTEMA</strong> ya existe.";

                }
                if (string.IsNullOrEmpty(obj.CODSISTEMA.Trim()))
                    return "El <strong>CÓDIGO SISTEMA</strong> es un dato obligatorio.";
                if (string.IsNullOrEmpty(obj.DESCSISTEMA.Trim()))
                    return "La <strong>DESCRIPCIÓN</strong> es un dato obligatorio.";
                if (Utiles.TieneCaracterInvalido(obj.NOMBREEXEC))
                    return "El <strong>NOMBRE DEL EXE</strong> contiene caracteres inválidos.";

                if (obj.update)
                {
                    COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado.dcActualizarSistemaHabilitado objDC = new COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado.dcActualizarSistemaHabilitado();
                    COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado.SE_SIST_HABILITADOS objSistema = new COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado.SE_SIST_HABILITADOS();

                    objSistema = new Entidades.ClasesWs.dcActualizarSistemaHabilitado.SE_SIST_HABILITADOS()
                    {
                        IDSISTEMA = obj.IDSISTEMA,
                        NOMBREEXEC = obj.NOMBREEXEC.Trim(),
                        PAGINAPORDEFECTO = obj.PAGINAPORDEFECTO.Trim(),
                        SISTEMAHABILITADO = obj.SISTEMAHABILITADO.Trim(),
                        CODSISTEMA = obj.CODSISTEMA.Trim(),
                        DESCSISTEMA = obj.DESCSISTEMA.Trim(),
                        ICONO = obj.ICONO.Trim()
                    };

                    objDC.lstSE_SIST_HABILITADOS = new List<COA.WebCipol.Entidades.ClasesWs.dcActualizarSistemaHabilitado.SE_SIST_HABILITADOS>();
                    objDC.lstSE_SIST_HABILITADOS.Add(objSistema);

                    if (objFachada.ActualizarSistemaHabilitado(objDC) == -1)
                    {
                        return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador.";
                    }
                }
                else
                {
                    COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado.dcInsertarSistemaHabilitado objDC = new COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado.dcInsertarSistemaHabilitado();
                    COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado.SE_SIST_HABILITADOS objSistema = new COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado.SE_SIST_HABILITADOS();

                    objSistema = new Entidades.ClasesWs.dcInsertarSistemaHabilitado.SE_SIST_HABILITADOS()
                    {
                        IDSISTEMA = obj.IDSISTEMA,
                        NOMBREEXEC = obj.NOMBREEXEC.Trim(),
                        PAGINAPORDEFECTO = obj.PAGINAPORDEFECTO.Trim(),
                        SISTEMAHABILITADO = obj.SISTEMAHABILITADO.Trim(),
                        CODSISTEMA = obj.CODSISTEMA.Trim(),
                        DESCSISTEMA = obj.DESCSISTEMA.Trim(),
                        ICONO = obj.ICONO.Trim()
                    };

                    objDC.lstSE_SIST_HABILITADOS = new List<COA.WebCipol.Entidades.ClasesWs.dcInsertarSistemaHabilitado.SE_SIST_HABILITADOS>();
                    objDC.lstSE_SIST_HABILITADOS.Add(objSistema);

                    if (objFachada.InsertarSistemaHabilitado(objDC) == -1)
                    {
                        return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador.";
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region "Terminales"
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarCboAreas()
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboAreas objUI = new UIcboAreas();

                UIdgcboAreas ctl = (UIdgcboAreas)objUI.LoadControl("UIControlsHelper/DropDownList/UIdgcboAreas.ascx");
                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtro"></param>
        /// <param name="area"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.SE_TERMINALES> RecuperarDatosParaABMTerminales(vTerminalFiltro obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.dcRecuperarDatosParaABMTerminales objResp = objFachada.RecuperarDatosParaABMTerminales();

                //martinv - se deció filtar por código de terminal.
                var query = from item in objResp.lstSE_teminales
                            where (string.IsNullOrEmpty(obj.filtro.Trim()) || (item.CODTERMINAL != null && item.CODTERMINAL.ToUpper().StartsWith(obj.filtro.Trim().ToUpper())))
                            && (string.IsNullOrEmpty(obj.area.Trim()) || (item.NOMBREAREA != null && item.NOMBREAREA.ToUpper().StartsWith(obj.area.Trim().ToUpper())))
                            select item;

                return query.ToList();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string EliminarTerminal(short Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                if (!objFachada.EliminarTerminal(Id))
                {
                    return "No se ha podido eliminar la terminal ocurrió en error inesperado. Consulte al administrador";
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vterminal RecuperarElementoTerminal(int Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.dcRecuperarDatosParaABMTerminales objResp = objFachada.RecuperarDatosParaABMTerminales();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.SE_TERMINALES obj = null;


                var query = from item in objResp.lstSE_teminales
                            where item.IDTERMINAL == Id
                            select item;

                if (query != null && query.Count() > 0)
                    obj = query.ElementAt(0);

                if (obj != null)
                {
                    return new vterminal()
                    {
                        IDAREA = obj.IDAREA,
                        IDTERMINAL = obj.IDTERMINAL,
                        CANTMEMORIARAM = obj.CANTMEMORIARAM,
                        CODTERMINAL = obj.CODTERMINAL.Trim(),
                        DESCADICIONAL = !string.IsNullOrEmpty(obj.DESCADICIONAL) ? obj.DESCADICIONAL.Trim() : "",
                        MODELOACELVIDEO = !string.IsNullOrEmpty(obj.MODELOACELVIDEO) ? obj.MODELOACELVIDEO.Trim() : "",
                        MODELOMONITOR = !string.IsNullOrEmpty(obj.MODELOMONITOR) ? obj.MODELOMONITOR.Trim() : "",
                        MODELOPROCESADOR = !string.IsNullOrEmpty(obj.MODELOPROCESADOR) ? obj.MODELOPROCESADOR.Trim() : "",
                        NOMBREAREA = !string.IsNullOrEmpty(obj.NOMBREAREA) ? obj.NOMBREAREA.Trim() : "",
                        NOMBRECOMPUTADORA = !string.IsNullOrEmpty(obj.NOMBRECOMPUTADORA) ? obj.NOMBRECOMPUTADORA.Trim() : "",
                        //NombreNetBios = obj.NombreNetBios,
                        ORIGENACTUALIZACION = !string.IsNullOrEmpty(obj.ORIGENACTUALIZACION) ? obj.ORIGENACTUALIZACION.Trim() : "",
                        TAMANIODISCO = obj.TAMANIODISCO,
                        USOHABILITADO = obj.USOHABILITADO.Trim()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vterminal RecuperarElementoTerminalBase()
        {
            try
            {
                return new vterminal();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AdministrarTerminales(vterminal obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();


                //Validaciones.
                if ((string.IsNullOrEmpty(obj.CODTERMINAL.Trim())))
                    return "El <strong>CÓDIGO DE TERMINAL</strong> es un dato obligatorio.";

                if (string.IsNullOrEmpty(obj.ORIGENACTUALIZACION))
                    return "El <strong>MODO DE ACTUALIZACIÓN</strong> es un dato obligatorio";

                //Valida la existencia del IDSISTEMA.
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales.dcRecuperarDatosParaABMTerminales objResp = objFachada.RecuperarDatosParaABMTerminales();

                var query = from item in objResp.lstSE_teminales
                            where item.CODTERMINAL.Trim() == obj.CODTERMINAL.Trim() && item.IDTERMINAL != obj.IDTERMINAL
                            select item;

                if (query != null && query.Count() > 0)
                    return "El <strong>CÓDIGO DE TERMINAL</strong> ya existe.";


                if ((string.IsNullOrEmpty(obj.NOMBRECOMPUTADORA.Trim())))
                    return "El <strong>NOMBRE NETBIOS</strong> es un dato obligatorio.";

                //Valida el nombre del netbios.
                string rta = Utiles.VerifNombreNETBIOS(obj.NOMBRECOMPUTADORA.Trim());
                if (string.IsNullOrEmpty(rta))
                {
                    var queryaux = from item in objResp.lstSE_teminales
                                   where item.NOMBRECOMPUTADORA.Trim() == obj.NOMBRECOMPUTADORA.Trim() && item.IDTERMINAL != obj.IDTERMINAL
                                   select item;

                    if (queryaux != null && queryaux.Count() > 0)
                        return "El <strong>NOMBRE NETBIOS</strong> ya existe.";
                }
                else
                    return rta;

                //[AngelL] 20/02/2005 - Verificacion de la pc contra el dominio
                //si se usa seguridad integrada.
                //Una vez seguros que la información sobre el NombreNetBios fué
                //cargada, ActiveDirectory validará que el nombre exista en su
                //base si la seguridad es integrada.


                string strPath = null;
                string[] strdom = null;
                Int32 inti = default(Int32);
                DirectoryEntry objIngreso = default(DirectoryEntry);
                DirectorySearcher objBuscar = default(DirectorySearcher);
                SearchResult objResultado = default(SearchResult);
                //si el dominio no es nulo, o sea, si se esta usando
                //seguridad integrada al dominio, se verifica 
                //la pc contra el servicio de directorio usando la sintaxis LDAP
                if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio)
                {
                    try
                    {
                        strPath = "LDAP://";
                        strdom = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio.Split('.');
                        for (inti = 0; inti <= strdom.GetUpperBound(0); inti++)
                        {
                            strPath += "DC=";
                            strPath += strdom[inti];
                            strPath += ",";
                        }
                        strPath = strPath.Substring(0, strPath.Length - 1);
                        objIngreso = new DirectoryEntry(strPath);

                        //construido el path, se agrega el filtro al buscador de
                        //directorio
                        objBuscar = new DirectorySearcher(objIngreso);
                        objBuscar.Filter = "(&(objectClass=computer)(cn=" + obj.NOMBRECOMPUTADORA.Trim() + "))";
                        objResultado = objBuscar.FindOne();
                    }
                    catch (Exception ex)
                    {
                        objResultado = null;
                        return ex.Message;
                        //lblMSJAltaModif.Text = ex.StackTrace;
                    }

                    //si no se obtuvieron resultados, se advierte
                    if (objResultado == null)
                    {
                        return "La terminal indicada no pertenece al Dominio " + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio + " o en este momento no se puede establecer conexión con el Dominio. Verifique. Terminal no encontrada.";
                    }
                }

                if (!(obj.IDAREA >= 0))
                    return "El <strong>ÁREA DE LA TERMINAL</strong> es un datos obligatorio.";

                COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.dcAdministrarTerminales objDC = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.dcAdministrarTerminales();
                COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES objAux = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES();

                objAux = new Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES()
                {
                    CANTMEMORIARAM = obj.CANTMEMORIARAM,
                    CODTERMINAL = obj.CODTERMINAL.Trim(),
                    DESCADICIONAL = obj.DESCADICIONAL.Trim(),
                    IDAREA = obj.IDAREA,
                    IDTERMINAL = (obj.update) ? obj.IDTERMINAL : -1,
                    MODELOACELVIDEO = obj.MODELOACELVIDEO,
                    MODELOMONITOR = obj.MODELOMONITOR,
                    MODELOPROCESADOR = obj.MODELOPROCESADOR,
                    NOMBREAREA = obj.NOMBREAREA,
                    NOMBRECOMPUTADORA = obj.NOMBRECOMPUTADORA.Trim(),
                    ORIGENACTUALIZACION = obj.ORIGENACTUALIZACION,
                    TAMANIODISCO = obj.TAMANIODISCO,
                    USOHABILITADO = obj.USOHABILITADO
                };

                objDC.lstSE_TERMINALES = new List<COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES>();
                objDC.lstSE_TERMINALES.Add(objAux);

                if (objFachada.AdministrarTerminales(objDC) == -1)
                {
                    return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador";
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region "Areas"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <param name="baja"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas.SIST_KAREAS> RecuperarAreas(vAreaFiltro obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas.dcRecuperarAreas objResp = objFachada.RecuperarAreas();

                //martinv - se deció filtar por nombre de área y estado baja.
                var query = from item in objResp.lstKAreas
                            where (string.IsNullOrEmpty(obj.area.Trim()) || (item.NOMBREAREA != null && item.NOMBREAREA.Trim().ToUpper().StartsWith(obj.area.Trim().ToUpper())))
                            && (string.IsNullOrEmpty(obj.baja.Trim()) || (item.BAJA != null && item.BAJA.ToUpper().Equals(obj.baja.Trim().ToUpper())))
                            select item;

                //Ordenamiento
                //[MiguelP]         22/10/2014      Cambio GCP - Se modifica el ordenamiento 
                //return query.ToList().OrderBy(v => v.NOMBREAREA).ToList();
                return query.ToList().OrderBy(v => v.IDAREA).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);// throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vArea RecuperarElementoArea(int Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas.dcRecuperarAreas objResp = objFachada.RecuperarAreas();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas.SIST_KAREAS obj = null;


                var query = from item in objResp.lstKAreas
                            where item.IDAREA == Id
                            select item;

                if (query != null && query.Count() > 0)
                    obj = query.ElementAt(0);

                if (obj != null)
                {
                    return new vArea()
                    {
                        IDAREA = obj.IDAREA,
                        CARGORESPONSABLE = obj.CARGORESPONSABLE.Trim(),
                        COMENTARIOS = obj.COMENTARIOS.Trim(),
                        FICTICIA = obj.FICTICIA.Trim(),
                        NOMBREAREA = obj.NOMBREAREA.Trim(),
                        RESPONSABLE = obj.RESPONSABLE.Trim(),
                        BAJA = obj.BAJA.Trim()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vAreaElemntoBase RecuperarElementoAreaBase()
        {
            try
            {
                return new vAreaElemntoBase();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string EliminarArea(short Id)
        {
            try
            {

                FSeguridad objFachada = new FSeguridad();
                if (objFachada.EliminarArea(Id) < 0)
                {
                    return "Ha ocurrido un error intentando guardar, por favor comuníquese con un adm. del sistema";
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string ActivarArea(short Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                if (objFachada.AgregarArea(Id) < 0)
                {
                    return "Ha ocurrido un error intentando guardar, por favor comuníquese con un adm. del sistema";
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AdministrarArea(vArea obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                if (!string.IsNullOrEmpty(obj.BAJA) && obj.BAJA.Trim().ToUpper().Equals("S"))
                    return "No se puede modificar este registro porque está dado de baja";
                //Validaciones.
                if (string.IsNullOrEmpty(obj.NOMBREAREA.Trim()))
                    return "El <strong>ÁREA</strong> es un dato obligatorio";

                //Valida la existencia del nombrearea.
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas.dcRecuperarAreas objResp = objFachada.RecuperarAreas();

                var query = from item in objResp.lstKAreas
                            where item.NOMBREAREA.Trim().ToUpper() == obj.NOMBREAREA.Trim().ToUpper() && item.IDAREA != obj.IDAREA
                            select item;

                if (query != null && query.Count() > 0)
                    return "El nombre de area ya existe";

                if (obj.update)
                {
                    COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.dcActualizarArea objDC = new COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.dcActualizarArea();
                    COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.SIST_KAREAS objAux = new COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.SIST_KAREAS();

                    objAux = new COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.SIST_KAREAS()
                    {
                        CARGORESPONSABLE = obj.CARGORESPONSABLE.Trim(),
                        COMENTARIOS = obj.COMENTARIOS.Trim(),
                        FICTICIA = obj.FICTICIA.Trim(),
                        IDAREA = obj.IDAREA,
                        NOMBREAREA = obj.NOMBREAREA.Trim(),
                        RESPONSABLE = obj.RESPONSABLE.Trim(),
                        BAJA = obj.BAJA.Trim()
                    };

                    objDC.lstKAreas = new List<COA.WebCipol.Entidades.ClasesWs.dcActualizarArea.SIST_KAREAS>();
                    objDC.lstKAreas.Add(objAux);

                    if (objFachada.ActualizarArea(objDC) < 0)
                    {
                        return "Ha ocurrido un error intentando guardar, por favor comuníquese con un adm. del sistema";
                    }
                }
                else
                {
                    COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.dcAltaDeAreas objDC = new COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.dcAltaDeAreas();
                    COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.SIST_KAREAS objAux = new COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.SIST_KAREAS();

                    objAux = new COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.SIST_KAREAS()
                    {
                        CARGORESPONSABLE = obj.CARGORESPONSABLE.Trim(),
                        COMENTARIOS = obj.COMENTARIOS.Trim(),
                        FICTICIA = obj.FICTICIA.Trim(),
                        //IDAREA = obj.IDAREA,
                        BAJA = "N",
                        NOMBREAREA = obj.NOMBREAREA.Trim(),
                        RESPONSABLE = obj.RESPONSABLE.Trim()
                    };

                    objDC.lstKAreas = new List<COA.WebCipol.Entidades.ClasesWs.dcAltaDeAreas.SIST_KAREAS>();
                    objDC.lstKAreas.Add(objAux);

                    if (objFachada.AltaDeAreas(objDC) < 0)
                    {
                        return "Ha ocurrido un error intentando guardar, por favor comuníquese con un adm. del sistema";
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region "Tareas primitivas"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IdControl"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarCboSistema(string IdControl)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.dcRecuperarTareasPrimitivas objResp = objFachada.RecuperarTareasPrimitivas();

                RecuperarControles rc = new RecuperarControles();
                return rc.GenerarCombos(new DatosCboGenerico() { DataSource = objResp.lstSE_SIST_HABILITADOS, DataTextField = "DESCSISTEMA", DataValueField = "IDSISTEMA", Id = IdControl, itemTodos = new System.Web.UI.WebControls.ListItem() { Value = "0", Text = "(Ninguno)" } });
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filtroCODIGOTAREA"></param>
        /// <param name="filtroDESCRIPCIONTAREA"></param>
        /// <param name="filtroIDAUTORIZACION"></param>
        /// <param name="filtroIDTAREA"></param>
        /// <param name="filtroSistema"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<Entidades.ClasesWs.dcRecuperarTareasPrimitivas.TAREAS> RecuperarTareasPrimitivas(vTareasPrimitivasFiltros obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                TareasPrimitivas objRetorno = new TareasPrimitivas();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.dcRecuperarTareasPrimitivas objResp = objFachada.RecuperarTareasPrimitivas();

                //martinv - se deció filtar por nombre de área y estado baja.
                var query = from item in objResp.lstTAREAS
                            where (obj.filtroSistema == 0 || item.IDSISTEMA == obj.filtroSistema)
                            && (string.IsNullOrEmpty(obj.filtroDESCRIPCIONTAREA.Trim()) || (item.DESCRIPCIONTAREA != null && item.DESCRIPCIONTAREA.ToUpper().Contains(obj.filtroDESCRIPCIONTAREA.Trim().ToUpper())))
                            && (string.IsNullOrEmpty(obj.filtroCODIGOTAREA.Trim()) || (item.CODIGOTAREA != null && item.CODIGOTAREA.ToUpper().StartsWith(obj.filtroCODIGOTAREA.Trim().ToUpper())))
                            && (obj.filtroIDTAREA == 0 || item.IDTAREA == obj.filtroIDTAREA)
                            && (string.IsNullOrEmpty(obj.filtroIDAUTORIZACION.Trim()) || (item.IDAUTORIZACION != null && (obj.filtroIDAUTORIZACION.Trim().Equals("S")) ? item.IDAUTORIZACION == 1 : item.IDAUTORIZACION == 0))
                            select item;

                return query.ToList();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string EliminarTareaPrimitiva(int Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcEliminarTareaPrimitiva.dcEliminarTareaPrimitiva obj = new Entidades.ClasesWs.dcEliminarTareaPrimitiva.dcEliminarTareaPrimitiva();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.dcRecuperarTareasPrimitivas objResp = objFachada.RecuperarTareasPrimitivas();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.TAREAS objTarea = null;


                var query = from item in objResp.lstTAREAS
                            where item.IDTAREA == Id
                            select item;

                if (query != null && query.Count() > 0)
                    objTarea = query.ElementAt(0);
                else
                    return "No ha seleccionado la tarea a eliminar. Verifique";

                if (objTarea.IDAUTORIZACION != null && objTarea.IDAUTORIZACION > 0)
                    return "La tarea primitiva posee una o mas tareas autorizantes. Imposible continuar";

                if (objTarea.IDAUTORIZACION == null)
                    return "Esta es una tarea autorizante. Imposible continuar";

                Entidades.ClasesWs.dcEliminarTareaPrimitiva.TAREAS objAux = new Entidades.ClasesWs.dcEliminarTareaPrimitiva.TAREAS()
                {
                    IDTAREA = Id,
                    IDSISTEMA = objTarea.IDSISTEMA,
                    CODIGOTAREA = objTarea.CODIGOTAREA,
                    DESCRIPCIONTAREA = objTarea.CODIGOTAREA,
                    DESCSISTEMA = objTarea.DESCSISTEMA,
                };

                if (objTarea.IDGRUPO != null)
                    objAux.IDGRUPO = objTarea.IDGRUPO;

                if (objTarea.IDAUTORIZACION != null)
                    objAux.IDAUTORIZACION = objTarea.IDAUTORIZACION;

                objAux.REQUIEREAUDITORIA = objTarea.REQUIEREAUDITORIA;
                //Eliminación.
                obj.lstTAREAS.Add(objAux);

                int retorno = objFachada.EliminarTareaPrimitiva(obj);
                if (retorno == -1)
                    return "No se puede eliminar la tarea, posee roles asociados";
                if (retorno == -2)
                    return "No se puede eliminar la tarea, posee usuarios asociados";
                if (retorno <= 0)
                    return "No se pudieron guardar los datos del sistema habilitado";

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vTareaPrimitiva RecuperarElementoTareaPrimitiva(int Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.dcRecuperarTareasPrimitivas objResp = objFachada.RecuperarTareasPrimitivas();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.TAREAS obj = null;


                var query = from item in objResp.lstTAREAS
                            where item.IDTAREA == Id
                            select item;

                if (query != null && query.Count() > 0)
                    obj = query.ElementAt(0);

                if (obj != null)
                {
                    return new vTareaPrimitiva()
                    {
                        CODIGOTAREA = obj.CODIGOTAREA.Trim(),
                        DESCRIPCIONTAREA = obj.DESCRIPCIONTAREA.Trim(),
                        DESCSISTEMA = obj.DESCSISTEMA.Trim(),
                        IDSISTEMA = obj.IDSISTEMA,
                        IDTAREA = obj.IDTAREA,
                        REQUIEREAUDITORIA = obj.REQUIEREAUDITORIA.Trim()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vTareaPrimitiva RecuperarElementoTareaPrimitivaBase()
        {
            try
            {
                return new vTareaPrimitiva();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AdministrarTareasPrimitivas(vTareaPrimitiva obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();


                if (obj.IDTAREA <= 0)
                    return "El <strong>ID TAREA</strong> es un dato obligatorio";

                if (!obj.update)
                {
                    int intretorno = objFachada.VerificarExistenciaIDTarea(obj.IDTAREA);

                    if (intretorno < 0)
                        return "No se pudo verificar la existencia de la Tarea";

                    if (intretorno > 0)
                        return "El Id Tarea: <strong>\"" + obj.IDTAREA.ToString() + "\"</strong> ya existe. Seleccione otro.";
                }

                if (string.IsNullOrEmpty(obj.CODIGOTAREA.Trim()))
                    return "El <strong>CÓDIGO</strong> es un dato obligatorio";

                if (string.IsNullOrEmpty(obj.DESCRIPCIONTAREA.Trim()))
                    return "El <strong>NOMBRE DE TAREA</strong> es un dato obligatorio";

                if (obj.IDSISTEMA <= 0)
                    return "El <strong>SISTEMA</strong> es un dato obligatorio";

                if (obj.update)
                {
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.dcRecuperarTareasPrimitivas objResp = objFachada.RecuperarTareasPrimitivas();
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPrimitivas.TAREAS objTarea = null;


                    var query = from item in objResp.lstTAREAS
                                where item.IDTAREA == obj.IDTAREA
                                select item;

                    if (query != null && query.Count() > 0)
                        objTarea = query.ElementAt(0);
                    else
                        return "No ha seleccionado la tarea. Verifique";


                    COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva.dcActualizarTareaPrimitiva objDC = new COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva.dcActualizarTareaPrimitiva();
                    COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva.TAREAS objAux;

                    objAux = new COA.WebCipol.Entidades.ClasesWs.dcActualizarTareaPrimitiva.TAREAS()
                    {
                        CODIGOTAREA = obj.CODIGOTAREA.Trim(),
                        DESCRIPCIONTAREA = obj.DESCRIPCIONTAREA.Trim(),
                        IDSISTEMA = obj.IDSISTEMA,
                        IDTAREA = obj.IDTAREA,
                        REQUIEREAUDITORIA = obj.REQUIEREAUDITORIA.Trim()
                    };

                    if (objTarea.IDGRUPO != null)
                        objAux.IDGRUPO = objTarea.IDGRUPO;

                    if (objTarea.IDAUTORIZACION != null)
                        objAux.IDAUTORIZACION = objTarea.IDAUTORIZACION;

                    objDC.lstTAREAS = new List<Entidades.ClasesWs.dcActualizarTareaPrimitiva.TAREAS>();
                    objDC.lstTAREAS.Add(objAux);

                    if (objFachada.ActualizarTareaPrimitiva(objDC) == -1)
                    {
                        return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador";
                    }
                    return "";
                }
                else
                {
                    COA.WebCipol.Entidades.ClasesWs.dcInsertarTareaPrimitiva.dcInsertarTareaPrimitiva objDC = new COA.WebCipol.Entidades.ClasesWs.dcInsertarTareaPrimitiva.dcInsertarTareaPrimitiva();
                    COA.WebCipol.Entidades.ClasesWs.dcInsertarTareaPrimitiva.TAREAS objAux;

                    TresDES objEncriptarNET;
                    objEncriptarNET = new TresDES();
                    objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
                    objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;

                    objAux = new Entidades.ClasesWs.dcInsertarTareaPrimitiva.TAREAS()
                    {
                        CODIGOTAREA = obj.CODIGOTAREA.Trim(),
                        DESCRIPCIONTAREA = obj.DESCRIPCIONTAREA.Trim(),
                        IDSISTEMA = obj.IDSISTEMA,
                        IDTAREA = obj.IDTAREA,
                        REQUIEREAUDITORIA = objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "N")
                    };

                    objDC.lstTAREAS = new List<Entidades.ClasesWs.dcInsertarTareaPrimitiva.TAREAS>();
                    objDC.lstTAREAS.Add(objAux);

                    if (objFachada.InsertarTareaPrimitiva(objDC) == -1)
                    {
                        return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador";
                    }
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string VerificarTareasPrimitiva(string IdTarea)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                int intTarea;
                if (string.IsNullOrEmpty(IdTarea))
                    return "Debe ingresar un <strong>ID TAREA</strong>. Verifique";
                if (!int.TryParse(IdTarea, out intTarea))
                    return "El <strong>ID TAREA</strong> es incorrecto. Verifique";
                //Verifica la existencia del id de tarea.
                int retorno = objFachada.VerificarExistenciaIDTarea(intTarea);

                if (retorno < 0)
                    return "No se pudo verificar la existencia de la Tarea";
                if (retorno > 0)
                    return "La tarea con el ID <strong>\"" + intTarea.ToString() + "\"</strong> ya existe. Seleccione otro.";

                return "El Id Tarea <strong>\"" + intTarea.ToString() + "\"</strong> esta disponible";

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        #endregion

        #region "Tareas Autorizantes"
        //[WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        //public string RecuperarListBoxTareaPrimitivasTareaAutorizante(short Id)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIListBoxTareaPrimitiva objUI = new UIListBoxTareaPrimitiva();
        //        UIListBoxTareaPrimitivaDG ctl = (UIListBoxTareaPrimitivaDG)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxTareaPrimitivaDG.ascx");

        //        objUI.IdSistema = Id;

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarListBoxTareaPrimitivasTareaAutorizante(short Id)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                Fachada.FSeguridad Fachada = new FSeguridad();


                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");

                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasSinAutorizacion.dcRecuperarTareasSinAutorizacion objResp = objFachada.RecuperarTareasSinAutorizacion(Id);

                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = objResp.lstSE_TAREAS,
                    DataTextField = "DESCRIPCIONTAREA",
                    DataValueField = "IDTAREA",
                    //Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(279),
                    Id = "lbTareasPrimitivas"
                };
                //ctl.Attributes.Add("class", "col-xs-5");
                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }


        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarCboSistemaTareaAutorizante()
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                Fachada.FSeguridad Fachada = new FSeguridad();


                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");

                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = Fachada.RecuperarDatosParaABMTareasAutorizantes().lstSE_SIST_HABILITADOS,
                    DataTextField = "DESCSISTEMA",
                    DataValueField = "IDSISTEMA",
                    //Height = new System.Web.UI.WebControls.Unit(22),
                    //Widht = new System.Web.UI.WebControls.Unit(300),
                    Id = "cboSistema"
                };
                ctl.Attributes.Add("class", "col-md-8");
                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.TAREAS> RecuperarTareasAutorizantes()
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.dcRecuperarDatosParaABMTareasAutorizantes objResp = objFachada.RecuperarDatosParaABMTareasAutorizantes();

                //Se quitan todas las tareas autorizantes cuyos sistema no estan habilitados.Definido por JulianP,Martinv 
                var query = from item in objResp.lstTAREAS
                            where (from o in objResp.lstSE_SIST_HABILITADOS
                                   select o.IDSISTEMA).Contains(item.IDSISTEMA)
                            select item;

                return query.ToList().OrderBy(v => v.IDSISTEMA).ToList();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string EliminarTareaAutorizante(int Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcEliminarTareaPrimitiva.dcEliminarTareaPrimitiva obj = new Entidades.ClasesWs.dcEliminarTareaPrimitiva.dcEliminarTareaPrimitiva();

                int retorno = objFachada.VerificaTareasAutorizantesEnUso(Id);

                switch (retorno)
                {
                    case 1:
                        return "No se puede eliminar la tarea por estar asociada a uno o más Roles";
                    case 2:
                        return "No se puede eliminar la tarea por estar asociada a uno o más usuarios";
                    case 3:
                        return "No se puede eliminar la tarea por estar asociada a uno o más Roles y uno o más usuarios";
                }

                //Eliminación de la tarea autorizante.
                if (objFachada.EliminarTareasAutorizantes(Id) == 0)
                    return "Ha ocurrido un error al intentar realizar la operación, por favor vuelva a intentar.";

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vTareaAutorizante RecuperarElementoTareaAutorizante(int Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.dcRecuperarDatosParaABMTareasAutorizantes objResp = objFachada.RecuperarDatosParaABMTareasAutorizantes();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.TAREAS obj = null;


                var query = from item in objResp.lstTAREAS
                            where item.IDTAREA == Id
                            select item;

                if (query != null && query.Count() > 0)
                    obj = query.ElementAt(0);




                if (obj != null)
                {
                    COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasQueRequierenAutorizacion.dcRecuperarTareasQueRequierenAutorizacion objResp2 = objFachada.RecuperarTareasQueRequierenAutorizacion(obj.IDTAREA);

                    vTareaAutorizante objRetorno = new vTareaAutorizante()
                    {
                        IDTAREA = obj.IDTAREA,
                        CODIGOTAREA = obj.CODIGOTAREA,
                        IDSISTEMA = obj.IDSISTEMA,
                        REQUIEREAUDITORIA = obj.REQUIEREAUDITORIA,
                        DESCRIPCIONTAREA = obj.DESCRIPCIONTAREA,
                        IDTAREAAUTORIZANTE = objResp2.lstSE_TAREASAUTORIZADAS[0].IDTAREA,
                        DESCIDTAREAAUTORIZANTE = objResp2.lstSE_TAREASAUTORIZADAS[0].DESCRIPCIONTAREA
                    };
                    return objRetorno;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vTareaAutorizante RecuperarElementoTareaAutorizanteBase()
        {
            try
            {
                return new vTareaAutorizante();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AdministrarTareasAutorizante(vTareaAutorizante obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();


                if (string.IsNullOrEmpty(obj.CODIGOTAREA))
                    return "El <strong>CÓDIGO DE TAREA</strong> es un dato obligatorio.";

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.dcRecuperarDatosParaABMTareasAutorizantes objResp = objFachada.RecuperarDatosParaABMTareasAutorizantes();

                var query = from item in objResp.lstTAREAS
                            where item.IDTAREA != obj.IDTAREA && item.CODIGOTAREA.Trim().ToUpper() == obj.CODIGOTAREA.Trim().ToUpper()
                            select item;

                if (query != null && query.Count() > 0)
                    return "El <strong>CÓDIGO DE TAREA</strong> ya existe, imposible continuar.";



                if (string.IsNullOrEmpty(obj.DESCRIPCIONTAREA.Trim()))
                    return "la <strong>DESCRIPCIÓN</strong> de la tarea es un dato obligatorio.";

                if (obj.IDSISTEMA < 0)
                    return "El <strong>SISTEMA</strong> es un dato obligatorio.";

                //todo: Tareas autorizadas.
                //"Debe seleccionar una tarea Primitiva."
                if (!(obj.IDTAREAAUTORIZANTE > 0))
                    return "Debe seleccionar una <strong>TAREA PRIMITIVA</strong>.";

                if (obj.update)
                {
                    //MODIFICACION
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.dcAdministrarTareasSupervisantes objDC = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.dcAdministrarTareasSupervisantes();
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS objAux;
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes.TAREAS objAux2 = null;

                    var query2 = from item in objResp.lstTAREAS
                                 where item.IDTAREA == obj.IDTAREA
                                 select item;

                    if (query2 != null && query2.Count() > 0)
                        objAux2 = query2.ElementAt(0);

                    objAux = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS()
                    {
                        CODIGOTAREA = obj.CODIGOTAREA,
                        DESCRIPCIONTAREA = obj.DESCRIPCIONTAREA,
                        IDTAREA = obj.IDTAREA,
                        REQUIEREAUDITORIA = obj.REQUIEREAUDITORIA,
                        AUDITORIA = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(720, UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, NuevoValor: COA.WebCipol.Presentacion.Utiles.cAuditoria.Recuperar_Auditar_Cambios("Modif. Tarea Autorizante: " + obj.DESCRIPCIONTAREA.Trim()))
                    };

                    if (objAux2 != null)
                    {
                        objAux.IDSISTEMA = objAux2.IDSISTEMA;
                        objAux.DESCSISTEMA = objAux2.DESCSISTEMA;
                        objAux.REQUIEREAUDITORIA = objAux2.REQUIEREAUDITORIA;
                    }

                    objAux.IDAUTORIZACION = -1;
                    objAux.IDGRUPO = -1;

                    objDC.lstSE_TAREASAUTORIZADAS.Add(new Entidades.ClasesWs.dcAdministrarTareasSupervisantes.SE_TAREASAUTORIZADAS()
                    {
                        IDAUTORIZACION = 1,
                        DESCRIPCIONTAREA = obj.DESCIDTAREAAUTORIZANTE,
                        IDTAREA = obj.IDTAREAAUTORIZANTE
                    });

                    objDC.lstTAREAS = new List<COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS>();
                    objDC.lstTAREAS.Add(objAux);

                    if (objFachada.AdministrarTareasSupervisantes(objDC) == -1)
                    {
                        return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador";
                    }
                    return "";
                }
                else
                {
                    //NUEVA.    
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.dcAdministrarTareasSupervisantes objDC = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.dcAdministrarTareasSupervisantes();
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS objAux;

                    obj.blnREQUIEREAUDITORIA = false;

                    objAux = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS()
                    {
                        CODIGOTAREA = obj.CODIGOTAREA,
                        DESCRIPCIONTAREA = obj.DESCRIPCIONTAREA,
                        IDSISTEMA = obj.IDSISTEMA,
                        DESCSISTEMA = obj.DESCSISTEMA,
                        IDTAREA = obj.IDTAREA,
                        REQUIEREAUDITORIA = obj.REQUIEREAUDITORIA,
                        AUDITORIA = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(720, UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, NuevoValor: COA.WebCipol.Presentacion.Utiles.cAuditoria.Recuperar_Auditar_Cambios("Alta Tarea Autorizante: " + obj.DESCRIPCIONTAREA.Trim()))
                    };
                    objAux.IDTAREA = -1;
                    objAux.IDAUTORIZACION = -1;
                    objAux.IDGRUPO = -1;

                    objDC.lstSE_TAREASAUTORIZADAS.Add(new Entidades.ClasesWs.dcAdministrarTareasSupervisantes.SE_TAREASAUTORIZADAS()
                    {
                        IDAUTORIZACION = 1,
                        DESCRIPCIONTAREA = obj.DESCIDTAREAAUTORIZANTE,
                        IDTAREA = obj.IDTAREAAUTORIZANTE
                    });

                    objDC.lstTAREAS = new List<COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes.TAREAS>();
                    objDC.lstTAREAS.Add(objAux);

                    int intRetorno = objFachada.AdministrarTareasSupervisantes(objDC);

                    if (intRetorno == 0 && obj.update)
                        return "No se ha podido actualizar la tarea autorizante";
                    if (intRetorno == 0 && !obj.update)
                        return "No se ha podido ingresar la tarea autorizante";

                    if (intRetorno == -1)
                        return "No se han podido guardar los datos ocurrió en error inesperado. Consulte al administrador";
                    return "";
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string NoAutorizar(int Id, string nombre)
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id.ToString(), "Se ha eliminado la tarea primitiva: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string Autorizar(int Id, string nombre)
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id.ToString(), "Se ha ingresado la tarea primitiva: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #endregion

        #region "Cambiar contraseña"

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vContrasenia RecuperarElementoContraseniaBase()
        {
            try
            {
                ///     1 No obligatorio
                ManejoSesion.ModoCambioClave = 1;
                return new vContrasenia();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// [MartinV]          [miércoles, 24 de septiembre de 2014]       Modificado  GCP-Cambios 15584
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AdministrarContrasenia(vContrasenia obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.CONTRASENIA.Trim()))
                    return "La clave es un dato obligatorio.";

                if (string.IsNullOrEmpty(obj.NUEVACONTRASENIA.Trim()))
                    return "La Contraseña Nueva es un dato obligatorio.";

                if (string.IsNullOrEmpty(obj.REPETIRNUEVACONTRASENIA.Trim()))
                    return "Repetir Contraseña es un dato obligatorio.";

                if (obj.CONTRASENIA.Trim().Equals(obj.NUEVACONTRASENIA.Trim()))
                    return COA.Cipol.Presentacion.PaginaPadre.RetornaMensajeUsuario(50);


                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.dcRecuperarDatosParaABMUsuarios objResp = objFachada.RecuperarDatosParaABMUsuarios(ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario);




                string strMensaje = string.Empty;
                string strClave;
                System.Security.Cryptography.RSACryptoServiceProvider objRSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                EntidadesEmpresariales.PadreCipolCliente objCipol = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente;
                FInicioSesion objInicioSesion = new FInicioSesion();

                objRSA.ImportCspBlob(objCipol.gobjRSAServ);
                strClave = obj.CONTRASENIA.Trim();
                strClave = System.Convert.ToBase64String(objRSA.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(strClave), false));

                if (obj.OBLIGATORIO == 0)
                {
                    objInicioSesion.ValidarContraseña(objCipol.Login, strClave, ref strMensaje);
                    if (string.IsNullOrEmpty(strMensaje.Trim()))
                        return "No se ha podido realizar el proceso solicitado.";
                    else
                        return strMensaje;
                }
                else
                {
                    if (obj.NUEVACONTRASENIA.Trim().Length < ManejoSesion.gudParam.LongitudContraseña)
                        if (objInicioSesion.Auditar(COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(500, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, "", "")))
                            return "La contraseña especificada debe tener una longitud mínima de " + ManejoSesion.gudParam.LongitudContraseña + " caracteres.";
                        else
                            return "No se ha podido realizar el proceso solicitado.";

                    if (!obj.NUEVACONTRASENIA.Trim().Equals(obj.REPETIRNUEVACONTRASENIA.Trim()))
                        if (objInicioSesion.Auditar(COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(190, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, "", "")))
                            return COA.Cipol.Presentacion.PaginaPadre.RetornaMensajeUsuario(55);
                        else
                            return "No se ha podido realizar el proceso solicitado.";

                    string strNuevaContraseña;
                    strNuevaContraseña = obj.NUEVACONTRASENIA;
                    string strTemp = ValidarSeguridadContrasenia(ref strNuevaContraseña);

                    if (!string.IsNullOrEmpty(strTemp.Trim()))
                        if (objInicioSesion.Auditar(COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(205, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, "", "")))
                            return strTemp;
                        else
                            return "No se ha podido realizar el proceso solicitado.";

                    strNuevaContraseña = System.Convert.ToBase64String(objRSA.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(strNuevaContraseña), false));
                    if (objInicioSesion.CambiarContrasenia(ManejoSesion.gudParam.CantidadContraseñasAlmacenadas,
                                                        ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario,
                                                        ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login,
                                                        COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(210, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, "", ""),
                                                        strNuevaContraseña,
                                                        ManejoSesion.gudParam.DuracionContraseña,
                                                        ManejoSesion.ModoCambioClave,
                                                        strClave,
                                                        ref strMensaje,
                                                        ManejoSesion.gudParam.TiempoEnDiasNoPermitirCambiarContrasenia))
                    {

                    }
                    else
                    {
                        if (strMensaje.Trim().ToUpper().Equals("OCURRIO ERROR"))
                            return "No se ha podido realizar el proceso solicitado.";
                        if (string.IsNullOrEmpty(strMensaje))
                            return COA.Cipol.Presentacion.PaginaPadre.RetornaMensajeUsuario(50);
                        else
                            return strMensaje;
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region "Privadas"
        /// <summary> Valida si está configurado la autenticación SSO
        /// </summary>
        /// <returns></returns>
        /// <history> [JorgeI]  [20/02/2018]  TFS: 10686
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public bool ValidarLoginSSO()
        {
            string strURL_SSO = System.Configuration.ConfigurationManager.AppSettings["ServicioValidacionToken"];
            if (strURL_SSO == null)
            {
                return false;
            }
            if (!String.IsNullOrWhiteSpace(strURL_SSO.Trim()))
                return true;
            else
                return false;
        }
        
        private string ValidarSeguridadContrasenia(ref string pstrContrasenia)
        {
            if (ValidarLoginSSO())
            {
                return "";
            }
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

        #endregion
        #endregion

        #region "Grupo de tareas"

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vGrupoTareaGetCarga RecuperarGrupoTareaCarga()
        {
            try
            {

                FSeguridad objFachada = new FSeguridad();

                RecuperarControles rb = new RecuperarControles();

                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                vGrupoTareaGetCarga objRetorno = new vGrupoTareaGetCarga();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.dcRecuperarDatosParaABMGrupos objResp = objFachada.RecuperarDatosParaABMGrupos();
                //Recuperación de los sistemas.
                objRetorno.jsoncbosistemas = rb.GenerarCombos(new DatosCboGenerico() { DataSource = objResp.lstSE_SIST_HABILITADOS, DataTextField = "DESCSISTEMA", DataValueField = "IDSISTEMA", Id = "cboSistemas", itemTodos = null });
                objRetorno.jsoncbogrupo = rb.GenerarCombos(new DatosCboGenerico() { DataSource = objResp.lstSE_GRUPO_TAREA, DataTextField = "DESCGRUPO", DataValueField = "IDGRUPO", Id = "cboGrupos", itemTodos = null, Widht = new System.Web.UI.WebControls.Unit(350) });

                //quita la opción en caso de existir
                if (objResp.lstSE_GRUPO_TAREA != null && objResp.lstSE_GRUPO_TAREA.Count > 0)
                {
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.GRUPO_NO_EXCLUYENTE objRemove = null;
                    foreach (var item in objResp.lstGRUPO_NO_EXCLUYENTE)
                    {
                        if (item.IDGRUPO == objResp.lstSE_GRUPO_TAREA[0].IDGRUPO)
                        {
                            objRemove = item;
                            break;
                        }
                    }
                    if (objRemove != null)
                        objResp.lstGRUPO_NO_EXCLUYENTE.Remove(objRemove);
                }

                objRetorno.jsontareasnoasignandas = RecuperarTareasNoAsignadasGrupoTarea(objResp.lstSE_TAREAS);
                objRetorno.jsontareasasignadas = RecuperarTareasAsigandasGrupoTarea(objResp.lstSE_TAREAS_ASIGNADAS);
                objRetorno.jsongrupoexcluyentes = RecuperarGrupoExcluyenteGrupoTarea(objResp.lstGRUPO_EXCLUYENTE);
                objRetorno.jsongruposnoexcluyentes = RecuperarGrupoNoExcluyenteGrupoTarea(objResp.lstGRUPO_NO_EXCLUYENTE);

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vGrupoTareaGetItem RecuperarDatosDelGrupo(short Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                vGrupoTareaGetItem objRetorno = new vGrupoTareaGetItem();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo.dcRecuperarDatosDelGrupo objResp = objFachada.RecuperarDatosDelGrupo(Id);

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo.GRUPO_NO_EXCLUYENTE objRemove = null;
                foreach (var item in objResp.lstGRUPO_NO_EXCLUYENTE)
                {
                    if (item.IDGRUPO == Id)
                    {
                        objRemove = item;
                        break;
                    }
                }
                if (objRemove != null)
                    objResp.lstGRUPO_NO_EXCLUYENTE.Remove(objRemove);
                //Recuperación de los sistemas.
                objRetorno.jsontareasasignadas = RecuperarTareasAsigandasGrupoTarea(objResp.lstSE_TAREAS_ASIGNADAS);
                objRetorno.jsongrupoexcluyentes = RecuperarGrupoExcluyenteGrupoTarea(objResp.lstGRUPO_EXCLUYENTE);
                objRetorno.jsongruposnoexcluyentes = RecuperarGrupoNoExcluyenteGrupoTarea(objResp.lstGRUPO_NO_EXCLUYENTE);

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarTareasPorSistemaYGrupo(short Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPorSistemaYGrupo.dcRecuperarTareasPorSistemaYGrupo objResp = objFachada.RecuperarTareasPorSistemaYGrupo(Id, 0);
                //Recuperación de las tareas según el id sistema.
                return RecuperarTareasNoAsignadasGrupoTarea(objResp.lstSE_TAREAS);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vGrupoTareaGetCarga EliminarGrupo(short Id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                RecuperarControles rb = new RecuperarControles();

                vGrupoTareaGetCarga objRetorno = new vGrupoTareaGetCarga();
                if (objFachada.EliminarGrupo(Id))
                {
                    //CargarTareasNoAsignadas.
                    //COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasPorSistemaYGrupo.dcRecuperarTareasPorSistemaYGrupo objResp = objFachada.RecuperarTareasPorSistemaYGrupo(IdSistema, 0);
                    objRetorno.ResultadoEjecucion = true;
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.dcRecuperarDatosParaABMGrupos objResp = objFachada.RecuperarDatosParaABMGrupos();
                    //Recuperación de los sistemas.
                    objRetorno.jsoncbosistemas = rb.GenerarCombos(new DatosCboGenerico() { DataSource = objResp.lstSE_SIST_HABILITADOS, DataTextField = "DESCSISTEMA", DataValueField = "IDSISTEMA", Id = "cboSistemas", itemTodos = null });
                    objRetorno.jsoncbogrupo = rb.GenerarCombos(new DatosCboGenerico() { DataSource = objResp.lstSE_GRUPO_TAREA, DataTextField = "DESCGRUPO", DataValueField = "IDGRUPO", Id = "cboGrupos", itemTodos = null, Widht = new System.Web.UI.WebControls.Unit(350) });

                    //quita la opción en caso de existir
                    if (objResp.lstSE_GRUPO_TAREA != null && objResp.lstSE_GRUPO_TAREA.Count > 0)
                    {
                        COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.GRUPO_NO_EXCLUYENTE objRemove = null;
                        foreach (var item in objResp.lstGRUPO_NO_EXCLUYENTE)
                        {
                            if (item.IDGRUPO == objResp.lstSE_GRUPO_TAREA[0].IDGRUPO)
                            {
                                objRemove = item;
                                break;
                            }
                        }
                        if (objRemove != null)
                            objResp.lstGRUPO_NO_EXCLUYENTE.Remove(objRemove);
                    }

                    objRetorno.jsontareasnoasignandas = RecuperarTareasNoAsignadasGrupoTarea(objResp.lstSE_TAREAS);
                    objRetorno.jsontareasasignadas = RecuperarTareasAsigandasGrupoTarea(objResp.lstSE_TAREAS_ASIGNADAS);
                    objRetorno.jsongrupoexcluyentes = RecuperarGrupoExcluyenteGrupoTarea(objResp.lstGRUPO_EXCLUYENTE);
                    objRetorno.jsongruposnoexcluyentes = RecuperarGrupoNoExcluyenteGrupoTarea(objResp.lstGRUPO_NO_EXCLUYENTE);

                    objRetorno.MensajeServicio = "El grupo ha sido eliminado satisfactoriamente.";
                }
                else
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "No se pudo eliminar el grupo";
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarGrupoTareaNew()
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vAdministrarGrupoTareas AdministrarGrupoTareas(vGrupoTarea obj)
        {
            vAdministrarGrupoTareas objRetorno = new vAdministrarGrupoTareas();
            try
            {
                if (string.IsNullOrEmpty(obj.nombregrupo.Trim()))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <strong>NOMBRE DEL GRUPO</strong> es un dato obligatorio.";
                    return objRetorno;
                }

                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.dcRecuperarDatosParaABMGrupos objResp = objFachada.RecuperarDatosParaABMGrupos();

                var query = from item in objResp.lstSE_GRUPO_TAREA
                            where item.IDGRUPO != obj.idgrupo && item.DESCGRUPO.Trim().ToUpper() == obj.nombregrupo.Trim().ToUpper()
                            select item;

                if (query != null && query.Count() > 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <strong>NOMBRE DEL GRUPO</strong> ya existe";
                    return objRetorno;
                }

                if (obj.lstTareas != null && !(obj.lstTareas.Count > 0))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "No existen tareas asignadas al grupo.";
                    return objRetorno;
                }

                //todo: martinv ver que significa esto en el cipol original
                //Me.DtsGrupos.Auditoria.Clear()
                if (obj.update)
                {
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.dcAdministrarGrupo objDC;
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.SE_GRUPO_TAREA objGrupoAux;

                    objGrupoAux = new Entidades.ClasesWs.dcAdministrarGrupo.SE_GRUPO_TAREA()
                    {
                        IDGRUPO = obj.idgrupo,
                        DESCGRUPO = obj.nombregrupo
                    };

                    objDC = new Entidades.ClasesWs.dcAdministrarGrupo.dcAdministrarGrupo();

                    foreach (var item in obj.lstTareas)
                    {
                        objDC.lstSE_TAREAS_ASIGNADAS.Add(new Entidades.ClasesWs.dcAdministrarGrupo.SE_TAREAS_ASIGNADAS()
                        {
                            IDTAREA = item.Id,
                            DESCRIPCIONTAREA = item.nombre
                        });
                    }

                    foreach (var item in obj.lstGrupos)
                    {
                        objDC.lstGRUPO_EXCLUYENTE.Add(new Entidades.ClasesWs.dcAdministrarGrupo.GRUPO_EXCLUYENTE()
                        {
                            IDGRUPO = item.Id,
                            DESCGRUPO = item.nombre
                        });
                    }

                    objDC.lstSE_GRUPO_TAREA.Add(objGrupoAux);
                    objDC.lstAuditoria.Add(new Entidades.ClasesWs.dcAdministrarGrupo.Auditoria()
                    {
                        SQLAuditar = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(730, UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, NuevoValor: COA.WebCipol.Presentacion.Utiles.cAuditoria.Recuperar_Auditar_Cambios("Grupo: " + obj.nombregrupo.Trim()))
                    });

                    short respuesta = objFachada.AdministrarGrupo(objDC);

                    if (respuesta > 0)
                    {
                        objRetorno.ResultadoEjecucion = true;
                        objRetorno.idgrupro = respuesta;
                        return objRetorno;
                    }
                    else
                    {
                        if (obj.lstTareas != null && !(obj.lstTareas.Count > 0))
                        {
                            objRetorno.ResultadoEjecucion = false;
                            objRetorno.MensajeError = "No se ha podido modificar el grupo";
                            return objRetorno;
                        }
                    }
                }
                else
                {
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.dcAdministrarGrupo objDC;
                    COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo.SE_GRUPO_TAREA objGrupoAux;

                    objGrupoAux = new Entidades.ClasesWs.dcAdministrarGrupo.SE_GRUPO_TAREA()
                    {
                        IDGRUPO = -1,
                        DESCGRUPO = obj.nombregrupo
                    };

                    objDC = new Entidades.ClasesWs.dcAdministrarGrupo.dcAdministrarGrupo();

                    foreach (var item in obj.lstTareas)
                    {
                        objDC.lstSE_TAREAS_ASIGNADAS.Add(new Entidades.ClasesWs.dcAdministrarGrupo.SE_TAREAS_ASIGNADAS()
                        {
                            IDTAREA = item.Id,
                            DESCRIPCIONTAREA = item.nombre
                        });
                    }

                    foreach (var item in obj.lstGrupos)
                    {
                        objDC.lstGRUPO_EXCLUYENTE.Add(new Entidades.ClasesWs.dcAdministrarGrupo.GRUPO_EXCLUYENTE()
                        {
                            IDGRUPO = item.Id,
                            DESCGRUPO = item.nombre
                        });
                    }

                    objDC.lstSE_GRUPO_TAREA.Add(objGrupoAux);
                    objDC.lstAuditoria.Add(new Entidades.ClasesWs.dcAdministrarGrupo.Auditoria()
                    {
                        SQLAuditar = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(700, UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, NuevoValor: COA.WebCipol.Presentacion.Utiles.cAuditoria.Recuperar_Auditar_Cambios("Grupo: " + obj.nombregrupo.Trim()))
                    });

                    short respuesta = objFachada.AdministrarGrupo(objDC);

                    if (respuesta > 0)
                    {
                        objRetorno.ResultadoEjecucion = true;
                        objRetorno.idgrupro = respuesta;
                        return objRetorno;
                    }
                    else
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "No se ha podido ingresar el grupo";
                        return objRetorno;
                    }
                }
                objRetorno.ResultadoEjecucion = false;
                objRetorno.MensajeError = "No se ha podido procesar la solicutud";
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarTareaGrupoTarea(int Id, string nombre)
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha ingresado la tarea: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarTareaGrupoTarea(int Id, string nombre)
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha eliminado la tarea: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarGrupoGrupoTarea(int Id, string nombre)
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("G" + Id.ToString(), "Se ha ingresado el grupo excluyente: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarGrupoGrupoTarea(int Id, string nombre)
        {
            try
            {
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("G" + Id.ToString(), "Se ha eliminado el grupo excluyente: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarTareaGrupoTareaTodos(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha ingresado la tarea: " + item.nombre.Trim());

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarTareaGrupoTareaTodos(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha eliminado la tarea: " + item.nombre.Trim());
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarGrupoGrupoTareaTodos(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("G" + item.Id.ToString(), "Se ha ingresado el grupo excluyente: " + item.nombre.Trim());
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarGrupoGrupoTareaTodos(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("G" + item.Id.ToString(), "Se ha eliminado el grupo excluyente: " + item.nombre.Trim());
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region "Privadas - Grupo de tareas"

        //private string recuperarCboSistmasGrupoTarea(List<Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.SE_SIST_HABILITADOS> list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIcboSistemaABMGrupoTarea objUI = new UIcboSistemaABMGrupoTarea();
        //        UIcboSistemaDG ctl = (UIcboSistemaDG)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboSistemaDG.ascx");

        //        //Envio la lista de sistemas.
        //        objUI.lst = list;
        //        ctl.Configuracion("cboSistemas");

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        //private string recuperarCboGrupoGrupoTarea(List<Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos.SE_GRUPO_TAREA> list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIcboGenerico objUI = new UIcboGenerico();
        //        UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");


        //        ctl.datos = new DatosCboGenerico()
        //        {
        //            DataSource = list,
        //            DataTextField = "DESCGRUPO",
        //            DataValueField = "IDGRUPO",
        //            Height = new System.Web.UI.WebControls.Unit(22),
        //            Widht = new System.Web.UI.WebControls.Unit(150),
        //            Id = "cboGrupos"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        private string RecuperarGrupoNoExcluyenteGrupoTarea(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


                ctl.datos = new DatosListBoxGenerico()
                {
                    DataSource = list,
                    DataTextField = "DESCGRUPO",
                    DataValueField = "IDGRUPO",
                    Height = new System.Web.UI.WebControls.Unit(160),
                    Width = new System.Web.UI.WebControls.Unit(400),
                    Id = "lstGrupoNoexcluyente"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private string RecuperarGrupoExcluyenteGrupoTarea(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


                ctl.datos = new DatosListBoxGenerico()
                {
                    DataSource = list,
                    DataTextField = "DESCGRUPO",
                    DataValueField = "IDGRUPO",
                    Height = new System.Web.UI.WebControls.Unit(160),
                    Width = new System.Web.UI.WebControls.Unit(400),
                    Id = "lstGrupoExcluyente"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private string RecuperarTareasAsigandasGrupoTarea(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


                ctl.datos = new DatosListBoxGenerico()
                {
                    DataSource = list,
                    DataTextField = "DESCRIPCIONTAREA",
                    DataValueField = "IDTAREA",
                    Height = new System.Web.UI.WebControls.Unit(160),
                    Width = new System.Web.UI.WebControls.Unit(400),
                    Id = "lstTareasAsignadas"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private string RecuperarTareasNoAsignadasGrupoTarea(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


                ctl.datos = new DatosListBoxGenerico()
                {
                    DataSource = list,
                    DataTextField = "DESCRIPCIONTAREA",
                    DataValueField = "IDTAREA",
                    Height = new System.Web.UI.WebControls.Unit(160),
                    Width = new System.Web.UI.WebControls.Unit(400),
                    Id = "lstTareasRaiz"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        #endregion

        #endregion

        #region "Roles"
        #region "Variables de Seesion"
        private vSessionDatosRol objDatosRoles
        {
            get
            {
                return (vSessionDatosRol)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.ROL_DATOSROL];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.ROL_DATOSROL] = value;
            }
        }

        #endregion
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vRolGetCarga RecuperarRolCarga()
        {
            try
            {
                //Inicializo la variable de session del formulario Rol
                objDatosRoles = new vSessionDatosRol();
                FSeguridad objFachada = new FSeguridad();

                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                vRolGetCarga objRetorno = new vRolGetCarga();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.dcRecuperarDatosParaABMRoles objResp = objFachada.RecuperarDatosParaABMRoles();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.dcRecuperarComposicionRol objResp2 = null;
                if (objResp.lstSE_ROLES != null && objResp.lstSE_ROLES.Count() > 0)
                    objResp2 = objFachada.RecuperarComposicionRol(objResp.lstSE_ROLES[0].IDROL, true);

                objDatosRoles.dcRecuperarDatosParaABMRoles = objResp;
                if (objResp2 != null)
                {
                    objDatosRoles.dcRecuperarComposicionRol = objResp2;
                    objDatosRoles.listaOrignal = objResp2.lstRoles_Composicion.ToList();
                    objRetorno.jsontvtareasasignadas = recuperarTreeViewGruposRoles(objResp2.lstRoles_Composicion, objResp.lstArbolGrupo);
                }
                else
                {
                    objRetorno.jsontvtareasasignadas = recuperarTreeViewGruposRoles(new List<Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion>(), objResp.lstArbolGrupo);
                }

                objRetorno.jsoncboroles = recuperarCboRolesRoles(objResp.lstSE_ROLES);
                objRetorno.jsontvtareasdisponibles = recuperarTreeViewGruposRoles(objResp.lstArbolGrupo);
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vRolGetItem RecuperarDatosRol(short Id)
        {
            try
            {
                vRolGetItem objRetorno = new vRolGetItem();
                FSeguridad objFachada = new FSeguridad();
                objDatosRoles.dcRecuperarDatosParaABMRoles.lstRoles_Composicion.Clear();
                objDatosRoles.dcRecuperarDatosParaABMRoles.lsttblUsuariosXRoles.Clear();
                //limipiar la auditoria.
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.dcRecuperarComposicionRol objresp = objFachada.RecuperarComposicionRol(Id, true);
                //Guarda en session los datos recuperados
                objDatosRoles.dcRecuperarComposicionRol = objresp;
                objDatosRoles.listaOrignal = objresp.lstRoles_Composicion.ToList();

                if (objresp == null || objresp.lstRoles_Composicion == null)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "No se han podido recuperar las tareas del rol.";
                }

                objRetorno.jsontvtareasasignadas = recuperarTreeViewGruposRoles(objresp.lstRoles_Composicion, objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo);

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vRolEliminar EliminarRol(int Id, bool validar)
        {
            try
            {
                vRolEliminar objRetorno = new vRolEliminar();
                FSeguridad objFachada = new FSeguridad();

                if (validar)
                    if (objFachada.VerificarRolAsignadoAUsuarios(Id) > 0)
                    {
                        objRetorno.ResultadoEjecucion = true;
                        objRetorno.preguntausuarios = true;
                        objRetorno.MensajeError = "Existen usuarios que tienen este rol asignado, si elimina el mismo eliminara el rol de los usuarios.Presione OK para continuar con la eliminación o Cancelar para anular la acción";
                        return objRetorno;
                    }

                if (objFachada.EliminarRol(Id) == 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "Ha ocurrido un error inesperado, por favor vuelva a intentar.";
                    return objRetorno;
                }

                objRetorno.rolgetcarga = RecuperarRolCarga();

                objRetorno.ResultadoEjecucion = true;
                objRetorno.MensajeServicio = "El rol ha sido eliminado satisfactoriamente.";

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarRolNew()
        {
            try
            {
                objDatosRoles.dcRecuperarComposicionRol = new Entidades.ClasesWs.dcRecuperarComposicionRol.dcRecuperarComposicionRol();
                objDatosRoles.listaOrignal = new List<Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion>();
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vAdministrarRol AdministrarRoles(vRol obj)
        {
            try
            {
                vAdministrarRol objRetorno = new vAdministrarRol();
                COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.dcAdministrarRolesIN objDatos = new Entidades.ClasesWs.dcAdministrarRoles.dcAdministrarRolesIN();

                if (obj.nombrerol == null || string.IsNullOrEmpty(obj.nombrerol))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <strong>NOMBRE DEL ROL</strong> es un dato obligatorio.";
                    return objRetorno;
                }

                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.dcRecuperarDatosParaABMRoles objResp = objFachada.RecuperarDatosParaABMRoles();

                var query = from item in objResp.lstSE_ROLES
                            where item.IDROL != obj.idrol && item.DESCRIPCIONPERFIL.Trim().ToUpper() == obj.nombrerol.Trim().ToUpper()
                            select item;

                if (query != null && query.Count() > 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <strong>NOMBRE DEL ROL</strong> ya existe, imposible continuar.";
                    return objRetorno;
                }

                if (obj.lsttareas != null && !(obj.lsttareas.Count > 0))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "No existen tareas asignadas al Rol";
                    return objRetorno;
                }
                //Controla si hay tareas asignadas.
                if (!((from item in obj.lsttareas
                       where item.asignada == true
                       select item).Count() > 0))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "Al menos una de las tareas asignadas al rol debe estar habilitada";
                    return objRetorno;
                }

                objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarRoles.ParametrosDeABM()
                {
                    MensajesAuditoria = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(740, UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, NuevoValor: COA.WebCipol.Presentacion.Utiles.cAuditoria.Recuperar_Auditar_Cambios("Rol: " + obj.nombrerol.Trim()))
                });



                foreach (var item in objDatosRoles.dcRecuperarComposicionRol.lsttblUsuariosXRoles)
                {
                    objDatos.lsttblUsuariosXRoles.Add(new Entidades.ClasesWs.dcAdministrarRoles.tblUsuariosXRoles()
                    {
                        IdUsuario = item.IdUsuario
                    });
                }
                //Carga el rol.
                objDatos.lstSE_ROLES.Add(new Entidades.ClasesWs.dcAdministrarRoles.SE_ROLES() { IDROL = obj.idrol, DESCRIPCIONPERFIL = obj.nombrerol.Trim() });

                TresDES objEncriptarNET;
                objEncriptarNET = new TresDES();
                objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
                objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;

                //Se necesita cargar el valor de tarea inhibida.
                //Recupera las tareas elminadas del rol.
                //[GonzaloP]          [viernes, 15 de julio de 2016]       Work-Item: 7195 - Se agregan las tareas inhibidas a la lista para eliminar.
                var queryEliminados = from c in objDatosRoles.listaOrignal
                                      where !(from o in obj.lsttareas
                                              select int.Parse(o.Id.Split('/')[2]))
                                                .Contains(c.idTarea)
                                            || (from o in obj.lsttareas
                                                where !o.asignada
                                                select int.Parse(o.Id.Split('/')[2]))
                                                .Contains(c.idTarea)
                                      select c;

                if (queryEliminados != null && queryEliminados.Count() > 0)
                    foreach (var itemElim in queryEliminados)
                        objDatos.lstTareasNoAsignadas.Add(new Entidades.ClasesWs.dcAdministrarRoles.TareasNoAsignadas()
                        {
                            idTarea = itemElim.idTarea,
                            TareaInhibida = objEncriptarNET.Criptografia(Accion.Encriptacion, (ManejoSesion.gudParam.ModoAsignacionTareasYRoles.ToString().Trim().ToUpper().Equals("0")) ? "N" : "S")
                        });

                if (objDatosRoles.dcRecuperarComposicionRol.lsttblUsuariosXRoles.Count() > 0)
                {
                    var queryNuevas = from c in obj.lsttareas
                                      where c.asignada && !(from o in objDatosRoles.listaOrignal select o.idTarea).Contains(int.Parse(c.Id.Split('/')[2]))
                                      select c;

                    if (queryNuevas != null && queryNuevas.Count() > 0)
                        foreach (var itemNuevas in queryNuevas)
                            objDatos.lstTareasAsignadas.Add(new Entidades.ClasesWs.dcAdministrarRoles.TareasAsignadas()
                            {
                                idTarea = int.Parse(itemNuevas.Id.Split('/')[2]),
                                TareaInhibida = objEncriptarNET.Criptografia(Accion.Encriptacion, (ManejoSesion.gudParam.ModoAsignacionTareasYRoles.ToString().Trim().ToUpper().Equals("0")) ? "N" : "S")
                            });
                }
                else
                {
                    foreach (var item in obj.lsttareas)
                    {
                        if (item.asignada)
                            objDatos.lstTareasAsignadas.Add(new Entidades.ClasesWs.dcAdministrarRoles.TareasAsignadas()
                                    {
                                        idTarea = int.Parse(item.Id.Split('/')[2]),
                                        TareaInhibida = objEncriptarNET.Criptografia(Accion.Encriptacion, (ManejoSesion.gudParam.ModoAsignacionTareasYRoles.ToString().Trim().ToUpper().Equals("0")) ? "N" : "S")
                                    });
                    }
                }

                string strEliminados = "";
                //armado del string de eliminados.
                if (obj.update)
                {
                    foreach (var item in objDatosRoles.listaOrignal)
                    {
                        var queryAux = from itemAux in obj.lsttareas
                                       where int.Parse(itemAux.Id.Split('/')[2]) == item.idTarea
                                       select new { id = item.idTarea };
                        if (queryAux == null || queryAux.Count() == 0)
                        {
                            //se tiene que armar asi !!! -> fuck!! IdG2IdS2IdT¤1222,
                            strEliminados += "IdG" + item.IdGrupo + "IdS" + item.idSistema + "IdT¤" + item.idTarea + ",";
                        }
                    }
                }

                objDatos.pStrElementosEliminados = strEliminados;

                COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles.dcAdministrarRoles objResultado = objFachada.AdministrarRoles(objDatos);
                if (objResultado.intRetorno == 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "Ha ocurrido un error inesperado, por favor vuelva a intentar.";
                    return objRetorno;
                }
                //Ejecuta la búsqueda nuevamente para dejar en sessión los valores guardados.
                objDatosRoles.dcRecuperarDatosParaABMRoles.lstRoles_Composicion.Clear();
                objDatosRoles.dcRecuperarDatosParaABMRoles.lsttblUsuariosXRoles.Clear();
                //limipiar la auditoria.
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.dcRecuperarComposicionRol objresp = objFachada.RecuperarComposicionRol(objResultado.intRetorno, true);
                //Guarda en session los datos recuperados
                objDatosRoles.dcRecuperarComposicionRol = objresp;
                objDatosRoles.listaOrignal = objresp.lstRoles_Composicion.ToList();
                //carga los paramtros de ABM.
                objRetorno.idrol = objResultado.intRetorno;
                objRetorno.ResultadoEjecucion = true;
                objRetorno.MensajeError = "Los datos han sido guardados guardados.";
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vRolAsignar asignarTareaRol(string Id)
        {
            try
            {
                vRolAsignar objRetorno = new vRolAsignar();
                string mensaje = "";
                string[] valores = Id.Split('/');

                if (ExistenGruposExcluyentes(Id, ref mensaje))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = mensaje;
                    return objRetorno;
                }

                switch (valores.Length)
                {
                    case 1:
                        var query = from item in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                    where item.IDGRUPO == int.Parse(valores[0])
                                    select item;
                        if (query != null && query.Count() > 0)
                        {
                            foreach (var item in query)
                            {
                                if (!(objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.Where(p => p.idTarea == item.IDTAREA).Count() > 0))
                                {
                                    objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.Add(new Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion()
                                    {
                                        idTarea = item.IDTAREA,
                                        idSistema = item.IDSISTEMA,
                                        IdGrupo = item.IDGRUPO
                                    });
                                }
                            }
                            //Grupo
                            COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha ingresado Grupo:" + query.First().DESCGRUPO);
                        }
                        break;
                    case 2:
                        var query2 = from item in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                     where item.IDGRUPO == int.Parse(valores[0]) && item.IDSISTEMA == int.Parse(valores[1])
                                     select item;

                        if (query2 != null && query2.Count() > 0)
                        {
                            foreach (var item in query2)
                            {
                                if (!(objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.Where(p => p.idTarea == item.IDTAREA).Count() > 0))
                                {
                                    objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.Add(new Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion()
                                    {
                                        idTarea = item.IDTAREA,
                                        idSistema = item.IDSISTEMA,
                                        IdGrupo = item.IDGRUPO
                                    });
                                }
                            }
                            //Sistema.
                            COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha ingresado Grupo:" + query2.First().DESCGRUPO + "- Sistema: " + query2.First().DESCSISTEMA);
                        }
                        break;
                    case 3:
                        var query3 = from item in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                     where item.IDGRUPO == int.Parse(valores[0]) && item.IDSISTEMA == int.Parse(valores[1]) && item.IDTAREA == int.Parse(valores[2])
                                     select item;

                        if (query3 != null && query3.Count() > 0)
                        {
                            foreach (var item in query3)
                            {
                                if (!(objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.Where(p => p.idTarea == item.IDTAREA).Count() > 0))
                                {
                                    objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.Add(new Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion()
                                    {
                                        idTarea = item.IDTAREA,
                                        idSistema = item.IDSISTEMA,
                                        IdGrupo = item.IDGRUPO
                                    });
                                }
                            }
                            //Tarea
                            COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha ingresado Grupo:" + query3.First().DESCGRUPO + " - Sistema: " + query3.First().DESCSISTEMA + " - Tarea: " + query3.First().DESCRIPCIONTAREA);
                        }
                        break;
                    default:
                        break;
                }
                objRetorno.ResultadoEjecucion = true;

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vRolAsignar desasignarRolTarea(string Id)
        {
            try
            {
                vRolAsignar objRetorno = new vRolAsignar();
                string[] valores = Id.Split('/');
                switch (valores.Length)
                {
                    case 1:
                        var query = from item in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                    where item.IDGRUPO == int.Parse(valores[0])
                                    select item;
                        //Elimino todas las tareas del Grupo.
                        objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.RemoveAll(p => p.IdGrupo == int.Parse(valores[0]));

                        if (query != null && query.Count() > 0)
                        {
                            //Grupo
                            COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha eliminado Grupo:" + query.First().DESCGRUPO);
                        }
                        break;
                    case 2:
                        var query2 = from item in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                     where item.IDGRUPO == int.Parse(valores[0]) && item.IDSISTEMA == int.Parse(valores[1])
                                     select item;

                        //Elimino todas las tareas del Grupo y sistema.
                        objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.RemoveAll(p => p.IdGrupo == int.Parse(valores[0]) && p.idSistema == int.Parse(valores[1]));

                        if (query2 != null && query2.Count() > 0)
                        {
                            //Sistema.
                            COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha eliminado Grupo:" + query2.First().DESCGRUPO + "- Sistema: " + query2.First().DESCSISTEMA);
                        }
                        break;
                    case 3:
                        var query3 = from item in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                     where item.IDGRUPO == int.Parse(valores[0]) && item.IDSISTEMA == int.Parse(valores[1]) && item.IDTAREA == int.Parse(valores[2])
                                     select item;
                        //Elimino todas las tareas del Grupo y sistema.
                        objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion.RemoveAll(p => p.IdGrupo == int.Parse(valores[0])
                                                                                                && p.idSistema == int.Parse(valores[1])
                                                                                                && p.idTarea == int.Parse(valores[2]));
                        if (query3 != null && query3.Count() > 0)
                        {
                            //Tarea
                            COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha eliminado Grupo:" + query3.First().DESCGRUPO + " - Sistema: " + query3.First().DESCSISTEMA + " - Tarea: " + query3.First().DESCRIPCIONTAREA);
                        }
                        break;
                    default:
                        break;
                }
                objRetorno.ResultadoEjecucion = true;
                //Arma el treeview.
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region "Privadas - Roles"

        private string recuperarCboRolesRoles(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.SE_ROLES> list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");


                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = list,
                    DataTextField = "DESCRIPCIONPERFIL",
                    DataValueField = "IDROL",
                    Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(300),
                    Id = "cboRol"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        //private string recuperarTreeViewGruposRoles(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo> list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UITreeViewTareasDisponibles objUI = new UITreeViewTareasDisponibles();
        //        UITreeViewTareasDisponiblesUC ctl = (UITreeViewTareasDisponiblesUC)objUI.LoadControl("UIControlsHelper/TreeView/UITreeViewTareasDisponiblesUC.ascx");


        //        ctl.datos = new dcTreeView()
        //        {
        //            list = list,
        //            Height = new System.Web.UI.WebControls.Unit(22),
        //            Width = new System.Web.UI.WebControls.Unit(390),
        //            Id = "tvTareasDisponibles"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        //private string recuperarTreeViewGruposRoles2(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo> list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UITreeViewTareasDisponibles objUI = new UITreeViewTareasDisponibles();
        //        UITreeViewTareasDisponiblesUC ctl = (UITreeViewTareasDisponiblesUC)objUI.LoadControl("UIControlsHelper/TreeView/UITreeViewTareasDisponiblesUC.ascx");


        //        ctl.datos = new dcTreeView()
        //        {
        //            list = list,
        //            Height = new System.Web.UI.WebControls.Unit(22),
        //            Width = new System.Web.UI.WebControls.Unit(150),
        //            Id = "tvTareasDisponibles2"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        private string recuperarTreeViewGruposRoles(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo> list)
        {
            StringBuilder sbHtml = new StringBuilder();
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            MemoryStream objMemoria = new MemoryStream();
            ElementoLista objLista = new ElementoLista();

            if (list.Count == 0)
                return "";
            sbHtml.Append("<div id='dvTareasDisponibles' runat='Server' style=' overflow: scroll; height: 400px; width:450px;background:#FFFFFF !important; border:2px solid #ababab !important'>");
            sbHtml.Append("<ul class='nivel1'>");
            foreach (var item in list.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo>(p => p.IDGRUPO)))
            {

                sbHtml.Append("<li id='" + item.IDGRUPO.ToString() + "' class='grupo' ><img src='./Imagenes/group.png' /><a> " + item.DESCGRUPO + "</a>");//style='list-style-image:url(./Imagenes/plusbox.gif)'

                sbHtml.Append("<ul class='nivel2'>");
                foreach (var itemAux in list.Where(p => p.IDGRUPO == item.IDGRUPO).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo>(p => p.IDSISTEMA)))
                {
                    if (itemAux.IDGRUPO != item.IDGRUPO)
                        continue;
                    sbHtml.Append("<li id='" + itemAux.IDGRUPO.ToString() + "/" + itemAux.IDSISTEMA.ToString() + "' class='sistema'><img src='./Imagenes/sistema.bmp' /><a>" + itemAux.DESCSISTEMA + "</a>");//
                    sbHtml.Append("<ul class='nivel3'>");

                    foreach (var itemAux2 in list)
                    {
                        if (itemAux.IDGRUPO != itemAux2.IDGRUPO || itemAux.IDSISTEMA != itemAux2.IDSISTEMA)
                            continue;
                        sbHtml.Append("<li id='" + itemAux2.IDGRUPO.ToString() + "/" + itemAux2.IDSISTEMA.ToString() + "/" + itemAux2.IDTAREA.ToString() + "' class='tarea'><img src='./Imagenes/task-icon.png' /><a> " + itemAux2.DESCRIPCIONTAREA + "</a>");
                        sbHtml.Append("</li>");
                    }
                    sbHtml.Append("</ul></li>");
                }
                sbHtml.Append("</ul></li>");
            }
            sbHtml.Append("</ul>");
            sbHtml.Append("</div>");

            objLista.Lista = Server.HtmlEncode(sbHtml.ToString());
            objSerializador = new DataContractJsonSerializer(objLista.GetType());
            objSerializador.WriteObject(objMemoria, objLista);
            strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

            objMemoria.Close();
            return strRtaJson;
        }

        private string recuperarTreeViewGruposRoles(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion> list, List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.ArbolGrupo> listDatos)
        {
            StringBuilder sbHtml = new StringBuilder();
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            MemoryStream objMemoria = new MemoryStream();
            ElementoLista objLista = new ElementoLista();

            //if (list.Count == 0)
            //return "";

            foreach (var item in list)
            {
                var query = from itemAux in listDatos
                            where itemAux.IDGRUPO == item.IdGrupo && itemAux.IDSISTEMA == item.idSistema && itemAux.IDTAREA == item.idTarea
                            select itemAux;

                if (query != null && query.Count() > 0)
                {
                    item.DescGrupo = query.First().DESCGRUPO;
                    item.DescSistema = query.First().DESCSISTEMA;
                    item.DescripcionTarea = query.First().DESCRIPCIONTAREA;
                }
            }

            sbHtml.Append("<div id='dvtareasasignadas' runat='Server' style='overflow: scroll; height: 400px; width:450px;background:#FFFFFF !important;border:2px solid #ababab !important'>");
            sbHtml.Append("<ul class='nivel1'>");
            foreach (var item in list.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion>(p => p.IdGrupo)))
            {
                sbHtml.Append("<li id='" + item.IdGrupo.ToString() + "' class='grupo' ><img src='./Imagenes/group.png' /><a> " + item.DescGrupo + "</a>");//' style='list-style-image:url(./Imagenes/plusbox.gif)'

                sbHtml.Append("<ul class='nivel2'>");
                foreach (var itemAux in list.Where(p => p.IdGrupo == item.IdGrupo).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion>(p => p.idSistema)))
                {
                    if (itemAux.IdGrupo != item.IdGrupo)
                        continue;
                    sbHtml.Append("<li id='" + itemAux.IdGrupo.ToString() + "/" + itemAux.idSistema.ToString() + "' class='sistema'><img src='./Imagenes/sistema.bmp' /><a>" + itemAux.DescSistema + "</a>");
                    sbHtml.Append("<ul class='nivel3'>");

                    foreach (var itemAux2 in list)
                    {
                        if (itemAux.IdGrupo != itemAux2.IdGrupo || itemAux.idSistema != itemAux2.idSistema)
                            continue;
                        sbHtml.Append("<li id='" + itemAux2.IdGrupo.ToString() + "/" + itemAux2.idSistema.ToString() + "/" + itemAux2.idTarea.ToString() + "' class='asignada'><img src='./Imagenes/task-assign-icon.png' /><a> " + itemAux2.DescripcionTarea + "</a>");
                        sbHtml.Append("</li>");
                    }
                    sbHtml.Append("</ul></li>");
                }
                sbHtml.Append("</ul></li>");
            }
            sbHtml.Append("</ul>");
            sbHtml.Append("</div>");

            objLista.Lista = Server.HtmlEncode(sbHtml.ToString());
            objSerializador = new DataContractJsonSerializer(objLista.GetType());
            objSerializador.WriteObject(objMemoria, objLista);
            strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

            objMemoria.Close();
            return strRtaJson;
        }

        private bool ExistenGruposExcluyentes(string Id, ref string mensaje)
        {
            if (objDatosRoles.dcRecuperarDatosParaABMRoles.lstSE_GRUPO_EXCLUSION == null || objDatosRoles.dcRecuperarComposicionRol == null || objDatosRoles.dcRecuperarDatosParaABMRoles.lstSE_GRUPO_EXCLUSION.Count() == 0)
                return false;

            string strIdGrupo;
            string strGrupoActual = "";
            string strGrupoExc = "";
            string[] node = Id.Split('/');

            strIdGrupo = node[0];

            foreach (var item in objDatosRoles.dcRecuperarComposicionRol.lstRoles_Composicion)
            {
                //Controla que no exista exclución de grupos
                var querry = from itemAux in objDatosRoles.dcRecuperarDatosParaABMRoles.lstSE_GRUPO_EXCLUSION
                             where (itemAux.IDGRUPOACTUAL == item.IdGrupo && itemAux.IDGRUPEXCLUYENTE == int.Parse(strIdGrupo))
                                     || (itemAux.IDGRUPEXCLUYENTE == item.IdGrupo && itemAux.IDGRUPOACTUAL == int.Parse(strIdGrupo))
                             select itemAux;
                if (querry != null && querry.Count() > 0)
                {
                    var nombregrupo = (from itemNombre in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                       where itemNombre.IDGRUPO == int.Parse(strIdGrupo)
                                       select itemNombre.DESCGRUPO).Distinct();
                    if (nombregrupo != null && nombregrupo.Count() > 0)
                        strGrupoActual = nombregrupo.First();

                    int idExclutente = 0;
                    if (querry.First().IDGRUPOACTUAL == int.Parse(strIdGrupo))
                        idExclutente = querry.First().IDGRUPEXCLUYENTE;
                    if (querry.First().IDGRUPEXCLUYENTE == int.Parse(strIdGrupo))
                        idExclutente = querry.First().IDGRUPOACTUAL;

                    var nombregrupoExc = (from itemNombre in objDatosRoles.dcRecuperarDatosParaABMRoles.lstArbolGrupo
                                          where itemNombre.IDGRUPO == idExclutente
                                          select itemNombre.DESCGRUPO).Distinct();

                    if (nombregrupoExc != null && nombregrupoExc.Count() > 0)
                        strGrupoExc = nombregrupoExc.First();

                    //"El grupo " & strGrupoActual & " es excluyente" & vbCrLf & "del grupo " & strGrupos(intI).Substring(strGrupos(intI).IndexOf("¤"c) + 1) & "."
                    mensaje = "El grupo " + strGrupoActual + " es excluyente " + "del grupo " + strGrupoExc;
                    return true;
                }
            }

            return false;
        }

        #endregion

        #endregion

        #region "Sistemas Bloqueados"

        /// <summary>
        /// [WebMethod] - Recupera los datos iniciales del formulario Sistemas Bloqueados
        /// </summary>
        /// <returns>Sistemas Bloqueados, Sistemas Desbloqueados, Usuarios Bloqueados, Usuarios Desbloqueados</returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSistemasBloqueados RecuperarSistBloqueados()
        {
            try
            {
                string idSistemaBloqueado = "";

                FSeguridad objFachada = new FSeguridad();
                RecuperarControles rc = new RecuperarControles();

                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();

                vSistemasBloqueados objRetorno = new vSistemasBloqueados();

                objDatosSistemasBloqueados = new vSessionDatosSistemasBloqueados();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.dcRecuperarSistBloqueados objResp =
                    objFachada.RecuperarSistBloqueados(-1, -1);

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_HABILITADOS objRemove = null;

                List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_HABILITADOS> lstSIST_HABILITADOS =
                    objResp.lstSE_SIST_HABILITADOS.OrderBy(sh => sh.DESCSISTEMA).ToList();

                List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS> lstSIST_BLOQUEADOS =
                    new List<Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS>();

                // Operaciones para obtener los sistemas Bloqueados y Desbloqueados
                var query = objResp.lstSE_SIST_BLOQUEADOS.GroupBy(s => s.IDSISTEMA)
                                                    .Select(sb => sb.ToList())
                                                    .ToList();

                if (objResp.lstSE_SIST_BLOQUEADOS.Count > 0 && objResp.lstSE_SIST_HABILITADOS != null)
                {
                    foreach (var sitBloqueado in query)
                    {
                        foreach (var sistemasHabilitados in objResp.lstSE_SIST_HABILITADOS)
                        {
                            if (sitBloqueado[0].IDSISTEMA == sistemasHabilitados.IDSISTEMA)
                            {
                                objRemove = sistemasHabilitados;
                                break;
                            }
                        }

                        lstSIST_HABILITADOS.Remove(objRemove);

                        COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS objSISTEMABLOQUEADO = new Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS();

                        objSISTEMABLOQUEADO.IDSISTEMA = sitBloqueado[0].IDSISTEMA;
                        objSISTEMABLOQUEADO.DESCSISTEMA = sitBloqueado[0].DESCSISTEMA;

                        lstSIST_BLOQUEADOS.Add(objSISTEMABLOQUEADO);
                    }
                    idSistemaBloqueado = objResp.lstSE_SIST_BLOQUEADOS[0].IDSISTEMA.ToString();
                }
                else
                {
                    idSistemaBloqueado = "-1";
                }

                objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS = objResp.lstSE_SIST_BLOQUEADOS;
                this.ActualizarUsuarios(idSistemaBloqueado, objResp.lstSE_USUARIOS, ref objRetorno);

                objRetorno.jsonsistemasbloqueados = rc.GenerarListBox(lstSIST_BLOQUEADOS, "DESCSISTEMA", "IDSISTEMA", "lstSistemasBloqueados");
                objRetorno.jsonsistemasdesbloqueados = rc.GenerarListBox(lstSIST_HABILITADOS, "DESCSISTEMA", "IDSISTEMA", "lstSistemasDesbloqueados");
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Se actualizan las listas de usuarios bloqueados y desbloqueados
        /// </summary>
        /// <param name="id">Id Sistema Bloqueado</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSistemasBloqueados ActualizarUsuarios(string id)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                COA.WebCipol.Presentacion.Utiles.cAuditoria.Limpiar();

                vSistemasBloqueados objRetorno = new vSistemasBloqueados();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.dcRecuperarSistBloqueados objResp = objFachada.RecuperarSistBloqueados(-1, -1);

                this.ActualizarUsuarios(id, objResp.lstSE_USUARIOS, ref objRetorno);

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Se actualizan las listas de usuarios bloqueados y desbloqueados
        /// </summary>
        /// <param name="id">Id Sistema Bloqueado</param>
        /// <param name="lstUSUARIOS">Lista de usuarios (Blqoueados y Desbloqeuados)</param>
        /// <param name="objRetorno">Objeto para bindear las listas</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        private void ActualizarUsuarios(string idSistemaBloqueado,
                                            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS> lst_SE_USUARIOS,
                                            ref vSistemasBloqueados objRetorno)
        {
            RecuperarControles rc = new RecuperarControles();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS> lstUSUARIOS_BLOQUEADOS =
                lst_SE_USUARIOS;

            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS> lstUSUARIOS_DESBLOQUEADOS =
                new List<Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS>();

            COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS objRemoveUsuario = null;

            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS> lstSIST_BLOQUEADOS =
                objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS;

            int countUSU = 0;

            if (lstSIST_BLOQUEADOS.Count > 0)
            {
                do
                {
                    foreach (var usuario in lst_SE_USUARIOS)
                    {
                        if (lstSIST_BLOQUEADOS[countUSU].IDUSUARIO == usuario.IDUSUARIO && usuario.IDUSUARIO != 0 && lstSIST_BLOQUEADOS[countUSU].IDSISTEMA.ToString() == idSistemaBloqueado)
                        {
                            lstUSUARIOS_DESBLOQUEADOS.Add(usuario);
                            objRemoveUsuario = usuario;
                            break;
                        }
                    }
                    lstUSUARIOS_BLOQUEADOS.Remove(objRemoveUsuario);
                } while (lstSIST_BLOQUEADOS[countUSU].IDSISTEMA.ToString() == lstSIST_BLOQUEADOS[countUSU++].IDSISTEMA.ToString() && countUSU < lstSIST_BLOQUEADOS.Count);
                lstUSUARIOS_BLOQUEADOS.RemoveAll(u => u.IDUSUARIO == 0);
            }
            else
            {   // Si no hay sistemas bloqueados entonces tampoco se deben mostar usuarios bloqueados.
                lstUSUARIOS_BLOQUEADOS.Clear();
            }
            objRetorno.jsonusuariosdesbloqueados = rc.GenerarListBox(lstUSUARIOS_DESBLOQUEADOS, "NOMBRES", "IDUSUARIO", "lstUsuariosDesbloqueados");//.CargarUsuariosDesbloqueados(lstUSUARIOS_DESBLOQUEADOS);
            objRetorno.jsonusuariosbloqueados = rc.GenerarListBox(lstUSUARIOS_BLOQUEADOS, "NOMBRES", "IDUSUARIO", "lstUsuariosBloqueados");// CargarUsuariosBloqueados(lstUSUARIOS_BLOQUEADOS);

        }

        ///// <summary>
        ///// Carga los usuarios Desbloqueados
        ///// </summary>
        ///// <param name="lst_SE_USUARIOS">Lista de usuarios desbloqueados</param>
        ///// <returns></returns>
        ///// <history>
        ///// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        ///// </history>
        //private string CargarUsuariosDesbloqueados(List<Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_USUARIOS> lst_SE_USUARIOS)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIListBoxGenerica objUI = new UIListBoxGenerica();
        //        UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


        //        ctl.datos = new DatosListBoxGenerico()
        //        {
        //            DataSource = lst_SE_USUARIOS,
        //            DataTextField = "NOMBRES",
        //            DataValueField = "IDUSUARIO",
        //            Height = new System.Web.UI.WebControls.Unit(160),
        //            Width = new System.Web.UI.WebControls.Unit(300),
        //            Id = "lstUsuariosDesbloqueados"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        ///// <summary>
        ///// Carga los Usuarios Bloqueados
        ///// </summary>
        ///// <param name="list">Lista de usuarios Bloqueados</param>
        ///// <returns></returns>
        ///// <history>
        ///// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        ///// </history>
        //private string CargarUsuariosBloqueados(Object list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIListBoxGenerica objUI = new UIListBoxGenerica();
        //        UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


        //        ctl.datos = new DatosListBoxGenerico()
        //        {
        //            DataSource = list,
        //            DataTextField = "NOMBRES",
        //            DataValueField = "IDUSUARIO",
        //            Height = new System.Web.UI.WebControls.Unit(160),
        //            Width = new System.Web.UI.WebControls.Unit(300),
        //            Id = "lstUsuariosBloqueados"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        ///// <summary>
        ///// Carga los Sistemas Desbloqueados
        ///// </summary>
        ///// <param name="list">Lista de Sistemas Desbloqueados</param>
        ///// <returns></returns>
        ///// <history>
        ///// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        ///// </history>
        //private string CargarSistemasDesbloqueados(Object list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIListBoxGenerica objUI = new UIListBoxGenerica();
        //        UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


        //        ctl.datos = new DatosListBoxGenerico()
        //        {
        //            DataSource = list,
        //            DataTextField = "DESCSISTEMA",
        //            DataValueField = "IDSISTEMA",
        //            Height = new System.Web.UI.WebControls.Unit(160),
        //            Width = new System.Web.UI.WebControls.Unit(300),
        //            Id = "lstSistemasDesbloqueados"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        ///// <summary>
        ///// Carga los Sistemas Bloqueados
        ///// </summary>
        ///// <param name="list">Lista de Sistemas Bloqueados</param>
        ///// <returns></returns>
        ///// <history>
        ///// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        ///// </history>
        //private string CargarSistemasBloqueados(Object list)
        //{
        //    DataContractJsonSerializer objSerializador = null;
        //    String strRtaJson;
        //    ElementoLista objLista = new ElementoLista();
        //    MemoryStream objMemoria = new MemoryStream();
        //    StringBuilder sb = new StringBuilder();

        //    try
        //    {
        //        UIListBoxGenerica objUI = new UIListBoxGenerica();
        //        UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");


        //        ctl.datos = new DatosListBoxGenerico()
        //        {
        //            DataSource = list,
        //            DataTextField = "DESCSISTEMA",
        //            DataValueField = "IDSISTEMA",
        //            Height = new System.Web.UI.WebControls.Unit(160),
        //            Width = new System.Web.UI.WebControls.Unit(300),
        //            Id = "lstSistemasBloqueados"
        //        };

        //        objUI.EnableEventValidation = false; objUI.EnableViewState = false;
        //        HtmlForm _form = new HtmlForm();
        //        objUI.Controls.Add(_form);
        //        _form.Controls.Add(ctl);
        //        StringWriter writer = new StringWriter();
        //        Server.Execute(objUI, writer, false);
        //        sb.Append(writer.ToString());
        //        writer.Close();

        //        objLista.Lista = Server.HtmlEncode(sb.ToString());
        //        objSerializador = new DataContractJsonSerializer(objLista.GetType());
        //        objSerializador.WriteObject(objMemoria, objLista);
        //        strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

        //        objMemoria.Close();
        //        return strRtaJson;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);

        //    }
        //}

        /// <summary>
        /// [WebMethod] - Pasa un sistema de Desbloqueado a Bloqueado - Auditoria de la acción
        /// </summary>
        /// <param name="Id">Id del sistema</param>
        /// /// <param name="desc">Descripción del sistema</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarSistema(int Id, string desc)
        {
            try
            {
                this.AsignarSistema(Id, desc);
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha ingresado el sistema: " + desc);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa un sistema de Bloqueado a Desbloqueado - Auditoria de la acción
        /// </summary>
        /// <param name="Id">Id del sistema</param>
        /// <param name="desc">Descripción del sistema</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarSistema(int Id, string desc)
        {
            try
            {
                this.DesasignarSistema(Id);
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha eliminado el sistema: " + desc);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa Todos los sistemas a bloqueados - Auditoria de la acción
        /// </summary>
        /// <param name="obj">Objeto que representa un sistema con sus propiedades</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarSistemasTodos(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                    {
                        this.AsignarSistema(item.Id, item.nombre);
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha ingresado el sistema " + item.nombre.Trim());
                    }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa Todos los sistemas a desbloqueados - Auditoria de la acción
        /// </summary>
        /// <param name="obj">Objeto que representa un sistema con sus propiedades</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarSistemasTodos(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                    {
                        this.DesasignarSistema(item.Id);
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha eliminado el sistema: " + item.nombre.Trim());
                    }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Desbloquea un ususario para un sistema - Auditoria de la acción
        /// </summary>
        /// <param name="Id">Id de susuario</param>
        /// <param name="nombre">Nombre de ususario</param>
        /// <param name="IdSistemaBloqueado">Id del sistema bloqueado</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarUsuario(int Id, string nombre, int IdSistemaBloqueado)
        {
            try
            {
                this.AsignarUsuario(Id, nombre, IdSistemaBloqueado);
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha ingresado el usuario: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// [WebMethod] - Desbloquea todos los usuarios para un sistema - Auditoria de la acción
        /// </summary>
        /// <param name="obj">Objeto con los datos de los usuarios y el sistema bloqueado</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string asignarUsuariosTodos(List<SistemaBloqueado> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                    {
                        this.AsignarUsuario(item.Id, item.nombre.Trim(), item.IdSistemaBloqueado);
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha ingresado el usuaio " + item.nombre.Trim());
                    }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// [WebMethod] - Bloquea un usuario para un sistema - Auditoria de la acción
        /// </summary>
        /// <param name="Id">Id de susuario</param>
        /// <param name="nombre">Nombre de ususario</param>
        /// <param name="IdSistemaBloqueado">Id del sistema bloqueado</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarUsuario(int Id, string nombre, int IdSistemaBloqueado)
        {
            try
            {
                this.DesasignarUsuario(Id, IdSistemaBloqueado);

                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha eliminado el usuario: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Bloquea todos los usuarios para un sistema - Auditoria de la acción
        /// </summary>
        /// <param name="obj">Objeto con los datos de los usuarios y el sistema bloqueado</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string desasignarUsuariosTodos(List<SistemaBloqueado> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                    {
                        this.DesasignarUsuario(item.Id, item.IdSistemaBloqueado);
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha eliminado el usuaio " + item.nombre.Trim());
                    }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Guarda los cambios realizados
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSistemasBloqueados AdministrarSistemasBloqueados()
        {
            vSistemasBloqueados objRetorno = new vSistemasBloqueados();
            try
            {
                FSeguridad objFachada = new FSeguridad();
                Entidades.ClasesWs.dcInsertarSistBloqueados.dcInsertarSistBloqueados objDatos = new Entidades.ClasesWs.dcInsertarSistBloqueados.dcInsertarSistBloqueados();


                foreach (var item in objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS)
                {
                    objDatos.lstSE_SIST_BLOQUEADOS.Add(new Entidades.ClasesWs.dcInsertarSistBloqueados.SE_SIST_BLOQUEADOS()
                    {
                        IDSISTEMA = item.IDSISTEMA,
                        DESCSISTEMA = item.DESCSISTEMA,
                        IDUSUARIO = item.IDUSUARIO,
                        NOMBRES = item.NOMBRES
                    });
                }

                int res = objFachada.InsertarSistBloqueados(objDatos);

                if (res > 0)
                {
                    objRetorno.ResultadoEjecucion = true;
                    return objRetorno;
                }
                else
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "No se pudieron guardar los datos de sistemas bloqueados";
                    return objRetorno;
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region "Variables de Sesion"

        /// <summary>
        /// variable de sesiòn en donde se mantienen todas las conbinaciones realizadas por el ususario antes de guardar los cambios
        /// </summary>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14413
        /// </history>
        private vSessionDatosSistemasBloqueados objDatosSistemasBloqueados
        {
            get
            {
                return (vSessionDatosSistemasBloqueados)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.SISTEMAS_BLOQUEADOS];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.SISTEMAS_BLOQUEADOS] = value;
            }
        }

        #endregion

        #region Metodos Privados para mantener la variable de sesión


        /// <summary>
        /// Guarda en sesiòn el sistema bloqueado
        /// </summary>
        /// <param name="Id">Id de sistema/param>
        /// <param name="desc">Descripciòn del sistema</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 
        /// </history>
        private void AsignarSistema(int Id, string desc)
        {
            Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS sistemaBloqueado = new Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS();

            sistemaBloqueado.IDSISTEMA = Id;
            sistemaBloqueado.DESCSISTEMA = desc;
            sistemaBloqueado.IDUSUARIO = 0;
            sistemaBloqueado.NOMBRES = "Super Usuario";

            objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS.Add(sistemaBloqueado);
        }

        /// <summary>
        /// Elimina de sesion el sistema bloqueado 
        /// </summary>
        /// <param name="Id">Id de sistema/param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 
        /// </history>
        private void DesasignarSistema(int Id)
        {
            objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS.RemoveAll(s => s.IDSISTEMA == Id);
        }

        /// <summary>
        /// Elimina de sesion el usuario bloqueado 
        /// </summary>
        /// <param name="Id">Id de sistema/param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 
        /// </history>   
        private void DesasignarUsuario(int Id, int IdSistemaBloqueado)
        {
            objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS.RemoveAll(s => s.IDSISTEMA == IdSistemaBloqueado && s.IDUSUARIO == Id);
        }

        /// <summary>
        /// Guarda en sesion el usuario desbloqueado 
        /// </summary>
        /// <param name="Id">Id de sistema/param>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 
        /// </history>
        private void AsignarUsuario(int Id, string nombre, int IdSistemaBloqueado)
        {
            Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS sistemaBloqueado = new Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS();
            string descSistemaBloqueado = (from sb in objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS
                                           where sb.IDSISTEMA == IdSistemaBloqueado
                                           select sb.DESCSISTEMA).FirstOrDefault();

            sistemaBloqueado.DESCSISTEMA = descSistemaBloqueado;
            sistemaBloqueado.IDSISTEMA = IdSistemaBloqueado;
            sistemaBloqueado.IDUSUARIO = Id;
            sistemaBloqueado.NOMBRES = nombre;

            objDatosSistemasBloqueados.lstSISTEMAS_BLOQUEADOS.Add(sistemaBloqueado);
        }
        #endregion


        #endregion

        #region "Usuarios"

        #region "usuario"
        #region "Variables sessiones"
        private vSessionDatosUsuarios objDatosUsuarios
        {
            get
            {
                if (HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIOS_DATOSUSUARIOS] == null)
                    HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIOS_DATOSUSUARIOS] = new vSessionDatosUsuarios();
                return (vSessionDatosUsuarios)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIOS_DATOSUSUARIOS];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIOS_DATOSUSUARIOS] = value;
            }
        }
        #endregion

        //RecuperarUsuariosCarga
        //RecuperarUsuarios

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioCarga RecuperarUsuariosCarga()
        {
            try
            {
                vUsuarioCarga objRetorno = new vUsuarioCarga();
                Fachada.FSeguridad objFachada = new FSeguridad();


                COA.WebCipol.Entidades.ClasesWs.Filtros.dcFiltrosUsuarios fil = new Entidades.ClasesWs.Filtros.dcFiltrosUsuarios();
                //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos (en este caso viaja vacia)
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.dcRecuperarDatosParaGrillaABMUsuarios objresp = objFachada.RecuperarDatosParaGrillaABMUsuarios(fil);

                objDatosUsuarios.lstComposicionDeRoles = objresp.lstComposicionDeRoles;
                objDatosUsuarios.lstSE_PARAMETROS = objresp.lstSE_PARAMETROS;
                objDatosUsuarios.lstSE_TERMINALES = objresp.lstSE_TERMINALES;
                objDatosUsuarios.lstSist_Usuarios = objresp.lstSist_Usuarios;
                objDatosUsuarios.lstSE_Horarios_Usuario = objresp.lstSE_Horarios_Usuario;
                objDatosUsuarios.lstSE_GRUPO_EXCLUSION = objresp.lstSE_GRUPO_EXCLUSION;


                //objRetorno.jsonusuarios = RecuperarUsuariosUsuario("", "TODOS", true, false, false, false, objresp);
                //Carga los valores del combo estado.
                List<Item> lstDatos = new List<Item>();
                lstDatos.Add(new Item() { Valor = 0, Descripcion = "Todos" });
                lstDatos.Add(new Item() { Valor = 1, Descripcion = "Si" });
                lstDatos.Add(new Item() { Valor = 2, Descripcion = "No" });

                objRetorno.jsoncboestado = recuperarCboEstadosUsuario(lstDatos);
                objRetorno.jsoncboarea = recuperarCboAreasUsuario(objresp.lstKAREAS);
                objRetorno.jsoncbofiltroareaterminal = RecuperarCboAreasFiltroTerminal(objresp.lstKAREAS);
                objRetorno.jsoncbotipodocumento = recuperarCboTipoDocumentoUsuario(objresp.lstKDocumentos);

                objRetorno.blnNombreDominio = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio;
                objRetorno.blnNombreDominio = string.IsNullOrEmpty(ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio);

                if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario > 0)
                {
                    /*Se controlan las tareas asignadas al usuario*/
                    objRetorno.permiso.blnNuevoVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1020");
                    objRetorno.permiso.blnModificarVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1021");
                    objRetorno.permiso.blnEliminarVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1022");
                    objRetorno.permiso.blnTerminalVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1023");
                    objRetorno.permiso.blnHorarioVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1024");
                    objRetorno.permiso.blnAsignarRolVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1025");
                    objRetorno.permiso.blnDesasignarRolVisible = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1026");
                }
                else
                {
                    objRetorno.permiso.blnNuevoVisible =
                    objRetorno.permiso.blnModificarVisible =
                    objRetorno.permiso.blnEliminarVisible =
                    objRetorno.permiso.blnTerminalVisible =
                    objRetorno.permiso.blnHorarioVisible =
                    objRetorno.permiso.blnAsignarRolVisible =
                    objRetorno.permiso.blnDesasignarRolVisible = true;
                }

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.Sist_Usuarios>
        RecuperarUsuarios(vUsuarioFiltro obj)//string filtro, string filtrobaja, bool chkUsu, bool chkNombre, bool chkArea, bool chkSubCadenas)
        {

            try
            {
                Fachada.FSeguridad objFachada = new FSeguridad();

                //[Miguelp]               31/08/2016               TFS WI : 7674 - Se creo una entidad para poder transportar los filtros hastan la capa de datos
                COA.WebCipol.Entidades.ClasesWs.Filtros.dcFiltrosUsuarios fil = new Entidades.ClasesWs.Filtros.dcFiltrosUsuarios
                {
                    chkArea = obj.chkArea,
                    chkNombre = obj.chkNombre,
                    chkSubCadenas = obj.chkSubCadenas,
                    chkUsu = obj.chkUsu,
                    filtro = obj.filtro,
                    filtrobaja = obj.filtrobaja
                };

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.dcRecuperarDatosParaGrillaABMUsuarios objresp = objFachada.RecuperarDatosParaGrillaABMUsuarios(fil);
                //return RecuperarUsuariosUsuario(filtro, filtrobaja, chkUsu, chkNombre, chkArea, chkSubCadenas, objresp);

                var query = from item in objresp.lstSist_Usuarios
                            where (
                                    (obj.chkArea
                                        && ((obj.chkSubCadenas && item.NombreArea.Trim().ToUpper().Contains(obj.filtro.Trim().ToUpper()))
                                            || (!obj.chkSubCadenas && item.NombreArea.Trim().ToUpper().StartsWith(obj.filtro.Trim().ToUpper())))
                                    )
                                    ||
                                    (obj.chkNombre
                                        && ((obj.chkSubCadenas && item.Nombres.Trim().ToUpper().Contains(obj.filtro.Trim().ToUpper()))
                                            || (!obj.chkSubCadenas && item.Nombres.Trim().ToUpper().StartsWith(obj.filtro.Trim().ToUpper())))
                                    )
                                    ||
                                    (obj.chkUsu
                                        && ((obj.chkSubCadenas && item.Usuario.Trim().ToUpper().Contains(obj.filtro.Trim().ToUpper()))
                                            || (!obj.chkSubCadenas && item.Usuario.Trim().ToUpper().StartsWith(obj.filtro.Trim().ToUpper())))
                                    )
                                   )
                                    &&
                                    ((obj.filtrobaja.Trim().ToUpper().Equals("SI") && item.FechaBaja != null)
                                    || (obj.filtrobaja.Trim().ToUpper().Equals("NO") && item.FechaBaja == null)
                                    || (obj.filtrobaja.Trim().ToUpper().Equals("TODOS")))
                            select item;

                //Ordenamiento
                return query.ToList().OrderBy(v => v.Usuario).ToList();
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="blnModificar"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14414
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// [GonzaloP]          [viernes, 15 de julio de 2016]       Work-Item: 7193
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioGetItem RecuperarElementoUsuario(int Id, bool blnModificar, string TipoAccion)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.dcRecuperarDatosParaABMUsuarios objResp = objFachada.RecuperarDatosParaABMUsuarios(Id);
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Sist_Usuarios obj = null;

                CifrarDatos.TresDES objEncriptarNET;
                objEncriptarNET = new CifrarDatos.TresDES();
                objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
                objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;

                objDatosUsuarios.objDatosUsuario = objResp;
                //martinv: leer el usuario de sessión . gobjUsuarioCipol.IDUsuario <> 0 
                if (blnModificar && Id == 0)
                    return new vUsuarioGetItem() { ResultadoEjecucion = false, MensajeError = "Imposible modificar el usuario Master." };

                obj = objResp.lstSist_Usuarios[0];

                if (obj != null)
                {
                    //Si el usuario se encuentra dado de baja no se permite modificar.
                    if (blnModificar && obj.FechaBaja != null)
                        return new vUsuarioGetItem() { ResultadoEjecucion = false, MensajeError = "El usuario se encuentra dado de Baja. Imposible continuar." };
                    //Validación de tarea 1027 - Administración de Usuarios - No Modificar Usuarios con tareas de CIPOL
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario > 0 && ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1027") && TipoAccion.Equals("M"))
                    {
                        if (objResp.lstRoles_Composicion.Select(p => p.idSistema = 1).Count() > 0)
                            return new vUsuarioGetItem() { ResultadoEjecucion = false, MensajeError = "No esta autorizado a modificar usuarios que poseen permisos de Seguridad." };
                    }

                    return new vUsuarioGetItem()
                    {
                        ResultadoEjecucion = true,
                        elemento = new vUsuario()
                        {
                            ResultadoEjecucion = true,
                            ALIAS_USUARIO = obj.ALIAS_USUARIO,
                            CantIntInvUsoCta = obj.CantIntInvUsoCta,
                            Comentario = obj.Comentario,
                            CtaBloqueada = obj.CtaBloqueada,
                            CtaBloqueadaDes = obj.CtaBloqueadaDes,
                            CtaBloqueadaDesLetra = obj.CtaBloqueadaDesLetra,
                            blnCtaBloqueada = objEncriptarNET.Criptografia(Accion.Desencriptacion, obj.CtaBloqueada).Equals("1"),
                            Domicilio = obj.Domicilio,
                            Email = obj.Email,
                            strFechaAlta = vUtiles.DateToString(obj.FechaAlta),
                            strFechaBaja = vUtiles.DateToString(obj.FechaBaja),
                            strFechaBloqueo = vUtiles.DateToString(obj.FechaBloqueo),
                            //FechaUltUsoCta = obj.FechaUltUsoCta,
                            FICTICIA = obj.FICTICIA,
                            ForzarCambio = objEncriptarNET.Criptografia(Accion.Desencriptacion, obj.ForzarCambio),
                            ForzarCambioDes = objEncriptarNET.Criptografia(Accion.Desencriptacion, obj.ForzarCambioDes),
                            blnIntegradoAlDominio = obj.IntegradoAlDominio,
                            //blnMostrarForzarCambio = (!(obj.IntegradoAlDominio) || !string.IsNullOrEmpty(ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio)),
                            idArea = obj.idArea,
                            IdTipoDoc = obj.IdTipoDoc,
                            IdUsuario = obj.IdUsuario,
                            NombreArea = obj.NombreArea,
                            Nombres = obj.Nombres,
                            NroDocumento = obj.NroDocumento,
                            TipoAbreviado = obj.TipoAbreviado,
                            Usuario = obj.Usuario,
                            contrasenia = new string('*', 10),
                            repetircontrasenia = new string('*', 10)
                        },
                        jsonterminaleshabilitadas = CargarTerminalesHabilitadas(),
                        jsonterminalesnohabilitadas = CargarTerminalesNOHabilitadas(),
                        jsontareasdisponibles = recuperarTreeViewuUsuarioRoles(objDatosUsuarios.lstComposicionDeRoles),
                        jsontareasasignadas = recuperarTreeViewuUsuarioRolesAsignados(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion),
                        jsoncbotareasasignadas = recuperarCboUsuarioRolesAsignados(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion)
                    };

                }
                return null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioGetItem NewElementoUsuario()
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.dcRecuperarDatosParaABMUsuarios objResp = new dcRecuperarDatosParaABMUsuarios();

                objDatosUsuarios.objDatosUsuario = objResp;

                return new vUsuarioGetItem()
                    {
                        ResultadoEjecucion = true,
                        jsonterminaleshabilitadas = CargarTerminalesHabilitadas(),
                        jsonterminalesnohabilitadas = CargarTerminalesNOHabilitadas(),
                        jsontareasasignadas = recuperarTreeViewuUsuarioRolesAsignados(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion)
                        //Ver si conviene traer las tareas asignadas.???.
                    };
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioAdministrar AdministrarUsuario(vUsuario obj)
        {
            try
            {
                Fachada.FSeguridad objFachada = new FSeguridad();
                vUsuarioAdministrar objRetorno = new vUsuarioAdministrar();

                CifrarDatos.TresDES objEncriptarNET;
                objEncriptarNET = new CifrarDatos.TresDES();
                objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
                objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;


                /*Validaciones*/
                const string ContraseniaIntegrado = "CIPOL Integrado";
                //Valida el nombre del usuario.
                if (string.IsNullOrEmpty(obj.Usuario.Trim()))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "La <strong>CUENTA DE USUARIO</strong> es un dato obligatorio.";
                    return objRetorno;
                }

                if (string.IsNullOrEmpty(obj.Nombres))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <strong>NOMBRE Y APELLIDO</strong> es un dato obligatorio.";
                    return objRetorno;
                }
                if (obj.idArea < 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <strong>ÁREA</strong> es un dato obligatorio.";
                    return objRetorno;
                }

                if (obj.CtaBloqueadaDesLetra.Equals("S"))
                {

                    if (String.IsNullOrWhiteSpace(obj.strFechaBloqueo))
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "La <strong>FECHA DE BLOQUEO HASTA</strong> es un dato obligatorio.";
                        return objRetorno;
                    }

                    try
                    {
                        vUtiles.StringToDateNull(obj.strFechaBloqueo);
                    }
                    catch
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "La <strong>FECHA DE BLOQUEO HASTA</strong> es inválida.";
                        return objRetorno;
                    }



                    if (vUtiles.StringToDateNull(obj.strFechaBloqueo) <= DateTime.Today)
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "La <strong>FECHA DE BLOQUEO HASTA</strong> debe ser mayor a la fecha actual.";
                        return objRetorno;
                    }

                }

                if (ValidarLoginSSO())
                {
                    obj.contrasenia = "Login262SSO"; 
                    obj.repetircontrasenia = "Login262SSO";
                }
                else
                {
                    if (obj.blnIntegradoAlDominio && (!obj.update || obj.IdUsuario > 0))
                    {
                        if (BuscarUsuarioDominio(obj.Usuario))
                        {
                            objRetorno.ResultadoEjecucion = false;
                            objRetorno.MensajeError = "El usuario no pertenece al Dominio " + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio + " o en este momento no se puede establecer conexión con el Dominio. Verifique.";
                            return objRetorno;
                        }
                        obj.contrasenia = ContraseniaIntegrado.Trim(); //Máx 15 caracteres
                        obj.repetircontrasenia = ContraseniaIntegrado.Trim();
                    }
                    if (string.IsNullOrEmpty(obj.contrasenia))
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "La <strong>CONTRASEÑA</strong> es un dato obligatorio.";
                        return objRetorno;
                    }

                    if (string.IsNullOrEmpty(obj.repetircontrasenia))
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "Debe ingresar nuevamente la <strong>CONTRASEÑA</strong>.";
                        return objRetorno;
                    }

                    //objDatos.
                    if (!obj.update || obj.blnCambioContrasenia)
                    {
                        if (obj.contrasenia.Contains('*'))
                        {
                            objRetorno.ResultadoEjecucion = false;
                            objRetorno.MensajeError = "El símbolo <strong>' * '</strong> no es un caracter permitido.";
                            return objRetorno;
                        }

                        byte mbytLongClaveMin;
                        string[] strColumna4 = Strings.Split(objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, objDatosUsuarios.lstSE_PARAMETROS[0].COLUMNA4), Constantes.gstrSepParam);
                        mbytLongClaveMin = byte.Parse(strColumna4[3]);

                        if (obj.contrasenia.Trim().Length < mbytLongClaveMin)
                        {
                            objRetorno.ResultadoEjecucion = false;
                            objRetorno.MensajeError = "La <strong>CONTRASEÑA</string> especificada debe tener una longitud mínima de " + Convert.ToString(mbytLongClaveMin) + " caracteres.";
                            return objRetorno;
                        }

                        if (!(obj.contrasenia.Trim().Equals(obj.repetircontrasenia.Trim())))
                        {
                            objRetorno.ResultadoEjecucion = false;
                            objRetorno.MensajeError = "La nueva <strong>CONTRASEÑA</strong> no fué confirmada correctamente. Asegurese de que la <strong>CONTRASEÑA</strong> de confirmación coincida con la nueva.";
                            return objRetorno;
                        }
                        if (!obj.blnIntegradoAlDominio)
                        {
                            string strContrasenia = obj.contrasenia;
                            string mensaje = ValidarSeguridadContrasenia(ref strContrasenia, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio, ManejoSesion.gudParam.NivelSeguridadContraseña);

                            if (mensaje.Trim() != "")
                            {
                                //MensajeUsuario(50)
                                objRetorno.ResultadoEjecucion = false;
                                objRetorno.MensajeError = mensaje;
                                return objRetorno;
                            }

                        }
                    }
                }

                //if (obj.IdUsuario != 0)
                //{
                //todo:martinv -> count roles
                if (objDatosUsuarios.objDatosUsuario.lstRoles_Composicion == null || objDatosUsuarios.objDatosUsuario.lstRoles_Composicion.Count() == 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El usuario no posee ningún Rol asignado.";
                    return objRetorno;
                }
                //}
                //martinv\shirlih - No se validan las contraseñas anteriores 
                //if (!obj.blnIntegradoAlDominio)
                //{


                //    byte mbytCantHistorial;
                //    string[] strColumna4 = Strings.Split(objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, objDatosUsuarios.lstSE_PARAMETROS[0].COLUMNA4), Constantes.gstrSepParam);
                //    mbytCantHistorial = byte.Parse(strColumna4[7]);

                //    if (obj.update && obj.blnCambioContrasenia && mbytCantHistorial > 0)
                //    {
                //        foreach (var item in objDatosUsuarios.objDatosUsuario.lstSE_HISTORIAL_USUARIO)
                //        {
                //            if (item.SINONIMO == COA.CifrarDatos.Hash.CrearHash(COA.CifrarDatos.TipoHash.SHA1, obj.contrasenia, obj.IdUsuario.ToString()))
                //            {
                //                objRetorno.ResultadoEjecucion = false;
                //                objRetorno.MensajeError = "La <strong>CONTRASEÑA</strong> especificada no cumple los requisitos establecidos.";
                //                return objRetorno;
                //            }
                //        }
                //    }

                //}

                if (obj.ALIAS_USUARIO.Trim().Length > 0)
                {
                    if ((from item in objDatosUsuarios.lstSist_Usuarios
                         where item.IdUsuario != obj.IdUsuario && item.ALIAS_USUARIO.Trim().Equals(obj.ALIAS_USUARIO.Trim())
                         select item).Count() > 0)
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "El <strong>ALIAS</strong> ingresado lo posee otro usuario. Imposible continuar";
                        return objRetorno;
                    }
                }

                if (obj.Email.Trim().Length > 0 && !ValidarEmail(obj.Email.Trim()))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El <string>EMAIL</strong> ingresado no es correcto. Imposible continuar.";
                    return objRetorno;
                }

                //Fin de las validaciones.
                COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.dcAdministrarAbmUsuariosIN objDatos = new Entidades.ClasesWs.dcAdministrarAbmUsuarios.dcAdministrarAbmUsuariosIN();

                byte mbytVencClave;
                string[] strColumna = Strings.Split(objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Desencriptacion, objDatosUsuarios.lstSE_PARAMETROS[0].COLUMNA4), Constantes.gstrSepParam);
                mbytVencClave = byte.Parse(strColumna[4]);

                if (obj.update)
                {
                    if (ValidarLoginSSO())
                    {
                        obj.blnCambioContrasenia = false;
                    }
                    else
                    {
                        if (objDatosUsuarios.objDatosUsuario.lstSist_Usuarios[0].IntegradoAlDominio != obj.blnIntegradoAlDominio)
                            obj.blnCambioContrasenia = true;
                    }
                    objDatos.lstSist_Usuarios.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.Sist_Usuarios()
                    {
                        IdUsuario = obj.IdUsuario,
                        Usuario = obj.Usuario.Trim().ToLower(),
                        Nombres = obj.Nombres.Trim(),
                        Domicilio = obj.Domicilio.Trim(),
                        Email = obj.Email.Trim(),
                        ALIAS_USUARIO = obj.ALIAS_USUARIO.Trim(),
                        IdTipoDoc = obj.IdTipoDoc,
                        NroDocumento = obj.NroDocumento,
                        idArea = obj.idArea,
                        ForzarCambio = obj.ForzarCambio,
                        ForzarCambioDes = obj.ForzarCambioDes,
                        CtaBloqueada = obj.CtaBloqueada,
                        CtaBloqueadaDes = obj.CtaBloqueadaDes,
                        CtaBloqueadaDesLetra = obj.CtaBloqueadaDesLetra,
                        CantIntInvUsoCta = obj.CantIntInvUsoCta,
                        FechaBloqueo = (obj.blnCtaBloqueada) ? vUtiles.StringToDateNull(obj.strFechaBloqueo) : null,
                        Comentario = (string.IsNullOrEmpty(obj.Comentario)) ? " " : obj.Comentario.Trim(),
                        NombreArea = obj.NombreArea,
                        FICTICIA = "N",
                        TipoAbreviado = obj.TipoAbreviado,
                        IntegradoAlDominio = obj.blnIntegradoAlDominio
                    });
                    //Si la cuenta estaba bloqueda y fué desbloqueada.
                    if (objEncriptarNET.Criptografia(Accion.Desencriptacion, objDatosUsuarios.objDatosUsuario.lstSist_Usuarios[0].CtaBloqueada).Equals("1") && !obj.blnCtaBloqueada)
                    {
                        objDatos.lstSist_Usuarios[0].CantIntInvUsoCta = "0";
                        objDatos.lstSist_Usuarios[0].BLNFECHADESBLOQUEO = true;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(objDatosUsuarios.objDatosUsuario.lstSist_Usuarios[0].CantIntInvUsoCta))
                            objDatos.lstSist_Usuarios[0].CantIntInvUsoCta = "0";
                        else
                            objDatos.lstSist_Usuarios[0].CantIntInvUsoCta = objEncriptarNET.Criptografia(Accion.Desencriptacion, objDatosUsuarios.objDatosUsuario.lstSist_Usuarios[0].CantIntInvUsoCta);
                    }


                    //Si se bloqueó la cuenta.
                    if (obj.blnCtaBloqueada)
                        objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM()
                        {
                            CambioContrasenia = (obj.blnCambioContrasenia) ? "SI" : "NO",
                            MensajesAuditoria = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(610, Usuario: obj.Usuario.Trim(), UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login),
                            DiasVencimientoDeClave = mbytVencClave.ToString()
                        });

                    if (obj.blnCambioContrasenia)
                    {
                        objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM()
                        {
                            CambioContrasenia = "SI",
                            MensajesAuditoria = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(620, Usuario: obj.Usuario.Trim(), UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login),
                            DiasVencimientoDeClave = mbytVencClave.ToString()
                        });

                        if (objDatosUsuarios.objDatosUsuario.lstSE_HISTORIAL_USUARIO != null && objDatosUsuarios.objDatosUsuario.lstSE_HISTORIAL_USUARIO.Count() > 0)
                        {
                            objDatos.lstSE_HISTORIAL_USUARIO.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_HISTORIAL_USUARIO()
                            {
                                ORDEN = 0,
                                FechaVencimiento = new DateTime(1900, 1, 1),
                                SINONIMO = CifrarDatos.Hash.CrearHash(CifrarDatos.TipoHash.SHA1, obj.contrasenia, objDatosUsuarios.objDatosUsuario.lstSist_Usuarios[0].IdUsuario.ToString())
                            });
                        }
                        else
                        {
                            objDatos.lstSE_HISTORIAL_USUARIO.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_HISTORIAL_USUARIO()
                            {
                                ORDEN = 0,
                                FechaVencimiento = null,
                                SINONIMO = CifrarDatos.Hash.CrearHash(CifrarDatos.TipoHash.SHA1, obj.contrasenia, objDatosUsuarios.objDatosUsuario.lstSist_Usuarios[0].IdUsuario.ToString())
                            });
                        }
                    }
                    else
                    {
                        objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM()
                        {
                            CambioContrasenia = "NO",
                            MensajesAuditoria = "",
                            DiasVencimientoDeClave = mbytVencClave.ToString()
                        });
                    }

                    objDatos.lstSist_Usuarios[0].CantIntInvUsoCta = objEncriptarNET.Criptografia(Accion.Encriptacion, objDatos.lstSist_Usuarios[0].CantIntInvUsoCta);
                }
                else
                {
                    objDatos.lstSist_Usuarios.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.Sist_Usuarios()
                    {
                        IdUsuario = -1,
                        Usuario = obj.Usuario.Trim().ToLower(),
                        Nombres = obj.Nombres.Trim(),
                        NombreArea = obj.NombreArea,
                        Domicilio = obj.Domicilio.Trim(),
                        TipoAbreviado = obj.TipoAbreviado,
                        NroDocumento = obj.NroDocumento,
                        ForzarCambio = obj.ForzarCambio,
                        ForzarCambioDes = obj.ForzarCambioDes,
                        CtaBloqueada = obj.CtaBloqueada,
                        CtaBloqueadaDes = obj.CtaBloqueadaDes,
                        CtaBloqueadaDesLetra = obj.CtaBloqueadaDesLetra,
                        Comentario = (string.IsNullOrEmpty(obj.Comentario.Trim())) ? " " : obj.Comentario,
                        //Identificar si estaba bloqueda y fué desbloqueda.
                        //FechaBloqueo = (obj.blnCtaBloqueada) ? obj.FechaBloqueo : null,
                        FICTICIA = "N",
                        IdTipoDoc = obj.IdTipoDoc,
                        idArea = obj.idArea,
                        CantIntInvUsoCta = objEncriptarNET.Criptografia(COA.CifrarDatos.Accion.Encriptacion, "0"),
                        ALIAS_USUARIO = obj.ALIAS_USUARIO.Trim(),
                        IntegradoAlDominio = obj.blnIntegradoAlDominio,
                        Email = obj.Email.Trim()
                    });

                    //NO hace falta hacer nada con el historial de usuaios.
                    objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM()
                    {
                        CambioContrasenia = (obj.blnCambioContrasenia) ? "SI" : "NO",
                        MensajesAuditoria = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(750, Usuario: obj.Usuario.Trim(), UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login),
                        DiasVencimientoDeClave = mbytVencClave.ToString()
                    });

                    objDatos.lstSE_HISTORIAL_USUARIO.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_HISTORIAL_USUARIO()
                    {
                        ORDEN = 0,
                        FechaVencimiento = null,
                        SINONIMO = obj.contrasenia
                    });

                }

                if (!obj.update || obj.blnCambioContrasenia)
                {
                    //Ingreso la nueva clave
                    objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM()
                    {
                        CambioContrasenia = (obj.blnCambioContrasenia) ? "SI" : "NO",
                        MensajesAuditoria = "",
                        DiasVencimientoDeClave = mbytVencClave.ToString()
                    });
                }

                if (obj.blnRolParaUsuarioGuardado)
                {
                    objDatos.lstParametrosDeABM.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.ParametrosDeABM()
                    {
                        CambioContrasenia = (obj.blnCambioContrasenia) ? "SI" : "NO",
                        MensajesAuditoria = COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(770, Usuario: obj.Usuario.Trim(), UsuarioAdm: ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login, NuevoValor: COA.WebCipol.Presentacion.Utiles.cAuditoria.Recuperar_Auditar_Cambios("[ Detalle de Roles ]")),
                        DiasVencimientoDeClave = mbytVencClave.ToString()
                    });
                }
                //Carga las terminales bloqueadas.
                if (objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario != null)
                    foreach (var item in objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario)
                    {
                        objDatos.lstSE_Term_Usuario.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_Term_Usuario()
                        {
                            IdTerminal = item.IdTerminal,
                            CODTERMINAL = item.CODTERMINAL,
                            idArea = item.idArea
                        });
                    }

                //Carga las tareas asignadas por rol.
                if (obj.lsttareas != null && obj.lsttareas.Count() > 0)
                {
                    foreach (var item in obj.lsttareas)
                    {
                        var query = from itemAux in objDatosUsuarios.lstComposicionDeRoles
                                    where itemAux.IdRol == int.Parse(item.Id.Split('/')[0])
                                    && itemAux.IdGrupo == int.Parse(item.Id.Split('/')[1])
                                    && itemAux.IdSistema == int.Parse(item.Id.Split('/')[2])
                                    && itemAux.IdTarea == int.Parse(item.Id.Split('/')[3])
                                    select itemAux;
                        if (query != null && query.Count() > 0)
                        {

                            objDatos.lstRoles_Composicion.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.Roles_Composicion()
                            {
                                IdRol = query.First().IdRol,
                                DescripcionPerfil = query.First().DescripcionPerfil,
                                IdGrupo = query.First().IdGrupo,
                                DescGrupo = query.First().DescGrupo,
                                idSistema = query.First().IdSistema,
                                DescSistema = query.First().DescSistema,
                                idTarea = query.First().IdTarea,
                                DescripcionTarea = query.First().DescripcionTarea,
                                TareaInhibida = objEncriptarNET.Criptografia(Accion.Encriptacion, (item.asignada) ? "N" : "S")
                            });
                        }
                    }
                }

                //foreach (var item in objDatosUsuarios.objDatosUsuario.lstSE_Horarios_Usuario)
                foreach (var item in obj.lstHorarios)
                {
                    objDatos.lstSE_Horarios_Usuario.Add(new Entidades.ClasesWs.dcAdministrarAbmUsuarios.SE_Horarios_Usuario()
                    {
                        idDia = Convert.ToInt32(item.idDia),
                        IdHorario = Convert.ToInt32(item.idHorario)
                    });
                }


                COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios.dcAdministrarAbmUsuarios objResultado = objFachada.AdministrarAbmUsuarios(objDatos);

                if (objResultado.intRetorno == 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = objResultado.pstrError;
                    return objRetorno;
                }
                else if (objResultado.intRetorno < 0)
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "No se ha podido guardar el usuario.";
                    return objRetorno;
                }

                //carga los paramtros de ABM.
                objRetorno.IdUsuario = objResultado.intRetorno;
                objRetorno.ResultadoEjecucion = true;
                objRetorno.MensajeError = "Los datos han sido guardados con exito.";
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string VerificarUsuario(string usuario)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                //[MiguelP]     30/10/2014      GCP - 15599  - Convierte lo caracteres extraños
                usuario = HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default);

                if (string.IsNullOrEmpty(usuario))
                    return "La <strong>CUENTA DE USUARIO</strong> es un dato obligatorio.";

                //si no se obtuvieron resultados, se advierte
                if (!BuscarUsuarioDominio(usuario.Trim()))
                    return "El usuario pertenece al dominio " + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio;
                else
                    return "El usuario no pertenece al dominio " + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio + " o en este momento no se puede establecer conexión con el Dominio. Verifique. Terminal no encontrada.";

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioEliminar EliminarUsuario(int Id, string usuario, string fechabaja, bool validar)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.dcRecuperarDatosParaABMUsuarios objResp = objFachada.RecuperarDatosParaABMUsuarios(Id);

                usuario = HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default);

                if (Id.Equals(0))
                    return new vUsuarioEliminar()
                    {
                        ResultadoEjecucion = false,
                        MensajeError = "El superusuario <strong>\"" + HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default) + "\"</strong> no puede ser eliminado. Imposible continuar"
                    };
                DateTime? dtmTemporal = vUtiles.StringToDateNull(fechabaja);
                if (!dtmTemporal.HasValue)
                {
                    //[GonzaloP]          [viernes, 15 de julio de 2016]       Work-Item: 7193 - No se permite eliminar usuarios si no se tiene el permiso adecuado
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IDUsuario > 0 && ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1027"))
                    {
                        if (objResp.lstRoles_Composicion.Select(p => p.idSistema = 1).Count() > 0)
                            return new vUsuarioEliminar() { ResultadoEjecucion = false, MensajeError = "No esta autorizado a modificar usuarios que poseen permisos de Seguridad." };
                    }

                    if (validar)
                        return new vUsuarioEliminar()
                        {
                            ResultadoEjecucion = true,
                            pregunta = true,
                            MensajeError = "Va a dar de baja el usuario <strong>\"" + HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default) + "\"</strong>. ¿Desea Continuar?"

                        };
                    int intTemporal = objFachada.EliminarUsuario(Id, COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(760, usuario, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login));

                    if (intTemporal.Equals(0))
                        return new vUsuarioEliminar()
                        {
                            ResultadoEjecucion = false,
                            MensajeError = "Ha ocurrido un error inesperado, por favor vuelva a intentar"
                        };

                    return new vUsuarioEliminar()
                    {
                        ResultadoEjecucion = true,
                        pregunta = false,
                        MensajeServicio = "El usuario <strong>\"" + HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default) + "\"</strong> ha sido dado de baja correctamente."
                    };
                }
                else
                {
                    if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IsInRole("1013"))
                    {
                        if (validar)
                            return new vUsuarioEliminar()
                            {
                                ResultadoEjecucion = true,
                                pregunta = true,
                                MensajeError = "Va a Activar el usuario <strong>\"" + HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default) + "\"</strong>. ¿Desea Continuar?"

                            };
                        int intTemporal = objFachada.ActivarUsuario(Id, COA.WebCipol.Presentacion.Utiles.cPrincipal.MensajeAuditoria(780, usuario, ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login));
                        if (intTemporal == 0)
                            return new vUsuarioEliminar()
                            {
                                ResultadoEjecucion = false,
                                MensajeError = "Ha ocurrido un error inesperado, por favor vuelva a intentar"
                            };

                        return new vUsuarioEliminar()
                        {
                            ResultadoEjecucion = true,
                            pregunta = false,
                            MensajeServicio = "El usuario <strong>\"" + HttpUtility.UrlDecode(usuario, System.Text.Encoding.Default) + "\"</strong> fue activado correctamente."
                        };
                    }
                    else
                    {
                        return new vUsuarioEliminar()
                        {
                            ResultadoEjecucion = false,
                            MensajeError = "No dispone de permisos suficientes para realizar la operación. Imposible continúar."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        #region "Funciones privadas"

        private bool ValidarEmail(string myEmail)
        {
            ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //               DESCRIPCION DE VARIABLES LOCALES
            //strRegExPattern    : Expresion Regular utilizada para validar
            ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            string strRegExPattern = null;
            strRegExPattern = "^[a-zA-Z][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$";
            if (System.Text.RegularExpressions.Regex.IsMatch(myEmail, strRegExPattern))
                return true;
            else
                return false;
        }

        private bool BuscarUsuarioDominio(string usuario)
        {
            string strPath = null;
            string[] strdom = null;
            Int32 inti = default(Int32);
            DirectoryEntry objIngreso = default(DirectoryEntry);
            DirectorySearcher objBuscar = default(DirectorySearcher);
            SearchResult objResultado = default(SearchResult);
            //si el dominio no es nulo, o sea, si se esta usando
            //seguridad integrada al dominio, se verifica 
            //la pc contra el servicio de directorio usando la sintaxis LDAP
            //if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio)
            //{
            try
            {
                strPath = "LDAP://";
                strdom = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio.Split('.');
                for (inti = 0; inti <= strdom.GetUpperBound(0); inti++)
                {
                    strPath += "DC=";
                    strPath += strdom[inti];
                    strPath += ",";
                }
                strPath = strPath.Substring(0, strPath.Length - 1);
                objIngreso = new DirectoryEntry(strPath);

                //construido el path, se agrega el filtro al buscador de
                //directorio
                objBuscar = new DirectorySearcher(objIngreso);
                objBuscar.Filter = "(&(objectClass=user)(samaccountname=" + usuario.Trim() + "))";
                objResultado = objBuscar.FindOne();
                return (objResultado == null);
            }
            catch (Exception)
            {
                objResultado = null;
                //return ex.Message;
                return false;
                //lblMSJAltaModif.Text = ex.StackTrace;
            }
            //}
        }

        private string ValidarSeguridadContrasenia(ref string pstrContrasenia, bool Seguridad_SoloDominio,
                                                 Constantes.genuNivelSeguridad NivelSeguridadContrasenia)
        {
            if (ValidarLoginSSO())
            {
                return "";
            }
            string functionReturnValue = null;
            bool blnTieneLetras;
            bool blnTieneNumeros;
            bool blnTieneCaracteresEspeciales;

            //verificamos si la seguridad está integrada al dominio, en caso
            //que lo esté (o sea, gstrDominio <> "") salimos pues derivamos la
            //responsabilidad de la seguridad a Windows.
            if (Seguridad_SoloDominio)
            {
                functionReturnValue = "";
                return functionReturnValue;
            }

            //si la seguridad no está integrada al dominio,
            //verificamos la contraseña de acuerdo al nivel de
            //seguridad indicado.
            //recorremos la contraseña verificando si tiene caracteres alfabeticos, numericos y especiales
            blnTieneNumeros = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"\d");
            blnTieneLetras = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"[a-zA-Z]");
            blnTieneCaracteresEspeciales = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"[^0-9a-zA-Z]");

            //ahora, de acuerdo al nivel de seguridad, verificamos
            //la contraseña
            switch (NivelSeguridadContrasenia)
            {
                case Constantes.genuNivelSeguridad.Sin_requerimiento_específico:
                    //sin seguridad, la contraseña siempre es válida
                    functionReturnValue = "";
                    return functionReturnValue;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_numeros:
                    //sólo por números, verificamos que la contraseña
                    //solo tenga números, no tenga caracteres especiales y no tenga letras
                    if (blnTieneNumeros && !blnTieneCaracteresEspeciales && !blnTieneLetras)
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        bool blnSoloNros = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"^[0-9]+$");
                        functionReturnValue = (blnSoloNros ? "" : "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por números.").ToString();
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_letras:
                    //contraseña compuesta solo por letras, verificamos
                    //que así sea
                    if (blnTieneLetras & (!blnTieneCaracteresEspeciales) & (!blnTieneNumeros))
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        //si se encontró un caracter que no es alfabético, rechazamos la contraseña
                        functionReturnValue = "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por letras.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_y_numeros:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & (!blnTieneCaracteresEspeciales))
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        functionReturnValue = "El nivel de seguridad parametrizado requiere que la contraseña posea solo letras y números.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & blnTieneCaracteresEspeciales)
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        functionReturnValue = "El nivel de seguridad parametrizado requiere que la contraseña posea letras, números y caracteres especiales.";
                    }
                    break;
                default:
                    throw new Exception("Nivel de seguridad no soportado.");
            }
            return functionReturnValue;
        }

        private string recuperarCboEstadosUsuario(List<Item> list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");


                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = list,
                    DataTextField = "Descripcion",
                    DataValueField = "Valor",
                    Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(150),
                    Id = "cboEstado"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private string recuperarCboTipoDocumentoUsuario(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");


                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = list,
                    DataTextField = "TipoAbreviado",
                    DataValueField = "IdTipoDoc",
                    Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(250),
                    Id = "cboTipoDoc"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private string recuperarCboAreasUsuario(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");


                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = list,
                    DataTextField = "NombreArea",
                    DataValueField = "IdArea",
                    Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(150),
                    Id = "cboArea"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private string recuperarCboAreas(Object list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");


                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = list,
                    DataTextField = "NombreArea",
                    DataValueField = "IdArea",
                    Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(150),
                    Id = "cboAreaFiltro"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }


        #endregion
        #endregion

        #region Terminales

        /// <summary>Obtiene un json de las terminales habilitadas
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private string CargarTerminalesHabilitadas()
        {
            List<SE_TERMINALES> lst_SE_TERMINALES = ObtenerTerminalesHabilitadas();

            return CargarTerminalesHabilitadas(lst_SE_TERMINALES);
        }

        /// <summary>Obtiene un json de las terminales habilitadas
        /// </summary>
        /// <param name="lstTerminalesHabilitadas">Lista de terminales</param>
        /// <returns>json de terminales</returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private string CargarTerminalesHabilitadas(List<SE_TERMINALES> lstTerminalesHabilitadas)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();
            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");

                lstTerminalesHabilitadas.Sort((p, q) => string.Compare(p.CODTERMINAL, q.CODTERMINAL));
                ctl.datos = new DatosListBoxGenerico()
                {
                    DataSource = lstTerminalesHabilitadas,
                    DataTextField = "CODTERMINAL",
                    DataValueField = "IDTERMINAL",
                    Height = new System.Web.UI.WebControls.Unit(160),
                    Width = new System.Web.UI.WebControls.Unit(300),
                    Id = "lstTerminalesHabilitadas"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>Se obtienen las terminales hablitadas
        /// </summary>
        /// <param name=""></param>
        /// <returns>Lista de terminales habilitadas</returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private List<SE_TERMINALES> ObtenerTerminalesHabilitadas()
        {
            List<SE_TERMINALES> lstTerminalesHabilitadas = new List<SE_TERMINALES>(objDatosUsuarios.lstSE_TERMINALES);
            foreach (var item in objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario)
            {
                SE_TERMINALES objAux = lstTerminalesHabilitadas.Find(s => s.IDTERMINAL == item.IdTerminal);
                item.CODTERMINAL = objAux.CODTERMINAL;
                item.idArea = objAux.IDAREA;
                lstTerminalesHabilitadas.RemoveAll(s => s.IDTERMINAL == item.IdTerminal);
            }
            return lstTerminalesHabilitadas;
        }

        /// <summary>Obtiene un json de las terminales NO habilitadas
        /// </summary>
        /// <param name="lstTerminalesHabilitadas">Lista de terminales</param>
        /// <returns>json de terminales</returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private string CargarTerminalesNOHabilitadas()
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario.Sort((p, q) => string.Compare(p.CODTERMINAL, q.CODTERMINAL));
            try
            {
                UIListBoxGenerica objUI = new UIListBoxGenerica();
                UIListBoxGenericaUC ctl = (UIListBoxGenericaUC)objUI.LoadControl("UIControlsHelper/ListBox/UIListBoxGenericaUC.ascx");

                ctl.datos = new DatosListBoxGenerico()
                 {
                     DataSource = objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario,
                     DataTextField = "CODTERMINAL",
                     DataValueField = "IDTERMINAL",
                     Height = new System.Web.UI.WebControls.Unit(160),
                     Width = new System.Web.UI.WebControls.Unit(300),
                     Id = "lstTerminalesNOHabilitadas"
                 };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>crea un objeto json
        /// </summary>
        /// <param name="lstAreas">Listas de areas</param>
        /// <returns>json</returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private string RecuperarCboAreasFiltroTerminal(List<KAREAS> lstAreas)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");

                lstAreas.RemoveAll(a => a.baja != "N");

                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = lstAreas,
                    DataTextField = "NombreArea",
                    DataValueField = "IdArea",
                    Height = new System.Web.UI.WebControls.Unit(22),
                    Widht = new System.Web.UI.WebControls.Unit(150),
                    itemTodos = new System.Web.UI.WebControls.ListItem() { Text = "(Todas)", Value = "-1" },
                    Id = "cboAreaFiltro"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Inicializa la variable de sesión que mantiene los cambios efectuados al abrir y cerrar el formulario de terminales
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public void InicializarVariales()
        {
            try
            {
                objDatosTerminalesUsusario.SE_TERM_USUARIO = new List<SE_Term_Usuario>(objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Recupera las terminales Habilitadas y NO Habilitadas para mostrar en el formulario
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vAlertas InicializarFormularioAlertas()
        {
            try
            {
                return new vAlertas
                {
                    jsonterminaleshabilitadas = CargarTerminalesHabilitadas(),
                    jsonterminalesnohabilitadas = CargarTerminalesNOHabilitadas()
                };
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Cierra el formulario de terminales deshaciendo los cambio efectuados desde que se ingreso por ultima vez a dicho formulario
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public void CerrarFormularioTerminales()
        {
            try
            {
                objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario = new List<SE_Term_Usuario>(objDatosTerminalesUsusario.SE_TERM_USUARIO);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Cierra el formulario de terminales guardando los cambio en pantalla 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public void AceptarTerminalesDialog()
        {
            try
            {
                objDatosTerminalesUsusario.SE_TERM_USUARIO = new List<SE_Term_Usuario>(objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Filtra las terminales habilitadas por area
        /// </summary>
        /// <param name="Id">Id de area</param>
        /// <returns>Terminales pertenecientes al área</returns>
        /// <history>
        /// [IvanSa]          [Lunes, 07 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string BuscarTerminalesPorArea(int idArea)
        {
            try
            {
                List<SE_TERMINALES> lst_SeTerminales = null;
                if (idArea != -1)
                {
                    lst_SeTerminales = ObtenerTerminalesHabilitadas().FindAll(t => t.IDAREA == idArea);
                }
                else
                {
                    lst_SeTerminales = ObtenerTerminalesHabilitadas();
                }

                return CargarTerminalesHabilitadas(lst_SeTerminales);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa una terminal de Habilitada a NO Habilitada - Auditoria de la acción
        /// </summary>
        /// <param name="Id">Id de la terminal</param>
        /// <param name="desc">Nombre de la terminal</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Lunes, 07 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AsignarTerminal(int Id, string nombre)
        {
            try
            {
                this.AsignarTerminal(Id);
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha ingresado la terminal: " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa un una terminal de NO Habilitada a Habilitada - Auditoria de la acción
        /// </summary>
        /// <param name="Id">Id de la terminal</param>
        /// <param name="desc">Nombre de la terminal</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Lunes, 07 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string DesasignarTerminal(int Id, string nombre)
        {
            try
            {
                this.DesasignarTerminal(Id);
                COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + Id.ToString(), "Se ha eliminado la terminal " + nombre);
                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa Todas las terminales a NO Habilitadas - Auditoria de la acción
        /// </summary>
        /// <param name="obj">Coleccion de terminales</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Lunes, 07 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string AsignarTerminalesTodas(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                    {
                        this.AsignarTerminal(item.Id);
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha ingresado la terminal " + item.nombre.Trim());
                    }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// [WebMethod] - Pasa Todas las terminales a Habilitadas - Auditoria de la acción
        /// </summary>
        /// <param name="obj">Colección  de terminales</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Lunes, 07 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string DesasignarTerminalesTodas(List<itemGenerico> obj)
        {
            try
            {
                if (obj != null)
                    foreach (var item in obj)
                    {
                        this.DesasignarTerminal(item.Id);
                        COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios("T" + item.Id.ToString(), "Se ha eliminado la terminal: " + item.nombre.Trim());
                    }

                return "";
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region Metodos Privados para mantener la variable de sesión

        /// <summary>
        /// Guarda en sesión el sistema bloqueado
        /// </summary>
        /// <param name="Id">Id de sistema/param>
        /// <param name="desc">Descripciòn del sistema</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 01 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private void AsignarTerminal(int Id)
        {
            SE_TERMINALES seTerminal = new SE_TERMINALES();
            seTerminal = (from t in objDatosUsuarios.lstSE_TERMINALES
                          where t.IDTERMINAL == Id
                          select t).FirstOrDefault();

            SE_Term_Usuario seTermUsuario = new SE_Term_Usuario
            {
                CODTERMINAL = seTerminal.CODTERMINAL,
                IdTerminal = seTerminal.IDTERMINAL,
                idArea = seTerminal.IDAREA,
            };

            objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario.Add(seTermUsuario);
        }

        /// <summary>
        /// Elimina de sesión la terminal NO Habilitada 
        /// </summary>
        /// <param name="Id">Id de terminal</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private void DesasignarTerminal(int Id)
        {
            objDatosUsuarios.objDatosUsuario.lstSE_Term_Usuario.RemoveAll(t => t.IdTerminal == Id);
        }

        #endregion

        #region "Variables de Sesion"

        /// <summary>
        /// variable de sesiòn para obtener las terminales bloqueadas por si sale del formulario de terminales por medio del boton cerrar
        /// </summary>
        /// <history>
        /// [IvanSa]          [Martes, 08 de octubre de 2013]       Creado GCP-Cambios ID: 14418
        /// </history>
        private vSessionDatosTerminalesUsuario objDatosTerminalesUsusario
        {
            get
            {
                if (HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.TERMINALES_USUARIO] == null)
                    HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.TERMINALES_USUARIO] = new vSessionDatosTerminalesUsuario();
                return (vSessionDatosTerminalesUsuario)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.TERMINALES_USUARIO];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.TERMINALES_USUARIO] = value;
            }
        }

        #endregion

        #endregion

        #region "Roles"
        private vSessionDatosUsuarioRoles objDatosUsusariosRoles
        {
            get
            {
                if (HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIO_ROLES] == null)
                    HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIO_ROLES] = new vSessionDatosUsuarioRoles();
                return (vSessionDatosUsuarioRoles)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIO_ROLES];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.USUARIO_ROLES] = value;
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioRolCarga RecuperarUsuarioRolCarga()
        {
            try
            {
                objDatosUsusariosRoles.Roles_Composicion = new List<Roles_Composicion>(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion);
                return new vUsuarioRolCarga()
                {
                    ResultadoEjecucion = true,
                    jsontareasdisponibles = recuperarTreeViewuUsuarioRoles(objDatosUsuarios.lstComposicionDeRoles),
                    jsontareasasignadas = recuperarTreeViewuUsuarioRolesAsignados(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion),
                    jsoncbotareasasignadas = recuperarCboUsuarioRolesAsignados(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion)
                };
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioRolAsignar asignarTareaUsuarioRol(string Id)
        {
            try
            {
                vUsuarioRolAsignar objRetorno = new vUsuarioRolAsignar();
                string mensaje = "";
                string[] valores = Id.Split('/');
                CifrarDatos.TresDES objEncriptarNET;
                objEncriptarNET = new CifrarDatos.TresDES();
                objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
                objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;

                if (ExistenGruposExcluyentesUsurioRol(Id, ref mensaje))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = mensaje;
                    return objRetorno;
                }

                //
                string[] node = Id.Split('/');

                var query0 = from item in objDatosUsuarios.lstComposicionDeRoles select item;
                switch (node.Length)
                {
                    case 1:
                        //Recupera todos los grupos del rol.
                        query0 = from item in objDatosUsuarios.lstComposicionDeRoles
                                 where item.IdRol == int.Parse(valores[0])
                                 select item;
                        break;
                    case 2:
                        query0 = from item in objDatosUsuarios.lstComposicionDeRoles
                                 where item.IdRol == int.Parse(valores[0]) && item.IdGrupo == int.Parse(valores[1])
                                 select item;
                        break;
                    case 3:
                        query0 = from item in objDatosUsuarios.lstComposicionDeRoles
                                 where item.IdRol == int.Parse(valores[0]) && item.IdGrupo == int.Parse(valores[1])
                                 && item.IdSistema == int.Parse(valores[2])
                                 select item;
                        break;
                    case 4:
                        query0 = from item in objDatosUsuarios.lstComposicionDeRoles
                                 where item.IdRol == int.Parse(valores[0]) && item.IdGrupo == int.Parse(valores[1])
                                 && item.IdSistema == int.Parse(valores[2]) && item.IdTarea == int.Parse(valores[3])
                                 select item;
                        break;
                    default:
                        break;
                }

                if (query0 != null && query0.Count() > 0)
                {
                    foreach (var item in query0)
                    {
                        if (!(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion.Where(p => p.idTarea == item.IdTarea).Count() > 0))
                        {
                            objDatosUsuarios.objDatosUsuario.lstRoles_Composicion.Add(new COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion()
                            {
                                IdRol = item.IdRol,
                                idTarea = item.IdTarea,
                                idSistema = item.IdSistema,
                                IdGrupo = item.IdGrupo,
                                TareaInhibida = objEncriptarNET.Criptografia(Accion.Encriptacion, "N"),
                                DescGrupo = item.DescGrupo,
                                DescripcionPerfil = item.DescripcionPerfil,
                                DescripcionTarea = item.DescripcionTarea,
                                DescSistema = item.DescSistema
                            });
                        }
                    }
                    //Rol
                    COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha ingresado Rol: " + query0.First().DescripcionPerfil);
                }

                objRetorno.ResultadoEjecucion = true;
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vUsuarioRolAsignar desasignarTareaUsuarioRol(string Id)
        {
            try
            {
                vUsuarioRolAsignar objRetorno = new vUsuarioRolAsignar();
                string[] valores = Id.Split('/');

                var query = from item in objDatosUsuarios.lstComposicionDeRoles
                            where item.IdRol == int.Parse(valores[0])
                            select item;

                //Elimino todas las tareas del Grupo.
                objDatosUsuarios.objDatosUsuario.lstRoles_Composicion.RemoveAll(p => p.IdRol == int.Parse(valores[0]));

                if (query != null && query.Count() > 0)
                {
                    //Grupo
                    COA.WebCipol.Presentacion.Utiles.cAuditoria.Auditar_Cambios(Id, "Se ha eliminado el Rol:" + query.First().DescripcionPerfil);
                }
                objRetorno.ResultadoEjecucion = true;
                //Arma el treeview.
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        //[WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        //public void AceptarFormularioUsuarioRoles()
        //{
        //    try
        //    {
        //        objDatosUsusariosRoles.Roles_Composicion = new List<Roles_Composicion>(objDatosUsuarios.objDatosUsuario.lstRoles_Composicion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

        //[WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        //public void CerrarFormularioUsuarioRoles()
        //{
        //    try
        //    {
        //        objDatosUsuarios.objDatosUsuario.lstRoles_Composicion = new List<Roles_Composicion>(objDatosUsusariosRoles.Roles_Composicion);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}


        #region "Funciones Privadas"
        private string recuperarTreeViewuUsuarioRoles(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRoles> list)
        {
            StringBuilder sbHtml = new StringBuilder();
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            MemoryStream objMemoria = new MemoryStream();
            ElementoLista objLista = new ElementoLista();

            if (list.Count == 0)
                return "";

            //sbHtml.Append("<div id='dvTareasDisponibles' runat='Server' style='overflow: scroll; height: 400px; width:350px;background:#FFFFFF !important;'>");
            sbHtml.Append("<div id='dvTareasDisponibles' runat='Server'>");
            sbHtml.Append("<ul class='nivel1'>");
            foreach (var item in list.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRoles>(p => p.IdRol)))
            {

                sbHtml.Append("<li id='" + item.IdRol.ToString() + "' class='grupo' ><img src='./Imagenes/group.png' /><a> " + item.DescripcionPerfil + "</a>");

                sbHtml.Append("<ul class='nivel2'>");
                foreach (var itemAux in list.Where(p => p.IdRol == item.IdRol).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRoles>(p => p.IdGrupo)))
                {
                    if (itemAux.IdRol != item.IdRol)
                        continue;
                    sbHtml.Append("<li id='" + itemAux.IdRol.ToString() + "/" + itemAux.IdGrupo.ToString() + "' class='sistema'><img src='./Imagenes/sistema.bmp' /><a>" + itemAux.DescGrupo + "</a>");//

                    sbHtml.Append("<ul class='nivel3'>");
                    foreach (var itemAux2 in list.Where(p => p.IdRol == item.IdRol && p.IdGrupo == itemAux.IdGrupo).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRoles>(p => p.IdSistema)))
                    {
                        if (itemAux2.IdRol != item.IdRol || itemAux2.IdGrupo != itemAux.IdGrupo)
                            continue;
                        sbHtml.Append("<li id='" + itemAux2.IdRol.ToString() + "/" + itemAux2.IdGrupo.ToString() + "/" + itemAux2.IdSistema.ToString() + "' class='tarea'><img src='./Imagenes/task-icon.png' /><a>" + itemAux2.DescSistema + "</a>");//

                        sbHtml.Append("<ul class='nivel4'>");
                        foreach (var itemAux3 in list)
                        {
                            if (itemAux3.IdRol != item.IdRol || itemAux3.IdGrupo != itemAux.IdGrupo || itemAux3.IdSistema != itemAux2.IdSistema)
                                continue;
                            sbHtml.Append("<li id='" + itemAux3.IdRol.ToString() + "/" + itemAux3.IdGrupo.ToString() + "/" + itemAux3.IdSistema.ToString() + "/" + itemAux3.IdTarea.ToString() + "' class='asignada'><img src='./Imagenes/task-assign-icon.png' /><a> " + itemAux3.DescripcionTarea + "</a>");
                            sbHtml.Append("</li>");
                        }
                        sbHtml.Append("</ul></li>");
                    }
                    sbHtml.Append("</ul></li>");

                }
                sbHtml.Append("</ul></li>");
            }
            sbHtml.Append("</ul>");
            sbHtml.Append("</div>");

            objLista.Lista = Server.HtmlEncode(sbHtml.ToString());
            objSerializador = new DataContractJsonSerializer(objLista.GetType());
            objSerializador.WriteObject(objMemoria, objLista);
            strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

            objMemoria.Close();
            return strRtaJson;
        }

        private string recuperarTreeViewuUsuarioRolesAsignados(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion> list)
        {
            StringBuilder sbHtml = new StringBuilder();
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            MemoryStream objMemoria = new MemoryStream();
            ElementoLista objLista = new ElementoLista();

            CifrarDatos.TresDES objEncriptarNET;
            objEncriptarNET = new CifrarDatos.TresDES();
            objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
            objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;

            sbHtml.Append("<div id='dvTareasAsignadas' runat='Server'>");
            sbHtml.Append("<ul class='nivel1'>");
            if (list.Count() > 0)
            {

                foreach (var item in list.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion>(p => p.IdRol)))
                {

                    sbHtml.Append("<li id='" + item.IdRol.ToString() + "' class='grupo' ><img src='./Imagenes/group.png' /><a> " + item.DescripcionPerfil + "</a>");

                    sbHtml.Append("<ul class='nivel2'>");
                    foreach (var itemAux in list.Where(p => p.IdRol == item.IdRol).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion>(p => p.IdGrupo)))
                    {
                        if (itemAux.IdRol != item.IdRol)
                            continue;
                        sbHtml.Append("<li id='" + itemAux.IdRol.ToString() + "/" + itemAux.IdGrupo.ToString() + "' class='sistema'><img src='./Imagenes/sistema.bmp' /><a>" + itemAux.DescGrupo + "</a>");//

                        sbHtml.Append("<ul class='nivel3'>");
                        foreach (var itemAux2 in list.Where(p => p.IdRol == item.IdRol && p.IdGrupo == itemAux.IdGrupo).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion>(p => p.idSistema)))
                        {
                            if (itemAux2.IdRol != item.IdRol || itemAux2.IdGrupo != itemAux.IdGrupo)
                                continue;
                            sbHtml.Append("<li id='" + itemAux2.IdRol.ToString() + "/" + itemAux2.IdGrupo.ToString() + "/" + itemAux2.idSistema.ToString() + "' class='tarea'><img src='./Imagenes/task-icon.png' /><a>" + itemAux2.DescSistema + "</a>");//

                            sbHtml.Append("<ul class='nivel4'>");
                            foreach (var itemAux3 in list)
                            {
                                if (itemAux3.IdRol != item.IdRol || itemAux3.IdGrupo != itemAux.IdGrupo || itemAux3.idSistema != itemAux2.idSistema)
                                    continue;
                                sbHtml.Append("<li id='" + itemAux3.IdRol.ToString() + "/" + itemAux3.IdGrupo.ToString() + "/" + itemAux3.idSistema.ToString() + "/" + itemAux3.idTarea.ToString() + "' class='" + ((objEncriptarNET.Criptografia(Accion.Desencriptacion, itemAux3.TareaInhibida).Equals("N")) ? "asignada'><img src='./Imagenes/task-assign-icon.png' />" : "bloqueada'><img src='./Imagenes/task-block-icon.png' />") + "<a> " + itemAux3.DescripcionTarea + "</a>");
                                sbHtml.Append("</li>");
                            }
                            sbHtml.Append("</ul></li>");
                        }
                        sbHtml.Append("</ul></li>");

                    }
                    sbHtml.Append("</ul></li>");
                }
            }
            sbHtml.Append("</ul>");
            sbHtml.Append("</div>");

            objLista.Lista = Server.HtmlEncode(sbHtml.ToString());
            objSerializador = new DataContractJsonSerializer(objLista.GetType());
            objSerializador.WriteObject(objMemoria, objLista);
            strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

            objMemoria.Close();
            return strRtaJson;
        }
        /// <summary>
        /// Arma el json del combo de roles asignados
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        /// <history>
        /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14414
        /// </history>
        private string recuperarCboUsuarioRolesAsignados(List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion> list)
        {
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            ElementoLista objLista = new ElementoLista();
            MemoryStream objMemoria = new MemoryStream();
            StringBuilder sb = new StringBuilder();

            try
            {
                UIcboGenerico objUI = new UIcboGenerico();
                UIcboGenericoUC ctl = (UIcboGenericoUC)objUI.LoadControl("UIControlsHelper/DropDownList/UIcboGenericoUC.ascx");

                ctl.datos = new DatosCboGenerico()
                {
                    DataSource = list.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion>(p => p.IdRol)),
                    DataTextField = "DescripcionPerfil",
                    DataValueField = "IdRol",
                    //Height = new System.Web.UI.WebControls.Unit(22),
                    //Widht = new System.Web.UI.WebControls.Unit(150),
                    Id = "cbousuariorolesasignados"
                };

                objUI.EnableEventValidation = false; objUI.EnableViewState = false;
                HtmlForm _form = new HtmlForm();
                objUI.Controls.Add(_form);
                _form.Controls.Add(ctl);
                StringWriter writer = new StringWriter();
                Server.Execute(objUI, writer, false);
                sb.Append(writer.ToString());
                writer.Close();

                objLista.Lista = Server.HtmlEncode(sb.ToString());
                objSerializador = new DataContractJsonSerializer(objLista.GetType());
                objSerializador.WriteObject(objMemoria, objLista);
                strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

                objMemoria.Close();
                return strRtaJson;
            }
            catch (Exception ex)
            {
                throw (ex);

            }
        }

        private bool ExistenGruposExcluyentesUsurioRol(string Id, ref string mensaje)
        {
            if (objDatosUsuarios.lstSE_GRUPO_EXCLUSION == null || objDatosUsuarios.objDatosUsuario.lstRoles_Composicion == null || objDatosUsuarios.lstSE_GRUPO_EXCLUSION.Count() == 0)
                return false;

            bool blnRetorno = false;
            string strGrupoActual = "";
            string strGrupoExc = "";
            string[] node = Id.Split('/');

            List<string> lstIdGrupos = new List<string>();
            switch (node.Length)
            {
                case 1:
                    //Recupera todos los grupos del rol.
                    foreach (var itemAux in objDatosUsuarios.lstComposicionDeRoles.Where(p => p.IdRol == int.Parse(node[0])).Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRoles>(p => p.IdGrupo)))
                        lstIdGrupos.Add(itemAux.IdGrupo.ToString());
                    break;
                case 2:
                case 3:
                case 4:
                    lstIdGrupos.Add(node[1]);
                    break;
                default:
                    break;
            }

            foreach (string strIdGrupo in lstIdGrupos)
            {

                List<decimal> lista = objDatosUsuarios.objDatosUsuario.lstRoles_Composicion.Distinct(new GenericComparer<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion>(p => p.IdGrupo)).Select(p => (decimal)p.IdGrupo).ToList();
                //martinv -> por el momento se puede dar que el rol a asignar tenga grupos excluyentes en si.

                lista = lista.Concat((from o in lstIdGrupos
                                      where !(from c in lista
                                              select c).Contains(decimal.Parse(o)) && o != strIdGrupo
                                      select decimal.Parse(o)).ToList()).ToList();

                foreach (var item in lista)
                {
                    //Controla que no exista exclución de grupos
                    var querry = from itemAux in objDatosUsuarios.lstSE_GRUPO_EXCLUSION
                                 where (((itemAux.IDGRUPOACTUAL == item && itemAux.IDGRUPEXCLUYENTE == int.Parse(strIdGrupo))
                                         || (itemAux.IDGRUPEXCLUYENTE == item && itemAux.IDGRUPOACTUAL == int.Parse(strIdGrupo))) && (item != int.Parse(strIdGrupo)))
                                 select itemAux;
                    if (querry != null && querry.Count() > 0)
                    {
                        var nombregrupo = (from itemNombre in objDatosUsuarios.lstComposicionDeRoles
                                           where itemNombre.IdGrupo == int.Parse(strIdGrupo)
                                           select itemNombre.DescGrupo).Distinct();
                        if (nombregrupo != null && nombregrupo.Count() > 0)
                            strGrupoActual = nombregrupo.First();

                        int idExclutente = 0;
                        if (querry.First().IDGRUPOACTUAL == int.Parse(strIdGrupo))
                            idExclutente = querry.First().IDGRUPEXCLUYENTE;
                        if (querry.First().IDGRUPEXCLUYENTE == int.Parse(strIdGrupo))
                            idExclutente = querry.First().IDGRUPOACTUAL;

                        var nombregrupoExc = (from itemNombre in objDatosUsuarios.lstComposicionDeRoles
                                              where itemNombre.IdGrupo == idExclutente
                                              select itemNombre.DescGrupo).Distinct();

                        if (nombregrupoExc != null && nombregrupoExc.Count() > 0)
                            strGrupoExc = nombregrupoExc.First();

                        //"El grupo " & strGrupoActual & " es excluyente" & vbCrLf & "del grupo " & strGrupos(intI).Substring(strGrupos(intI).IndexOf("¤"c) + 1) & "."
                        mensaje += "El grupo " + strGrupoActual + " es excluyente " + "del grupo " + strGrupoExc + ". \r\n";
                        blnRetorno = true;
                    }
                }
            }




            return blnRetorno;
        }

        #endregion
        #endregion

        #region Horarios

        /// <summary>
        /// [WebMethod] - 
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Martes, 15 de octubre de 2013]       Creado GCP-Cambios ID: 14419
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RecuperarHorarios()
        {
            try
            {
                return CargarHorarios();
            }
            catch (Exception ex)
            {
                throw (ex);
                //return new vSucesosSeguridadCarga() { ResultadoEjecucion = false, MensajeError = ex.Message };
            }
        }

        private string CargarHorarios()
        {
            StringBuilder sbHtml = new StringBuilder();
            DataContractJsonSerializer objSerializador = null;
            String strRtaJson;
            MemoryStream objMemoria = new MemoryStream();
            ElementoLista objLista = new ElementoLista();

            //if (objDatosUsuarios.lstSE_Horarios_Usuario.Count > 0)
            if (objDatosUsuarios.objDatosUsuario.lstSE_Horarios_Usuario.Count > 0)
                sbHtml = this.CargarHorarios(objDatosUsuarios.objDatosUsuario.lstSE_Horarios_Usuario);
            else
                sbHtml = this.CargarHorarios(objDatosUsuarios.lstSE_Horarios_Usuario);

            objLista.Lista = Server.HtmlEncode(sbHtml.ToString());
            objSerializador = new DataContractJsonSerializer(objLista.GetType());
            objSerializador.WriteObject(objMemoria, objLista);
            strRtaJson = Encoding.UTF8.GetString(objMemoria.ToArray());

            objMemoria.Close();
            return strRtaJson;
        }


        private StringBuilder CargarHorarios<T>(List<T> list) where T : ISE_Horarios_Usuario
        {
            StringBuilder sbHtml = new StringBuilder();
            string[] dias = { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sabado", "Domingo" };
            string[] horas = { "00:00", "01:00", "02:00" , "03:00" , "04:00" , "05:00" , "06:00" , "07:00" , "08:00" , "09:00" , "10:00" , "11:00" , "12:00" ,
                               "13:00", "14:00", "15:00" , "16:00" , "17:00" , "18:00" , "19:00" , "20:00" , "21:00" , "22:00" , "23:00" , "24:00" };

            sbHtml.Append("<div class='fila'>");
            sbHtml.Append("<div class='dia'><span style=\"color:#000000\">Dias/Horas</span></div>");
            for (int i = 0; i < 24; i++)
            {
                sbHtml.Append("<div  class='dia-hora-h green'>");
                sbHtml.Append("<div  class='dia-hora-superior' title='Desde: " + horas[(i)] + " Hs. Hasta: " + horas[(i + 1)] + " Hs.'><span>" + i.ToString() + "</span></div>");
                sbHtml.Append("<div  id='" + i.ToString() + " 'class='seleccionar-columna'></div>");
                sbHtml.Append("</div>");
            }

            sbHtml.Append("<div style=\"clear:both\"></div>");

            sbHtml.Append("</div>");

            for (int dia = 1; dia <= 7; dia++)
            {
                sbHtml.Append("<div class='fila alert-success'>");
                for (int hora = 0; hora <= 24; hora++)
                {
                    if (hora == 0)
                    {
                        sbHtml.Append("<div class='dia green'>");
                        sbHtml.Append("<div class='left'><span>" + dias[dia - 1] + "</span></div>");
                        sbHtml.Append("<div id='" + dia + "' class='right seleccionar-fila'></div>");
                        sbHtml.Append("</div>");
                    }
                    else
                    {
                        var hora_dia = list.Find(dh => dh.idDia == dia && dh.IdHorario == (hora - 1));
                        if (hora_dia != null)
                        {
                            sbHtml.Append("<div id='" + (dia + "-" + (hora - 1)) + "' class='dia-hora' title='" + dias[dia - 1] + " - Desde: " + horas[(hora - 1)] + " Hs. Hasta: " + horas[(hora)] + " Hs.' ></div>");
                        }
                        else
                        {
                            sbHtml.Append("<div id='" + (dia + "-" + (hora - 1)) + "' class='dia-hora alert-danger' title='" + dias[dia - 1] + " - Desde: " + horas[(hora - 1)] + " Hs. Hasta: " + horas[(hora)] + " Hs.'></div>");
                        }
                    }
                }
                sbHtml.Append("<div style='clear:both'></div>");
                sbHtml.Append("</div>"); //fin div fila
            }
            return sbHtml;
        }

        /// <summary>
        /// [WebMethod] -  Guarda la configuración de horarios bloqueados
        /// </summary>
        /// <param name="dias_horas">lista con la configuración de horarios establecida en la grilla</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Miercoles, 16 de octubre de 2013]       Creado GCP-Cambios ID: 14419
        /// </history>
        //[WebMethod(EnableSession = true)]
        //[System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        //public void GuardarHorarios(List<Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_Horarios_Usuario> dias_horas)
        //{
        //    try
        //    {
        //        // Se obtiene una lista con dias y horarios permitidos
        //        List<Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_Horarios_Usuario> query =
        //             (from c in objDatosUsuarios.lstSE_Horarios_Usuario
        //              where !(from o in dias_horas
        //                      select new { idDia = o.idDia, idHorario = o.IdHorario })
        //              .Contains(new { idDia = c.idDia, idHorario = c.IdHorario })
        //              select c).ToList();

        //        // Se guarda en la variable de sesión del uduario los dias y horas permitidas
        //        objDatosUsuarios.objDatosUsuario.lstSE_Horarios_Usuario.Clear();
        //        foreach (var item in query)
        //        {
        //            Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.SE_Horarios_Usuario dhb =
        //                new Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.SE_Horarios_Usuario
        //            {
        //                idDia = item.idDia,
        //                IdHorario = item.IdHorario
        //            };
        //            objDatosUsuarios.objDatosUsuario.lstSE_Horarios_Usuario.Add(dhb);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}
        #endregion

        #endregion

        #region Visor de Sucesos de Seguridad

        /// <summary>
        /// [WebMethod] - Recupera los datos para inicializar los controles al ingresar a la pantalla "Visor de sucesos de seguridad"
        /// </summary>
        /// <param name=""></param>
        /// <returns>Datos para inicializar controles</returns>
        /// <history>
        /// [IvanSa]          [Viernes, 04 de octubre de 2013]       Creado GCP-Cambios ID: 
        /// [GonzaloP]          [viernes, 15 de julio de 2016]       Work-Item: 7196
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vSucesosSeguridadCarga RecuperarSucesosSeguridadCarga()
        {
            try
            {
                RecuperarControles rc = new RecuperarControles();
                vSucesosSeguridadCarga objRetorno = new vSucesosSeguridadCarga();
                Fachada.FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaVisorSucesosSeguridad.dcRecuperarDatosParaVisorSucesosSeguridad objResp = objFachada.RecuperarDatosParaVisorSucesosSeguridad();

                //Carga los valores del combo adminitradores.
                List<Sist_UsuariosConTareasCipol> lstAdminitradores = new List<Sist_UsuariosConTareasCipol>();


                objResp.lstSist_UsuariosConTareasCipol.Add(new Sist_UsuariosConTareasCipol() { IdUsuario = -99, Usuario = "master" });
                //lstAdminitradores.Add(new Sist_UsuariosConTareasCipol() { IdUsuario = -99, Nombres = "master" });

                //lstAdminitradores.Sort((p, q) => string.Compare(p.Nombres, q.Nombres));
                objResp.lstSist_UsuariosConTareasCipol.Sort((p, q) => string.Compare(p.Usuario, q.Usuario));
                objRetorno.jsoncboadministradores = rc.GenerarCombos(new DatosCboGenerico()
                {
                    DataSource = objResp.lstSist_UsuariosConTareasCipol,//lstAdminitradores,
                    DataTextField = "Usuario", //"Nombres",
                    DataValueField = "IdUsuario",
                    Id = "cboAdministradores",
                    itemTodos = new System.Web.UI.WebControls.ListItem() { Text = "(Todos)", Value = "-98" }
                });

                objResp.lstSistUsuarios.Sort((p, q) => string.Compare(p.Usuario, q.Usuario));
                objRetorno.jsoncboafectado = rc.GenerarCombos(new DatosCboGenerico()
                {
                    DataSource = objResp.lstSistUsuarios,
                    DataTextField = "Usuario",
                    DataValueField = "IdUsuario",
                    Id = "cboAfectados",
                    itemTodos = new System.Web.UI.WebControls.ListItem() { Text = "(Todos)", Value = "-98" }
                });

                objResp.lstCodAuditoria.Sort((p, q) => string.Compare(p.CODAUDITORIA, q.CODAUDITORIA));
                objRetorno.jsoncbocodigomensaje = rc.GenerarCombos(new DatosCboGenerico()
                {
                    DataSource = objResp.lstCodAuditoria,
                    DataTextField = "CODAUDITORIA",
                    DataValueField = "CODAUDITORIA",
                    Id = "cboCodigoMensaje",
                    itemTodos = new System.Web.UI.WebControls.ListItem() { Text = "(Todos)", Value = "-1" }
                });

                objRetorno.ResultadoEjecucion = true;
                return objRetorno;
            }
            catch (Exception ex)
            {
                return new vSucesosSeguridadCarga() { ResultadoEjecucion = false, MensajeError = ex.Message };
            }
        }

        /// <summary>
        /// [WebMethod] - Recupera los sucesos segun los criterios del filtro
        /// </summary>
        /// <param name="fechaDesde">Fecha desde especificada en el filtro</param>
        /// <param name="fechaHatsa">Fecha hasta especificada en el filtro</param>
        /// <param name="usuarioAdministrador">Usuario Administrador especificado en el filtro</param>
        /// <param name="usuarioAfectado">Usuario Afectado especificado en el filtro</param>
        /// <param name="codigoMensaje">Codigo de Mesanje especificado en el filtro</param>
        /// <returns>Retorna los resultados de la busqueda para ser mostrados en la grilla de resultados</returns>
        /// <history>
        /// [IvanSa]          [Viernes, 04 de octubre de 2013]       Creado GCP-Cambios ID: 
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria.SE_AUDITORIA> RecuperarSucesos(string fechaDesde, string fechaHasta, string usuarioAdministrador,
                                            string usuarioAfectado, string codigoMensaje)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                vSucesosSeguridad objRetorno = new vSucesosSeguridad();

                COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria.dcRetornarLogAuditoria objResp =
                objFachada.RetornarLogAuditoria(new Entidades.ClasesWs.dcRetornarLogAuditoria.dcRetornarLogAuditoriaIN()
                {
                    fechadesde = new DateTime(int.Parse(fechaDesde.Substring(6, 4)), int.Parse(fechaDesde.Substring(3, 2)), int.Parse(fechaDesde.Substring(0, 2))),
                    fechahasta = new DateTime(int.Parse(fechaHasta.Substring(6, 4)), int.Parse(fechaHasta.Substring(3, 2)), int.Parse(fechaHasta.Substring(0, 2))),
                    UsuarioActuante = HttpUtility.UrlDecode(usuarioAdministrador, System.Text.Encoding.Default),//usuarioAdministrador,
                    //usuarioafectado = usuarioAfectado,
                    usuarioafectado = HttpUtility.UrlDecode(usuarioAfectado, System.Text.Encoding.Default),
                    CodigoEvento = HttpUtility.UrlDecode(codigoMensaje, System.Text.Encoding.Default) //codigoMensaje
                });

                return objResp.lstAuditoria.OrderByDescending(v => v.FECHAHORALOG).ToList();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        #endregion

        #region Monitor Actividades

        /// <summary>
        /// [WebMethod] - Recupera los datos para llenar la grilla
        /// </summary>
        /// <param name="area">Nombre de Area</param>
        /// <param name="nombre">Nombre Usuario</param>
        /// <param name="usuario">Usuario</param>
        /// <param name="terminal">Terminal</param>
        /// <returns>Grilla con los resultados de la busqueda</returns>
        /// <history>
        /// [IvanSa]          [Lunes, 21 de octubre de 2013]       Creado GCP-Cambios ID: 14489
        /// [MartinV]          [miércoles, 24 de septiembre de 2014]       Modificado  GCP-Cambios 15586
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaMonitorActividades.Sist_sesionesactivas> RecuperarActividades(vMonitorFiltro obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaMonitorActividades.dcRecuperarDatosParaMonitorActividades objResp =
             objFachada.RecuperarDatosParaMonitorActividades();

                var query = (from a in objResp.lstSist_sesionesactivas
                             where (string.IsNullOrEmpty(obj.area) || a.NOMBREAREA.Trim().ToUpper().Contains(obj.area.Trim().ToUpper()))
                             && (string.IsNullOrEmpty(obj.nombre) || a.NOMBRES.Trim().ToUpper().Contains(obj.nombre.Trim().ToUpper()))
                             && (string.IsNullOrEmpty(obj.terminal) || a.Terminal.Trim().ToUpper().Contains(obj.terminal.Trim().ToUpper()))
                             && (string.IsNullOrEmpty(obj.usuario) || a.Usuario.Trim().ToUpper().Contains(obj.usuario.Trim().ToUpper()))
                             select a).ToList();


                return query.OrderBy(a => a.NOMBRES).ToList();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        /// <summary>
        /// [WebMethod] - Elimina los registros seleccionados en la grilla
        /// </summary>
        /// <param name="obj">{usuario, terminal}</param>
        /// <returns></returns>
        /// <history>
        /// [IvanSa]          [Lunes, 21 de octubre de 2013]       Creado GCP-Cambios ID: 14489
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string Eliminar(List<itemActividad> obj)
        {
            try
            {
                FSeguridad objFachada = new FSeguridad();

                Entidades.ClasesWs.dcEliminarSesionActiva.dcEliminarSesionActiva dcSesionesActiva = new Entidades.ClasesWs.dcEliminarSesionActiva.dcEliminarSesionActiva();

                if (obj != null)
                    foreach (var item in obj)
                    {
                        COA.WebCipol.Entidades.ClasesWs.dcEliminarSesionActiva.Sist_sesionesactivas sist_sesionesactivas =
                            new Entidades.ClasesWs.dcEliminarSesionActiva.Sist_sesionesactivas()
                            {
                                Usuario = item.usuario.Trim(),
                                Terminal = item.terminal.Trim()
                            };
                        dcSesionesActiva.lstSist_sesionesactivas.Add(sist_sesionesactivas);
                    }

                int res = objFachada.EliminarSesionActiva(dcSesionesActiva);

                return res.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <history>
        /// [MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vMonitorActividades RecuperarElementoMonitorBase()
        {
            try
            {
                return new vMonitorActividades();
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        #endregion

        #region Analisis de Auditoria

        /// <summary>
        /// [WebMethod] - Carga el estado incial de la pantalla
        /// </summary>
        /// <returns>Json para los combos del filtro</returns>
        /// <history>
        /// [IvanSa]          [Lunes, 21 de octubre de 2013]       Creado GCP-Cambios ID: 14499
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vAnalisisAuditoria CargarPagina()
        {
            try
            {
                vAnalisisAuditoria objRetorno = new vAnalisisAuditoria();
                Fachada.FSeguridad objFachada = new FSeguridad();
                RecuperarControles rc = new RecuperarControles();

                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSIST_Habilitados.dcRecuperarSIST_Habilitados objRespSistemas = objFachada.RecuperarSIST_Habilitados();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSupervisores.dcRecuperarSupervisores objResoSupervisores = objFachada.RecuperarSupervisores();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarUsuarios.dcRecuperarUsuarios objRespUsuarios = objFachada.RecuperarUsuarios();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarSupervisores.dcRecuperarSupervisores objRespSupervisores = objFachada.RecuperarSupervisores();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperaTerminales.dcRecuperaTerminales objRespTerminales = objFachada.RecuperaTerminales();
                COA.WebCipol.Entidades.ClasesWs.dcRecuperaTablasDeSistemas.dcRecuperaTablasDeSistemas objRespTablas = objFachada.RecuperaTablasDeSistemas();

                objRetorno.jsoncbonombrepc = rc.GenerarCombos(new DatosCboGenerico() { DataSource = objRespTerminales.lstTerminales, DataTextField = "CODTERMINAL", DataValueField = "CODTERMINAL", Id = "cboTerminales", itemTodos = null });
                objRetorno.jsoncbosupervisores = rc.GenerarCombos(new DatosCboGenerico() { DataSource = objResoSupervisores.lstDtSupervisores, DataTextField = "NOMBRES", DataValueField = "USUARIO", Id = "cboSupervisor", itemTodos = null });
                objRetorno.jsoncbousuarios = rc.GenerarCombos(new DatosCboGenerico() { DataSource = objRespUsuarios.lstUsuarios, DataTextField = "NOMBRES", DataValueField = "USUARIO", Id = "cboUsuario", itemTodos = null });
                objRetorno.jsoncbotablas = rc.GenerarCombos(new DatosCboGenerico() { DataSource = objRespTablas.lstTablasDeSistema, DataTextField = "NombreTabla", DataValueField = "NombreTabla", Id = "cboTabla", itemTodos = null });
                objRetorno.jsoncbosistemas = rc.GenerarCombos(new DatosCboGenerico() { DataSource = objRespSistemas.lstSE_SIST_HABILITADOS, DataTextField = "DESCSISTEMA", DataValueField = "DESCSISTEMA", Id = "cboSistema", itemTodos = null });

                objRetorno.ResultadoEjecucion = true;

                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        /// <summary>
        /// [WebMethod] - Recupera los eventos del sistema
        /// </summary>
        /// <param name="fechaDesde">Fecha Desde</param>
        /// <param name="fechaHasta">Fecha Hasta</param>
        /// <param name="tabla">Nombre de la tabla de la base de datos</param>
        /// <param name="usuario">Usuarios</param>
        /// <param name="operacion">Operación</param>
        /// <param name="supervisor">Supervisor</param>
        /// <param name="nombrePC">Nombre PC</param>
        /// <param name="sistema">Sistema</param>
        /// <param name="textoBusqueda">Texto de Busqueda</param>
        /// <returns>Lista de eventos</returns>
        /// <history>
        /// [IvanSa]          [Martes, 22 de octubre de 2013]       Creado GCP-Cambios ID: 14499
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// [LucianoP]          [viernes, 2 de junio de 2017]    Se aplican validaciones sobre el filtro seleccionado
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public dcRecuperarEventosAuditoria RecuperarEventosAuditoria(vAuditoriaFiltro obj)
        {
            try
            {
                vEventosAuditoria objRetorno = new vEventosAuditoria();
                FSeguridad objFachada = new FSeguridad();

                /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //1. VALIDACIONES SOBRE EL FILTRO SELECCIONADO
                DateTime fechaD = vUtiles.StringToDateNull(obj.fechaDesde).Value;
                DateTime fechaH = vUtiles.StringToDateNull(obj.fechaHasta).Value.AddHours(23).AddMinutes(59).AddSeconds(59);

                //1.1 FECHAS
                if (fechaH < fechaD)
                {
                    return new dcRecuperarEventosAuditoria()
                    {
                        CantRegistrosTotal = -1,
                        Sist_Eventos = null,
                        Validaciones = new ValidacionesEventosAuditoria()
                        {
                            ResultadoEjecucion = false,
                            Mensaje = "La fecha hasta no puede ser menor a la fecha desde."
                        }
                    };
                }

                //1.2 RANGO DE FECHAS
                if (fechaD < fechaH.AddMonths(-1))
                {
                    return new dcRecuperarEventosAuditoria()
                    {
                        CantRegistrosTotal = -1,
                        Sist_Eventos = null,
                        Validaciones = new ValidacionesEventosAuditoria()
                        {
                            ResultadoEjecucion = false,
                            Mensaje = "El intervalo de fechas no puede ser mayor a un mes."
                        }
                    };
                }

                //1.3 SELECCION DE FILTROS
                if (obj.nombrePC.ToUpper().Trim() == "(TODAS)" && obj.sistema.ToUpper().Trim() == "(TODOS)" &&
                    obj.supervisor.ToUpper().Trim() == "(TODOS)" && obj.tabla.ToUpper().Trim() == "(TODAS)" &&
                    obj.usuario.ToUpper().Trim() == "(TODOS)")
                {
                    return new dcRecuperarEventosAuditoria()
                    {
                        CantRegistrosTotal = -1,
                        Sist_Eventos = null,
                        Validaciones = new ValidacionesEventosAuditoria()
                        {
                            ResultadoEjecucion = false,
                            Mensaje = " Debe establecer al menos un filtro para la consulta."
                        }
                    };
                }

                dcRecuperarEventosAuditoriaIN auditoriaIN = new dcRecuperarEventosAuditoriaIN();

                DtFiltros filtro = new DtFiltros()
                {
                    FECHADESDE = fechaD,
                    FECHAHASTA = fechaH,
                    TABLA = obj.tabla.Trim(),
                    OPERACION = obj.operacion.Trim(),
                    SUPERVISOR = obj.supervisor.Trim(),
                    SISTEMA = obj.sistema.Trim(),
                    USUARIO = obj.usuario.Trim(),
                    TEXTOBUSCAR = obj.textoBusqueda.Trim(),
                    NOMBREPC = obj.nombrePC.Trim(),
                    CantidadRegistrosDefault = obj.CantidadRegistrosDefault
                };

                auditoriaIN.lstFiltros.Add(filtro);

                dcRecuperarEventosAuditoria objResp = objFachada.RecuperarEventosAuditoria(auditoriaIN);

                objResp.Validaciones = new ValidacionesEventosAuditoria() { ResultadoEjecucion = true };

                return objResp;
            }
            catch (Exception Ex)
            {
                PublicarEx(Ex);
                return new dcRecuperarEventosAuditoria()
                {
                    CantRegistrosTotal = -1,
                    Sist_Eventos = null,
                    Validaciones = new ValidacionesEventosAuditoria()
                    {
                        ResultadoEjecucion = false,
                        Mensaje = "Ocurrió un error al intentar recuperar el registro de eventos [" + Ex.Message + "]"
                    }
                };
            }
        }

        /// <summary>
        /// [WebMethod] - Retorna la consulta SQL efectuada de acuerdo a los campos del filtro seleccionados
        /// </summary>
        /// <param name="fechaDesde">Fecha Desde</param>
        /// <param name="fechaHasta">Fecha Hasta</param>
        /// <param name="tabla">Nombre de la tabla de la base de datos</param>
        /// <param name="usuario">Usuarios</param>
        /// <param name="operacion">Operación</param>
        /// <param name="supervisor">Supervisor</param>
        /// <param name="nombrePC">Nombre PC</param>
        /// <param name="sistema">Sistema</param>
        /// <param name="textoBusqueda">Texto de Busqueda</param>
        /// <returns>Consulta SQL efectuada de acuerdo a los campos del filtro seleccionados</returns>
        /// <history>
        /// [IvanSa]          [Miercoles, 23 de octubre de 2013]       Creado GCP-Cambios ID: 14500
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string RetornarStringSQL(string fechaDesde, string fechaHasta, string tabla, string usuario,
                                                                string operacion, string supervisor, string nombrePC,
                                                                string sistema, string textoBusqueda)
        {
            try
            {
                StringSQL SQL = new StringSQL();



                DateTime fechaD = vUtiles.StringToDateNull(fechaDesde).Value;
                DateTime fechaH = vUtiles.StringToDateNull(fechaHasta).Value.AddHours(23).AddMinutes(59).AddSeconds(59);

                FSeguridad objFachada = new FSeguridad();

                COA.WebCipol.Entidades.ClasesWs.dcRetornaStringSQL.dcRetornaStringSQL objSQL = new Entidades.ClasesWs.dcRetornaStringSQL.dcRetornaStringSQL();

                COA.WebCipol.Entidades.ClasesWs.dcRetornaStringSQL.DtFiltros filtro =
                new Entidades.ClasesWs.dcRetornaStringSQL.DtFiltros()
                {
                    FECHADESDE = fechaD,
                    FECHAHASTA = fechaH,
                    TABLA = tabla,
                    OPERACION = operacion,
                    SUPERVISOR = supervisor,
                    SISTEMA = sistema,
                    USUARIO = usuario,
                    TEXTOBUSCAR = textoBusqueda,
                    NOMBREPC = nombrePC
                };

                objSQL.lstFiltros.Add(filtro);

                SQL.STRINGSQL = objFachada.RetornaStringSQL(objSQL);

                var json = new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(SQL);

                return json;

            }
            catch (Exception ex)
            {
                PublicarEx(ex);
                throw (ex);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <history>
        /// [MartinV]          [miércoles, 20 de noviembre de 2013]       Modificado  GCP-Cambios 14665
        /// </history>
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public void ExportarSQL(StringSQL obj)
        {
            try
            {
                objDatosStringSQL = new vSessionStringSQL();
                objDatosStringSQL.stringSQL = obj;
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }

        #region "Variables de Sesion"

        /// <summary>
        /// Variable de sesión donde se guarada la consulta SQL luego para luego poder ser exportada a un archivo ".sql"
        /// </summary>
        /// <history>
        /// [IvanSa]          [Miercoles, 23 de octubre de 2013]       Creado GCP-Cambios ID: 14500 - 14499
        /// </history>
        private vSessionStringSQL objDatosStringSQL
        {
            get
            {
                return (vSessionStringSQL)HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.STRING_SQL];
            }
            set
            {
                HttpContext.Current.Session[COA.WebCipol.Comun.Constantes.VariablesDeSesion.STRING_SQL] = value;
            }
        }

        #endregion

        #endregion

        #region "Salir"

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public string SalirSistema()
        {
            try
            {
                //Fachada.FInicioSesion objInicioSesion = new FInicioSesion();
                ////cierra la sessión.
                //objInicioSesion.CerrarSesion();

                string mensaje = ManejoSesion.MensajeCerrar;
                HttpContext.Current.Session.Abandon();

                return mensaje;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        private static void PublicarEx(Exception Ex)
        {
            global::Fachada.PadreFachada objFac = new global::Fachada.PadreFachada();
            objFac.PublicarExcepcion(Ex);
        }
    }
}
