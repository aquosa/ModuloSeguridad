using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using COA.Cipol.Presentacion._UIHelpers;
using System.Data;
using System.DirectoryServices;
using Microsoft.VisualBasic;
using COA.WebCipol.Entidades;
using EntidadesEmpresariales;
using COA.WebCipol.Comun;
using Fachada;
using COA.WebCipol.Presentacion.Controlers;

namespace COA.Cipol.Presentacion
{
    public class PaginaPadre : System.Web.UI.Page
    {
        public virtual string IDTarea
        {
            get { return "0"; }
        }

        public virtual bool DebeValidarse
        {
            get { return false; }
        }

        public virtual string NombrePagina
        {
            get
            {
                return Request.FilePath.Substring((Request.FilePath.LastIndexOf("/") + 1));
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidarSession();

            //Verifica que no se ingrese a una página no habilitada para el usuario.
            AplicarSeguridad(sender, e);

            //Ejecuta el evento Load
            Evento_load(sender, e);
        }

        private void AplicarSeguridad(object sender, EventArgs e)
        {
            // Puede valdar por tarea o por nombre de página
            if (!String.IsNullOrEmpty(IDTarea) || DebeValidarse)
            {
                //Verifica los ID de tareas que posee el usuario para cada item de menu (IsInRole te devuelve un bool). 
                EntidadesEmpresariales.PadreCipolCliente objCipol = (EntidadesEmpresariales.PadreCipolCliente)ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente;
                int intIDTarea = 0;


                if (!this.IsPostBack)
                {
                    if (objCipol != null)
                    {
                        //usuario master
                        if (objCipol.IDUsuario.Equals(0)) return;
                        //Controla si debe validarse por nombre de página
                        if (DebeValidarse)
                            objCipol.PermiteRequest(NombrePagina);

                        if (IDTarea != "0")
                        {
                            try
                            {
                                intIDTarea = Convert.ToInt32(IDTarea);
                                if (!objCipol.IsInRole(intIDTarea.ToString()))
                                    Response.Redirect("frmNoDisponeDePermiso.aspx");
                            }
                            catch (Exception) //Si el IDTarea es no numérico, tira excepción el método IsInRole().
                            {
                                Response.Redirect("frmNoDisponeDePermiso.aspx");
                            }
                        }
                        else
                            Response.Redirect("frmNoDisponeDePermiso.aspx");

                        string strEvento = "";
                        int IdTareaSupervisor = intIDTarea;

                        if (objCipol.RequiereSupervision(NombrePagina, ref IdTareaSupervisor))
                        {
                            System.Web.UI.HtmlControls.HtmlControl objHTML = (System.Web.UI.HtmlControls.HtmlControl)this.Master.FindControl("cuerpo");
                            if (objHTML == null)
                                throw new Exception("No existe el tag <Body> como control de servidor en la página" + NombrePagina);
                            else
                                objHTML.Attributes["onload"] = "appCIPOLPRESENTACION.SupervisarModal('" + strEvento + "');";
                            Session["IDTareaSupervisar"] = IdTareaSupervisor;
                        }
                    }
                    //Ver si se debe hacer algo en este caso.
                }
            }
            //Ver si se debe hacer algo en este caso.
        }

        /// <summary>
        /// Evento que reemplaza al Load en cada página que hereda de PáginaPadre.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void Evento_load(object sender, EventArgs e)
        {
            //usar este evento en lugar del Page_Load() en las págs. heredadas!!!
        }

        /// <summary>
        /// Valida que la sesion del usuario este activa
        /// </summary>
        /// <history>
        /// [LucianoP]          [jueves, 6 de abril de 2017]      Se registra el cierre de sesion involuntario
        /// </history>
        private void ValidarSession()
        {
            try
            {
                if (ManejoSesion.DatosCIPOLSesion == null)
                {
                    cInicioSesion objIS = new cInicioSesion();
                    objIS.RegistrarExpiroSesion();

                    COA.Logger.Logueador.Loggear("Expiró la sesión", System.Diagnostics.EventLogEntryType.Error);
                    Response.Redirect("frmInicio.aspx");
                }
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                Response.Redirect("frmInicio.aspx");
            }
        }
        #region Auditoría

        /// <summary>
        /// Devuelve un mensaje a mostrar al usuario, según el código correspondiente.
        ///     El dataset gdtsAuditoria debe estar en sesión (DEBE CARGARSE AL INICIAR EL SISTEMA).
        /// </summary>
        /// <param name="CodMensaje"></param>
        /// <param name="CantDias"></param>
        /// <returns></returns>
        /// <history>
        /// [PabloC]          [viernes, 02 de marzo de 2012]       Migrado a C# y modificado para Entorno Web.
        /// </history>
        public static string RetornaMensajeUsuario(short CodMensaje, int? CantDias = null)
        {
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //Autor: Gustavo Mazzaglia
            //Fecha de creación: 28/12/2001
            //Modificaciones:
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //                   DESCRIPCION DE VARIABLES LOCALES
            //strMensaje: Mensaje a mostrar
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            SE_MENSAJES rowFilas = null;

            string strMensajeRet = null;//TODO[CIPOLWEB] 

            rowFilas = ManejoSesion.gdtsAuditoria.lstMensajes.FirstOrDefault(P => P.CODMENSAJE == CodMensaje);

            if (rowFilas != null)
            {
                strMensajeRet = rowFilas.TEXTOMENSAJE.Trim();
                switch (CodMensaje)
                {
                    case 25:
                    case 30:
                        if ((CantDias.HasValue))
                            strMensajeRet = strMensajeRet.Replace("@", CantDias.Value.ToString()); //TODO[CIPOLWEB] 
                        break;
                    default:
                        break;
                }
            }
            return strMensajeRet;

        }

        //public bool BuscarUsuarioDominio(string Usuario)
        //{
        //    ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    //Autor: Angel lubenov
        //    //Fecha de creación: 06/12/2005
        //    //Modificaciones:
        //    ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    //               DESCRIPCION DE VARIABLES LOCALES
        //    //strPAth            : ruta de acceso al servicio de directorio
        //    //strDom             : dominio del servicio de directorios
        //    //intI               : contador para el for.
        //    //objBuscador        : buscador del servicio de directorios para el usuario.
        //    ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    string strPath = null;
        //    string[] strDom = null;
        //    Int32 intI = default(Int32);
        //    System.DirectoryServices.DirectorySearcher objBuscador = new DirectorySearcher();

        //    try
        //    {
        //        strPath = "LDAP://";
        //        strDom = ManejoSesion.UsuarioCipol.NombreDominio.Split('.');
        //        for (intI = 0; intI <= strDom.GetUpperBound(0); intI++)
        //        {
        //            strPath += "DC=";
        //            strPath += strDom[intI].Split('æ')[0];
        //            strPath += ",";
        //        }
        //        strPath = strPath.Substring(0, strPath.Length - 1);


        //        objBuscador = new DirectorySearcher((new System.DirectoryServices.DirectoryEntry(strPath)));
        //        //una vez creado la entrada con el dominio de coa, 
        //        //agregamos un filtro para que solo muestre los usuarios
        //        //con el login indicado
        //        objBuscador.Filter = "(&(objectClass=user)(samaccountname=" + Usuario.Trim() + "))";

        //        return (objBuscador.FindOne() != null);

        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }

        //}

        #endregion

    }

}
