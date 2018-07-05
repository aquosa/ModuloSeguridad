using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasQueRequierenAutorizacion
{
    [Serializable]
    public class dcRecuperarTareasQueRequierenAutorizacion
    {
        /*
         * SE_TAREASAUTORIZADAS
         */
        public dcRecuperarTareasQueRequierenAutorizacion()
        {
            lstSE_TAREASAUTORIZADAS = new List<SE_TAREASAUTORIZADAS>();
        }
        public List<SE_TAREASAUTORIZADAS> lstSE_TAREASAUTORIZADAS { get; set; }
    }
    [Serializable]
    public class SE_TAREASAUTORIZADAS
    {

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

        private int iDAUTORIZACIONField;

        public SE_TAREASAUTORIZADAS()
        {
            this.dESCRIPCIONTAREAField = "_";
            this.iDAUTORIZACIONField = -1;
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
        public int IDAUTORIZACION
        {
            get
            {
                return this.iDAUTORIZACIONField;
            }
            set
            {
                this.iDAUTORIZACIONField = value;
            }
        }
    }
}

