using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados
{
    [Serializable]
    public class dcRecuperarSistBloqueados
    {
        /*
         * SE_SIST_BLOQUEADOS
         * SE_SIST_HABILITADOS
         * SE_USUARIOS
         */
        public dcRecuperarSistBloqueados()
        {
            lstSE_SIST_BLOQUEADOS = new List<SE_SIST_BLOQUEADOS>();
            lstSE_SIST_HABILITADOS = new List<SE_SIST_HABILITADOS>();
            lstSE_USUARIOS = new List<SE_USUARIOS>();
        }

        public List<SE_USUARIOS> lstSE_USUARIOS { get; set; }
        public List<SE_SIST_HABILITADOS> lstSE_SIST_HABILITADOS { get; set; }
        public List<SE_SIST_BLOQUEADOS> lstSE_SIST_BLOQUEADOS { get; set; }
    }
    [Serializable]
    public class SE_USUARIOS
    {

        private short iDUSUARIOField;

        private string nOMBRESField;

        /// <comentarios/>
        public short IDUSUARIO
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
    }
    [Serializable]
    public class SE_SIST_HABILITADOS
    {

        private short iDSISTEMAField;

        private string dESCSISTEMAField;

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
    }
    [Serializable]
    public class SE_SIST_BLOQUEADOS
    {

        private decimal iDSISTEMAField;

        private decimal iDUSUARIOField;

        private string dESCSISTEMAField;

        private string nOMBRESField;

        /// <comentarios/>
        public decimal IDSISTEMA
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
        public decimal IDUSUARIO
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
    }
}

