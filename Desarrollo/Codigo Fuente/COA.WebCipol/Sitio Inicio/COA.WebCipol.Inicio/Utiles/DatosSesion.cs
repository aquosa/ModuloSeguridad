using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.WebCipol.Comun;
using EntidadesEmpresariales;
using WebAppControlDualLogin.Model;
using System.Configuration;
using COA.WebCipol.Inicio.Controlers;
using System.Web.SessionState;

namespace COA.Cipol.Inicio._UIHelpers
{
    /// <summary>
    /// Clase helper que incluye los nombres de las variables de sesión utilizadas globalmente en el Sistema.
    /// </summary>
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


        /// <summary>
        /// Control de login de usuarios
        /// </summary>
        public class Control
        {
            /// <summary>
            /// Validacion de sesiones del usuario 
            /// </summary>
            /// <param name="Login">Login de usuario</param>
            /// <param name="Sesiones">Lista de sesiones de usuarios</param>
            /// <param name="Mensaje">Mensaje resultante del proceso de validacion</param>
            /// <returns>true si el login esta permitido, false en caso contrario</returns>
            /// <history>
            /// [LucianoP]          [jueves, 13 de julio de 2017]    Creado 
            /// </history>
            public static bool Verificar(string Login, List<mSession> Sesiones, out string Mensaje)
            {
                Mensaje = "";

                cFormLogin objFormLogin = new cFormLogin();
                bool priorityToFirstLogin = Convert.ToBoolean(ConfigurationManager.AppSettings["PriorityToFirstLogin"]);

                var session = Sesiones.FirstOrDefault(ses => ses.User == Login);

                if (session != null)
                {
                    if (priorityToFirstLogin)
                    {
                        Mensaje = objFormLogin.AuditarIntentoInicioSesionConSesionActiva(Login);
                        return false;
                    }
                    else
                    {
                        Sesiones.Remove(session);
                        return true;
                    }
                }

                return true;
            }

            /// <summary>
            /// Verifica si el id de sesion del usuario logeado coincide con el id de la sesion actual
            /// </summary>
            /// <param name="Sesiones">Lista de sesiones de usuarios</param>
            /// <param name="Sesion">Sesion ASPNET activa del usuario logeado</param>
            /// <returns>true si el id de sesion coincide o se permite continuar, false en caso contrario</returns>
            /// <history>
            /// [LucianoP]          [viernes, 14 de julio de 2017]    Creado 
            /// </history>
            public static bool VerificarId(List<mSession> Sesiones, HttpSessionState Sesion)
            {
                var priorityToFirstLogin = Convert.ToBoolean(ConfigurationManager.AppSettings["PriorityToFirstLogin"]);

                if (!priorityToFirstLogin)
                {
                    var session = Sesion["Id"] as mSession;

                    return Sesiones.Any(ses => ses.Id == session.Id);
                }

                return true;
            }

            /// <summary>
            /// Permite guardar la sesion del usuario para futuras validaciones
            /// </summary>
            /// <param name="Login">Login de usuario</param>
            /// <param name="Sesiones">Lista de sesiones de usuarios</param>
            /// <param name="Sesion">Sesion ASPNET activa del usuario logeado</param>
            /// <history>
            /// [LucianoP]          [jueves, 13 de julio de 2017]    Creado 
            /// </history>
            public static void Guardar(string Login, List<mSession> Sesiones, HttpSessionState Sesion, ref mSession SesionUsuario)
            {
                var sessionModel = new mSession(Sesion.SessionID, Login);
                Sesion["Id"] = sessionModel;
                SesionUsuario = sessionModel;
                Sesiones.Add(sessionModel);
            }

            /// <summary>
            /// Permite eliminar la sesion del usuario
            /// </summary>
            /// <returns>true si el objeto fue removido exitosamente de la sesion</returns>
            /// <history>
            /// [LucianoP]          [jueves, 13 de julio de 2017]    Creado 
            /// </history>
            public static bool Eliminar(List<mSession> Sesiones, mSession DatosSesion)
            {
                if (Sesiones == null || DatosSesion == null)
                    return false;

                return Sesiones.Remove(DatosSesion);
            }
        }
    }
}