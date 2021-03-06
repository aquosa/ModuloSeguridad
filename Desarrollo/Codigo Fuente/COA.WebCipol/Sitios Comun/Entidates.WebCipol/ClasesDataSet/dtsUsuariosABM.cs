﻿namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsUsuariosABM
    {

    }

    public class dtsUsuariosABMParametrosDeABM
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

    public class dtsUsuariosABMRoles_Composicion
    {

        private decimal IDROLField;

        private string descripcionPerfilField;

        private decimal IDGRUPOField;

        private string descGrupoField;

        private int IDSISTEMAField;

        private string descSistemaField;

        private decimal IDTAREAField;

        private string descripcionTareaField;

        private string tareaInhibidaField;

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
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
        public decimal IDGRUPO
        {
            get
            {
                return this.IDGRUPOField;
            }
            set
            {
                this.IDGRUPOField = value;
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
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
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
        public decimal IDTAREA
        {
            get
            {
                return this.IDTAREAField;
            }
            set
            {
                this.IDTAREAField = value;
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

    public class dtsUsuariosABMRoles_X_Usuarios
    {

        private string descripcionPerfilField;

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
    }

    public class dtsUsuariosABMSE_HISTORIAL_USUARIO
    {

        private decimal oRDENField;

        private string sINONIMOField;

        private System.DateTime fechaVencimientoField;

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
        public System.DateTime FechaVencimiento
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

    public class dtsUsuariosABMSE_Horarios_Usuario
    {

        private decimal IdHorarioField;

        private decimal IdDiaField;

        /// <comentarios/>
        public decimal IDHORARIO
        {
            get
            {
                return this.IdHorarioField;
            }
            set
            {
                this.IdHorarioField = value;
            }
        }

        /// <comentarios/>
        public decimal IDDIA
        {
            get
            {
                return this.IdDiaField;
            }
            set
            {
                this.IdDiaField = value;
            }
        }
    }

    public class dtsUsuariosABMSE_TAREAS_USUARIO
    {

        private decimal IDTAREAField;

        private decimal IDROLField;

        private decimal IDUSUARIOField;

        private decimal cANTIDADAUTORIZADAField;

        private string tAREAINHIBIDAField;

        private System.DateTime fECHAULTMODIFField;

        private decimal iDUSUARIOULTMODIFField;

        /// <comentarios/>
        public decimal IDTAREA
        {
            get
            {
                return this.IDTAREAField;
            }
            set
            {
                this.IDTAREAField = value;
            }
        }

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
            }
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
        public decimal CANTIDADAUTORIZADA
        {
            get
            {
                return this.cANTIDADAUTORIZADAField;
            }
            set
            {
                this.cANTIDADAUTORIZADAField = value;
            }
        }

        /// <comentarios/>
        public string TAREAINHIBIDA
        {
            get
            {
                return this.tAREAINHIBIDAField;
            }
            set
            {
                this.tAREAINHIBIDAField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAULTMODIF
        {
            get
            {
                return this.fECHAULTMODIFField;
            }
            set
            {
                this.fECHAULTMODIFField = value;
            }
        }

        /// <comentarios/>
        public decimal IDUSUARIOULTMODIF
        {
            get
            {
                return this.iDUSUARIOULTMODIFField;
            }
            set
            {
                this.iDUSUARIOULTMODIFField = value;
            }
        }
    }

    public class dtsUsuariosABMSE_Term_Usuario
    {

        private decimal IDTERMINALField;

        private decimal IDUSUARIOField;

        private string cODTERMINALField;

        private int idAreaField;

        /// <comentarios/>
        public decimal IDTERMINAL
        {
            get
            {
                return this.IDTERMINALField;
            }
            set
            {
                this.IDTERMINALField = value;
            }
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

    public class dtsUsuariosABMSist_Usuarios
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

        private System.DateTime fechaAltaField;

        private System.DateTime fechaBajaField;

        private System.DateTime fechaBloqueoField;

        private string fICTICIAField;

        public decimal IdTipoDocField;

        private int idAreaField;

        private string cantIntInvUsoCtaField;

        private string aLIAS_USUARIOField;

        private bool integradoAlDominioField;

        private string emailField;

        private string comentarioField;

        public dtsUsuariosABMSist_Usuarios()
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
            this.fICTICIAField = "_";
            this.IdTipoDocField = -1;
            this.idAreaField = -1;
            this.integradoAlDominioField = false;
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
        public bool IntegradoAlDominio
        {
            get
            {
                return this.integradoAlDominioField;
            }
            set
            {
                this.integradoAlDominioField = value;
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
    }
}