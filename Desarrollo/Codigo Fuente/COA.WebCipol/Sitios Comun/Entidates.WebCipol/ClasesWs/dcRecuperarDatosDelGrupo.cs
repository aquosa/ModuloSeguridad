using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosDelGrupo
{
    [Serializable]
    public class dcRecuperarDatosDelGrupo
    {
        /*
         * SE_TAREAS_ASIGNADAS
         * GRUPO_EXCLUYENTE
         * GRUPO_NO_EXCLUYENTE
         */
        public dcRecuperarDatosDelGrupo()
        {
            lstGRUPO_EXCLUYENTE = new List<GRUPO_EXCLUYENTE>();
            lstGRUPO_NO_EXCLUYENTE = new List<GRUPO_NO_EXCLUYENTE>();
            lstSE_TAREAS_ASIGNADAS = new List<SE_TAREAS_ASIGNADAS>();
        }
        public List<GRUPO_EXCLUYENTE> lstGRUPO_EXCLUYENTE { get; set; }
        public List<GRUPO_NO_EXCLUYENTE> lstGRUPO_NO_EXCLUYENTE { get; set; }
        public List<SE_TAREAS_ASIGNADAS> lstSE_TAREAS_ASIGNADAS { get; set; }
    }
    [Serializable]
    public class GRUPO_EXCLUYENTE
    {

        private short iDGRUPOField;

        private string dESCGRUPOField;
    
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
    }
    [Serializable]
    public class GRUPO_NO_EXCLUYENTE
    {

        private short iDGRUPOField;

        private string dESCGRUPOField;

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
    }
    [Serializable]
    public class SE_TAREAS_ASIGNADAS
    {

        private int iDTAREAField;

        private string dESCRIPCIONTAREAField;

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

