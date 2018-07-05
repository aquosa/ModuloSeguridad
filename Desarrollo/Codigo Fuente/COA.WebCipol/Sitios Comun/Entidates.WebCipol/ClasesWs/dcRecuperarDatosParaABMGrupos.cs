using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMGrupos
{
    [Serializable]
    public class dcRecuperarDatosParaABMGrupos
    {
        public dcRecuperarDatosParaABMGrupos()
        {
            lstGRUPO_EXCLUYENTE = new List<GRUPO_EXCLUYENTE>();
            lstGRUPO_NO_EXCLUYENTE = new List<GRUPO_NO_EXCLUYENTE>();
            lstSE_GRUPO_TAREA = new List<SE_GRUPO_TAREA>();
            lstSE_SIST_HABILITADOS = new List<SE_SIST_HABILITADOS>();
            lstSE_TAREAS = new List<SE_TAREAS>();
            lstSE_TAREAS_ASIGNADAS = new List<SE_TAREAS_ASIGNADAS>();
        }
        /*
         * SE_SIST_HABILITADOS
         * SE_Grupo_Tarea
         * SE_Tareas
         * SE_TAREAS_ASIGNADAS
         * GRUPO_EXCLUYENTE
         * GRUPO_NO_EXCLUYENTE
         */
        public List<GRUPO_NO_EXCLUYENTE> lstGRUPO_NO_EXCLUYENTE { get; set; }
        public List<GRUPO_EXCLUYENTE> lstGRUPO_EXCLUYENTE { get; set; }
        public List<SE_TAREAS_ASIGNADAS> lstSE_TAREAS_ASIGNADAS { get; set; }
        public List<SE_TAREAS> lstSE_TAREAS { get; set; }
        public List<SE_GRUPO_TAREA> lstSE_GRUPO_TAREA { get; set; }
        public List<SE_SIST_HABILITADOS> lstSE_SIST_HABILITADOS { get; set; }
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
    [Serializable]
    public class SE_TAREAS
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
    [Serializable]
    public class SE_GRUPO_TAREA
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
}

