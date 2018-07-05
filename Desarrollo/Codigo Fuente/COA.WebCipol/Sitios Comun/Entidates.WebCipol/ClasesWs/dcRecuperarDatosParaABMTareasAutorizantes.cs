using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMTareasAutorizantes
{
    [Serializable]
    public class dcRecuperarDatosParaABMTareasAutorizantes
    {
        /*
         * SE_SIST_HABILITADOS
         * SE_Tareas
         * TAREAS
         */
        public dcRecuperarDatosParaABMTareasAutorizantes()
        {
            lstSE_SIST_HABILITADOS = new List<SE_SIST_HABILITADOS>();
            lstSE_TAREAS = new List<SE_TAREAS>();
            lstTAREAS = new List<TAREAS>();
        }
        public List<SE_SIST_HABILITADOS> lstSE_SIST_HABILITADOS { get; set; }
        public List<SE_TAREAS> lstSE_TAREAS { get; set; }
        public List<TAREAS> lstTAREAS { get; set; }
    }
    [Serializable]
    public class SE_SIST_HABILITADOS
    {

        private string dESCSISTEMAField;

        private int iDSISTEMAField;

        private bool iDSISTEMAFieldSpecified;

        private string cODSISTEMAField;

        private string iDCODSISTEMAField;

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
        public int IDSISTEMA
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
        public bool IDSISTEMASpecified
        {
            get
            {
                return this.iDSISTEMAFieldSpecified;
            }
            set
            {
                this.iDSISTEMAFieldSpecified = value;
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
        public string IDCODSISTEMA
        {
            get
            {
                return this.iDCODSISTEMAField;
            }
            set
            {
                this.iDCODSISTEMAField = value;
            }
        }
    }
    [Serializable]
    public class SE_TAREAS
    {

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

        /// <comentarios/>
        public int IDTAREA
        {
            get
            {
                return this.iDTAREAField;
            }
            set
            {
                this.iDTAREAField = value;
            }
        }

        /// <comentarios/>
        public string DESCRIPCIONTAREA
        {
            get
            {
                return this.dESCRIPCIONTAREAField;
            }
            set
            {
                this.dESCRIPCIONTAREAField = value;
            }
        }
    }
    [Serializable]
    public class TAREAS
    {

        private int iDTAREAField;

        private string cODIGOTAREAField;

        private string dESCRIPCIONTAREAField;

        private string rEQUIEREAUDITORIAField;

        private int iDSISTEMAField;

        private string dESCSISTEMAField;

        private string aUDITORIAField;

        private decimal iDAUTORIZACIONField;

        private decimal iDGRUPOField;

        private bool iDGRUPOFieldSpecified;

        private bool iDAUTORIZACIONBOOLField;

        /// <comentarios/>
        public int IDTAREA
        {
            get
            {
                return this.iDTAREAField;
            }
            set
            {
                this.iDTAREAField = value;
            }
        }

        /// <comentarios/>
        public string CODIGOTAREA
        {
            get
            {
                return this.cODIGOTAREAField;
            }
            set
            {
                this.cODIGOTAREAField = value;
            }
        }

        /// <comentarios/>
        public string DESCRIPCIONTAREA
        {
            get
            {
                return this.dESCRIPCIONTAREAField;
            }
            set
            {
                this.dESCRIPCIONTAREAField = value;
            }
        }

        /// <comentarios/>
        public string REQUIEREAUDITORIA
        {
            get
            {
                return this.rEQUIEREAUDITORIAField;
            }
            set
            {
                this.rEQUIEREAUDITORIAField = value;
            }
        }

        /// <comentarios/>
        public int IDSISTEMA
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
        public string AUDITORIA
        {
            get
            {
                return this.aUDITORIAField;
            }
            set
            {
                this.aUDITORIAField = value;
            }
        }

        /// <comentarios/>
        public decimal IDAUTORIZACION
        {
            get
            {
                return this.iDAUTORIZACIONField;
            }
            set
            {
                this.iDAUTORIZACIONField = value;
            }
        }

        /// <comentarios/>
        public decimal IDGRUPO
        {
            get
            {
                return this.iDGRUPOField;
            }
            set
            {
                this.iDGRUPOField = value;
            }
        }

        /// <comentarios/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IDGRUPOSpecified
        {
            get
            {
                return this.iDGRUPOFieldSpecified;
            }
            set
            {
                this.iDGRUPOFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public bool IDAUTORIZACIONBOOL
        {
            get
            {
                return this.iDAUTORIZACIONBOOLField;
            }
            set
            {
                this.iDAUTORIZACIONBOOLField = value;
            }
        }

    }
}

