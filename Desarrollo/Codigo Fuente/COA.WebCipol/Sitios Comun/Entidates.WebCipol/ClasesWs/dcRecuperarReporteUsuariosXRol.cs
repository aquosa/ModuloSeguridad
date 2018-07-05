using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXRol
{
    [Serializable]
    public class dcRecuperarReporteUsuariosXRol
    {
        /*
         * RolesXUsuarios
         * SE_TAREAS_USUARIO
         */
        public dcRecuperarReporteUsuariosXRol()
        {
            lstRolesXUsuarios = new List<RolesXUsuarios>();
            lstSE_TAREAS_USUARIO = new List<SE_TAREAS_USUARIO>();
        }
        public List<RolesXUsuarios> lstRolesXUsuarios { get; set; }
        public List<SE_TAREAS_USUARIO> lstSE_TAREAS_USUARIO { get; set; }
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
    public class SE_TAREAS_USUARIO
    {

        private int iDTAREAField;

        private int iDROLField;

        private int iDUSUARIOField;

        private decimal cANTIDADAUTORIZADAField;

        private string tAREAINHIBIDAField;

        private System.DateTime fECHAULTMODIFField;

        private decimal iDUSUARIOULTMODIFField;

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
        public decimal CANTIDADAUTORIZADA
        {
            get
            {
                return this.cANTIDADAUTORIZADAField;
            }
            set
            {
                this.cANTIDADAUTORIZADAField = value;
            }
        }

        /// <comentarios/>
        public string TAREAINHIBIDA
        {
            get
            {
                return this.tAREAINHIBIDAField;
            }
            set
            {
                this.tAREAINHIBIDAField = value;
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
        public decimal IDUSUARIOULTMODIF
        {
            get
            {
                return this.iDUSUARIOULTMODIFField;
            }
            set
            {
                this.iDUSUARIOULTMODIFField = value;
            }
        }
    }
}

