using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsAuditoria
    {
        public List<dtsAuditoriaSE_AUDITORIA> lstSE_Auditoria { get; set; }
    }

    public class dtsAuditoriaSE_AUDITORIA
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

}
