using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using COA.Cipol.Inicio._UIHelpers;
using COA.WebCipol.Comun;
using System.Net;
using System.IO;
using System.Text;
using COA.Cipol.Presentacion;
using COA.WebCipol.Inicio.Controlers;

namespace COA.WebCipol.Presentacion
{
    public partial class frmSistemasPermitidos : PaginaPadre
    {
        public override string IDTarea
        {
            get
            {
                return IDTAREA_SISTEMAS_PERMITIDOS;
            }
        }

        public string URLSesionExpiro
        {
            get
            {
                return this.ResolveClientUrl("~/SesionExpirada.aspx");
            }
        }

        /// <summary>
        /// Load
        /// </summary>
        /// <history>
        ///  [LeandroF]     [miércoles, 26 de agosto de 2015]      TFS-WorkItem ID: 4750
        ///  [LucianoP]          [jueves, 6 de abril de 2017]      Se registra el cierre de sesion involuntario
        /// </history>
        protected override void Evento_load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                //    Response.Redirect("frmLogin.aspx");ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio == null || ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio == Constantes.SeguridadNODefinida)
                if (ManejoSesion.DatosCIPOLSesion == null)
                {

                    cInicioSesion objIS = new cInicioSesion();
                    objIS.RegistrarExpiroSesion();

                    Response.Redirect("frmLogin.aspx");
                }

                ///     1 No obligatorio
                ManejoSesion.ModoCambioClave = 1;
                List<Se_SistemasHabilitados> objLstSistemas = ManejoSesion.DatosCIPOLSesion.ObtenerListaSistemas();
                System.Web.UI.HtmlControls.HtmlGenericControl createDiv = new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                //si no es integrado a dominio lo habilita
                iCambiarContrasenia.Disabled = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.OtrosDatos("usuariodeldominio").Equals("1");
                //si no es integrado a dominio verifica si es SSO
                if(!iCambiarContrasenia.Disabled)
                {
                    if(ValidarLoginSSO())
                    {//si es SSO lo deshabilita
                        iCambiarContrasenia.Disabled = false;
                    }
                }
                //OtrosDatos("usuariodeldominio") = "0"

                StringBuilder htmlBuilder = new StringBuilder();
                foreach (Se_SistemasHabilitados objSistema in objLstSistemas)
                {
                    if (String.IsNullOrEmpty(objSistema.PaginaPorDefecto))
                    {
                        continue;
                    }

                    string classColor = "green";
                    string classIcon = "icon-grid-view";
                    string[] clases = null;
                    if (!string.IsNullOrEmpty(objSistema.Icono))
                        clases = objSistema.Icono.Split('*');

                    if (clases != null)
                    {
                        if (clases.Length > 0)
                            classIcon = clases[0].ToString();

                        if (clases.Length > 1)
                            classColor = clases[1].ToString();
                    }
                    //TFS-WorkItem ID: 4750
                    htmlBuilder.Append("<div class=\"tile " + classColor + "\" onclick=\"javascript:page.AbrirSistemaCIPOL('" + objSistema.PaginaPorDefecto + "','" + objSistema.IDSistema + "');return false\">");
                    htmlBuilder.Append("    <div class=\"tile-content icon\">");
                    htmlBuilder.Append("       <i class=\"" + classIcon + "\" ></i>");
                    htmlBuilder.Append("    </div>");
                    htmlBuilder.Append("    <div class=\"tile-status\">");
                    htmlBuilder.Append("        <span class=\"name\">" + objSistema.DescSistema + "</span>");
                    htmlBuilder.Append("    </div>");
                    htmlBuilder.Append("</div>");
                }
                htmlBuilder.Append("<div style=\"clear:both\"></div>");
                MenuSys.InnerHtml = htmlBuilder.ToString();
            }
        }
        /// <summary> Validar auteticación SSO
        /// </summary>
        /// <returns></returns>
        private bool ValidarLoginSSO()
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
    }
}