using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol
{
    [Serializable]
    public class dcRecuperarComposicionRol
    {
        /*
         * Roles_Composicion
         * tblUsuariosXRoles
         */
        public dcRecuperarComposicionRol()
        {
            lstRoles_Composicion = new List<Roles_Composicion>();
            lsttblUsuariosXRoles = new List<tblUsuariosXRoles>();
        }

        public List<Roles_Composicion> lstRoles_Composicion { get; set; }
        public List<tblUsuariosXRoles> lsttblUsuariosXRoles { get; set; }
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
    public class tblUsuariosXRoles
    {
        public Decimal IdUsuario { get; set; }
    }
}

