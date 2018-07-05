using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Runtime.Serialization;
using COA.WebCipol.Presentacion.Clases;
using COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales;
using System.DirectoryServices;
using COA.WebCipol.Fachada;
using COA.Cipol.Presentacion._UIHelpers;
using COA.CifrarDatos;

namespace COA.WebCipol.Presentacion.Ajax
{
    /// <summary>
    /// Descripción breve de wsTerminales
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class wsTerminales : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public Retorno NuevaTerminal(Filtro[] filtro)
        {
            Terminal filtro2 = new Terminal();
            TresDES objEncriptarNET;
            Retorno objRetorno = new Retorno();
            dcAdministrarTerminales dcDatos = new dcAdministrarTerminales();
            COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES rowTerm = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES();
            int shtID = 0;
            string strPath = null;
            string[] strdom = null;
            Int32 inti = default(Int32);
            DirectoryEntry objIngreso = default(DirectoryEntry);
            DirectorySearcher objBuscar = default(DirectorySearcher);
            SearchResult objResultado = default(SearchResult);
            FSeguridad objFacTerminales = new FSeguridad();

            objEncriptarNET = new TresDES();
            objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
            objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;


            if (string.IsNullOrEmpty(filtro2.codterminal.Trim()))
            {
                objRetorno.MensajeError = "El Código de Terminal es un dato obligatorio.";
                objRetorno.ResultadoEjecucion = false;
                return objRetorno;
            }
            else
            {
                if (VerificarSiExisteCODTERMINAL())
                {
                    objRetorno.MensajeError = "El Código de Terminal ya existe.";
                    objRetorno.ResultadoEjecucion = false;
                    return objRetorno;
                }
            }
            //GCP-Cambio ID: 9145
            if (string.IsNullOrEmpty(filtro2.codterminal.Trim()))
            {
                objRetorno.MensajeError = "El Nombre NETBIOS es un dato obligatorio.";
                objRetorno.ResultadoEjecucion = false;
                return objRetorno;
            }
            else
            {
                string strErrorNombre = VerifNombreNETBIOS(filtro2.nombrecomputadora.Trim());
                if (string.IsNullOrEmpty(strErrorNombre))
                {
                    if (VerificarSiExisteNombreComputadora())
                    {
                        objRetorno.MensajeError = "El Nombre NETBIOS es un dato obligatorio.";
                        objRetorno.ResultadoEjecucion = false;
                        return objRetorno;
                    }
                }
                else
                {
                    objRetorno.MensajeError= strErrorNombre;
                    objRetorno.ResultadoEjecucion = false;
                    return objRetorno;
                }
            }

            //[AngelL] 20/02/2005 - Verificacion de la pc contra el dominio
            //si se usa seguridad integrada.
            //Una vez seguros que la información sobre el NombreNetBios fué
            //cargada, ActiveDirectory validará que el nombre exista en su
            //base si la seguridad es integrada.


            //si el dominio no es nulo, o sea, si se esta usando
            //seguridad integrada al dominio, se verifica 
            //la pc contra el servicio de directorio usando la sintaxis LDAP
            if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio)
            {
                try
                {
                    strPath = "LDAP://";
                    strdom = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio.Split('.');
                    for (inti = 0; inti <= strdom.GetUpperBound(0); inti++)
                    {
                        strPath += "DC=";
                        strPath += strdom[inti];
                        strPath += ",";
                    }
                    strPath = strPath.Substring(0, strPath.Length - 1);
                    objIngreso = new DirectoryEntry(strPath);

                    //construido el path, se agrega el filtro al buscador de
                    //directorio
                    objBuscar = new DirectorySearcher(objIngreso);
                    objBuscar.Filter = "(&(objectClass=computer)(cn=" + filtro2.nombrecomputadora.Trim() + "))";
                    objResultado = objBuscar.FindOne();
                }
                catch (Exception ex)
                {
                    objResultado = null;
                    objRetorno.MensajeError = ex.Message;
                    //lblMSJAltaModif.Text = ex.StackTrace;
                }

                //si no se obtuvieron resultados, se advierte
                if (objResultado == null)
                {
                    objRetorno.MensajeError = "La terminal indicada no pertenece al Dominio " + ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreDominio + " o en este momento no se puede establecer conexión con el Dominio. Verifique. Terminal no encontrada.";
                    objRetorno.ResultadoEjecucion = false;
                    return objRetorno;
                }
            }

