using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SIST_KAREAS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDAREA;
        private decimal? _mdecIDAREA;
        public decimal? IDAREA
        {
            get { return mdecIDAREA; }
            set { mdecIDAREA = value; }
        }

        private string mstrNOMBREAREA = "";
        private string _mstrNOMBREAREA = "";
        public string NOMBREAREA
        {
            get { return mstrNOMBREAREA; }
            set { mstrNOMBREAREA = value; }
        }

        private string mstrRESPONSABLE = "";
        private string _mstrRESPONSABLE = "";
        public string RESPONSABLE
        {
            get { return mstrRESPONSABLE; }
            set { mstrRESPONSABLE = value; }
        }

        private string mstrCARGORESPONSABLE = "";
        private string _mstrCARGORESPONSABLE = "";
        public string CARGORESPONSABLE
        {
            get { return mstrCARGORESPONSABLE; }
            set { mstrCARGORESPONSABLE = value; }
        }

        private string mstrCOMENTARIOS = "";
        private string _mstrCOMENTARIOS = "";
        public string COMENTARIOS
        {
            get { return mstrCOMENTARIOS; }
            set { mstrCOMENTARIOS = value; }
        }

        //private string mstrBAJA = "";
        //private string _mstrBAJA = "";
        //public string BAJA
        //{
        //    get { return mstrBAJA; }
        //    set { mstrBAJA = value; }
        //}

        private string mstrFICTICIA = "";
        private string _mstrFICTICIA = "";
        public string FICTICIA
        {
            get { return mstrFICTICIA; }
            set { mstrFICTICIA = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDAREA = mdecIDAREA;
            _mstrNOMBREAREA = mstrNOMBREAREA;
            _mstrRESPONSABLE = mstrRESPONSABLE;
            _mstrCARGORESPONSABLE = mstrCARGORESPONSABLE;
            _mstrCOMENTARIOS = mstrCOMENTARIOS;
            //_mstrBAJA = mstrBAJA;
            _mstrFICTICIA = mstrFICTICIA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDAREA = _mdecIDAREA;
                mstrNOMBREAREA = _mstrNOMBREAREA;
                mstrRESPONSABLE = _mstrRESPONSABLE;
                mstrCARGORESPONSABLE = _mstrCARGORESPONSABLE;
                mstrCOMENTARIOS = _mstrCOMENTARIOS;
                //mstrBAJA = _mstrBAJA;
                mstrFICTICIA = _mstrFICTICIA;

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
        public class Sort : IComparer<SIST_KAREAS>
        {
            public int Compare(SIST_KAREAS x, SIST_KAREAS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
