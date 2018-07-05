namespace COA.WebCipol.Entidades.ClasesDataSet
{
    public class dtsRoles
    {

    }

    public class dtsRolesArbolGrupo
    {

        private decimal IDGRUPOField;

        private string dESCGRUPOField;

        private decimal IDSISTEMAField;

        private string dESCSISTEMAField;

        private decimal IDTAREAField;

        private string dESCRIPCIONTAREAField;

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

    public class dtsRolesParametrosDeABM
    {

        private string cambioContraseniaField;

        private string mensajesAuditoriaField;

        private string diasVencimientoDeClaveField;

        /// <comentarios/>
        public string CambioContrasenia
        {
            get
            {
                return this.cambioContraseniaField;
            }
            set
            {
                this.cambioContraseniaField = value;
            }
        }

        /// <comentarios/>
        public string MensajesAuditoria
        {
            get
            {
                return this.mensajesAuditoriaField;
            }
            set
            {
                this.mensajesAuditoriaField = value;
            }
        }

        /// <comentarios/>
        public string DiasVencimientoDeClave
        {
            get
            {
                return this.diasVencimientoDeClaveField;
            }
            set
            {
                this.diasVencimientoDeClaveField = value;
            }
        }
    }

    public class dtsRolesRoles_Composicion
    {

        private decimal IDROLField;

        private bool IDROLFieldSpecified;

        private string descripcionPerfilField;

        private decimal IDGRUPOField;

        private bool IDGRUPOFieldSpecified;

        private string descGrupoField;

        private int IDSISTEMAField;

        private bool IDSISTEMAFieldSpecified;

        private string descSistemaField;

        private decimal IDTAREAField;

        private bool IDTAREAFieldSpecified;

        private string descripcionTareaField;

        private string tareaInhibidaField;

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
            }
        }

        /// <comentarios/>
        public bool IdRolSpecified
        {
            get
            {
                return this.IDROLFieldSpecified;
            }
            set
            {
                this.IDROLFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string DescripcionPerfil
        {
            get
            {
                return this.descripcionPerfilField;
            }
            set
            {
                this.descripcionPerfilField = value;
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
        public bool IdGrupoSpecified
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
        public string DescGrupo
        {
            get
            {
                return this.descGrupoField;
            }
            set
            {
                this.descGrupoField = value;
            }
        }

        /// <comentarios/>
        public int idSistema
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
        public bool idSistemaSpecified
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
        public string DescSistema
        {
            get
            {
                return this.descSistemaField;
            }
            set
            {
                this.descSistemaField = value;
            }
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
        public bool idTareaSpecified
        {
            get
            {
                return this.IDTAREAFieldSpecified;
            }
            set
            {
                this.IDTAREAFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string DescripcionTarea
        {
            get
            {
                return this.descripcionTareaField;
            }
            set
            {
                this.descripcionTareaField = value;
            }
        }

        /// <comentarios/>
        public string TareaInhibida
        {
            get
            {
                return this.tareaInhibidaField;
            }
            set
            {
                this.tareaInhibidaField = value;
            }
        }
    }

    public class dtsRolesSE_GRUPO_EXCLUSION
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

    public class dtsRolesSE_ROLES
    {

        private decimal IDROLField;

        private string dESCRIPCIONPERFILField;

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
            }
        }

        /// <comentarios/>
        public string DESCRIPCIONPERFIL
        {
            get
            {
                return this.dESCRIPCIONPERFILField;
            }
            set
            {
                this.dESCRIPCIONPERFILField = value;
            }
        }
    }

    public class dtsRolesTareasAsignadas
    {

        private decimal IDTAREAField;

        private bool IDTAREAFieldSpecified;

        private string tareaInhibidaField;

        public dtsRolesTareasAsignadas()
        {
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
        public bool idTareaSpecified
        {
            get
            {
                return this.IDTAREAFieldSpecified;
            }
            set
            {
                this.IDTAREAFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string TareaInhibida
        {
            get
            {
                return this.tareaInhibidaField;
            }
            set
            {
                this.tareaInhibidaField = value;
            }
        }
    }

    public class dtsRolesTareasNoAsignadas
    {

        private decimal IDTAREAField;

        private bool IDTAREAFieldSpecified;

        private string tareaInhibidaField;

        public dtsRolesTareasNoAsignadas()
        {
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
        public bool idTareaSpecified
        {
            get
            {
                return this.IDTAREAFieldSpecified;
            }
            set
            {
                this.IDTAREAFieldSpecified = value;
            }
        }

        /// <comentarios/>
        public string TareaInhibida
        {
            get
            {
                return this.tareaInhibidaField;
            }
            set
            {
                this.tareaInhibidaField = value;
            }
        }
    }

    public class dtsRolesUsuariosPorRol
    {

        private decimal IDROLField;

        private decimal IDSISTEMAField;

        private decimal IDTAREAField;

        private string dESCRIPCIONPERFILField;

        private string dESCSISTEMAField;

        private string dESCRIPCIONTAREAField;

        /// <comentarios/>
        public decimal IDROL
        {
            get
            {
                return this.IDROLField;
            }
            set
            {
                this.IDROLField = value;
            }
        }

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
        public string DESCRIPCIONPERFIL
        {
            get
            {
                return this.dESCRIPCIONPERFILField;
            }
            set
            {
                this.dESCRIPCIONPERFILField = value;
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