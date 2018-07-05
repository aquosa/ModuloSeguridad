using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRetornaStringSQL
{
    [Serializable]
    public class dcRetornaStringSQL
    {
        public dcRetornaStringSQL()
        {
            lstFiltros = new List<DtFiltros>();
        }
        public List<DtFiltros> lstFiltros { get; set; }
    }
    [Serializable]
    public class DtFiltros
    {

        private System.DateTime fECHADESDEField;

        private System.DateTime fECHAHASTAField;

        private string oPERACIONField;

        private string tABLAField;

        private string uSUARIOField;

        private string sUPERVISORField;

        private string nOMBREPCField;

        private string sISTEMAField;

        private string tEXTOBUSCARField;

        /// <comentarios/>
        public System.DateTime FECHADESDE
        {
            get
            {
                return this.fECHADESDEField;
            }
            set
            {
                this.fECHADESDEField = value;
            }
        }

        /// <comentarios/>
        public System.DateTime FECHAHASTA
        {
            get
            {
                return this.fECHAHASTAField;
            }
            set
            {
                this.fECHAHASTAField = value;
            }
        }

        /// <comentarios/>
        public string OPERACION
        {
            get
            {
                return this.oPERACIONField;
            }
            set
            {
                this.oPERACIONField = value;
            }
        }

        /// <comentarios/>
        public string TABLA
        {
            get
            {
                return this.tABLAField;
            }
            set
            {
                this.tABLAField = value;
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
        public string SUPERVISOR
        {
            get
            {
                return this.sUPERVISORField;
            }
            set
            {
                this.sUPERVISORField = value;
            }
        }

        /// <comentarios/>
        public string NOMBREPC
        {
            get
            {
                return this.nOMBREPCField;
            }
            set
            {
                this.nOMBREPCField = value;
            }
        }

        /// <comentarios/>
        public string SISTEMA
        {
            get
            {
                return this.sISTEMAField;
            }
            set
            {
                this.sISTEMAField = value;
            }
        }

        /// <comentarios/>
        public string TEXTOBUSCAR
        {
            get
            {
                return this.tEXTOBUSCARField;
            }
            set
            {
                this.tEXTOBUSCARField = value;
            }
        }
    }
}
