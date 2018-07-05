
namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsGrupos
    {

    }

    public class dtsGruposAuditoria
    {

        private string sQLAuditarField;

        /// <comentarios/>
        public string SQLAuditar
        {
            get
            {
                return this.sQLAuditarField;
            }
            set
            {
                this.sQLAuditarField = value;
            }
        }
    }

    public class dtsGruposAuditoria1
    {

        private string sQLAuditarField;

        /// <comentarios/>
        public string SQLAuditar
        {
            get
            {
                return this.sQLAuditarField;
            }
            set
            {
                this.sQLAuditarField = value;
            }
        }
    }

    public class dtsGruposGRUPO_EXCLUYENTE
    {

        private decimal IDGRUPOField;

        private string dESCGRUPOField;

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
        public string DESCGRUPO
        {
            get
            {
                return this.dESCGRUPOField;
            }
            set
            {
                this.dESCGRUPOField = value;
            }
        }
    }

    public class dtsGruposGRUPO_NO_EXCLUYENTE
    {

        private decimal IDGRUPOField;

        private string dESCGRUPOField;

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
        public string DESCGRUPO
        {
            get
            {
                return this.dESCGRUPOField;
            }
            set
            {
                this.dESCGRUPOField = value;
            }
        }
    }

    public class dtsGruposSE_GRUPO_TAREA
    {

        private decimal IDGRUPOField;

        private string dESCGRUPOField;

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
        public string DESCGRUPO
        {
            get
            {
                return this.dESCGRUPOField;
            }
            set
            {
                this.dESCGRUPOField = value;
            }
        }
    }

    public class dtsGruposSE_Grupo_Exclusion
    {

        private decimal IDGRUPOACTUALField;

        private decimal IDGRUPEXCLUYENTEField;

        /// <comentarios/>
        public decimal IDGRUPOACTUAL
        {
            get
            {
                return this.IDGRUPOACTUALField;
            }
            set
            {
                this.IDGRUPOACTUALField = value;
            }
        }

        /// <comentarios/>
        public decimal IDGRUPEXCLUYENTE
        {
            get
            {
                return this.IDGRUPEXCLUYENTEField;
            }
            set
            {
                this.IDGRUPEXCLUYENTEField = value;
            }
        }
    }

    public class dtsGruposSE_SIST_HABILITADOS
    {

        private decimal IDSISTEMAField;

        private string dESCSISTEMAField;

        /// <comentarios/>
        public decimal IDSISTEMA
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
    }

    public class dtsGruposSE_TAREAS
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

    public class dtsGruposSE_TAREAS_ASIGNADAS
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
}