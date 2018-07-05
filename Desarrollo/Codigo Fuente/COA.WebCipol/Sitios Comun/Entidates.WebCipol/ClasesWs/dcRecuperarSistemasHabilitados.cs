using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistemasHabilitados
{
    [Serializable]
    public class dcRecuperarSistemasHabilitados
    {
        public List<SE_SIST_HABILITADOS> lstdtsSistHabilitadosSE_SIST_HABILITADOS { get; set; }
    }
    [Serializable]
    public class SE_SIST_HABILITADOS
    {

        private decimal iDSISTEMAField;

        private string cODSISTEMAField;

        private string dESCSISTEMAField;

        private System.DateTime fECHAHABILITACIONField;

        private bool fECHAHABILITACIONFieldSpecified;

        private string nOMBREEXECField;

        private string sISTEMAHABILITADOField;

        private string iCONOField;

        private string oBSERVACIONESField;

        private string pAGINAPORDEFECTOField;

        private string dESCRIPCIONCORTAField;

        private string iMPACTACAJAField;

        /// <comentarios/>
        public decimal IDSISTEMA
        {
            get
            {
                return this.iDSISTEMAField;
            }
            set
            {
                this.iDSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string CODSISTEMA
        {
            get
            {
                return this.cODSISTEMAField;
            }
            set
            {
                this.cODSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAHABILITACION
        {
            get
            {
                return this.fECHAHABILITACIONField;
            }
            set
            {
                this.fECHAHABILITACIONField = value;
            }
        }

        /// <comentarios/>
        public bool FECHAHABILITACIONSpecified
        {
            get
            {
                return this.fECHAHABILITACIONFieldSpecified;
            }
            set
            {
                this.fECHAHABILITACIONFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string NOMBREEXEC
        {
            get
            {
                return this.nOMBREEXECField;
            }
            set
            {
                this.nOMBREEXECField = value;
            }
        }

        /// <comentarios/>
        public string SISTEMAHABILITADO
        {
            get
            {
                return this.sISTEMAHABILITADOField;
            }
            set
            {
                this.sISTEMAHABILITADOField = value;
            }
        }

        /// <comentarios/>
        public string ICONO
        {
            get
            {
                return this.iCONOField;
            }
            set
            {
                this.iCONOField = value;
            }
        }

        /// <comentarios/>
        public string OBSERVACIONES
        {
            get
            {
                return this.oBSERVACIONESField;
            }
            set
            {
                this.oBSERVACIONESField = value;
            }
        }

        /// <comentarios/>
        public string PAGINAPORDEFECTO
        {
            get
            {
                return this.pAGINAPORDEFECTOField;
            }
            set
            {
                this.pAGINAPORDEFECTOField = value;
            }
        }

        /// <comentarios/>
        public string DESCRIPCIONCORTA
        {
            get
            {
                return this.dESCRIPCIONCORTAField;
            }
            set
            {
                this.dESCRIPCIONCORTAField = value;
            }
        }

        /// <comentarios/>
        public string IMPACTACAJA
        {
            get
            {
                return this.iMPACTACAJAField;
            }
            set
            {
                this.iMPACTACAJAField = value;
            }
        }
    }
}

