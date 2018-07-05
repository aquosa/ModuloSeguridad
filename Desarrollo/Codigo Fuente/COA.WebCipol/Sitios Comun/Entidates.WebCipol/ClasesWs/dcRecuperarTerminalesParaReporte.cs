using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarTerminalesParaReporte
{
    [Serializable]
    public class dcRecuperarTerminalesParaReporte
    {
        /*
         * SE_TERMINALES
         */
        public dcRecuperarTerminalesParaReporte()
        {
            lstSE_TERMINALES = new List<SE_TERMINALES>();
        }
        public List<SE_TERMINALES> lstSE_TERMINALES { get; set; }
    }
    [Serializable]
    public class SE_TERMINALES
    {

        private int iDTERMINALField;

        private string cODTERMINALField;

        private string nOMBRECOMPUTADORAField;

        private string uSOHABILITADOField;

        private string mODELOPROCESADORField;

        private long cANTMEMORIARAMField;

        private long tAMANIODISCOField;

        private string mODELOMONITORField;

        private string mODELOACELVIDEOField;

        private string dESCADICIONALField;

        private int iDAREAField;

        private string nOMBREAREAField;

        private string nombreNetBiosField;

        private string uSOHABILITADOSINENCField;

        private string oRIGENACTUALIZACIONField;

        /// <comentarios/>
        public int IDTERMINAL
        {
            get
            {
                return this.iDTERMINALField;
            }
            set
            {
                this.iDTERMINALField = value;
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
        public string NOMBRECOMPUTADORA
        {
            get
            {
                return this.nOMBRECOMPUTADORAField;
            }
            set
            {
                this.nOMBRECOMPUTADORAField = value;
            }
        }

        /// <comentarios/>
        public string USOHABILITADO
        {
            get
            {
                return this.uSOHABILITADOField;
            }
            set
            {
                this.uSOHABILITADOField = value;
            }
        }

        /// <comentarios/>
        public string MODELOPROCESADOR
        {
            get
            {
                return this.mODELOPROCESADORField;
            }
            set
            {
                this.mODELOPROCESADORField = value;
            }
        }

        /// <comentarios/>
        public long CANTMEMORIARAM
        {
            get
            {
                return this.cANTMEMORIARAMField;
            }
            set
            {
                this.cANTMEMORIARAMField = value;
            }
        }

        /// <comentarios/>
        public long TAMANIODISCO
        {
            get
            {
                return this.tAMANIODISCOField;
            }
            set
            {
                this.tAMANIODISCOField = value;
            }
        }


        /// <comentarios/>
        public string MODELOMONITOR
        {
            get
            {
                return this.mODELOMONITORField;
            }
            set
            {
                this.mODELOMONITORField = value;
            }
        }

        /// <comentarios/>
        public string MODELOACELVIDEO
        {
            get
            {
                return this.mODELOACELVIDEOField;
            }
            set
            {
                this.mODELOACELVIDEOField = value;
            }
        }

        /// <comentarios/>
        public string DESCADICIONAL
        {
            get
            {
                return this.dESCADICIONALField;
            }
            set
            {
                this.dESCADICIONALField = value;
            }
        }

        /// <comentarios/>
        public int IDAREA
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

        /// <comentarios/>
        public string NombreNetBios
        {
            get
            {
                return this.nombreNetBiosField;
            }
            set
            {
                this.nombreNetBiosField = value;
            }
        }

        /// <comentarios/>
        public string USOHABILITADOSINENC
        {
            get
            {
                return this.uSOHABILITADOSINENCField;
            }
            set
            {
                this.uSOHABILITADOSINENCField = value;
            }
        }

        /// <comentarios/>
        public string ORIGENACTUALIZACION
        {
            get
            {
                return this.oRIGENACTUALIZACIONField;
            }
            set
            {
                this.oRIGENACTUALIZACIONField = value;
            }
        }
    }
}

