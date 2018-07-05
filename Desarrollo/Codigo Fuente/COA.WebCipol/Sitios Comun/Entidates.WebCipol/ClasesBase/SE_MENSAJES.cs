using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_MENSAJES : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecCODMENSAJE;
        private decimal? _mdecCODMENSAJE;
        public decimal? CODMENSAJE
        {
            get { return mdecCODMENSAJE; }
            set { mdecCODMENSAJE = value; }
        }

        private string mstrTEXTOMENSAJE = "";
        private string _mstrTEXTOMENSAJE = "";
        public string TEXTOMENSAJE
        {
            get { return mstrTEXTOMENSAJE; }
            set { mstrTEXTOMENSAJE = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecCODMENSAJE = mdecCODMENSAJE;
            _mstrTEXTOMENSAJE = mstrTEXTOMENSAJE;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecCODMENSAJE = _mdecCODMENSAJE;
                mstrTEXTOMENSAJE = _mstrTEXTOMENSAJE;

            }
        }
        void IEditableObject.EndEdit()
        {
            if (mblnEdit)
            {
                mblnEdit = false;
            }
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
        public class Sort : IComparer<SE_MENSAJES>
        {
            public int Compare(SE_MENSAJES x, SE_MENSAJES y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
