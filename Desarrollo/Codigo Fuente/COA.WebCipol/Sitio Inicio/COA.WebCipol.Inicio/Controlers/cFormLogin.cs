using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.WebCipol.Inicio.model;
using COA.WebCipol.Comun;
using EntidadesEmpresariales;
using COA.CifrarDatos;
using COA.WebCipol.Fachada;
using COA.Cipol.Inicio._UIHelpers;
using Microsoft.VisualBasic;
using System.Net;
using Fachada;

namespace COA.WebCipol.Inicio.Controlers
{
    public class cFormLogin
    {
        /// <summary>
        /// Realizar el inicio de sesión para un usuario en la BD.
        /// </summary>
        /// <param name="NombreUsuario"></param>
        /// <param name="Pwd"></param>
        /// <returns>Objeto "RetornoInicioSesion" que indica el Resultado(true o false), Datos Globales del Sistema, el objeto Usuario CIPOL y un posible Mensaje de error.</returns>
        /// <history>
        /// [MartinV]          [jueves, 25 de septiembre de 2014]       Modificado  GCP-Cambios 15585
        /// </history>
        private mFormLogin IniciarSesion(string NombreUsuario, string Pwd, System.Net.CookieContainer cokie, string ip)
        {
            ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //                    DESCRIPCION DE VARIABLES LOCALES
            //strUsuario : Nombre del usuario
            //objProxy   : objeto proxy de conexion al servicio web
            //strCipol   : objeto serializado de sipol, 
            //strErro    : string con mensaje de error si lo hubiera.
            //objEncSer  : Objeto de encriptación RSA que contiene la clave pública
            //             del servidor
            //strClave   : Clave de encriptación
            //objEncCli  : Objeto de encriptación RSA que contiene la clave pública
            //             y privada del cliente
            ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            string strUsuario = null;
            COA.WebCipol.Fachada.FInicioSesion facInicioSesion = new COA.WebCipol.Fachada.FInicioSesion();
            string strCipol = null;
            string strError = "";
            string strClave = null;
            string strTerminal = null;
            mFormLogin objRetIS = new mFormLogin();

            //Define variables locales.
            //System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objDeserializador;
            //System.IO.MemoryStream objFlujo;

            byte[] bytPub;
            System.Security.Cryptography.RSACryptoServiceProvider objEncServ = new System.Security.Cryptography.RSACryptoServiceProvider();
            System.Security.Cryptography.RSACryptoServiceProvider objEncCli = new System.Security.Cryptography.RSACryptoServiceProvider();

            EntidadesEmpresariales.PadreCipolCliente objUsuarioCipol;

            TresDES objEncriptarNET;
            General objGeneral;

            try
            {

                strUsuario = NombreUsuario.Trim();
                if (string.IsNullOrEmpty(strUsuario))
                {
                    objRetIS.Mensaje = "El nombre del usuario es un dato obligatorio.";
                    objRetIS.ResultadoProcesoInicioSesion = false;
                    return objRetIS;
                }
                if (Pwd.Trim() == string.Empty)
                {
                    objRetIS.Mensaje = "La contraseña es un dato obligatorio.";
                    objRetIS.ResultadoProcesoInicioSesion = false;
                    return objRetIS;
                }

                strClave = Pwd;
                ManejoSesion.CookieMaster = cokie;
                System.Net.CookieContainer objCookieMASTER = ManejoSesion.CookieMaster;

                bytPub = facInicioSesion.GetClavePublica(objEncCli.ExportCspBlob(false), objCookieMASTER);
                if ((bytPub == null))
                {
                    objRetIS.Mensaje = "No se ha podido recuperar la clave pública.";
                    objRetIS.ResultadoProcesoInicioSesion = false;
                    return objRetIS;
                }
                // Prepara el algoritmo asimétrico del servidor
                objEncServ.ImportCspBlob(bytPub);
                // Encripta con la clave pública
                strClave = System.Convert.ToBase64String(objEncServ.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(strClave), false));

                strTerminal = COA.WebCipol.Inicio.Utiles.cPrincipal.ObtenerTerminal(ip);

                strCipol = facInicioSesion.IniciarSesion(strUsuario, strTerminal, ref strError, strClave, objCookieMASTER);
                if (strCipol == null || string.IsNullOrEmpty(strCipol))
                {
                    objRetIS.Mensaje = "No se ha podido iniciar sesión" + (String.IsNullOrEmpty(strError) ? "" : ": " + strError).ToString();
                    objRetIS.ResultadoProcesoInicioSesion = false;
                    return objRetIS;
                }
                if (Validaciones.ValidarCadenaNulaOVacia(strError))
                {
                    objRetIS.Mensaje = strError;
                    objRetIS.ResultadoProcesoInicioSesion = false;
                    return objRetIS;
                }

                //Dim objFlujo As System.IO.MemoryStream
                System.IO.MemoryStream objFlu;
                //Dim objDeserializador As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objDeser = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //Dim objSerializar As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter()
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objSerializar = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                //objFlujo = New System.IO.MemoryStream(System.Convert.FromBase64CharArray(pStrCipol.ToCharArray, 0, pStrCipol.Length))
                objFlu = new System.IO.MemoryStream(System.Convert.FromBase64CharArray(strCipol.ToCharArray(), 0, strCipol.Length));

                //gobjUsuarioCipol = CType(objDeserializador.Deserialize(objFlujo), EntidadesEmpresariales.PadreCipolCliente)
                objUsuarioCipol = (EntidadesEmpresariales.PadreCipolCliente)objDeser.Deserialize(objFlu);


                //Desencripta los valores encriptados en el servidor con la clave pública del RSA cliente
                //gobjUsuarioCipol.OtrosDatos("clave.usuario", System.Text.UTF8Encoding.UTF8.GetString(objEncCli.Decrypt(System.Convert.FromBase64String(gobjUsuarioCipol.OtrosDatos("clave.usuario")), False)))
                objUsuarioCipol.OtrosDatos("clave.usuario", System.Text.UTF8Encoding.UTF8.GetString(objEncCli.Decrypt(System.Convert.FromBase64String(objUsuarioCipol.OtrosDatos("clave.usuario")), false)));

                //gobjUsuarioCipol.Key = System.Convert.ToBase64String(objEncCli.Decrypt(System.Convert.FromBase64String(gobjUsuarioCipol.Key), False))
                objUsuarioCipol.Key = System.Convert.ToBase64String(objEncCli.Decrypt(System.Convert.FromBase64String(objUsuarioCipol.Key), false));

                //gobjUsuarioCipol.IV = System.Convert.ToBase64String(objEncCli.Decrypt(System.Convert.FromBase64String(gobjUsuarioCipol.IV), False))
                objUsuarioCipol.IV = System.Convert.ToBase64String(objEncCli.Decrypt(System.Convert.FromBase64String(objUsuarioCipol.IV), false));

                //TODO: VER QUE PASA CON LAS COOKIES
                //gobjUsuarioCipol.objColeccionDeCookies = pCookies
                //objUsuarioCipol.objColeccionDeCookiesCipol = 

                //gobjUsuarioCipol.gobjRSAServ = objEncServ.ExportCspBlob(False) 
                objUsuarioCipol.gobjRSAServ = objEncServ.ExportCspBlob(false);

                //gobjUsuarioCipol.OtrosDatos("urlwsInicioSesion", UrlWsInicioSesion)

                //objFlujo = New System.IO.MemoryStream()
                //objFlu= new System.IO.MemoryStream();

                //objSerializar.Serialize(objFlujo, gobjUsuarioCipol)
                //objSerializar.Serialize(objFlu, objUsuarioCipol);

                //gstrUsuarioCipol = System.Convert.ToBase64String(objFlujo.ToArray())
                //gstrUsuarioCipol = System.Convert.ToBase64String(objFlujo.ToArray())

                //Crea el objeto para encriptar.
                objEncriptarNET = new TresDES();
                objEncriptarNET.IV = objUsuarioCipol.IV;
                objEncriptarNET.Key = objUsuarioCipol.Key;

                //Crea el objeto con datos generales del usuario/sistema.
                objGeneral = new General(System.Reflection.Assembly.GetExecutingAssembly());
                objGeneral.AcercaDe_Descripcion = "Componente de Seguridad. Desarrollado por COA S.A.";
                objGeneral.AcercaDe_Detalle = "Configurador Interactivo de Políticas de seguridad de los sistemas. Resuelve las funciones operativas propias de la seguridad de sistemas (implementación de políticas, administración de usuarios,  roles, acceso a subsistemas).";
                //TODO: HAY QUE EVALUAR COMO SE TRABAJA CON ESTA INFORMACION SI ES NECESARIA
                //objGeneral.AcercaDe_Logo = objGeneral.RutaArchivos + "img_CIPOL_CIPOL.jpg";                  
                //objGeneral.AcercaDe_Logo = "Imagenes/prod_cipol.gif";//PRUEBA.. ver la imagen a poner!!
                //objGeneral.AcercaDe_Icono = objGeneral.RutaArchivos + "CIPOL32.ico";   
                objGeneral.AcercaDe_Cliente = objUsuarioCipol.NombreOrganizacion;
                objGeneral.UsuarioCIPOL = objUsuarioCipol.Login;
                                
                objGeneral.Hoy = objUsuarioCipol.FechaServidor;

                //Pasa al objeto Datos Sistema, que se va a guardar en sesión.
                //objDatosS.NombreSistema = objGeneral.NombreSistema;
                //objDatosS.EncriptarNET = objEncriptarNET; 
                DatosSistema objDatosS = new DatosSistema();
                objDatosS.DatosGenerales = objGeneral;

                //Pasa al objeto de Retorno.
                objRetIS.DatosSistema = objDatosS;
                DatosCIPOL objDatosC = new DatosCIPOL();
                objDatosC.DatosPadreCIPOLCliente = objUsuarioCipol;
                objDatosC.strCipol = strCipol;

                objDatosC.DatosPadreCIPOLCliente.objColeccionDeCookies = objCookieMASTER;
                objDatosC.DatosPadreCIPOLCliente.objColeccionDeCookiesCipol = objCookieMASTER;

                objRetIS.DatosCipol = objDatosC;
                objRetIS.Mensaje = "El proceso de inicio de sesión se realizó exitosamente";
                objRetIS.ResultadoProcesoInicioSesion = true;

                return objRetIS;

            }
            catch (Exception ex)
            {
                COA.Logger.Logueador.Loggear(ex, System.Diagnostics.EventLogEntryType.Error);
                objRetIS.ResultadoProcesoInicioSesion = false;
                objRetIS.Mensaje = "Ocurrió un error en el proceso de inicio de sesión.";
                return objRetIS;
            }
        }

