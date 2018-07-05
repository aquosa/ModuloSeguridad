using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcAdministrarTareasSupervisantes
{
    [Serializable]
    public class dcAdministrarTareasSupervisantes
    {
        /*
         * TAREAS
         * SE_TAREASAUTORIZADAS
         */
        public dcAdministrarTareasSupervisantes()
        {
            lstSE_TAREASAUTORIZADAS = new List<SE_TAREASAUTORIZADAS>();
        }
        public List<SE_TAREASAUTORIZADAS> lstSE_TAREASAUTORIZADAS { get; set; }
        public List<TAREAS> lstTAREAS { get; set; }
    }
    [Serializable]
    public class SE_TAREASAUTORIZADAS
    {

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

        private int iDAUTORIZACIONField;

        public SE_TAREASAUTORIZADAS()
        {
            this.dESCRIPCIONTAREAField = "_";
            this.iDAUTORIZACIONField = -1;
        }

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

        /// <comentarios/>
        public int IDAUTORIZACION
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

