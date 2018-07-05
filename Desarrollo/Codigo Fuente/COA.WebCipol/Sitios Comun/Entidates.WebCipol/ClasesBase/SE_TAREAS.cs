using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_TAREAS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTAREA;
        private decimal? _mdecIDTAREA;
        public decimal? IDTAREA
        {
            get { return mdecIDTAREA; }
            set { mdecIDTAREA = value; }
        }

        private string mstrCODIGOTAREA = "";
        private string _mstrCODIGOTAREA = "";
        public string CODIGOTAREA
        {
            get { return mstrCODIGOTAREA; }
            set { mstrCODIGOTAREA = value; }
        }

        private string mstrDESCRIPCIONTAREA = "";
        private string _mstrDESCRIPCIONTAREA = "";
        public string DESCRIPCIONTAREA
        {
            get { return mstrDESCRIPCIONTAREA; }
            set { mstrDESCRIPCIONTAREA = value; }
        }

        private string mstrREQUIEREAUDITORIA = "";
        private string _mstrREQUIEREAUDITORIA = "";
        public string REQUIEREAUDITORIA
        {
            get { return mstrREQUIEREAUDITORIA; }
            set { mstrREQUIEREAUDITORIA = value; }
        }

        private decimal? mdecIDAUTORIZACION;
        private decimal? _mdecIDAUTORIZACION;
        public decimal? IDAUTORIZACION
        {
            get { return mdecIDAUTORIZACION; }
            set { mdecIDAUTORIZACION = value; }
        }

        private decimal? mdecIDSISTEMA;
        private decimal? _mdecIDSISTEMA;
        public decimal? IDSISTEMA
        {
            get { return mdecIDSISTEMA; }
            set { mdecIDSISTEMA = value; }
        }

        private decimal? mdecIDGRUPO;
        private decimal? _mdecIDGRUPO;
        public decimal? IDGRUPO
        {
            get { return mdecIDGRUPO; }
            set { mdecIDGRUPO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTAREA = mdecIDTAREA;
            _mstrCODIGOTAREA = mstrCODIGOTAREA;
            _mstrDESCRIPCIONTAREA = mstrDESCRIPCIONTAREA;
            _mstrREQUIEREAUDITORIA = mstrREQUIEREAUDITORIA;
            _mdecIDAUTORIZACION = mdecIDAUTORIZACION;
            _mdecIDSISTEMA = mdecIDSISTEMA;
            _mdecIDGRUPO = mdecIDGRUPO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTAREA = _mdecIDTAREA;
                mstrCODIGOTAREA = _mstrCODIGOTAREA;
                mstrDESCRIPCIONTAREA = _mstrDESCRIPCIONTAREA;
                mstrREQUIEREAUDITORIA = _mstrREQUIEREAUDITORIA;
                mdecIDAUTORIZACION = _mdecIDAUTORIZACION;
                mdecIDSISTEMA = _mdecIDSISTEMA;
                mdecIDGRUPO = _mdecIDGRUPO;

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
        public class Sort : IComparer<SE_TAREAS>
        {
            public int Compare(SE_TAREAS x, SE_TAREAS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