        public string ValidarSeguridadContraseña(ref string pstrContrasenia)
        {
            string strMensajeRet = null;
            bool blnTieneLetras = false;
            bool blnTieneNumeros = false;
            bool blnTieneCaracteresEspeciales = false;
            Int32 intTemp = default(Int32);
            Int32 intI = default(Int32);

            //seteamos los flags
            blnTieneLetras = false;
            blnTieneNumeros = false;
            blnTieneCaracteresEspeciales = false;
            intTemp = 0;

            //Verificamos si la seguridad está integrada al dominio, en caso
            //que lo esté (o sea, gstrDominio <> "") salimos pues derivamos la
            //responsabilidad de la seguridad a Windows.
            if (ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Seguridad_SoloDominio) //ver el usuario, si tiene datos..
            {
                strMensajeRet = "";
                return strMensajeRet;
            }

            //Si la seguridad no está integrada al dominio,
            //verificamos la contraseña de acuerdo al nivel de
            //seguridad indicado.
            //Recorremos la contraseña verificando si tiene caracteres alfabéticos, numéricos y especiales.
            for (intI = 1; intI <= pstrContrasenia.Length; intI++)
            {
                intTemp = Strings.InStr(1, "1234567890ABCDEFGHIJKLMNÑOPQRSTUVWXYZ", Strings.Mid(pstrContrasenia.ToUpper(), intI, 1)); //TODO[CIPOLWEB]

                //si hay algún caracter que no sea letra ni número, seteamos el flag
                //de caracteres especiales
                if (intTemp == 0)
                    blnTieneCaracteresEspeciales = true;
                //verificamos que inttemp sea < 11 para ver si hubo numeros
                if (intTemp < 11 & intTemp > 0)
                    blnTieneNumeros = true;
                //verficamos que haya habido caracteres solo alfabeticos
                if (intTemp >= 11)
                    blnTieneLetras = true;
            }

            //Ahora, de acuerdo al nivel de seguridad, verificamos
            //la contraseña.
            switch (ManejoSesion.gudParam.NivelSeguridadContraseña) //ver si tiene datos gudParam!
            {
                case Constantes.genuNivelSeguridad.Sin_requerimiento_específico:
                    //sin seguridad, la contraseña siempre es válida
                    strMensajeRet = "";
                    return strMensajeRet;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_numeros:
                    //sólo por números, verificamos que la contraseña
                    //solo tenga números, no tenga caracteres especiales y no tenga letras
                    if (blnTieneNumeros & (!blnTieneCaracteresEspeciales) & (!blnTieneLetras))
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        strMensajeRet = (Information.IsNumeric(pstrContrasenia) ? "" : "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por números.").ToString();
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_letras:
                    //contraseña compuesta solo por letras, verificamos
                    //que así sea
                    if (blnTieneLetras & (!blnTieneCaracteresEspeciales) & (!blnTieneNumeros))
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        //si se encontró un caracter que no es alfabético, rechazamos la contraseña
                        strMensajeRet = "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por letras.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_y_numeros:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & (!blnTieneCaracteresEspeciales))
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        strMensajeRet = "El nivel de seguridad parametrizado requiere que la contraseña posea solo letras y números.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & blnTieneCaracteresEspeciales)
                    {
                        strMensajeRet = "";
                    }
                    else
                    {
                        strMensajeRet = "El nivel de seguridad parametrizado requiere que la contraseña posea letras, números y caracteres especiales.";
                    }
                    break;
                default:
                    throw new Exception("Nivel de seguridad no soportado.");
            }

            return strMensajeRet;
        }

