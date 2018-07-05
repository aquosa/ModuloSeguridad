using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Presentacion;
using COA.WebCipol.Fachada;
using Microsoft.Reporting.WebForms;
using COA.WebCipol.Presentacion.view;

namespace COA.WebCipol.Presentacion
{
    public partial class frmRptAnalisisAuditoria : PaginaPadre
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
                try
                {
                    lblError.Text = "";
                    divError.Visible = false;

                    string strFechaDesde = Request.QueryString["fechaDesde"].ToString();
                    string strFechaHasta = Request.QueryString["fechaHasta"].ToString();
                    string strTabla = Request.QueryString["tabla"].ToString();
                    string strUsuario = Request.QueryString["usuario"].ToString();
                    string strOperacion = Request.QueryString["operacion"].ToString();
                    string strSupervisor = Request.QueryString["supervisor"].ToString();
                    string strNombrePC = Request.QueryString["nombrePC"].ToString();
                    string strSistema = Request.QueryString["sistema"].ToString();
                    string strTextoBusqueda = Request.QueryString["textoBusqueda"].ToString();
                    string strCantidadRegistrosDefault = Request.QueryString["CantidadRegistrosDefault"].ToString();

                    DateTime? fechaDesde = vUtiles.StringToDateNull(strFechaDesde);
                    DateTime fechaHasta = vUtiles.StringToDateNull(strFechaHasta).Value.AddHours(23).AddMinutes(59).AddSeconds(59);

                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria.dcRecuperarEventosAuditoriaIN auditoriaIN =
                  new Entidades.ClasesWs.dcRecuperarEventosAuditoria.dcRecuperarEventosAuditoriaIN();

                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria.DtFiltros filtro =
                    new Entidades.ClasesWs.dcRecuperarEventosAuditoria.DtFiltros()
                    {
                        FECHADESDE = fechaDesde.Value,
                        FECHAHASTA = fechaHasta,
                        TABLA = strTabla.Trim(),
                        OPERACION = strOperacion.Trim(),
                        SUPERVISOR = strSupervisor.Trim(),
                        SISTEMA = strSistema.Trim(),
                        USUARIO = strUsuario.Trim(),
                        TEXTOBUSCAR = strTextoBusqueda.Trim(),
                        NOMBREPC = strNombrePC.Trim(),
                        CantidadRegistrosDefault = strCantidadRegistrosDefault
                    };

                    auditoriaIN.lstFiltros.Add(filtro);

                    FSeguridad objFachada = new FSeguridad();
                    COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria.dcRecuperarEventosAuditoria objResp =
                      objFachada.RecuperarEventosAuditoria(auditoriaIN);

                    List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarEventosAuditoria.SIST_EVENTOS> lstEventos = null;

                    if (objResp.Sist_Eventos.Count > 0)
                    {
                        lblSinDatos.Visible = false;
                        divInfo.Visible = false;
                        //if (objResp.Sist_Eventos.Count > 100)
                        //{
                        //    lstEventos = objResp.Sist_Eventos.Take(100).ToList();
                        //}
                        //else
                        //{
                        lstEventos = objResp.Sist_Eventos;
                        //}
                    }
                    else
                    {
                        lblSinDatos.Visible = true;
                        divInfo.Visible = true;
                    }


                    ReportDataSource objDS = new ReportDataSource("lstEventos", lstEventos);
                    ReportViewer VisualizarReporte = new ReportViewer();
                    VisualizarReporte.ProcessingMode = ProcessingMode.Local;
                    VisualizarReporte.LocalReport.ReportPath = this.MapPath("~/Reportes/rptAnalisisAuditoria.rdlc");

                    VisualizarReporte.Visible = true;
                    VisualizarReporte.LocalReport.DataSources.Clear();
                    VisualizarReporte.LocalReport.DataSources.Add(objDS);
                    VisualizarReporte.ZoomMode = ZoomMode.Percent;

                    // Variables
                    Warning[] warnings;
                    string[] streamIds;
                    string mimeType = string.Empty;
                    string encoding = string.Empty;
                    string extension = string.Empty;

                    byte[] bytPDF = VisualizarReporte.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                    string NombrePDF = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_Documento.xls";

                    Response.Clear();
                    Response.ClearContent();
                    Response.ClearHeaders();

                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "filename=" + NombrePDF);
                    Response.AddHeader("content-length", bytPDF.Length.ToString());
                    Response.BinaryWrite(bytPDF);
                    Response.Flush();
                    Response.End();
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
}