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
using COA.WebCipol.Presentacion.view;

namespace COA.WebCipol.Presentacion
{
    public partial class frmRptControlInactividad : PaginaPadre
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

                DateTime dtFechaHasta = new DateTime();
                string strCtrlInactividad = "";
                if (this.rdbRangoFechas.Checked)
                {
                    if (String.IsNullOrEmpty(this.txtFechaHasta.Value))
                    {
                        
                        lblError.Text = "Debe ingresar la fecha hasta si selecciona esta opción. Verifique";
                        divError.Visible = true;
                        return;
                    }
                    dtFechaHasta = vUtiles.StringToDateNull(this.txtFechaHasta.Value).Value;
                }
                else
                {
                    dtFechaHasta = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.FechaServidor.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                    switch (this.cboLapso.SelectedValue)
                    {
                        case "MENOR30":
                            strCtrlInactividad = "<= 30";
                            break;
                        case "MAYOR30":
                            strCtrlInactividad = "> 30";
                            break;
                    }
                }
                
                FReportes objFac = new FReportes();
                List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteControlInactividad.Sist_Usuarios> lstUsuarios = objFac.RecuperarReporteControlInactividad(dtFechaHasta, this.rdbPeriodo.Checked ? strCtrlInactividad : "");

                if (lstUsuarios.Count == 0)
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

                ReportViewer VisualizarReporte = new ReportViewer();
                VisualizarReporte.ProcessingMode = ProcessingMode.Local;
                VisualizarReporte.LocalReport.ReportPath = this.MapPath("~/Reportes/rptControlInactividad.rdlc");
                ReportDataSource objDS = new ReportDataSource("lstCtrlInactividad", lstUsuarios);
                VisualizarReporte.LocalReport.DataSources.Clear();
                VisualizarReporte.LocalReport.DataSources.Add(objDS);

                List<ReportParameter> lstParametros = new List<ReportParameter>();
                lstParametros.Add(new ReportParameter("fmlCliente", ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente));
                lstParametros.Add(new ReportParameter("rptPiePagina", ManejoSesion.DatosSistemaSesion.DatosGenerales.rptPiePagina()));
                lstParametros.Add(new ReportParameter("fecHasta", dtFechaHasta.ToShortDateString()));
                lstParametros.Add(new ReportParameter("lapInact", strCtrlInactividad));
                VisualizarReporte.LocalReport.SetParameters(lstParametros);

                VisualizarReporte.Visible = true;

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