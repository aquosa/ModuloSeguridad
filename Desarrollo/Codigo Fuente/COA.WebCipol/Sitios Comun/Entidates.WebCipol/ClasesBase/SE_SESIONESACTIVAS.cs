using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_SESIONESACTIVAS : IEditableObject
    {
        private bool mblnEdit = false;
        private string mstrSTRUSUARIO = "";
        private string _mstrSTRUSUARIO = "";
        public string STRUSUARIO
        {
            get { return mstrSTRUSUARIO; }
            set { mstrSTRUSUARIO = value; }
        }

        private DateTime? mdtmINICIOTAREA;
        private DateTime? _mdtmINICIOTAREA;
        public DateTime? INICIOTAREA
        {
            get { return mdtmINICIOTAREA; }
            set { mdtmINICIOTAREA = value; }
        }

        private string mstrNOMBRENETBIOS = "";
        private string _mstrNOMBRENETBIOS = "";
        public string NOMBRENETBIOS
        {
            get { return mstrNOMBRENETBIOS; }
            set { mstrNOMBRENETBIOS = value; }
        }

        private string mstrCODIGO = "";
        private string _mstrCODIGO = "";
        public string CODIGO
        {
            get { return mstrCODIGO; }
            set { mstrCODIGO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mstrSTRUSUARIO = mstrSTRUSUARIO;
            _mdtmINICIOTAREA = mdtmINICIOTAREA;
            _mstrNOMBRENETBIOS = mstrNOMBRENETBIOS;
            _mstrCODIGO = mstrCODIGO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mstrSTRUSUARIO = _mstrSTRUSUARIO;
                mdtmINICIOTAREA = _mdtmINICIOTAREA;
                mstrNOMBRENETBIOS = _mstrNOMBRENETBIOS;
                mstrCODIGO = _mstrCODIGO;

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
        public class Sort : IComparer<SE_SESIONESACTIVAS>
        {
            public int Compare(SE_SESIONESACTIVAS x, SE_SESIONESACTIVAS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
