using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace COA.WebCipol.Comun
{
    ///TODO[CIPOLWEB] 
    ///que hacemos con esta clase??? 
    ///no quedó nada...
    ///
    public class Principal
    {
        #region ESTADO
        //public enum genuNivelSeguridad : int //ver si lo ponemos en constantes!!
        //{
        //    Sin_requerimiento_específico = 1,
        //    Compuesta_solo_por_letras = 2,
        //    Compuesta_solo_por_numeros = 3,
        //    Compuesta_por_letras_y_numeros = 4,
        //    Compuesta_por_letras_numeros_y_caracteres_especiales = 5
        //}

        //public static string RecuperarDescripcionGenuNivelSeguridad(genuNivelSeguridad enumerador)
        //{
        //    switch (enumerador)
        //    {
        //        case genuNivelSeguridad.Sin_requerimiento_específico: return "Sin requerimiento específico";
        //        case genuNivelSeguridad.Compuesta_solo_por_letras: return "Compuesta sólo por letras";
        //        case genuNivelSeguridad.Compuesta_solo_por_numeros: return "Compuesta sólo por números";
        //        case genuNivelSeguridad.Compuesta_por_letras_y_numeros: return "Compuesta por letras y números";
        //        case genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales: return "Compuesta por letras, números y caracteres especiales";
        //        default:
        //            return "";
        //    }
        //}

        ////Separador utilizado para Parámetros de Seguridad
        //public const string gstrSepParam = "[SC]"; //ver si lo ponemos en constantes!!

        //public static COA.Framework.Utilidades.CifrarDatos.TresDES gobjEncriptarNET;

        //public static System.Data.DataSet gdtsAuditoria; //??? VER si va o no acá.. o en sesión..

        //public struct gudtParametros //??? VER si va o no acá.. o en sesión..
        //{
        //    public short LogintudContraseña;
        //    public Int32 CantidadContraseniasAlmacenadas;
        //    public Int32 DuracionContrasenia;
        //    public genuNivelSeguridad NivelSeguridadContrasenia;
        //    public Int32 ModoAsignacionTareasYRoles;
        //    public Int32 Tiempo_Inactividad;
        //}
        //public static gudtParametros gudParam; //??? VER si va o no acá.. o en sesión..
        #endregion

        #region COMPORTAMIENTO
        ///// <summary>
        ///// Generación de auditoria    ?????
        ///// </summary>
        ///// <param name="CodMensaje">Login del usuario</param>
        ///// <param name="Usuario">Login del usuario que realiza el cambio</param>
        ///// <param name="UsuarioAdm">Login del usuario que realiza el cambio</param>
        ///// <param name="NuevoValor">Valor asignado</param>
        ///// <returns>String que se va a utilizar para generar la auditoría</returns>
        ///// <remarks></remarks>
        ///// <history>
        ///// 	[gustavom]	23/09/2005	Created
        ///// 	[PabloC]          [martes, 14 de febrero de 2012]       Migrado a C#       
        ///// </history>
        //public static string MensajeAuditoria(short CodMensaje, string Usuario, string UsuarioAdm, string NuevoValor)
        //{
        //    ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    //Autor: Gustavo Mazzaglia
        //    //Fecha de creación: 21/02/2002
        //    //Modificaciones:
        //    //           30/08/2005 - Angel Lubenov - Gcp Cambios ID:3342 - registración de
        //    //                       usuarios actuantes y afectados.
        //    //           12/09/2005 - Angel Lubenov - Gcp Camios Id:3044 - seguridad en la contraseña
        //    //           04/07/2008 - Andres Ravizzini - Se agrega CodMensaje 780
        //    ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    //                  DESCRIPCION DE VARIABLES LOCALES
        //    //strMensaje : Mensaje que se obtiene cuando se reemplazan las valores
        //    //             dinámicos (@)
        //    //rowAudit   : Objeto DataRow que contiene el código de auditoría
        //    ///'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    string strMensaje = string.Empty;
        //    System.Data.DataRow[] rowAudit = null;

        //    rowAudit = DatosSesion.ParametrosGenerales.Tables["SE_CodAuditoria"].Select("CodAuditoria = " + CodMensaje.ToString());
        //    switch (CodMensaje)
        //    {
        //        case 100:
        //            //Proceso de Login
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", System.Environment.MachineName);
        //            break;
        //        case 110:
        //        case 230:
        //        case 250:
        //            //Proceso de Login
        //            //strMensaje = Strings.Replace(rowAudit[0]["TextoAuditoria"].ToString(), "@", Usuario, , 1, CompareMethod.Text);
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", Usuario); //ver!!!
        //            strMensaje = strMensaje.Replace("@", System.Environment.MachineName);
        //            break;
        //        case 120:
        //        case 130:
        //        case 140:
        //        case 150:
        //        case 160:
        //        case 170:
        //        case 180:
        //        case 190:
        //        case 200:
        //        case 210:
        //        case 220:
        //        case 240:
        //        case 205:
        //            //Proceso de Login
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", Usuario);
        //            break;
        //        case 260:
        //        case 270:
        //            //Supervision
        //            //strMensaje = Strings.Replace(rowAudit[0]["TextoAuditoria"].ToString(), "@", System.Environment.MachineName, , 1, CompareMethod.Text);
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", System.Environment.MachineName); //ver!!!
        //            strMensaje = strMensaje.Replace("@", Usuario);
        //            break;
        //        case 400:
        //        case 410:
        //        case 420:
        //        case 430:
        //        case 440:
        //        case 450:
        //        case 460:
        //        case 470:
        //        case 480:
        //        case 490:
        //        case 500:
        //        case 510:
        //        case 520:
        //        case 530:
        //            //Políticas de Seguridad
        //            //strMensaje = Strings.Replace(rowAudit[0]["TextoAuditoria"].ToString(), "@", UsuarioAdm, , 1, CompareMethod.Text);
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", UsuarioAdm); //ver!!!

        //            //VER!!!
        //            //System.Environment.MachineNamestrMensaje = strMensaje.Replace("@", NuevoValor);
        //            //VER!!!
        //            break;
        //        case 600:
        //        case 610:
        //        case 620:
        //        case 750:
        //        case 760:
        //        case 770:
        //        case 780:
        //            //Administracion de Usuarios
        //            //strMensaje = Strings.Replace(rowAudit[0]["TextoAuditoria"].ToString(), "@", UsuarioAdm, , 1, CompareMethod.Text);
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", UsuarioAdm); //ver!!!
        //            strMensaje = strMensaje.Replace("@", Usuario);
        //            break;
        //        case 700:
        //        case 710:
        //        case 720:
        //        case 730:
        //        case 740:
        //            //ABM de Seguridadº
        //            strMensaje = rowAudit[0]["TextoAuditoria"].ToString().Replace("@", UsuarioAdm);
        //            break;
        //    }

        //    return CodMensaje + "Æ" + strMensaje + "Æ" + Usuario + "Æ" + UsuarioAdm + "æ";
        //    //Alt 146 y 145
        //}

        ///// <summary>
        ///// Devuelve un mensaje a mostrar al usuario, según el código correspondiente.
        /////     FALTA!!
        /////     El dataset gdtsAuditoria debe estar en sesión (DEBE CARGARSE AL INICIAR EL SISTEMA).
        ///// </summary>
        ///// <param name="CodMensaje"></param>
        ///// <param name="CantDias"></param>
        ///// <returns></returns>
        ///// <history>
        ///// [PabloC]          [viernes, 02 de marzo de 2012]       Migrado a C# y modificado para Entorno Web.
        ///// </history>
        //public static string RetornaMensajeUsuario(short CodMensaje, int? CantDias = null)
        //{
        //    ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    //Autor: Gustavo Mazzaglia
        //    //Fecha de creación: 28/12/2001
        //    //Modificaciones:
        //    ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    //                   DESCRIPCION DE VARIABLES LOCALES
        //    //strMensaje: Mensaje a mostrar
        //    ///''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        //    DataRow[] rowFilas = null;
        //    string strMensajeRet = null;//VER!!

        //    rowFilas = Constantes.gdtsAuditoria.Tables["grecMensajes"].Select("CodMensaje = " + CodMensaje.ToString()); //VER!!

        //    if (!rowFilas.Length.Equals(0))
        //    {
        //        strMensajeRet = rowFilas[0]["TextoMensaje"].ToString().Trim();
        //        switch (CodMensaje)
        //        {
        //            case 25:
        //            case 30:
        //                if ((CantDias.HasValue))
        //                    strMensajeRet = strMensajeRet.Replace("@", CantDias.Value.ToString()); //VER!!
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    return strMensajeRet;

        //}
        #endregion

        /// <summary>
        /// VALIDA LA CALIDAD DE LA CONTRASEÑA SEGUN EL NIVEL DE SEGURIDAD
        /// INDICADO EN LOS PARAMETROS.
        /// </summary>
        /// <param name="pstrContrasenia">la posible contraseña a ser aprobada (o no)</param>
        /// <param name="Seguridad_SoloDominio"></param>
        /// <param name="NivelSeguridadContrasenia"></param>
        /// <returns>El mensaje de error si la contraseñia no es válida, 
        /// o una cadena vacia si la contraseña es válida.</returns>
        /// <history>
        /// [AngelL]          12/09/2005
        /// [LucianoP]        04/05/2012    
        /// </history>
        public string ValidarSeguridadContrasenia(ref string pstrContrasenia, bool Seguridad_SoloDominio,
                                                  Constantes.genuNivelSeguridad NivelSeguridadContrasenia)
        {
            string functionReturnValue = null;
            bool blnTieneLetras;
            bool blnTieneNumeros;
            bool blnTieneCaracteresEspeciales;

            //verificamos si la seguridad está integrada al dominio, en caso
            //que lo esté (o sea, gstrDominio <> "") salimos pues derivamos la
            //responsabilidad de la seguridad a Windows.
            if (Seguridad_SoloDominio)
            {
                functionReturnValue = "";
                return functionReturnValue;
            }

            //si la seguridad no está integrada al dominio,
            //verificamos la contraseña de acuerdo al nivel de
            //seguridad indicado.
            //recorremos la contraseña verificando si tiene caracteres alfabeticos, numericos y especiales
            blnTieneNumeros = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"\d");
            blnTieneLetras = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"[a-zA-Z]");
            blnTieneCaracteresEspeciales = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"[^0-9a-zA-Z]");

            //ahora, de acuerdo al nivel de seguridad, verificamos
            //la contraseña
            switch (NivelSeguridadContrasenia)
            {
                case Constantes.genuNivelSeguridad.Sin_requerimiento_específico:
                    //sin seguridad, la contraseña siempre es válida
                    functionReturnValue = "";
                    return functionReturnValue;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_numeros:
                    //sólo por números, verificamos que la contraseña
                    //solo tenga números, no tenga caracteres especiales y no tenga letras
                    if (blnTieneNumeros && !blnTieneCaracteresEspeciales && !blnTieneLetras)
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        bool blnSoloNros = System.Text.RegularExpressions.Regex.IsMatch(pstrContrasenia, @"^[0-9]+$");
                        functionReturnValue = (blnSoloNros ? "" : "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por números.").ToString();
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_solo_por_letras:
                    //contraseña compuesta solo por letras, verificamos
                    //que así sea
                    if (blnTieneLetras & (!blnTieneCaracteresEspeciales) & (!blnTieneNumeros))
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        //si se encontró un caracter que no es alfabético, rechazamos la contraseña
                        functionReturnValue = "El nivel de seguridad parametrizado requiere que la contraseña esté compuesta solo por letras.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_y_numeros:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & (!blnTieneCaracteresEspeciales))
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        functionReturnValue = "El nivel de seguridad parametrizado requiere que la contraseña posea solo letras y números.";
                    }
                    break;
                case Constantes.genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales:
                    //solo letras y números, verificamos que así sea
                    //verificaremos que haya letras Y números
                    if (blnTieneNumeros & blnTieneLetras & blnTieneCaracteresEspeciales)
                    {
                        functionReturnValue = "";
                    }
                    else
                    {
                        functionReturnValue = "El nivel de seguridad parametrizado requiere que la contraseña posea letras, números y caracteres especiales.";
                    }
                    break;
                default:
                    throw new Exception("Nivel de seguridad no soportado.");
            }
            return functionReturnValue;
        }
    }
}
