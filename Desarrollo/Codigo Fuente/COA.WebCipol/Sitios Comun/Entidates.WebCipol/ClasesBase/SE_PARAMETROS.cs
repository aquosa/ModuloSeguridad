using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_PARAMETROS 
    {
        private string mstrCOLUMNA1 = "";
        public string COLUMNA1
        {
            get { return mstrCOLUMNA1; }
            set { mstrCOLUMNA1 = value; }
        }

        private string mstrCOLUMNA2 = "";
        public string COLUMNA2
        {
            get { return mstrCOLUMNA2; }
            set { mstrCOLUMNA2 = value; }
        }

        private string mstrCOLUMNA3 = "";
        public string COLUMNA3
        {
            get { return mstrCOLUMNA3; }
            set { mstrCOLUMNA3 = value; }
        }

        private string mstrCOLUMNA4 = "";
        public string COLUMNA4
        {
            get { return mstrCOLUMNA4; }
            set { mstrCOLUMNA4 = value; }
        }

        private string mstrCOLUMNA5 = "";
        public string COLUMNA5
        {
            get { return mstrCOLUMNA5; }
            set { mstrCOLUMNA5 = value; }
        }

        private object mobjauxiliar;
        /// <summary>
        /// Propiedad utilizada para fines generales. Ej: al realizar un ABM poder enviar un mensaje del servidor al cliente
        /// </summary>
        public object Auxiliar
        {
            get { return mobjauxiliar; }
            set { mobjauxiliar = value; }
        }
        /// <summary>
        /// Clase que puede ser utilizada por el método List<T>.Sort() de tal manera de enlazar la lista a una grilla en forma ordenada
        /// </summary>
        public class Sort : IComparer<SE_PARAMETROS>
        {
            public int Compare(SE_PARAMETROS x, SE_PARAMETROS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
