using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using COA.CifrarDatos;
using COA.Cipol.Inicio._UIHelpers;
using COA.WebCipol.Inicio.view;
using COA.WebCipol.Fachada;

namespace COA.WebCipol.Presentacion.PageBuilders.ws
{
    /// <summary>
    /// Descripción breve de wsTipoSeguridad
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsTipoSeguridad : System.Web.Services.WebService
    {

        #region "Tipo Seguridad"
        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vTipoSeguridadCarga RecuperarTipoSeguridadCarga(bool CambiarDominio)
        {
            try
            {
                vTipoSeguridadCarga objRetorno = new vTipoSeguridadCarga();
                if (CambiarDominio)
                    objRetorno.elemento = new vTipoSeguridad()
                    {
                        NombreDominio = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio,
                        NombreOrganizacion = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreOrganizacion,
                        Usuario = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Login,
                        optIntegrada = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio,
                        optCIPOL = !ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio,
                    };
                else
                    objRetorno.elemento = new vTipoSeguridad();

                objRetorno.EstadoOptCIPOL = !CambiarDominio;
                objRetorno.EstadoOptIntegrada = !CambiarDominio;
                return objRetorno;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        [WebMethod(EnableSession = true)]
        [System.Web.Script.Services.ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public vTipoSeguridadAdministrar AdministrarTipoSeguridad(vTipoSeguridad obj)
        {
            try
            {
                vTipoSeguridadAdministrar objRetorno = new vTipoSeguridadAdministrar();
                Fachada.FSeguridad objFachada = new FSeguridad();

                if (string.IsNullOrEmpty(obj.NombreOrganizacion.Trim()))
                {
                    objRetorno.ResultadoEjecucion = false;
                    objRetorno.MensajeError = "El nombre de la organización es un dato obligatorio";
                    return objRetorno;
                }
                if (obj.optIntegrada)
                {
                    if (string.IsNullOrEmpty(obj.NombreDominio.Trim()))
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "El nombre de dominio es un dato obligatorio";
                        return objRetorno;
                    }
                }
                else
                {
                    if (obj.validar)
                    {
                        if (string.IsNullOrEmpty(obj.NombreDominio.Trim()))
                        {
                            objRetorno.ResultadoEjecucion = true;
                            objRetorno.preguntar = true;
                            objRetorno.MensajeError = "No se ha ingresado un nombre de Dominio, solo se podrán ingresar usuarios no integrados a un dominio. ¿Continúa?";
                            return objRetorno;
                        }
                    }
                }
                string strRetorno = "";
                if (!string.IsNullOrEmpty(obj.NombreDominio.Trim()) && obj.NombreDominio.Trim().ToUpper() != "NULL")
                {
                    if (string.IsNullOrEmpty(obj.Usuario.Trim()))
                    {
                        objRetorno.ResultadoEjecucion = false;
                        objRetorno.MensajeError = "El nombre del usuario es un dato obligatorio";
                        return objRetorno;
                    }

                    if (obj.validar)
                    {
                        objRetorno.ResultadoEjecucion = true;
                        objRetorno.preguntar = true;
                        objRetorno.MensajeError = "A continuación" + " va a verificar si nombre de dominio " + obj.NombreDominio + " es válido. Esta operación puede tardar cierto tiempo. ¿Continúa?";
                        return objRetorno;
                    }
                    if (obj.validarDominio)
                    {
                        string strDomino = "";
                        strDomino = ObtenerPrimaryDomain(obj.NombreDominio, obj.Usuario, obj.Clave);
                        if (string.IsNullOrEmpty(strDomino))
                        {
                            objRetorno.ResultadoEjecucion = false;
                            objRetorno.MensajeError = "No se ha podido validar la existencia del domino " + obj.NombreDominio + ". Verifique los datos ingresados.";
                            return objRetorno;
                        }
                        strRetorno = strDomino;
                    }
                }

                strRetorno += "æ" + obj.NombreOrganizacion.Trim() + ((obj.optCIPOL) ? "æ1" : "");


                TresDES objEncriptarNET;

                objEncriptarNET = new TresDES();
                objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
                objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;


                if (objFachada.ActualizarTipoSeguridad(objEncriptarNET.Criptografia(Accion.Encriptacion, strRetorno)))
                {
                    objRetorno.ResultadoEjecucion = true;
                    objRetorno.MensajeServicio = "Deberá reiniciar la aplicación para que los cambios tengan efecto.";
                    return objRetorno;
                }
                else
                {
                    objRetorno.ResultadoEjecucion = true;
                    objRetorno.MensajeServicio = "No se ha podido modificar el manejo de seguridad de CIPOL, por favor vuelva a intentar.";
                    return objRetorno;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        #region "Funciones privadas"
        private string ObtenerPrimaryDomain(string Dominio, string Usuario, string Clave)
        {
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //               DESCRIPCION DE VARIABLES LOCALES
            //strDominio     : Nombre del dominio a verificar
            //objDirectorio  : Entrada del directorio
            //strPath        : Ubicación del recurso a buscar en el Active Directory
            //strItem        : Valor de array
            //strRet         : Valor de reotorno
            //objVerif       : Objeto DirectorySearcher que se utiliza para verificar si el dominio
            //                 existe
            //objResultado   : Resultado de la búsqueda
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            string strDominio = Dominio;
            System.DirectoryServices.DirectoryEntry objDirectorio = null;
            string strPath = null;
            string strItem = null;
            string strRet = string.Empty;
            System.DirectoryServices.DirectorySearcher objVerif = default(System.DirectoryServices.DirectorySearcher);
            System.DirectoryServices.SearchResult objResultado = default(System.DirectoryServices.SearchResult);

            //Si se envia un nombre de dominio en formato NETBIOS se incorpora la palabra local
            if (strDominio.IndexOf('.') == -1)
                strDominio += ".local";
            strPath = "LDAP://";
            foreach (string strItem_loopVariable in strDominio.Split('.'))
            {
                strItem = strItem_loopVariable;
                strPath += "DC=";
                strPath += strItem;
                strPath += ",";
            }
            strPath = strPath.Substring(0, strPath.Length - 1);

            try
            {
                objDirectorio = new System.DirectoryServices.DirectoryEntry(strPath, Usuario, Clave);
                objVerif = new System.DirectoryServices.DirectorySearcher(objDirectorio, "(objectClass=domain)");
                objResultado = objVerif.FindOne();

                if ((objResultado != null))
                    strRet = strDominio;
            }
            catch (Exception)
            {
                return "";
            }
            finally
            {
                objDirectorio.Close();
            }
            return strRet;
        }
        #endregion

        #endregion
    }
}
