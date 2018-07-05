using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.WebCipol.Comun;
using System.Data;
using EntidadesEmpresariales;
using COA.WebCipol.Entidades.ClasesWs;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarPoliticasGenerales;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaEntornoCIPOLAdministrador;

namespace COA.Cipol.Inicio._UIHelpers
{
    /// <summary>
    /// Clase helper que incluye los nombres de las variables de sesión utilizadas globalmente en el Sistema.
    /// </summary>
    [Serializable()]
    internal class ManejoSesion
    {

        public static System.Net.CookieContainer CookieMaster
        {
            get
            {
                return (System.Net.CookieContainer)HttpContext.Current.Session[Constantes.VariablesDeSesion.COOKIECONTAINER];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.COOKIECONTAINER] = value;
            }
        }
        #region ESTADO
        /// <summary>
        /// Datos globales de la aplicacion que tienen que persistir en el tiempo para el usuario Logeado.
        /// Luego son utilizados por los procesos/funciones/etc del sistema.
        /// </summary>
        public static DatosSistema DatosSistemaSesion
        {
            get
            {
                return (DatosSistema)HttpContext.Current.Session[Constantes.VariablesDeSesion.DATOS_SISTEMA];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.DATOS_SISTEMA] = value;
            }
        }

        /// <summary>
        /// Datos del usuario retornados por el proceso de Login.
        /// </summary>
        public static DatosCIPOL DatosCIPOLSesion
        {
            get
            {
                return (DatosCIPOL)HttpContext.Current.Session[Constantes.VariablesDeSesion.CIPOL];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.CIPOL] = value;
            }
        }
      

        /// <summary>
        /// Indica si está establecida la Seguridad, y que Tipo está establecida.
        /// </summary>
        public static String TipoSeguridadSeteada
        {
            get
            {
                return (String)HttpContext.Current.Session[Constantes.VariablesDeSesion.TIPOSEGURIDAD];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.TIPOSEGURIDAD] = value;
            }
        }

        /// <summary>
        /// 
        ///TODO ver si hacemos un enum!!!
        /// 
        /// Indica el Modo de Cambio de Clave:
        ///     0 Solicitar clave
        ///   Cambio de clave:
        ///     1 No obligatorio
        ///     2 Obligatorio por vencimiento clave
        ///     3 Obligatorio debido a que se debe forzar el cambio de la contraseña
        /// </summary>
        public static Byte ModoCambioClave
        {
            get
            {
                if (HttpContext.Current.Session[Constantes.VariablesDeSesion.MODOCAMBIOCLAVE] ==null)
                {
                    return 3;
                }
                return (Byte)HttpContext.Current.Session[Constantes.VariablesDeSesion.MODOCAMBIOCLAVE];                
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.MODOCAMBIOCLAVE] = value;
            }
        }

        public static dcRecuperarDatosParaEntornoCIPOLAdministrador gdtsAuditoria
        {
            get
            {
                return (dcRecuperarDatosParaEntornoCIPOLAdministrador)HttpContext.Current.Session[Constantes.VariablesDeSesion.DTSAUDITORIA];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.DTSAUDITORIA] = value;
            }
        }

        public static Constantes.gudtParametros gudParam
        {
            get
            {
                return (Constantes.gudtParametros)HttpContext.Current.Session[Constantes.VariablesDeSesion.GUDPARAM];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.GUDPARAM] = value;
            }
        }
        
        public static dcRecuperarPoliticasGenerales PoliticasGenerales
        {
            get
            {
                return (dcRecuperarPoliticasGenerales)HttpContext.Current.Session[Constantes.VariablesDeSesion.PARAMETROSGENERALES];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.PARAMETROSGENERALES] = value;
            }
        }

        /// <summary>
        /// Mensaje a Mostrar en la página de Mensajes previa a Cerrar la Sesión.
        /// </summary>
        public static String MensajeCerrar
        {
            get
            {
                return (String)HttpContext.Current.Session[Constantes.VariablesDeSesion.MENSAJECERRAR];
            }
            set
            {
                HttpContext.Current.Session[Constantes.VariablesDeSesion.MENSAJECERRAR] = value;
            }
        }
        #endregion
        
    }
}