        public string RecuperarTipoSeguridadYNombreDeDominio(System.Net.CookieContainer cokie)
        {
            FInicioSesion facIniSec = new FInicioSesion();

            string strNombreDominio = "";
            string strDominioMixto = "";

            //Recupera de la BD.
            strNombreDominio = facIniSec.RecuperarNombreDominio(cokie);

            if (strNombreDominio == Constantes.SeguridadNODefinida)
                return "(***Seguridad No Definida***)";

            if (strNombreDominio.Trim().Equals("*"))
                return "(Seguridad de CIPOL)";

            //if (strNombreDominio.PadRight(1) == "*")
            if (strNombreDominio.Substring((strNombreDominio.Length - 1), 1) == "*")
            {
                strDominioMixto = " (Seguridad Mixta: ";
                strNombreDominio = strNombreDominio.Replace("*", "");
            }
            else
                strDominioMixto = " (Seguridad Integrada al Dominio: ";

            return strDominioMixto + strNombreDominio + ")";
        }

        public bool CargarParametros(ref string MensajeError)
        {
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //Autor: Gustavo Mazzaglia
            //Fecha de creación: 28/12/2001
            //Modificaciones:
            //           09/09/2005 - Angel Lubenov - Gcp Cambios id:3044
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            //                  DESCRIPCION DE VARIABLES LOCALES
            //strParam   : Array que va a contener los parámetros de políticas de seguridad
            ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            string[] strParam = null;

            //Crea el objeto de encriptacion
            TresDES objEncriptarNET = objEncriptarNET = new TresDES();
            objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
            objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;

            //Recupera los parámetros generales.
            Fachada.FSeguridad facSEG = new Fachada.FSeguridad();
            ManejoSesion.gdtsAuditoria = facSEG.RecuperarDatosParaEntornoCIPOLAdministrador();

            if (ManejoSesion.gdtsAuditoria == null)
            {
                MensajeError = "No se han podido recuperar las políticas generales de seguridas";
                return false;
            }

            //Parámetros Vacíos??
            if (ManejoSesion.gdtsAuditoria == null || ManejoSesion.gdtsAuditoria.lstParametros.Count().Equals(0))
            {
                MensajeError = "No se han podido recuperar las políticas generales de seguridas";
                return false;
            }
            //Desencripta Parámetros.
            strParam = Strings.Split(objEncriptarNET.Criptografia(Accion.Desencriptacion, ManejoSesion.gdtsAuditoria.lstParametros[0].COLUMNA4), Constantes.gstrSepParam);

            ManejoSesion.gudParam = new Constantes.gudtParametros();
            ManejoSesion.gudParam.LongitudContraseña = short.Parse(strParam[3]);
            ManejoSesion.gudParam.DuracionContraseña = Int32.Parse(strParam[4]);
            ManejoSesion.gudParam.CantidadContraseñasAlmacenadas = Int32.Parse(strParam[7]);
            ManejoSesion.gudParam.TiempoEnDiasNoPermitirCambiarContrasenia = Int32.Parse(strParam[5]);
            ManejoSesion.gudParam.NivelSeguridadContraseña = (Constantes.genuNivelSeguridad)short.Parse(objEncriptarNET.Criptografia(Accion.Desencriptacion, ManejoSesion.gdtsAuditoria.lstParametros[0].COLUMNA5));

            //Angel Lubenov -  PARAMETRO 12, éste
            //indica el modo de seguridad aplicado a la hora de
            //asignar nuevos usuaris y/o roles, si está en modo
            //PERMISIVO (0) las tareas a los usuarios que ya
            //posean el rol en cuestion, aparecerán habilitadas,
            //si el modod e seguridad es RESTRICTIVO (1), entonces
            //las tareas apareceran deshabilitadas
            //verificamos si la directiva de seguridad
            //no estaba en el sistema (esto es un control por
            //viejas versiones)
            if (strParam.Length < 13)
            {
                ManejoSesion.gudParam.ModoAsignacionTareasYRoles = 1;
            }
            else
            {
                ManejoSesion.gudParam.ModoAsignacionTareasYRoles = short.Parse(strParam[12]);
            }

            return true;
        }
        