            //if (this.cboAreas.SelectedIndex == 0)
            if(filtro2.idarea.Equals(0))
            {
                objRetorno.MensajeError = "La Ubicación Física de la terminal es un datos obligatorio.";
                objRetorno.ResultadoEjecucion = false;
                return objRetorno;
            }

            //Si es un alta de terminal
            if (filtro2.idterminal == 1)
            {
                rowTerm = new COA.WebCipol.Entidades.ClasesWs.dcAdministrarTerminales.SE_TERMINALES();
                rowTerm.IDTERMINAL = -1;
            }
            else
            {
                rowTerm.IDTERMINAL = Convert.ToInt32(filtro2.idterminal);
            }
            rowTerm.CODTERMINAL = filtro2.codterminal;//this.txtCodTerminal.Text.Trim();
            rowTerm.NOMBRECOMPUTADORA = filtro2.nombrecomputadora;//this.txtNombreTerminal.Text.Trim();
            rowTerm.NOMBREAREA = filtro2.nombrearea;//this.cboAreas.SelectedItem.ToString().Trim();
            rowTerm.IDAREA = System.Convert.ToInt16(filtro2.idarea);//this.cboAreas.SelectedValue
            rowTerm.USOHABILITADO = objEncriptarNET.Criptografia(Accion.Encriptacion, (filtro2.Habilitada ? "1" : "0").ToString());
            rowTerm.MODELOPROCESADOR = filtro2.modeloprocesador;//this.txtProcesador.Text;
            rowTerm.CANTMEMORIARAM = Convert.ToInt16(filtro2.cantmemoriaram);
            rowTerm.TAMANIODISCO = Convert.ToInt16(filtro2.tamaniodisco);
            rowTerm.MODELOACELVIDEO = filtro2.modeloacelvideo;
            rowTerm.DESCADICIONAL = filtro2.descadicional;
            rowTerm.MODELOMONITOR = filtro2.modelomonitor;
            if (filtro2.origenactualizacion == "R")
            {
                rowTerm.ORIGENACTUALIZACION = "R";
            }
            else
            {
                rowTerm.ORIGENACTUALIZACION = "L";
            }

            dcDatos.lstSE_TERMINALES.Add(rowTerm);
            if (filtro2.idterminal == 0)
            {
                shtID = objFacTerminales.AdministrarTerminales(dcDatos);
                if (shtID == 0)
                {
                    objRetorno.MensajeError = "No se han podido actualizar los datos de la terminal en el servidor.";
                    objRetorno.ResultadoEjecucion = false;
                }
                else
                {
                    //LimpiarControles();
                    ////lblMSJAltaModif.Text = "Los datos fueron guardados";
                    //ScriptManager.RegisterStartupScript(this, GetType(), "", "alert('Los datos fueron guardados.');", true);
                    objRetorno.MensajeError = "Los datos fueron guardados.";
                    objRetorno.ResultadoEjecucion = false;
                }
            }
            else
            {
                if (objFacTerminales.AdministrarTerminales(dcDatos) > 0)
                {
                    //MostrarFormAltaModif(false);
                    //LlenarGrilla();
                    objRetorno.MensajeServicio = "Los datos fueron guardados";
                    objRetorno.ResultadoEjecucion = true;
                }
                else
                {
                    //MostrarFormAltaModif(false);
                    objRetorno.MensajeError = "No se han podido actualizar los datos de la terminal en el servidor.";
                    objRetorno.ResultadoEjecucion = false;
                }
            }
            //Retorna el resultado
            return objRetorno;
        }

        private bool VerificarSiExisteNombreComputadora()
        {
            throw new NotImplementedException();
        }

        private string VerifNombreNETBIOS(string p)
        {
            throw new NotImplementedException();
        }

        private bool VerificarSiExisteCODTERMINAL()
        {
            throw new NotImplementedException();
        }

    }

}
