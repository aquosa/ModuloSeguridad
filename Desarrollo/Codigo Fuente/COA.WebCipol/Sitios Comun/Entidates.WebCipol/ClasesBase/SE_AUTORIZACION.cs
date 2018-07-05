using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_AUTORIZACION : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDAUTORIZACION;
        private decimal? _mdecIDAUTORIZACION;
        public decimal? IDAUTORIZACION
        {
            get { return mdecIDAUTORIZACION; }
            set { mdecIDAUTORIZACION = value; }
        }

        private string mstrCODAUTORIZ = "";
        private string _mstrCODAUTORIZ = "";
        public string CODAUTORIZ
        {
            get { return mstrCODAUTORIZ; }
            set { mstrCODAUTORIZ = value; }
        }

        private string mstrDESCAUTORIZ = "";
        private string _mstrDESCAUTORIZ = "";
        public string DESCAUTORIZ
        {
            get { return mstrDESCAUTORIZ; }
            set { mstrDESCAUTORIZ = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDAUTORIZACION = mdecIDAUTORIZACION;
            _mstrCODAUTORIZ = mstrCODAUTORIZ;
            _mstrDESCAUTORIZ = mstrDESCAUTORIZ;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDAUTORIZACION = _mdecIDAUTORIZACION;
                mstrCODAUTORIZ = _mstrCODAUTORIZ;
                mstrDESCAUTORIZ = _mstrDESCAUTORIZ;

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
        public class Sort : IComparer<SE_AUTORIZACION>
        {
            public int Compare(SE_AUTORIZACION x, SE_AUTORIZACION y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
