using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SIST_KDOCUMENTOS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTIPODOC;
        private decimal? _mdecIDTIPODOC;
        public decimal? IDTIPODOC
        {
            get { return mdecIDTIPODOC; }
            set { mdecIDTIPODOC = value; }
        }

        private string mstrTIPODOCUMENTO = "";
        private string _mstrTIPODOCUMENTO = "";
        public string TIPODOCUMENTO
        {
            get { return mstrTIPODOCUMENTO; }
            set { mstrTIPODOCUMENTO = value; }
        }

        private string mstrTIPOABREVIADO = "";
        private string _mstrTIPOABREVIADO = "";
        public string TIPOABREVIADO
        {
            get { return mstrTIPOABREVIADO; }
            set { mstrTIPOABREVIADO = value; }
        }

        private string mstrDOCBAJA = "";
        private string _mstrDOCBAJA = "";
        public string DOCBAJA
        {
            get { return mstrDOCBAJA; }
            set { mstrDOCBAJA = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTIPODOC = mdecIDTIPODOC;
            _mstrTIPODOCUMENTO = mstrTIPODOCUMENTO;
            _mstrTIPOABREVIADO = mstrTIPOABREVIADO;
            _mstrDOCBAJA = mstrDOCBAJA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTIPODOC = _mdecIDTIPODOC;
                mstrTIPODOCUMENTO = _mstrTIPODOCUMENTO;
                mstrTIPOABREVIADO = _mstrTIPOABREVIADO;
                mstrDOCBAJA = _mstrDOCBAJA;

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
        public class Sort : IComparer<SIST_KDOCUMENTOS>
        {
            public int Compare(SIST_KDOCUMENTOS x, SIST_KDOCUMENTOS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
