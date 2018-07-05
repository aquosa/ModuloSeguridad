using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcAdministrarAbmUsuarios
{
    [Serializable]
    public class dcAdministrarAbmUsuarios
    {
        public dcAdministrarAbmUsuarios()
        {
        }
        public string pstrError { get; set; }
        public Int32 intRetorno { get; set; }
    }
    [Serializable]
    public class dcAdministrarAbmUsuariosIN
    {
        public dcAdministrarAbmUsuariosIN()
        {
            lstParametrosDeABM = new List<ParametrosDeABM>();
            lstRoles_Composicion = new List<Roles_Composicion>();
            lstSE_Horarios_Usuario = new List<SE_Horarios_Usuario>();
            lstSE_Term_Usuario = new List<SE_Term_Usuario>();
            lstSist_Usuarios = new List<Sist_Usuarios>();
            lstSE_HISTORIAL_USUARIO = new List<SE_HISTORIAL_USUARIO>();
            pstrError = "";
        }

        public string pstrError { get; set; }
        public string MensajeAuditoria { get; set; }
        public Int32 CantidadMaximaHistorialContraseniaa { get; set; }
        public List<ParametrosDeABM> lstParametrosDeABM { get; set; }
        public List<Roles_Composicion> lstRoles_Composicion { get; set; }
        public List<SE_Horarios_Usuario> lstSE_Horarios_Usuario { get; set; }
        public List<SE_Term_Usuario> lstSE_Term_Usuario { get; set; }
        public List<Sist_Usuarios> lstSist_Usuarios { get; set; }
        public List<SE_HISTORIAL_USUARIO> lstSE_HISTORIAL_USUARIO { get; set; }
    }
    [Serializable]
    public class ParametrosDeABM
    {

        private string cambioContraseniaField;

        private string mensajesAuditoriaField;

        private string diasVencimientoDeClaveField;

        /// <comentarios/>
        public string CambioContrasenia
        {
            get
            {
                return this.cambioContraseniaField;
            }
            set
            {
                this.cambioContraseniaField = value;
            }
        }

        /// <comentarios/>
        public string MensajesAuditoria
        {
            get
            {
                return this.mensajesAuditoriaField;
            }
            set
            {
                this.mensajesAuditoriaField = value;
            }
        }

        /// <comentarios/>
        public string DiasVencimientoDeClave
        {
            get
            {
                return this.diasVencimientoDeClaveField;
            }
            set
            {
                this.diasVencimientoDeClaveField = value;
            }
        }
    }
    [Serializable]
    public class Roles_Composicion
    {

        private int idRolField;

        private string descripcionPerfilField;

        private int idGrupoField;

        private string descGrupoField;

        private int idSistemaField;

        private string descSistemaField;

        private int idTareaField;

        private string descripcionTareaField;

        private string tareaInhibidaField;

        /// <comentarios/>
        public int IdRol
        {
            get
            {
                return this.idRolField;
            }
            set
            {
                this.idRolField = value;
            }
        }

        /// <comentarios/>
        public string DescripcionPerfil
        {
            get
            {
                return this.descripcionPerfilField;
            }
            set
            {
                this.descripcionPerfilField = value;
            }
        }

        /// <comentarios/>
        public int IdGrupo
        {
            get
            {
                return this.idGrupoField;
            }
            set
            {
                this.idGrupoField = value;
            }
        }

        /// <comentarios/>
        public string DescGrupo
        {
            get
            {
                return this.descGrupoField;
            }
            set
            {
                this.descGrupoField = value;
            }
        }

        /// <comentarios/>
        public int idSistema
        {
            get
            {
                return this.idSistemaField;
            }
            set
            {
                this.idSistemaField = value;
            }
        }

        /// <comentarios/>
        public string DescSistema
        {
            get
            {
                return this.descSistemaField;
            }
            set
            {
                this.descSistemaField = value;
            }
        }

        /// <comentarios/>
        public int idTarea
        {
            get
            {
                return this.idTareaField;
            }
            set
            {
                this.idTareaField = value;
            }
        }

        /// <comentarios/>
        public string DescripcionTarea
        {
            get
            {
                return this.descripcionTareaField;
            }
            set
            {
                this.descripcionTareaField = value;
            }
        }

        /// <comentarios/>
        public string TareaInhibida
        {
            get
            {
                return this.tareaInhibidaField;
            }
            set
            {
                this.tareaInhibidaField = value;
            }
        }
    }
    [Serializable]
    public class SE_Horarios_Usuario
    {

        private int idHorarioField;

        private int idDiaField;

        /// <comentarios/>
        public int IdHorario
        {
            get
            {
                return this.idHorarioField;
            }
            set
            {
                this.idHorarioField = value;
            }
        }

        /// <comentarios/>
        public int idDia
        {
            get
            {
                return this.idDiaField;
            }
            set
            {
                this.idDiaField = value;
            }
        }
    }
    [Serializable]
    public class SE_Term_Usuario
    {

        private int idTerminalField;

        private int idUsuarioField;

        private string cODTERMINALField;

        private int idAreaField;

        /// <comentarios/>
        public int IdTerminal
        {
            get
            {
                return this.idTerminalField;
            }
            set
            {
                this.idTerminalField = value;
            }
        }

        /// <comentarios/>
        public int IdUsuario
        {
            get
            {
                return this.idUsuarioField;
            }
            set
            {
                this.idUsuarioField = value;
            }
        }

        /// <comentarios/>
        public string CODTERMINAL
        {
            get
            {
                return this.cODTERMINALField;
            }
            set
            {
                this.cODTERMINALField = value;
            }
        }

        /// <comentarios/>
        public int idArea
        {
            get
            {
                return this.idAreaField;
            }
            set
            {
                this.idAreaField = value;
            }
        }

    }
    [Serializable]
    public class Sist_Usuarios
    {

        private int idUsuarioField;

        private string usuarioField;

        private string nombresField;

        private string nombreAreaField;

        private string domicilioField;

        private string tipoAbreviadoField;

        private string nroDocumentoField;

        private string forzarCambioField;

        private string forzarCambioDesField;

        private string ctaBloqueadaField;

        private string ctaBloqueadaDesField;

        private string ctaBloqueadaDesLetraField;

        private string comentarioField;

        private System.DateTime fechaAltaField;

        private System.DateTime? fechaBajaField;

        private System.DateTime? fechaBloqueoField;

        private string fICTICIAField;

        private int idTipoDocField;

        private int idAreaField;

        private string cantIntInvUsoCtaField;

        private string aLIAS_USUARIOField;

        private System.DateTime fechaUltUsoCtaField;

        private string emailField;

        public Sist_Usuarios()
        {
            this.idUsuarioField = -2;
            this.usuarioField = "_";
            this.nombresField = "_";
            this.nombreAreaField = "_";
            this.domicilioField = "_";
            this.tipoAbreviadoField = "_";
            this.nroDocumentoField = "_";
            this.forzarCambioField = "_";
            this.forzarCambioDesField = "_";
            this.ctaBloqueadaField = "_";
            this.ctaBloqueadaDesField = "_";
            this.ctaBloqueadaDesLetraField = "_";
            this.comentarioField = "_";
            this.fICTICIAField = "_";
            this.idTipoDocField = -1;
            this.idAreaField = -1;
        }

        /// <comentarios/>
        public int IdUsuario
        {
            get
            {
                return this.idUsuarioField;
            }
            set
            {
                this.idUsuarioField = value;
            }
        }

        /// <comentarios/>
        public string Usuario
        {
            get
            {
                return this.usuarioField;
            }
            set
            {
                this.usuarioField = value;
            }
        }

        /// <comentarios/>
        public string Nombres
        {
            get
            {
                return this.nombresField;
            }
            set
            {
                this.nombresField = value;
            }
        }

        /// <comentarios/>
        public string NombreArea
        {
            get
            {
                return this.nombreAreaField;
            }
            set
            {
                this.nombreAreaField = value;
            }
        }

        /// <comentarios/>
        public string Domicilio
        {
            get
            {
                return this.domicilioField;
            }
            set
            {
                this.domicilioField = value;
            }
        }

        /// <comentarios/>
        public string TipoAbreviado
        {
            get
            {
                return this.tipoAbreviadoField;
            }
            set
            {
                this.tipoAbreviadoField = value;
            }
        }

        /// <comentarios/>
        public string NroDocumento
        {
            get
            {
                return this.nroDocumentoField;
            }
            set
            {
                this.nroDocumentoField = value;
            }
        }

        /// <comentarios/>
        public string ForzarCambio
        {
            get
            {
                return this.forzarCambioField;
            }
            set
            {
                this.forzarCambioField = value;
            }
        }

        /// <comentarios/>
        public string ForzarCambioDes
        {
            get
            {
                return this.forzarCambioDesField;
            }
            set
            {
                this.forzarCambioDesField = value;
            }
        }

        /// <comentarios/>
        public string CtaBloqueada
        {
            get
            {
                return this.ctaBloqueadaField;
            }
            set
            {
                this.ctaBloqueadaField = value;
            }
        }

        /// <comentarios/>
        public string CtaBloqueadaDes
        {
            get
            {
                return this.ctaBloqueadaDesField;
            }
            set
            {
                this.ctaBloqueadaDesField = value;
            }
        }

        /// <comentarios/>
        public string CtaBloqueadaDesLetra
        {
            get
            {
                return this.ctaBloqueadaDesLetraField;
            }
            set
            {
                this.ctaBloqueadaDesLetraField = value;
            }
        }

        public bool BLNFECHADESBLOQUEO { get; set; }

        /// <comentarios/>
        public string Comentario
        {
            get
            {
                return this.comentarioField;
            }
            set
            {
                this.comentarioField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaAlta
        {
            get
            {
                return this.fechaAltaField;
            }
            set
            {
                this.fechaAltaField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime? FechaBaja
        {
            get
            {
                return this.fechaBajaField;
            }
            set
            {
                this.fechaBajaField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime? FechaBloqueo
        {
            get
            {
                return this.fechaBloqueoField;
            }
            set
            {
                this.fechaBloqueoField = value;
            }
        }

        /// <comentarios/>
        public string FICTICIA
        {
            get
            {
                return this.fICTICIAField;
            }
            set
            {
                this.fICTICIAField = value;
            }
        }

        public int IdTipoDoc
        {
            get
            {
                return this.idTipoDocField;
            }
            set
            {
                this.idTipoDocField = value;
            }
        }

        public int idArea
        {
            get
            {
                return this.idAreaField;
            }
            set
            {
                this.idAreaField = value;
            }
        }

        /// <comentarios/>
        public string CantIntInvUsoCta
        {
            get
            {
                return this.cantIntInvUsoCtaField;
            }
            set
            {
                this.cantIntInvUsoCtaField = value;
            }
        }

        /// <comentarios/>
        public string ALIAS_USUARIO
        {
            get
            {
                return this.aLIAS_USUARIOField;
            }
            set
            {
                this.aLIAS_USUARIOField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FechaUltUsoCta
        {
            get
            {
                return this.fechaUltUsoCtaField;
            }
            set
            {
                this.fechaUltUsoCtaField = value;
            }
        }

        /// <comentarios/>
        public string Email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        public bool IntegradoAlDominio { get; set; }
    }
    [Serializable]
    public class SE_HISTORIAL_USUARIO
    {

        private decimal oRDENField;

        private string sINONIMOField;

        private System.DateTime? fechaVencimientoField;

        /// <comentarios/>
        public decimal ORDEN
        {
            get
            {
                return this.oRDENField;
            }
            set
            {
                this.oRDENField = value;
            }
        }

        /// <comentarios/>
        public string SINONIMO
        {
            get
            {
                return this.sINONIMOField;
            }
            set
            {
                this.sINONIMOField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime? FechaVencimiento
        {
            get
            {
                return this.fechaVencimientoField;
            }
            set
            {
                this.fechaVencimientoField = value;
            }
        }

    }
}

