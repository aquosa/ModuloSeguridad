using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Comun;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaMonitorActividades
{
    [Serializable]
    public class dcRecuperarDatosParaMonitorActividades
    {
        public List<Sist_sesionesactivas> lstSist_sesionesactivas { get; set; }
    }

    [Serializable]
    public class Sist_sesionesactivas
    {

        private string usuarioField;

        private System.DateTime iNICIOTAREAField;

        private string terminalField;

        private Decimal iDUSUARIOField;

        private string nOMBRESField;

        private Decimal iDAREAField;

        private string nOMBREAREAField;

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
        public System.DateTime INICIOTAREA
        {
            get
            {
                return this.iNICIOTAREAField;
            }
            set
            {
                this.iNICIOTAREAField = value;
            }
        }

        public string INICIOTAREASTR
        {
            get { return FormatoFecha.DateToString(iNICIOTAREAField); }
        }

        /// <comentarios/>
        public string Terminal
        {
            get
            {
                return this.terminalField;
            }
            set
            {
                this.terminalField = value;
            }
        }

        /// <comentarios/>
        public Decimal IDUSUARIO
        {
            get
            {
                return this.iDUSUARIOField;
            }
            set
            {
                this.iDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }

        /// <comentarios/>
        public Decimal IDAREA
        {
            get
            {
                return this.iDAREAField;
            }
            set
            {
                this.iDAREAField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
            }
        }
    }
}

