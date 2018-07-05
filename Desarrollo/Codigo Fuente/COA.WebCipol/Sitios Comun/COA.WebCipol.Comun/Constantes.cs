using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace COA.WebCipol.Comun
{
    public class Constantes
    {
        /// <summary>
        /// NOMBRES DE VARIABLES DE SESIÓN.
        /// </summary>
        public class VariablesDeSesion
        {
            public const string COOKIECONTAINER = "COOKIECONTAINER";
            public const string AUDITORIAS = "AUDITORIAS";
            public const string DATOS_SISTEMA = "DATOSSESION";
            public const string CIPOL = "objCipol";
            public const string TIPOSEGURIDAD = "TipoSeguridad";
            public const string MODOCAMBIOCLAVE = "ModoCambioClave";
            
            //TODO[CIPOLWEB]: ver estos 3
            public const string DTSAUDITORIA = "gdtsAuditoria";
            public const string GUDPARAM = "gudParam";
            public const string PARAMETROSGENERALES = "ParametrosGenerales";
            public const string MENSAJECERRAR = "msjClose";
            public const string ROL_DATOSROL = "ROL_DATOSROL";
            public const string USUARIOS_DATOSUSUARIOS = "USUARIOS_DATOSUSUARIOS";
            public const string SISTEMAS_BLOQUEADOS = "SISTEMAS_BLOQUEADOS";
            public const string TERMINALES_USUARIO = "TERMINALES_USUARIO";
            public const string USUARIO_ROLES = "USUARIO_ROLES";
            public const string STRING_SQL = "STRING_SQL";
            public const string IDAUTORIZACION = "IDAUTORIZACION";
        }

        /// <summary>
        /// ID del Usuario Master.
        /// </summary>
        public const int IDUsuarioMaster = 0;

        /// <summary>
        /// Indica que la Seguridad no fue definida para el Sistema. 
        /// Esta "X" se seta con un Script de inicialización en la BD.
        /// </summary>
        public const string SeguridadNODefinida = "X";

        /// <summary>
        /// Indica que se debe forzar el cambio de clave de un usuario.
        /// Se utiliza como parámetro del método "ManejoSesion.UsuarioCipol.OtrosDatos(..)".
        /// </summary>
        public const string ForzadoDeClave = "ForzarCambioClave";

        //Separador utilizado para Parámetros de Seguridad.
        public const string gstrSepParam = "[SC]";

        /// <summary>
        /// Estructura de los parámetros!!
        /// </summary>
        public class gudtParametros
        {
            public short LongitudContraseña;
            public Int32 CantidadContraseñasAlmacenadas;
            public Int32 DuracionContraseña;
            public genuNivelSeguridad NivelSeguridadContraseña;
            public Int32 ModoAsignacionTareasYRoles;
            public Int32 TiempoEnDiasNoPermitirCambiarContrasenia;
        }
        //TODO[CIPOLWEB] 
        //public static gudtParametros gudParam;
        //public static DataSet gdtsAuditoria;
        //TODO[CIPOLWEB] 

        /// <summary>
        /// Tipos de Niveles de Seguridad para las Contraseñas.
        /// </summary>
        public enum genuNivelSeguridad : short
        {
            Sin_requerimiento_específico = 1,
            Compuesta_solo_por_letras = 2,
            Compuesta_solo_por_numeros = 3,
            Compuesta_por_letras_y_numeros = 4,
            Compuesta_por_letras_numeros_y_caracteres_especiales = 5
        }

        /// <summary>
        /// Devuelve el nombre de un Nivel de Seguridad de Contraseña.
        /// </summary>
        /// <param name="enumerador"></param>
        /// <returns></returns>
        public static string RecuperarDescripcionGenuNivelSeguridad(genuNivelSeguridad enumerador)
        {
            switch (enumerador)
            {
                case genuNivelSeguridad.Sin_requerimiento_específico: return "Sin requerimiento específico";
                case genuNivelSeguridad.Compuesta_solo_por_letras: return "Compuesta sólo por letras";
                case genuNivelSeguridad.Compuesta_solo_por_numeros: return "Compuesta sólo por números";
                case genuNivelSeguridad.Compuesta_por_letras_y_numeros: return "Compuesta por letras y números";
                case genuNivelSeguridad.Compuesta_por_letras_numeros_y_caracteres_especiales: return "Compuesta por letras, números y caracteres especiales";
                default:
                    return "";
            }
        }

        public class Reportes
        {
            public class ControlDeInactividad
            {
                public const string FILTRO_VACIO_DESC = "";
                public const string FILTRO_IGUAL_MENOR_30_DESC = "Menor o Igual a 30 días";
                public const string FILTRO_MAYOR_30_DESC = "Mayor a 30 días";
                public const int FILTRO_VACIO = 0;
                public const int FILTRO_IGUAL_MENOR_30 = 1;
                public const int FILTRO_MAYOR_30 = 2;
            }
        }
    }
}