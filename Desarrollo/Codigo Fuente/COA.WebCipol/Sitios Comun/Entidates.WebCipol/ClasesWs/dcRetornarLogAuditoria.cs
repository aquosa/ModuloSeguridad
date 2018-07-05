using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using COA.WebCipol.Comun;

namespace COA.WebCipol.Entidades.ClasesWs.dcRetornarLogAuditoria
{
    [Serializable]
    public class dcRetornarLogAuditoriaIN
    { 
        public DateTime fechadesde{get;set;}
        public DateTime fechahasta{get;set;}
        public string UsuarioActuante{get;set;}
        public string usuarioafectado{get;set;}
        public string CodigoEvento { get; set; }
    }
    [Serializable]
    public class dcRetornarLogAuditoria
    {
        public List<SE_AUDITORIA> lstAuditoria { get; set; }
    }
    [Serializable]
    public class SE_AUDITORIA
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

        public string fechacanonica { get; set; }

        public string FechaConFormato
        {
            //get { return FormatoFecha.DateToString(this.FECHAHORALOG); }
            get { return this.FECHAHORALOG.ToString(); }
        }
    }
}
