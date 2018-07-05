using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_MENUES : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDSISTEMAS;
        private decimal? _mdecIDSISTEMAS;
        public decimal? IDSISTEMAS
        {
            get { return mdecIDSISTEMAS; }
            set { mdecIDSISTEMAS = value; }
        }

        private decimal? mdecIDITEMMENU;
        private decimal? _mdecIDITEMMENU;
        public decimal? IDITEMMENU
        {
            get { return mdecIDITEMMENU; }
            set { mdecIDITEMMENU = value; }
        }

        private string mstrTEXTOITEM = "";
        private string _mstrTEXTOITEM = "";
        public string TEXTOITEM
        {
            get { return mstrTEXTOITEM; }
            set { mstrTEXTOITEM = value; }
        }

        private string mstrURL = "";
        private string _mstrURL = "";
        public string URL
        {
            get { return mstrURL; }
            set { mstrURL = value; }
        }

        private decimal? mdecIDITEMPADRE;
        private decimal? _mdecIDITEMPADRE;
        public decimal? IDITEMPADRE
        {
            get { return mdecIDITEMPADRE; }
            set { mdecIDITEMPADRE = value; }
        }

        private string mstrESPADRE = "";
        private string _mstrESPADRE = "";
        public string ESPADRE
        {
            get { return mstrESPADRE; }
            set { mstrESPADRE = value; }
        }

        private string mstrDESCRIPCION = "";
        private string _mstrDESCRIPCION = "";
        public string DESCRIPCION
        {
            get { return mstrDESCRIPCION; }
            set { mstrDESCRIPCION = value; }
        }

        private decimal? mdecIDTAREA;
        private decimal? _mdecIDTAREA;
        public decimal? IDTAREA
        {
            get { return mdecIDTAREA; }
            set { mdecIDTAREA = value; }
        }

        private decimal? mdecORDENSUBMENU;
        private decimal? _mdecORDENSUBMENU;
        public decimal? ORDENSUBMENU
        {
            get { return mdecORDENSUBMENU; }
            set { mdecORDENSUBMENU = value; }
        }

        private string mstrACCESODIRECTO = "";
        private string _mstrACCESODIRECTO = "";
        public string ACCESODIRECTO
        {
            get { return mstrACCESODIRECTO; }
            set { mstrACCESODIRECTO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDSISTEMAS = mdecIDSISTEMAS;
            _mdecIDITEMMENU = mdecIDITEMMENU;
            _mstrTEXTOITEM = mstrTEXTOITEM;
            _mstrURL = mstrURL;
            _mdecIDITEMPADRE = mdecIDITEMPADRE;
            _mstrESPADRE = mstrESPADRE;
            _mstrDESCRIPCION = mstrDESCRIPCION;
            _mdecIDTAREA = mdecIDTAREA;
            _mdecORDENSUBMENU = mdecORDENSUBMENU;
            _mstrACCESODIRECTO = mstrACCESODIRECTO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDSISTEMAS = _mdecIDSISTEMAS;
                mdecIDITEMMENU = _mdecIDITEMMENU;
                mstrTEXTOITEM = _mstrTEXTOITEM;
                mstrURL = _mstrURL;
                mdecIDITEMPADRE = _mdecIDITEMPADRE;
                mstrESPADRE = _mstrESPADRE;
                mstrDESCRIPCION = _mstrDESCRIPCION;
                mdecIDTAREA = _mdecIDTAREA;
                mdecORDENSUBMENU = _mdecORDENSUBMENU;
                mstrACCESODIRECTO = _mstrACCESODIRECTO;

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
        public class Sort : IComparer<SE_MENUES>
        {
            public int Compare(SE_MENUES x, SE_MENUES y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
