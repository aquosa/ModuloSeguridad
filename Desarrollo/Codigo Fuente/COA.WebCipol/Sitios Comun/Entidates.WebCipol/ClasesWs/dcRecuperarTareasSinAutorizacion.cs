﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarTareasSinAutorizacion
{
    [Serializable]
    public class dcRecuperarTareasSinAutorizacion
    {
        /*
         * SE_TAREAS
         */
        public dcRecuperarTareasSinAutorizacion()
        {
            lstSE_TAREAS = new List<SE_TAREAS>();

        }

        public List<SE_TAREAS> lstSE_TAREAS { get; set; }
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
}

