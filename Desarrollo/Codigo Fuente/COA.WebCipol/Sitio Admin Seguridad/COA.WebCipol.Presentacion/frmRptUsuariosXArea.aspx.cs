using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.WebCipol.Fachada;
using Microsoft.Reporting.WebForms;
using COA.Cipol.Presentacion._UIHelpers;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarAreas;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion
{
    public partial class frmRptUsuariosXArea : PaginaPadre
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
        protected override void Evento_load(object sender, EventArgs e)
        {
            if (!base.IsPostBack)
            {
                IncializarComboFiltro();
            }
        }

        private void IncializarComboFiltro()
        {
            FSeguridad objFac = new FSeguridad();
            List<SIST_KAREAS> lstAreas = objFac.RecuperarAreas().lstKAreas;
            List<SIST_KAREAS> lstAraesFiltradas = lstAreas.FindAll(x => x.FICTICIA == "N" && x.BAJA == "N").ToList();

            lstAraesFiltradas.Add(new SIST_KAREAS()
            {
                IDAREA = -99,
                NOMBREAREA = "Todas"
            });

            cboArea.DataSource = lstAraesFiltradas;
            cboArea.DataValueField = "IDAREA";
            cboArea.DataTextField = "NOMBREAREA";
            cboArea.DataBind();

            cboArea.SelectedValue = "-99";
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
                
                int intIDArea = Convert.ToInt32(cboArea.SelectedValue);

                List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXArea.UsuariosXAreas> lstUsuarios = objFac.RecuperarReporteUsuariosXArea(intIDArea);

                if (lstUsuarios.Count == 0)
                {
                    lblSinDatos.Visible = true;
                    this.divInfo.Visible = true;
                    return;
                }
                else
                {
                    lblSinDatos.Visible = false;
                    this.divInfo.Visible = false;
                }

                ReportDataSource objDS = new ReportDataSource("lstUsuariosXAreas", lstUsuarios);
                ReportViewer VisualizarReporte = new ReportViewer();
                VisualizarReporte.ProcessingMode = ProcessingMode.Local;
                VisualizarReporte.LocalReport.ReportPath = this.MapPath("~/Reportes/rptUsuariosXArea.rdlc");

                VisualizarReporte.LocalReport.DataSources.Clear();
                VisualizarReporte.LocalReport.DataSources.Add(objDS);
                VisualizarReporte.ZoomMode = ZoomMode.Percent;

                List<ReportParameter> lstParametros = new List<ReportParameter>();
                lstParametros.Add(new ReportParameter("fmlCliente", ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente));
                lstParametros.Add(new ReportParameter("rptPiePagina", ManejoSesion.DatosSistemaSesion.DatosGenerales.rptPiePagina()));
                
                VisualizarReporte.LocalReport.SetParameters(lstParametros);

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
    }
}