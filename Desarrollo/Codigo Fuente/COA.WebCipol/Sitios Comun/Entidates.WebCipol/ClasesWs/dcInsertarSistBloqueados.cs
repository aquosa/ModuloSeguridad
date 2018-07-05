using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcInsertarSistBloqueados
{
    [Serializable]
    public class dcInsertarSistBloqueados
    {
        public dcInsertarSistBloqueados()
        {
            /*
             * SE_SIST_BLOQUEADOS
             */
            lstSE_SIST_BLOQUEADOS = new List<SE_SIST_BLOQUEADOS>();
        }
        public List<SE_SIST_BLOQUEADOS> lstSE_SIST_BLOQUEADOS { get; set; }
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

