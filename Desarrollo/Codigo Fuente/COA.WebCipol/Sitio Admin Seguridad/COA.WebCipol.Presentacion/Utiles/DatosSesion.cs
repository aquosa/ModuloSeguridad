using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.WebCipol.Comun;
using EntidadesEmpresariales;

namespace COA.Cipol.Presentacion._UIHelpers
{
    /// <summary>
    /// Clase helper que incluye los nombres de las variables de sesión utilizadas globalmente en el Sistema.
    /// </summary>
    /// <history>
    /// [PabloC]          [viernes, 10 de febrero de 2012]       Creado 11273
    /// </history>
    [Serializable()]
    internal class DatosSesion
    {
        
        /// <summary>
        /// Datos globales de la aplicacion que tienen que persistir en el tiempo para el usuario Logeado.
        /// Luego son utilizados por los procesos/funciones/etc del sistema.
        /// </summary>
        public static DatosSistema DatosSistema
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
        public static PadreCipolCliente UsuarioCipol
        {
            get
            {
                return (PadreCipolCliente)HttpContext.Current.Session[Constantes.VariablesDeSesion.CIPOL];
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
        


    }
}