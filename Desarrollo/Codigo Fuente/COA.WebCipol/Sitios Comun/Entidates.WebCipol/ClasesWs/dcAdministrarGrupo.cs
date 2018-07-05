using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcAdministrarGrupo
{
    [Serializable]
    public class dcAdministrarGrupo
    {
        public dcAdministrarGrupo()
        {
            lstAuditoria = new List<Auditoria>();
            lstGRUPO_EXCLUYENTE = new List<GRUPO_EXCLUYENTE>();
            lstSE_GRUPO_TAREA = new List<SE_GRUPO_TAREA>();
            lstSE_TAREAS_ASIGNADAS = new List<SE_TAREAS_ASIGNADAS>();
        }
        /*
         * SE_GRUPO_TAREA
         * SE_TAREAS_ASIGNADAS
         * GRUPO_EXCLUYENTE
         * Auditoria
         */
        public List<Auditoria> lstAuditoria { get; set; }
        public List<GRUPO_EXCLUYENTE> lstGRUPO_EXCLUYENTE { get; set; }
        public List<SE_TAREAS_ASIGNADAS> lstSE_TAREAS_ASIGNADAS { get; set; }
        public List<SE_GRUPO_TAREA> lstSE_GRUPO_TAREA { get; set; }
    }
    [Serializable]
    public class Auditoria
    {

        private string sQLAuditarField;

        /// <comentarios/>
        public string SQLAuditar
        {
            get
            {
                return this.sQLAuditarField;
            }
            set
            {
                this.sQLAuditarField = value;
            }
        }
    }
    [Serializable]
    public class GRUPO_EXCLUYENTE
    {

        private int iDGRUPOField;

        private string dESCGRUPOField;

        /// <comentarios/>
        public int IDGRUPO
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
}

