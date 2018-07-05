using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.WebCipol.Fachada;
using Microsoft.Reporting.WebForms;
using COA.Cipol.Presentacion._UIHelpers;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion
{
    public partial class frmRptUsuarios : PaginaPadre
    {
        /// <summary>
        /// Retorna el id de tarea que permite acceder al formulario.
        /// </summary>
        /// <history>
        /// [MartinV]          [viernes, 15 de noviembre de 2013]       Modificado  GCP-Cambios 14583
        /// </history>
        public override string IDTarea
        {
            //Ver Reportes
            get { return "1009"; }
        }

        private List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Horarios_Usuario> dsSubReporteHorarios;
        private List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.RolesXUsuarios> dsSubReporteRoles;
        private List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.SE_Term_Usuario> dsSubReporteTerminales;

        protected override void Evento_load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                IncializarComboFiltro();
            }
        }

        private void IncializarComboFiltro()
        {
            FReportes objFac = new FReportes();
            List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteRolesXUsuarios.Sist_Usuarios> lstUsuarios = objFac.RecuperarDatosParaReporteRolesXUsuarios().lstSist_Usuarios;

            cboUsuario.DataSource = lstUsuarios;
            cboUsuario.DataValueField = "IDUSUARIO";
            cboUsuario.DataTextField = "NOMBRES";
            cboUsuario.DataBind();
                       
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <history>
        /// [MartinV]          [jueves, 18 de septiembre de 2014]       Modificado  GCP-Cambios 15581
        /// </history>
        protected void cmdGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Text = "";
                divError.Visible = false;
                FReportes objFac = new FReportes();
                int intIDUsuario = Convert.ToInt32(cboUsuario.SelectedValue);
                COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuario.dcRecuperarReporteUsuario objLista = objFac.RecuperarReporteUsuario(intIDUsuario);

                if (objLista.lstSist_Usuarios.Count == 0)
                {
                    lblSinDatos.Visible = true;
                    divInfo.Visible = true;
                    return;
                }
                else
                {
                    lblSinDatos.Visible = false;
                    divInfo.Visible = false;
                }

                dsSubReporteHorarios = objLista.lstSE_Horarios_Usuario;
                dsSubReporteRoles = objLista.lstRolesXUsuarios;
                dsSubReporteTerminales = objLista.lstSE_Term_Usuario;

                ReportDataSource objDS = new ReportDataSource("lstUsuarios", objLista.lstSist_Usuarios);
                ReportViewer VisualizarReporte = new ReportViewer();
                VisualizarReporte.ProcessingMode = ProcessingMode.Local;
                VisualizarReporte.LocalReport.ReportPath = this.MapPath("~/Reportes/rptUsuarios.rdlc");

                VisualizarReporte.LocalReport.DataSources.Clear();
                VisualizarReporte.LocalReport.DataSources.Add(objDS);
                VisualizarReporte.ZoomMode = ZoomMode.Percent;

                //Setea Parámetros generales de todos los reportes.
                //Se verifica si tiene permiso para todos los horarios 
                //7 dias x 24 horas = 168 registros, es decir, todos los horarios
                string strCondHorarios = "";
                if (objLista.lstSE_Horarios_Usuario.FindAll(x => x.IdUsuario.Equals(0)).Count == 168)
                {
                    strCondHorarios = "Todos";
                }
                if (objLista.lstSE_Horarios_Usuario.FindAll(x => x.IdUsuario.Equals(0)).Count == 0)
                {
                    strCondHorarios = "Ninguno";
                }

                //Se analiza si la cantidad de terminales, es igual a la cantidad de terminales permitidas para el usuario
                string strCondTerminales = "";
                if (objLista.lstSE_TERMINALES.Count == objLista.lstSE_Term_Usuario.Count)
                {
                    strCondTerminales = "Todas";
                }
                //Se analiza si no tiene terminales habilitadas el usuario
                if (objLista.lstSE_Term_Usuario.Count == 0)
                {
                    strCondTerminales = "Ninguna";
                }
                string strCondRoles = "";
                if (objLista.lstRolesXUsuarios.Count == 0)
                {
                    strCondRoles = "Ninguno";
                }
                else
                {
                    strCondRoles = "SI";
                }

                List<ReportParameter> lstParametros = new List<ReportParameter>();
                lstParametros.Add(new ReportParameter("fmlCliente", ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente));
                lstParametros.Add(new ReportParameter("rptPiePagina", ManejoSesion.DatosSistemaSesion.DatosGenerales.rptPiePagina()));
                lstParametros.Add(new ReportParameter("condSubHorarios", strCondHorarios));
                lstParametros.Add(new ReportParameter("condSubTerminales", strCondTerminales));
                lstParametros.Add(new ReportParameter("condSubRol", strCondRoles));

                VisualizarReporte.LocalReport.SetParameters(lstParametros);

                VisualizarReporte.Visible = true;

                //VisualizarReporte.DataBind();

                VisualizarReporte.LocalReport.Refresh();

                VisualizarReporte.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(UsuariosHorario_SubreportProcessing);

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                Session["DatosPDF"] = VisualizarReporte.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                string strScript_max = "f_open_window_max('VisualizadorPDF.aspx','" + DateTime.Now.ToString("hhmmss") + "');";
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Exportar", strScript_max, true);
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                lblError.Text = ex.Message;
                divError.Visible = true;
            }

        }

        void UsuariosHorario_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            short shtIdUsuario = Convert.ToInt16(e.Parameters["IdU"].Values[0]);

            switch (e.ReportPath)
            {
                case "rptSubHorarios":
                    ReportDataSource ReportDataSource1 = new ReportDataSource("TablaHorarios", dsSubReporteHorarios);
                    e.DataSources.Add(ReportDataSource1);
                    break;
                case "rptSubRolesXUsuarios":
                    ReportDataSource ReportDataSource2 = new ReportDataSource("lstRolesXUsuarios", dsSubReporteRoles);
                    e.DataSources.Add(ReportDataSource2);
                    break;
                case "rptSubTerminales":
                    ReportDataSource ReportDataSource3 = new ReportDataSource("TablaUsu", dsSubReporteTerminales);
                    e.DataSources.Add(ReportDataSource3);
                    break;
            }
        }
    }



}