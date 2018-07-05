using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarReporteUsuariosXArea
{
    [Serializable]
    public class dcRecuperarReporteUsuariosXArea
    {
        /*
         * UsuariosXAreas
         */
        public dcRecuperarReporteUsuariosXArea()
        {
            lstUsuariosXAreas = new List<UsuariosXAreas>();
        }
        public List<UsuariosXAreas> lstUsuariosXAreas { get; set; }
    }
    [Serializable]
    public class UsuariosXAreas
    {

        private int iDUSUARIOField;

        private string uSUARIOField;

        private string nOMBRESField;

        private System.DateTime fECHAALTAField;

        private System.DateTime fECHABAJAField;

        private string nRODOCUMENTOField;

        private string dESCRIPCIONPERFILField;

        private string tIPOABREVIADOField;

        private int iDAREAField;

        private string nOMBREAREAField;

        private string bAJAField;

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
        public string TIPOABREVIADO
        {
            get
            {
                return this.tIPOABREVIADOField;
            }
            set
            {
                this.tIPOABREVIADOField = value;
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

        /// <comentarios/>
        public string BAJA
        {
            get
            {
                return this.bAJAField;
            }
            set
            {
                this.bAJAField = value;
            }
        }
    }
}

