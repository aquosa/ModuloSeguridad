using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcAdministrarRoles
{
    [Serializable]
    public class dcAdministrarRolesIN
    {
        /*
         * SE_ROLES
         * TareasAsignadas
         * TareasNoAsignadas
         * ParametrosDeABM
         * tblUsuariosXRoles
         */
        public dcAdministrarRolesIN()
        {
            lstParametrosDeABM = new List<ParametrosDeABM>();
            lstSE_ROLES = new List<SE_ROLES>();
            lstTareasAsignadas = new List<TareasAsignadas>();
            lstTareasNoAsignadas = new List<TareasNoAsignadas>();
            lsttblUsuariosXRoles = new List<tblUsuariosXRoles>();
        }
        public string pStrElementosEliminados { get; set; }
        public List<TareasNoAsignadas> lstTareasNoAsignadas { get; set; }
        public List<TareasAsignadas> lstTareasAsignadas { get; set; }
        public List<SE_ROLES> lstSE_ROLES { get; set; }
        public List<ParametrosDeABM> lstParametrosDeABM { get; set; }
        public List<tblUsuariosXRoles> lsttblUsuariosXRoles { get; set; }
    }
    [Serializable]
    public class dcAdministrarRoles
    {
        /*
         * SE_ROLES
         * TareasAsignadas
         * TareasNoAsignadas
         * ParametrosDeABM
         * tblUsuariosXRoles
         */
        public dcAdministrarRoles()
        {
            lstParametrosDeABM = new List<ParametrosDeABM>();
            lstSE_ROLES = new List<SE_ROLES>();
            lstTareasAsignadas = new List<TareasAsignadas>();
            lstTareasNoAsignadas = new List<TareasNoAsignadas>();
            lsttblUsuariosXRoles = new List<tblUsuariosXRoles>();
        }
        public Int32 intRetorno { get; set; }
        public List<TareasNoAsignadas> lstTareasNoAsignadas { get; set; }
        public List<TareasAsignadas> lstTareasAsignadas { get; set; }
        public List<SE_ROLES> lstSE_ROLES { get; set; }
        public List<ParametrosDeABM> lstParametrosDeABM { get; set; }
        public List<tblUsuariosXRoles> lsttblUsuariosXRoles { get; set; }
    }
    [Serializable]
    public class tblUsuariosXRoles
    {
        public Decimal IdUsuario { get; set; }
    }
    [Serializable]
    public class ParametrosDeABM
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
    public class TareasAsignadas
    {

        private int idTareaField;

        private bool idTareaFieldSpecified;

        private string tareaInhibidaField;

        public TareasAsignadas()
        {
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
    public class TareasNoAsignadas
    {

        private int idTareaField;

        private bool idTareaFieldSpecified;

        private string tareaInhibidaField;

        public TareasNoAsignadas()
        {
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

