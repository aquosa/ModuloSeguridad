using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.WebCipol.Fachada;
using Microsoft.Reporting.WebForms;
using COA.Cipol.Presentacion._UIHelpers;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTerminales;
using COA.Cipol.Presentacion;

namespace COA.WebCipol.Presentacion
{
    public partial class frmRptTerminales : PaginaPadre
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
                IncializarCombosFiltro();
            }
        }

        private void IncializarCombosFiltro()
        {
            FSeguridad objFac = new FSeguridad();
            List<KAREAS> lstAreas = objFac.RecuperarDatosParaABMTerminales().lstKAREAS;

            lstAreas.Add(new KAREAS()
            {
                IDAREA = -1,
                NOMBREAREA = "(Todas)"
            });

            cboArea.DataSource = lstAreas;
            cboArea.DataValueField = "IDAREA";
            cboArea.DataTextField = "NOMBREAREA";
            cboArea.DataBind();

            cboArea.SelectedValue = "-1";
            
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
                
                int intArea = Convert.ToInt32(cboArea.SelectedValue);
                string strHabilitada = "";

                if (this.cboHabilitada.SelectedValue != "-1")
                    strHabilitada = (this.cboHabilitada.SelectedValue == "S" ? "1" : "0").ToString();

                List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarTerminalesParaReporte.SE_TERMINALES> lstTerminales = objFac.RecuperarTerminalesParaReporte(intArea, strHabilitada, intArea == -1);

                if (lstTerminales.Count == 0)
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

                ReportDataSource objDS = new ReportDataSource("lstTerminales", lstTerminales);
                ReportViewer VisualizarReporte = new ReportViewer();
                VisualizarReporte.ProcessingMode = ProcessingMode.Local;
                VisualizarReporte.LocalReport.ReportPath = this.MapPath("~/Reportes/rptTerminales.rdlc");

                //lblMensaje.Visible = false;
             
                VisualizarReporte.LocalReport.DataSources.Clear();
                VisualizarReporte.LocalReport.DataSources.Add(objDS);
                VisualizarReporte.ZoomMode = ZoomMode.Percent;

                //Setea Parámetros generales de todos los reportes.
                List<ReportParameter> lstParametros = new List<ReportParameter>();
                lstParametros.Add(new ReportParameter("fmlCliente", ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente));
                lstParametros.Add(new ReportParameter("rptPiePagina", ManejoSesion.DatosSistemaSesion.DatosGenerales.rptPiePagina()));
                
                VisualizarReporte.LocalReport.SetParameters(lstParametros);

                VisualizarReporte.Visible = true;

                // Variables
                Warning[] warnings;
                string[] streamIds;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string extension = string.Empty;

                Session["DatosPDF"] = VisualizarReporte.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                Session["NombrePDF"] = "rptTerminales.pdf";
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