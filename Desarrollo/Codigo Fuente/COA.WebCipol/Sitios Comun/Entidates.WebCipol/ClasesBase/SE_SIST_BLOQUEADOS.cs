using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_SIST_BLOQUEADOS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private decimal? mdecIDSISTEMA;
        private decimal? _mdecIDSISTEMA;
        public decimal? IDSISTEMA
        {
            get { return mdecIDSISTEMA; }
            set { mdecIDSISTEMA = value; }
        }

        private DateTime? mdtmFECHAULTMODIF;
        private DateTime? _mdtmFECHAULTMODIF;
        public DateTime? FECHAULTMODIF
        {
            get { return mdtmFECHAULTMODIF; }
            set { mdtmFECHAULTMODIF = value; }
        }

        private decimal? mdecIDUSUARIOULTMODIF;
        private decimal? _mdecIDUSUARIOULTMODIF;
        public decimal? IDUSUARIOULTMODIF
        {
            get { return mdecIDUSUARIOULTMODIF; }
            set { mdecIDUSUARIOULTMODIF = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mdecIDSISTEMA = mdecIDSISTEMA;
            _mdtmFECHAULTMODIF = mdtmFECHAULTMODIF;
            _mdecIDUSUARIOULTMODIF = mdecIDUSUARIOULTMODIF;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mdecIDSISTEMA = _mdecIDSISTEMA;
                mdtmFECHAULTMODIF = _mdtmFECHAULTMODIF;
                mdecIDUSUARIOULTMODIF = _mdecIDUSUARIOULTMODIF;

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
        public class Sort : IComparer<SE_SIST_BLOQUEADOS>
        {
            public int Compare(SE_SIST_BLOQUEADOS x, SE_SIST_BLOQUEADOS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
