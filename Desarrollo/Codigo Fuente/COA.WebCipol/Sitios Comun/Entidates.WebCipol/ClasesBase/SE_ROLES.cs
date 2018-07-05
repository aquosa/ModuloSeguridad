using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_ROLES : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDROL;
        private decimal? _mdecIDROL;
        public decimal? IDROL
        {
            get { return mdecIDROL; }
            set { mdecIDROL = value; }
        }

        private string mstrDESCRIPCIONPERFIL = "";
        private string _mstrDESCRIPCIONPERFIL = "";
        public string DESCRIPCIONPERFIL
        {
            get { return mstrDESCRIPCIONPERFIL; }
            set { mstrDESCRIPCIONPERFIL = value; }
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
            _mdecIDROL = mdecIDROL;
            _mstrDESCRIPCIONPERFIL = mstrDESCRIPCIONPERFIL;
            _mdtmFECHAULTMODIF = mdtmFECHAULTMODIF;
            _mdecIDUSUARIOULTMODIF = mdecIDUSUARIOULTMODIF;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDROL = _mdecIDROL;
                mstrDESCRIPCIONPERFIL = _mstrDESCRIPCIONPERFIL;
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
        public class Sort : IComparer<SE_ROLES>
        {
            public int Compare(SE_ROLES x, SE_ROLES y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
