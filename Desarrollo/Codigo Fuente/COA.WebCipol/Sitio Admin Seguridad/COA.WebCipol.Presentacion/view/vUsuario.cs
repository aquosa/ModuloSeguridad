using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COA.Cipol.Presentacion._UIHelpers;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios;
using COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios;

namespace COA.WebCipol.Presentacion.view
{
    [Serializable]
    public class vSessionDatosUsuarios
    {
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.ComposicionDeRoles> lstComposicionDeRoles { get; set; }
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_PARAMETROS> lstSE_PARAMETROS { get; set; }
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_TERMINALES> lstSE_TERMINALES { get; set; }
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.Sist_Usuarios> lstSist_Usuarios { get; set; }
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_GRUPO_EXCLUSION> lstSE_GRUPO_EXCLUSION { get; set; }
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuarios.SE_Horarios_Usuario> lstSE_Horarios_Usuario { get; set; }
        public COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.dcRecuperarDatosParaABMUsuarios objDatosUsuario { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14414
    /// </history>
    [Serializable]
    public class vUsuarioGetItem : EntidadesBase
    {
        public vUsuario elemento { get; set; }
        public string jsonterminaleshabilitadas { get; set; }
        public string jsonterminalesnohabilitadas { get; set; }
        public string jsontareasdisponibles { get; set; }
        public string jsontareasasignadas { get; set; }
        public string jsoncbotareasasignadas { get; set; }
    }
    [Serializable]
    public class vUsuario : EntidadesBase
    {
        private CifrarDatos.TresDES objEncriptarNET;
        public vUsuario()
        {
            objEncriptarNET = new CifrarDatos.TresDES();
            objEncriptarNET.Key = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.Key;
            objEncriptarNET.IV = ManejoSesion.DatosCIPOLSesion.DatosPadreCIPOLCliente.IV;
        }
        public bool update { get; set; }
        public string contrasenia { get; set; }
        public string repetircontrasenia { get; set; }
        public bool blnCambioContrasenia { get; set; }
        public bool blnRolParaUsuarioGuardado { get; set; }
        //public bool blnMostrarForzarCambio { get; set; }
        /// <comentarios/>
        public int IdUsuario { get; set; }
        /// <comentarios/>
        public string Usuario { get; set; }
        /// <comentarios/>
        public string Nombres { get; set; }
        /// <comentarios/>
        public string NombreArea { get; set; }
        /// <comentarios/>
        public string Domicilio { get; set; }
        /// <comentarios/>
        public string TipoAbreviado { get; set; }
        /// <comentarios/>
        public string NroDocumento { get; set; }
        /// <comentarios/>
        public string ForzarCambio { get; set; }
        /// <comentarios/>
        public string ForzarCambioDes { get; set; }

        public bool blnForzarCambios
        {
            get
            {
                if (!string.IsNullOrEmpty(ForzarCambio))
                    return (ForzarCambio == "1");
                else
                    return false;
            }
            set
            {
                ForzarCambio = (value) ? "1" : "0";
                ForzarCambioDes = (value) ? "1" : "0";
            }
        }
        /// <comentarios/>
        public string CtaBloqueada { get; set; }
        /// <comentarios/>
        public string CtaBloqueadaDes { get; set; }
        /// <comentarios/>
        public string CtaBloqueadaDesLetra { get; set; }

        public bool blnCtaBloqueada {
            get {
                if (!string.IsNullOrEmpty(CtaBloqueada))
                    return (CtaBloqueada == "1");
                else
                    return false;
            }
            set {
                CtaBloqueada = (value) ? "1" : "0";
                CtaBloqueadaDes = objEncriptarNET.Criptografia(CifrarDatos.Accion.Encriptacion, (value) ? "1" : "0");
                CtaBloqueadaDesLetra = (value) ? "S" : "";
            }
        }

        public bool blnIntegradoAlDominio { get; set; }
        /// <comentarios/>
        public string Comentario { get; set; }

        public string strFechaAlta { get; set; }

        public string strFechaBaja { get; set; }

        public string strFechaBloqueo { get; set; }
        /// <comentarios/>
        public string FICTICIA { get; set; }
        /// <comentarios/>
        public int IdTipoDoc { get; set; }
        /// <comentarios/>
        public int idArea { get; set; }
        /// <comentarios/>
        public string CantIntInvUsoCta { get; set; }
        /// <comentarios/>
        public string ALIAS_USUARIO { get; set; }

        public string strFechaUltUsoCta { get; set; }
        /// <comentarios/>
        public string Email { get; set; }

        public List<itemUsuarioRolGenerico> lsttareas { get; set; }

        public List<itemUsuarioHorario> lstHorarios { get; set; }
    }
    [Serializable]
    public class vAlertas : EntidadesBase
    {
        public string jsonterminaleshabilitadas { get; set; }

        public string jsonterminalesnohabilitadas { get; set; }
    }
    [Serializable]
    public class vUsuarioCarga : EntidadesBase
    {
        //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
        public vUsuarioCarga()
        {
            elemento = new vUsuario();
            permiso = new vPermisosUsuario();
            filtro = new vUsuarioFiltro();
        }
        public vUsuario elemento { get; set; }
        public vPermisosUsuario permiso { get; set; }
        public vUsuarioFiltro filtro { get; set; }
        public bool blnNombreDominio { get; set; }
        public bool blnSeguridad_SoloDominio { get; set; }
        public string jsonusuarios { get; set; }
        public string jsoncboestado { get; set; }
        public string jsoncboarea { get; set; }
        public string jsoncbotipodocumento { get; set; }
        public string jsoncbofiltroareaterminal { get; set; }
    }

    [Serializable]
    public class vPermisosUsuario : EntidadesBase
    {
        public bool blnNuevoVisible { get; set; }
        public bool blnModificarVisible { get; set; }
        public bool blnEliminarVisible { get; set; }
        public bool blnTerminalVisible { get; set; }
        public bool blnHorarioVisible { get; set; }
        public bool blnAsignarRolVisible { get; set; }
        public bool blnDesasignarRolVisible { get; set; }
    }

    //[MiguelP]         28/10/2014      GCP - Cambios 15598 - Se agrego la funcionalidad del filtro mediante JSON para poder poner comillas simples en las busquedas
    [Serializable]
    public class vUsuarioFiltro : EntidadesBase
    {
        public string filtro{ get; set; }
        public string filtrobaja{ get; set; }
        public bool chkUsu{ get; set; }
        public bool chkNombre{ get; set; }
        public bool chkArea{ get; set; }
        public bool chkSubCadenas { get; set; }
    }


    [Serializable]
    public class vUsuarioEliminar : EntidadesBase
    {
        public vUsuarioEliminar()
        {
            usuariogetcarga = new vUsuarioCarga();
        }
        public vUsuarioCarga usuariogetcarga { get; set; }
        public bool pregunta { get; set; }
    }

    [Serializable]
    public class vUsuarioAdministrar : EntidadesBase
    {
        public int IdUsuario { get; set; }
    }
    [Serializable]
    public class vSessionDatosTerminalesUsuario
    {
        public List<SE_Term_Usuario> SE_TERM_USUARIO { get; set; }
    }

    public class DiaHora
    {
        public string Id { get; set; }
    }

    #region "Roles"

    [Serializable]
    public class vSessionDatosUsuarioRoles
    {
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMUsuarios.Roles_Composicion> Roles_Composicion { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <history>
    /// [MartinV]          [jueves, 14 de noviembre de 2013]       Modificado  GCP-Cambios 14414
    /// </history>
    [Serializable]
    public class vUsuarioRolCarga : EntidadesBase
    {
        public string jsontareasdisponibles { get; set; }
        public string jsontareasasignadas { get; set; }
        public string jsoncbotareasasignadas { get; set; }
    }
    [Serializable]
    public class itemUsuarioRolGenerico
    {
        public string Id { get; set; }
        public string nombre { get; set; }
        public bool asignada { get; set; }
    }
    [Serializable]
    public class itemUsuarioHorario
    {
        public string idDia { get; set; }
        public string idHorario { get; set; }
    }
    [Serializable]
    public class vUsuarioRolAsignar : EntidadesBase
    { }
    #endregion
}