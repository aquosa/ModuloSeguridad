namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsTareas
    {

    }

    public class dtsTareasSE_SIST_HABILITADOS
    {

        private string dESCSISTEMAField;

        private int IDSISTEMAField;

        private bool IDSISTEMAFieldSpecified;

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
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
            }
        }

        /// <comentarios/>
        public bool IDSISTEMASpecified
        {
            get
            {
                return this.IDSISTEMAFieldSpecified;
            }
            set
            {
                this.IDSISTEMAFieldSpecified = value;
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


    public class dtsTareasSE_TAREAS
    {

        private decimal IDTAREAField;

        private string dESCRIPCIONTAREAField;

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

    public class dtsTareasSE_TAREASAUTORIZADAS
    {

        private decimal IDTAREAField;

        private string dESCRIPCIONTAREAField;

        private decimal IDAUTORIZACIONField;

        public dtsTareasSE_TAREASAUTORIZADAS()
        {
            this.dESCRIPCIONTAREAField = "_";
            this.IDAUTORIZACIONField = -1;
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
        public decimal IDAUTORIZACION
        {
            get
            {
                return this.IDAUTORIZACIONField;
            }
            set
            {
                this.IDAUTORIZACIONField = value;
            }
        }
    }

    public class dtsTareasTAREAS
    {

        private decimal IDTAREAField;

        private string cODIGOTAREAField;

        private string dESCRIPCIONTAREAField;

        private string rEQUIEREAUDITORIAField;

        private int IDSISTEMAField;

        private string dESCSISTEMAField;

        private string aUDITORIAField;

        private decimal IDAUTORIZACIONField;

        private decimal IDGRUPOField;

        private bool IDGRUPOFieldSpecified;

        private bool iDAUTORIZACIONBOOLField;

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
                return this.IDSISTEMAField;
            }
            set
            {
                this.IDSISTEMAField = value;
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
                return this.IDAUTORIZACIONField;
            }
            set
            {
                this.IDAUTORIZACIONField = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IDGRUPOSpecified
        {
            get
            {
                return this.IDGRUPOFieldSpecified;
            }
            set
            {
                this.IDGRUPOFieldSpecified = value;
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