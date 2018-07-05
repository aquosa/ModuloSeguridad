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
    public partial class frmRptVisorSucesos : PaginaPadre
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
                string strFechaDesde = Request.QueryString["FechaDesde"].ToString();
                string strFechaHasta = Request.QueryString["FechaHasta"].ToString();
                string strUsuAdm = Request.QueryString["UsuAdm"].ToString();
                string strUsuAfe = Request.QueryString["UsuAfe"].ToString();
                string strCodEvento = Request.QueryString["CodMsj"].ToString();


                DateTime dtFechaDesde = vUtiles.StringToDateNull(strFechaDesde).Value;
                DateTime dtFechaHasta = vUtiles.StringToDateNull(strFechaHasta).Value;
                
                FReportes objFac = new FReportes();
             
                COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria.dcRetornarLogAuditoria objResp =
                objFac.RetornarLogAuditoria(new Entidades.ClasesWs.dcRetornarLogAuditoria.dcRetornarLogAuditoriaIN()
                {
                    fechadesde = dtFechaDesde,
                    fechahasta = dtFechaHasta,
                    UsuarioActuante = strUsuAdm,
                    usuarioafectado = strUsuAfe,
                    CodigoEvento = strCodEvento
                });

                object objLista = objResp.lstAuditoria.OrderByDescending(v => v.FECHAHORALOG).ToList();

                ReportDataSource objDS = new ReportDataSource("lstAuditoria", objLista);
                ReportViewer VisualizarReporte = new ReportViewer();
                VisualizarReporte.ProcessingMode = ProcessingMode.Local;
                VisualizarReporte.LocalReport.ReportPath = this.MapPath("~/Reportes/rptVisorSucesos.rdlc");

                List<ReportParameter> lstParametros = new List<ReportParameter>();
                lstParametros.Add(new ReportParameter("fmlCliente", ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente));
                lstParametros.Add(new ReportParameter("rptPiePagina", ManejoSesion.DatosSistemaSesion.DatosGenerales.rptPiePagina()));
                lstParametros.Add(new ReportParameter("fmlFechaDesde", strFechaDesde));
                lstParametros.Add(new ReportParameter("fmlFechaHasta", strFechaHasta));
                lstParametros.Add(new ReportParameter("fmlUsuarioAdmin", strUsuAdm));
                lstParametros.Add(new ReportParameter("fmlUsuarioAfect", strUsuAfe));
                lstParametros.Add(new ReportParameter("fmlCodMensaje", strCodEvento));
                VisualizarReporte.LocalReport.SetParameters(lstParametros);
     
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

                byte[] bytPDF = VisualizarReporte.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);
                string NombrePDF = DateTime.Now.ToString("ddMMyyyyHHmmss") + "_Documento.pdf";

                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();

                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "filename=" + NombrePDF);
                Response.AddHeader("content-length", bytPDF.Length.ToString());
                Response.BinaryWrite(bytPDF);
                Response.Flush();
                Response.End();
            }
        }
    }
}