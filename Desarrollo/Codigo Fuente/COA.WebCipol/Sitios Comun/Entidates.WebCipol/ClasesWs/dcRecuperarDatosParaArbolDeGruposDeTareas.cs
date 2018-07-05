using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaArbolDeGruposDeTareas
{
    [Serializable]
    public class dcRecuperarDatosParaArbolDeGruposDeTareas
    {
        /*
         * ArbolGrupo
         */
        public dcRecuperarDatosParaArbolDeGruposDeTareas()
        {
            lstArbolGrupo = new List<ArbolGrupo>();
        }
        public List<ArbolGrupo> lstArbolGrupo { get; set; }
    }
    [Serializable]
    public class ArbolGrupo
    {

        private short iDGRUPOField;

        private string dESCGRUPOField;

        private short iDSISTEMAField;

        private string dESCSISTEMAField;

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

        /// <comentarios/>
        public short IDGRUPO
        {
            get
            {
                return this.iDGRUPOField;
            }
            set
            {
                this.iDGRUPOField = value;
            }
        }

        /// <comentarios/>
        public string DESCGRUPO
        {
            get
            {
                return this.dESCGRUPOField;
            }
            set
            {
                this.dESCGRUPOField = value;
            }
        }

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
    }

}

