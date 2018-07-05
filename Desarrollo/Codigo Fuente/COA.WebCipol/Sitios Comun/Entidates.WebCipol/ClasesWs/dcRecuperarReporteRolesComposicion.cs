using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteRolesComposicion
{
    [Serializable]
    public class dcRecuperarReporteRolesComposicion
    {
        /*
         * Roles_Composicion
         */
        public dcRecuperarReporteRolesComposicion()
        {
            lstRoles_Composicion = new List<Roles_Composicion>();
        }
        public List<Roles_Composicion> lstRoles_Composicion { get; set; }
    }
    [Serializable]
    public class Roles_Composicion
    {

        private int idRolField;

        private string descripcionPerfilField;

        private int idGrupoField;

        private string descGrupoField;

        private int idSistemaField;

        private string descSistemaField;

        private int idTareaField;

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
}

