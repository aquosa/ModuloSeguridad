using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles
{
    [Serializable]
    public class dcRecuperarDatosParaABMRoles
    {
        /*
         * SE_ROLES
         * Roles_Composicion
         * tblUsuariosXRoles
         * SE_GRUPO_EXCLUSION
         * ArbolGrupo
         */
        public dcRecuperarDatosParaABMRoles()
        {
            lstArbolGrupo = new List<ArbolGrupo>();
            lstRoles_Composicion = new List<Roles_Composicion>();
            lstSE_GRUPO_EXCLUSION = new List<SE_GRUPO_EXCLUSION>();
            lstSE_ROLES = new List<SE_ROLES>();
            lsttblUsuariosXRoles = new List<tblUsuariosXRoles>();
        }
        public List<SE_ROLES> lstSE_ROLES { get; set; }
        public List<Roles_Composicion> lstRoles_Composicion { get; set; }
        public List<SE_GRUPO_EXCLUSION> lstSE_GRUPO_EXCLUSION { get; set; }
        public List<ArbolGrupo> lstArbolGrupo { get; set; }
        public List<tblUsuariosXRoles> lsttblUsuariosXRoles { get; set; }
    }
    [Serializable]
    public class SE_ROLES
    {

        private int iDROLField;

        private string dESCRIPCIONPERFILField;

        /// <comentarios/>
        public int IDROL
        {
            get
            {
                return this.iDROLField;
            }
            set
            {
                this.iDROLField = value;
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
    [Serializable]
    public class Roles_Composicion
    {

        private int idRolField;

        private bool idRolFieldSpecified;

        private string descripcionPerfilField;

        private int idGrupoField;

        private bool idGrupoFieldSpecified;

        private string descGrupoField;

        private int idSistemaField;

        private bool idSistemaFieldSpecified;

        private string descSistemaField;

        private int idTareaField;

        private bool idTareaFieldSpecified;

        private string descripcionTareaField;

        private string tareaInhibidaField;

        /// <comentarios/>
        public int IdRol
        {
            get
            {
                return this.idRolField;
            }
            set
            {
                this.idRolField = value;
            }
        }

        /// <comentarios/>
        public bool IdRolSpecified
        {
            get
            {
                return this.idRolFieldSpecified;
            }
            set
            {
                this.idRolFieldSpecified = value;
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
        public int IdGrupo
        {
            get
            {
                return this.idGrupoField;
            }
            set
            {
                this.idGrupoField = value;
            }
        }

        /// <comentarios/>
        public bool IdGrupoSpecified
        {
            get
            {
                return this.idGrupoFieldSpecified;
            }
            set
            {
                this.idGrupoFieldSpecified = value;
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
                return this.idSistemaField;
            }
            set
            {
                this.idSistemaField = value;
            }
        }

        /// <comentarios/>
        public bool idSistemaSpecified
        {
            get
            {
                return this.idSistemaFieldSpecified;
            }
            set
            {
                this.idSistemaFieldSpecified = value;
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
        public int idTarea
        {
            get
            {
                return this.idTareaField;
            }
            set
            {
                this.idTareaField = value;
            }
        }

        /// <comentarios/>
        public bool idTareaSpecified
        {
            get
            {
                return this.idTareaFieldSpecified;
            }
            set
            {
                this.idTareaFieldSpecified = value;
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
    [Serializable]
    public class SE_GRUPO_EXCLUSION
    {

        private int iDGRUPOACTUALField;

        private int iDGRUPEXCLUYENTEField;

        /// <comentarios/>
        public int IDGRUPOACTUAL
        {
            get
            {
                return this.iDGRUPOACTUALField;
            }
            set
            {
                this.iDGRUPOACTUALField = value;
            }
        }

        /// <comentarios/>
        public int IDGRUPEXCLUYENTE
        {
            get
            {
                return this.iDGRUPEXCLUYENTEField;
            }
            set
            {
                this.iDGRUPEXCLUYENTEField = value;
            }
        }
    }
    [Serializable]
    public class ArbolGrupo
    {

        private short iDGRUPOField;

        private string dESCGRUPOField;

        private short iDSISTEMAField;

        private string dESCSISTEMAField;

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

        /// <comentarios/>
        public short IDGRUPO
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
        public short IDSISTEMA
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
    public class tblUsuariosXRoles
    {

        public Decimal IdUsuario { get; set; }
    }
}

