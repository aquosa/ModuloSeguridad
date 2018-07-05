using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRolDetalle
{
    [Serializable]
    public class dcRecuperarReporteUsuariosXRolDetalleIN
    {
        /*
         * RolesXUsuarios
         * UsuariosXRolDetalle
         */
        public dcRecuperarReporteUsuariosXRolDetalleIN()
        {
            lstRolesXUsuarios = new List<RolesXUsuarios>();
            lstUsuariosXRolDetalle = new List<UsuariosXRolDetalle>();
        }
        public Int32 pIdRol { get; set; }
        public List<RolesXUsuarios> lstRolesXUsuarios { get; set; }
        public List<UsuariosXRolDetalle> lstUsuariosXRolDetalle { get; set; }

    }
    [Serializable]
    public class dcRecuperarReporteUsuariosXRolDetalle
    {
        /*
         * RolesXUsuarios
         * UsuariosXRolDetalle
         */
        public dcRecuperarReporteUsuariosXRolDetalle()
        {
            lstRolesXUsuarios = new List<RolesXUsuarios>();
            lstUsuariosXRolDetalle = new List<UsuariosXRolDetalle>();
        }
        public Int32 intRetorno { get; set; }
        public List<RolesXUsuarios> lstRolesXUsuarios { get; set; }
        public List<UsuariosXRolDetalle> lstUsuariosXRolDetalle { get; set; }
    }
    [Serializable]
    public class RolesXUsuarios
    {

        private string nOMBRESField;

        private string dESCRIPCIONPERFILField;

        private int cANTTAREASROLField;

        private int cANTAREASUSUARIOROLField;

        private int iDUSUARIOField;

        private int iDROLField;

        private string cOMPLETOField;

        private System.DateTime fECHAALTAField;

        private System.DateTime fECHABAJAField;

        private string iDTIPODOCDESCField;

        private string nRODOCUMENTOField;

        private string uSUARIOField;

        private System.DateTime fECHAAMODIFICACIONField;

        private string uSUARIOMODIFField;

        private int iDAREAField;

        private string nOMBREAREAField;

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
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
        public int CANTTAREASROL
        {
            get
            {
                return this.cANTTAREASROLField;
            }
            set
            {
                this.cANTTAREASROLField = value;
            }
        }

        /// <comentarios/>
        public int CANTAREASUSUARIOROL
        {
            get
            {
                return this.cANTAREASUSUARIOROLField;
            }
            set
            {
                this.cANTAREASUSUARIOROLField = value;
            }
        }

        /// <comentarios/>
        public int IDUSUARIO
        {
            get
            {
                return this.iDUSUARIOField;
            }
            set
            {
                this.iDUSUARIOField = value;
            }
        }

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
        public string COMPLETO
        {
            get
            {
                return this.cOMPLETOField;
            }
            set
            {
                this.cOMPLETOField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAALTA
        {
            get
            {
                return this.fECHAALTAField;
            }
            set
            {
                this.fECHAALTAField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHABAJA
        {
            get
            {
                return this.fECHABAJAField;
            }
            set
            {
                this.fECHABAJAField = value;
            }
        }

        /// <comentarios/>
        public string IDTIPODOCDESC
        {
            get
            {
                return this.iDTIPODOCDESCField;
            }
            set
            {
                this.iDTIPODOCDESCField = value;
            }
        }

        /// <comentarios/>
        public string NRODOCUMENTO
        {
            get
            {
                return this.nRODOCUMENTOField;
            }
            set
            {
                this.nRODOCUMENTOField = value;
            }
        }

        /// <comentarios/>
        public string USUARIO
        {
            get
            {
                return this.uSUARIOField;
            }
            set
            {
                this.uSUARIOField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAAMODIFICACION
        {
            get
            {
                return this.fECHAAMODIFICACIONField;
            }
            set
            {
                this.fECHAAMODIFICACIONField = value;
            }
        }

        /// <comentarios/>
        public string USUARIOMODIF
        {
            get
            {
                return this.uSUARIOMODIFField;
            }
            set
            {
                this.uSUARIOMODIFField = value;
            }
        }

        /// <comentarios/>
        public int IDAREA
        {
            get
            {
                return this.iDAREAField;
            }
            set
            {
                this.iDAREAField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
            }
        }
    }
    [Serializable]
    public class UsuariosXRolDetalle
    {

        private int iDUSUARIOField;

        private string uSUARIOField;

        private string nOMBRESField;

        private string nRODOCUMENTOField;

        private string asignadoPorField;

        private System.DateTime fECHAULTMODIFField;

        private System.DateTime fECHABAJAField;

        private string nOMBREAREAField;

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

        private string tieneAsignadaField;

        private string usoField;

        private string tareaInhibidaField;

        public UsuariosXRolDetalle()
        {
            this.tareaInhibidaField = "NO";
        }

        /// <comentarios/>
        public int IDUSUARIO
        {
            get
            {
                return this.iDUSUARIOField;
            }
            set
            {
                this.iDUSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string USUARIO
        {
            get
            {
                return this.uSUARIOField;
            }
            set
            {
                this.uSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }

        /// <comentarios/>
        public string NRODOCUMENTO
        {
            get
            {
                return this.nRODOCUMENTOField;
            }
            set
            {
                this.nRODOCUMENTOField = value;
            }
        }

        /// <comentarios/>
        public string AsignadoPor
        {
            get
            {
                return this.asignadoPorField;
            }
            set
            {
                this.asignadoPorField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAULTMODIF
        {
            get
            {
                return this.fECHAULTMODIFField;
            }
            set
            {
                this.fECHAULTMODIFField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHABAJA
        {
            get
            {
                return this.fECHABAJAField;
            }
            set
            {
                this.fECHABAJAField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREAREA
        {
            get
            {
                return this.nOMBREAREAField;
            }
            set
            {
                this.nOMBREAREAField = value;
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

        /// <comentarios/>
        public string TieneAsignada
        {
            get
            {
                return this.tieneAsignadaField;
            }
            set
            {
                this.tieneAsignadaField = value;
            }
        }

        /// <comentarios/>
        public string Uso
        {
            get
            {
                return this.usoField;
            }
            set
            {
                this.usoField = value;
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

