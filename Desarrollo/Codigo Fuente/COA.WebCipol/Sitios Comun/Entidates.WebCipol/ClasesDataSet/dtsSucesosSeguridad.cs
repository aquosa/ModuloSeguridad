
namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsSucesosSeguridad
    {

    }

    public class dtsSucesosSeguridadSE_AUDITORIA
    {

        private System.DateTime fECHAHORALOGField;

        private decimal cODMENSAJEField;

        private string tEXTOMENSAJEField;

        private string uSUARIOACTUANTEField;

        private string uSUARIOAFECTADOField;

        /// <comentarios/>
        public System.DateTime FECHAHORALOG
        {
            get
            {
                return this.fECHAHORALOGField;
            }
            set
            {
                this.fECHAHORALOGField = value;
            }
        }

        /// <comentarios/>
        public decimal CODMENSAJE
        {
            get
            {
                return this.cODMENSAJEField;
            }
            set
            {
                this.cODMENSAJEField = value;
            }
        }

        /// <comentarios/>
        public string TEXTOMENSAJE
        {
            get
            {
                return this.tEXTOMENSAJEField;
            }
            set
            {
                this.tEXTOMENSAJEField = value;
            }
        }

        /// <comentarios/>
        public string USUARIOACTUANTE
        {
            get
            {
                return this.uSUARIOACTUANTEField;
            }
            set
            {
                this.uSUARIOACTUANTEField = value;
            }
        }

        /// <comentarios/>
        public string USUARIOAFECTADO
        {
            get
            {
                return this.uSUARIOAFECTADOField;
            }
            set
            {
                this.uSUARIOAFECTADOField = value;
            }
        }
    }

    public class dtsSucesosSeguridadSE_CODAUDITORIA
    {

        private string cODAUDITORIAField;

        private string tEXTOAUDITORIAField;

        public dtsSucesosSeguridadSE_CODAUDITORIA()
        {
            this.cODAUDITORIAField = "_";
            this.tEXTOAUDITORIAField = "_";
        }

        /// <comentarios/>
        public string CODAUDITORIA
        {
            get
            {
                return this.cODAUDITORIAField;
            }
            set
            {
                this.cODAUDITORIAField = value;
            }
        }

        /// <comentarios/>
        public string TEXTOAUDITORIA
        {
            get
            {
                return this.tEXTOAUDITORIAField;
            }
            set
            {
                this.tEXTOAUDITORIAField = value;
            }
        }
    }

    public class dtsSucesosSeguridadSist_Usuarios
    {

        private decimal IDUSUARIOField;

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

        public decimal IdTipoDocField;

        private int idAreaField;

        private string cantIntInvUsoCtaField;

        public dtsSucesosSeguridadSist_Usuarios()
        {
            this.IDUSUARIOField = -2;
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
            this.IdTipoDocField = -1;
            this.idAreaField = -1;
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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

        /// <comentarios/>
        public decimal IDTIPODOC
        {
            get
            {
                return this.IdTipoDocField;
            }
            set
            {
                this.IdTipoDocField = value;
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
    }

    public class dtsSucesosSeguridadSist_UsuariosConTareasCipol
    {

        private decimal IDUSUARIOField;

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

        public decimal IdTipoDocField;

        private int idAreaField;

        private string cantIntInvUsoCtaField;

        public dtsSucesosSeguridadSist_UsuariosConTareasCipol()
        {
            this.IDUSUARIOField = -2;
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
            this.IdTipoDocField = -1;
            this.idAreaField = -1;
        }

        /// <comentarios/>
        public decimal IDUSUARIO
        {
            get
            {
                return this.IDUSUARIOField;
            }
            set
            {
                this.IDUSUARIOField = value;
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

        /// <comentarios/>
        public decimal IDTIPODOC
        {
            get
            {
                return this.IdTipoDocField;
            }
            set
            {
                this.IdTipoDocField = value;
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
    }
}