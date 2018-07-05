using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_COMP_ROLES : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTAREA;
        private decimal? _mdecIDTAREA;
        public decimal? IDTAREA
        {
            get { return mdecIDTAREA; }
            set { mdecIDTAREA = value; }
        }

        private decimal? mdecIDROL;
        private decimal? _mdecIDROL;
        public decimal? IDROL
        {
            get { return mdecIDROL; }
            set { mdecIDROL = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTAREA = mdecIDTAREA;
            _mdecIDROL = mdecIDROL;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTAREA = _mdecIDTAREA;
                mdecIDROL = _mdecIDROL;

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
        public class Sort : IComparer<SE_COMP_ROLES>
        {
            public int Compare(SE_COMP_ROLES x, SE_COMP_ROLES y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
