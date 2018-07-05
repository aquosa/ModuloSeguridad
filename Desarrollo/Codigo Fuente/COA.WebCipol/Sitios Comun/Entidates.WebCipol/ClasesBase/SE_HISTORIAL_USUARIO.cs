using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_HISTORIAL_USUARIO : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private decimal? mdecORDEN;
        private decimal? _mdecORDEN;
        public decimal? ORDEN
        {
            get { return mdecORDEN; }
            set { mdecORDEN = value; }
        }

        private string mstrSINONIMO = "";
        private string _mstrSINONIMO = "";
        public string SINONIMO
        {
            get { return mstrSINONIMO; }
            set { mstrSINONIMO = value; }
        }

        private DateTime? mdtmFECHAULTACT;
        private DateTime? _mdtmFECHAULTACT;
        public DateTime? FECHAULTACT
        {
            get { return mdtmFECHAULTACT; }
            set { mdtmFECHAULTACT = value; }
        }

        private DateTime? mdtmFECHAVENCIMIENTO;
        private DateTime? _mdtmFECHAVENCIMIENTO;
        public DateTime? FECHAVENCIMIENTO
        {
            get { return mdtmFECHAVENCIMIENTO; }
            set { mdtmFECHAVENCIMIENTO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mdecORDEN = mdecORDEN;
            _mstrSINONIMO = mstrSINONIMO;
            _mdtmFECHAULTACT = mdtmFECHAULTACT;
            _mdtmFECHAVENCIMIENTO = mdtmFECHAVENCIMIENTO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mdecORDEN = _mdecORDEN;
                mstrSINONIMO = _mstrSINONIMO;
                mdtmFECHAULTACT = _mdtmFECHAULTACT;
                mdtmFECHAVENCIMIENTO = _mdtmFECHAVENCIMIENTO;

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
        public class Sort : IComparer<SE_HISTORIAL_USUARIO>
        {
            public int Compare(SE_HISTORIAL_USUARIO x, SE_HISTORIAL_USUARIO y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