        public bool AutenticarUsuario(ref string Mensaje, string usuario, string password, System.Net.CookieContainer cokie, string ip)
        {
            //Instancia objetos necesarios.
            cFormLogin objUILogin = new cFormLogin();

            //Inicia Sesión de Usuario en CIPOL.


            mFormLogin objResultadoIS = objUILogin.IniciarSesion(usuario, password, cokie,ip);

            if (!objResultadoIS.ResultadoProcesoInicioSesion)
            {
                //NO SE PUDO INICIAR SESIÓN en CIPOL:
                Mensaje = objResultadoIS.Mensaje;
                return false;
            }

            //INICIO SESIÓN SATISFACTORIO:
            //Guarda Datos necesarios en Sesión
            ManejoSesion.DatosSistemaSesion = objResultadoIS.DatosSistema;
            ManejoSesion.DatosCIPOLSesion = objResultadoIS.DatosCipol;

            
      
            //Carga los parámetros generales del Sistema en Sesión.
            if (!objUILogin.CargarParametros(ref Mensaje))
            {
                //Si falló al cargar los parámetros generales.
                return false;
            }

        

            //TIPO DE SEGURIDAD SÍ DEFINIDO:
            //Guarda en sesión el nombre de la organización.
            ManejoSesion.DatosSistemaSesion.DatosGenerales.AcercaDe_Cliente = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.NombreOrganizacion;

            //Autenticación OK.
            return true;
        }

        public string AuditarIntentoInicioSesionConSesionActiva(string Login)
        {
            FSeguridad objFS = new FSeguridad();
            return objFS.AuditarIntentoInicioSesionConSesionActiva(Login);
        }

    }
}
