
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsCtrlInactividad
    {
        public List<dtsCtrlInactividadSist_Usuarios> lstdtsCtrlInactividadSist_Usuarios { get; set; }
    }

    public class dtsCtrlInactividadSist_Usuarios
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

        private string aLIAS_USUARIOField;

        private System.DateTime fechaUltUsoCtaField;

        private string lapsoInactividadField;

        public dtsCtrlInactividadSist_Usuarios()
        {

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
        [System.ComponentModel.DefaultValueAttribute("_")]
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
        [System.ComponentModel.DefaultValueAttribute(-1)]
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
        [System.ComponentModel.DefaultValueAttribute(-1)]
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
        public string LapsoInactividad
        {
            get
            {
                return this.lapsoInactividadField;
            }
            set
            {
                this.lapsoInactividadField = value;
            }
        }
    }
}