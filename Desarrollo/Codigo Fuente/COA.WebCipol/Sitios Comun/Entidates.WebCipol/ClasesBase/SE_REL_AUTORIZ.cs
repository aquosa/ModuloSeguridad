using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_REL_AUTORIZ : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTAREAPRIMITIVA;
        private decimal? _mdecIDTAREAPRIMITIVA;
        public decimal? IDTAREAPRIMITIVA
        {
            get { return mdecIDTAREAPRIMITIVA; }
            set { mdecIDTAREAPRIMITIVA = value; }
        }

        private decimal? mdecIDTAREAAUTOR;
        private decimal? _mdecIDTAREAAUTOR;
        public decimal? IDTAREAAUTOR
        {
            get { return mdecIDTAREAAUTOR; }
            set { mdecIDTAREAAUTOR = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTAREAPRIMITIVA = mdecIDTAREAPRIMITIVA;
            _mdecIDTAREAAUTOR = mdecIDTAREAAUTOR;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTAREAPRIMITIVA = _mdecIDTAREAPRIMITIVA;
                mdecIDTAREAAUTOR = _mdecIDTAREAAUTOR;

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
        public class Sort : IComparer<SE_REL_AUTORIZ>
        {
            public int Compare(SE_REL_AUTORIZ x, SE_REL_AUTORIZ y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
