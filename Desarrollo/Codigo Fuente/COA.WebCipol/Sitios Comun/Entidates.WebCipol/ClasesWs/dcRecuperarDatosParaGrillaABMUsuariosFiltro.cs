using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaGrillaABMUsuariosFiltro
{
    [Serializable]
    public class dcRecuperarDatosParaGrillaABMUsuariosFiltro
    {
        public dcRecuperarDatosParaGrillaABMUsuariosFiltro()
        {
            lstSist_Usuarios = new List<Sist_Usuarios>();
        }
        public List<Sist_Usuarios> lstSist_Usuarios { get; set; }
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

        private System.DateTime fechaBajaField;

        private System.DateTime fechaBloqueoField;

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
        public System.DateTime FechaBaja
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
        public System.DateTime FechaBloqueo
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
    }
}

