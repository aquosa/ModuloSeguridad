using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_GRUPO_TAREA : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDGRUPO;
        private decimal? _mdecIDGRUPO;
        public decimal? IDGRUPO
        {
            get { return mdecIDGRUPO; }
            set { mdecIDGRUPO = value; }
        }

        private string mstrDESCGRUPO = "";
        private string _mstrDESCGRUPO = "";
        public string DESCGRUPO
        {
            get { return mstrDESCGRUPO; }
            set { mstrDESCGRUPO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDGRUPO = mdecIDGRUPO;
            _mstrDESCGRUPO = mstrDESCGRUPO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDGRUPO = _mdecIDGRUPO;
                mstrDESCGRUPO = _mstrDESCGRUPO;

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
        public class Sort : IComparer<SE_GRUPO_TAREA>
        {
            public int Compare(SE_GRUPO_TAREA x, SE_GRUPO_TAREA y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
