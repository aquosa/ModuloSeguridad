using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaReporteUsuariosXRoles
{
    [Serializable]
    public class dcRecuperarDatosParaReporteUsuariosXRoles
    {
        /*
         * SE_ROLES
         */
        public dcRecuperarDatosParaReporteUsuariosXRoles()
        {
            lstSE_ROLES = new List<SE_ROLES>();
        }
        public List<SE_ROLES> lstSE_ROLES { get; set; }
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
}

