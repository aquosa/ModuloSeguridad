using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_ATRIBUTOSTAREAS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTAREA;
        private decimal? _mdecIDTAREA;
        public decimal? IDTAREA
        {
            get { return mdecIDTAREA; }
            set { mdecIDTAREA = value; }
        }

        private decimal? mdecMOMENTO;
        private decimal? _mdecMOMENTO;
        public decimal? MOMENTO
        {
            get { return mdecMOMENTO; }
            set { mdecMOMENTO = value; }
        }

        private decimal? mdecTIPOMONTO;
        private decimal? _mdecTIPOMONTO;
        public decimal? TIPOMONTO
        {
            get { return mdecTIPOMONTO; }
            set { mdecTIPOMONTO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTAREA = mdecIDTAREA;
            _mdecMOMENTO = mdecMOMENTO;
            _mdecTIPOMONTO = mdecTIPOMONTO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTAREA = _mdecIDTAREA;
                mdecMOMENTO = _mdecMOMENTO;
                mdecTIPOMONTO = _mdecTIPOMONTO;

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
        public class Sort : IComparer<SE_ATRIBUTOSTAREAS>
        {
            public int Compare(SE_ATRIBUTOSTAREAS x, SE_ATRIBUTOSTAREAS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
