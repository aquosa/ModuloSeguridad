using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using COA.Cipol.Inicio._UIHelpers;
using System.Data;
using System.DirectoryServices;
using Microsoft.VisualBasic;
using COA.WebCipol.Entidades;
using EntidadesEmpresariales;
using COA.WebCipol.Comun;
using COA.WebCipol.Inicio.Controlers;
using System.Configuration;
using WebAppControlDualLogin.Model;

namespace COA.Cipol.Presentacion
{
    public class PaginaPadre : System.Web.UI.Page
    {
        protected string IDTAREA_SISTEMAS_PERMITIDOS = "-1";

        public virtual string IDTarea
        {
            get { return "0"; }
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
            if (!String.IsNullOrEmpty(IDTarea))
            {
                //Verifica los ID de tareas que posee el usuario para cada item de menu (IsInRole te devuelve un bool). 
                PadreCipolCliente objCipol = (PadreCipolCliente)ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente;
                if (objCipol != null)
                {
                    if (objCipol.IDUsuario.Equals(0) || IDTarea == IDTAREA_SISTEMAS_PERMITIDOS)
                        return;

                    if (IDTarea != "0")
                    {
                        try
                        {
                            int intIDTarea = Convert.ToInt32(IDTarea);

                            if (!objCipol.IsInRole(intIDTarea.ToString()))
                            {
                                Response.Redirect("frmNoDisponeDePermiso.aspx");
                            }
                        }
                        catch (Exception) //Si el IDTarea es no numérico, tira excepción el método IsInRole().
                        {
                            Response.Redirect("frmNoDisponeDePermiso.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("frmNoDisponeDePermiso.aspx");
                    }
                }
                //Ver si se debe hacer algo en este caso.
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
        /// [LucianoP]          [viernes, 14 de julio de 2017]    Se valida que el ID de sesion sea el mismo.
        /// </history>
        private void ValidarSession()
        {
            try
            {
                if (ManejoSesion.DatosCIPOLSesion == null)
                {
                    CerrarSesion();
                    return;
                }

                if (!DatosSesion.Control.VerificarId(Application["Sessions"] as List<mSession>, Session))
                {
                    CerrarSesion();
                    return;
                }
            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                Response.Redirect("SesionExpirada.aspx");
            }
        }

        private void CerrarSesion()
        {
            cInicioSesion objIS = new cInicioSesion();
            objIS.RegistrarExpiroSesion();

            COA.Logger.Logueador.Loggear("Expiró la sesión", System.Diagnostics.EventLogEntryType.Error);

            Session.Abandon();

            Response.Redirect("SesionExpirada.aspx");
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
