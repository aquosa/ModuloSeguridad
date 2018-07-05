using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_SIST_EVENTOS : IEditableObject
    {
        private bool mblnEdit = false;
        private DateTime? mdtmFECHAHORALOG;
        private DateTime? _mdtmFECHAHORALOG;
        public DateTime? FECHAHORALOG
        {
            get { return mdtmFECHAHORALOG; }
            set { mdtmFECHAHORALOG = value; }
        }

        private string mstrOPERACION = "";
        private string _mstrOPERACION = "";
        public string OPERACION
        {
            get { return mstrOPERACION; }
            set { mstrOPERACION = value; }
        }

        private string mstrTABLA = "";
        private string _mstrTABLA = "";
        public string TABLA
        {
            get { return mstrTABLA; }
            set { mstrTABLA = value; }
        }

        private string mstrSTRINGSQL = "";
        private string _mstrSTRINGSQL = "";
        public string STRINGSQL
        {
            get { return mstrSTRINGSQL; }
            set { mstrSTRINGSQL = value; }
        }

        private string mstrUSUARIO = "";
        private string _mstrUSUARIO = "";
        public string USUARIO
        {
            get { return mstrUSUARIO; }
            set { mstrUSUARIO = value; }
        }

        private string mstrSUPERVISOR = "";
        private string _mstrSUPERVISOR = "";
        public string SUPERVISOR
        {
            get { return mstrSUPERVISOR; }
            set { mstrSUPERVISOR = value; }
        }

        private string mstrNOMBREPC = "";
        private string _mstrNOMBREPC = "";
        public string NOMBREPC
        {
            get { return mstrNOMBREPC; }
            set { mstrNOMBREPC = value; }
        }

        private string mstrSISTEMA = "";
        private string _mstrSISTEMA = "";
        public string SISTEMA
        {
            get { return mstrSISTEMA; }
            set { mstrSISTEMA = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdtmFECHAHORALOG = mdtmFECHAHORALOG;
            _mstrOPERACION = mstrOPERACION;
            _mstrTABLA = mstrTABLA;
            _mstrSTRINGSQL = mstrSTRINGSQL;
            _mstrUSUARIO = mstrUSUARIO;
            _mstrSUPERVISOR = mstrSUPERVISOR;
            _mstrNOMBREPC = mstrNOMBREPC;
            _mstrSISTEMA = mstrSISTEMA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdtmFECHAHORALOG = _mdtmFECHAHORALOG;
                mstrOPERACION = _mstrOPERACION;
                mstrTABLA = _mstrTABLA;
                mstrSTRINGSQL = _mstrSTRINGSQL;
                mstrUSUARIO = _mstrUSUARIO;
                mstrSUPERVISOR = _mstrSUPERVISOR;
                mstrNOMBREPC = _mstrNOMBREPC;
                mstrSISTEMA = _mstrSISTEMA;

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
        public class Sort : IComparer<SE_SIST_EVENTOS>
        {
            public int Compare(SE_SIST_EVENTOS x, SE_SIST_EVENTOS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
