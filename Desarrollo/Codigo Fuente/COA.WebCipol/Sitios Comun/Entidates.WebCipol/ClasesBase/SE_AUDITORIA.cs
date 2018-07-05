using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_AUDITORIA : IEditableObject
    {
        private bool mblnEdit = false;
        private DateTime? mdtmFECHAHORALOG;
        private DateTime? _mdtmFECHAHORALOG;
        public DateTime? FECHAHORALOG
        {
            get { return mdtmFECHAHORALOG; }
            set { mdtmFECHAHORALOG = value; }
        }

        private decimal? mdecCODMENSAJE;
        private decimal? _mdecCODMENSAJE;
        public decimal? CODMENSAJE
        {
            get { return mdecCODMENSAJE; }
            set { mdecCODMENSAJE = value; }
        }

        private string mstrTEXTOMENSAJE = "";
        private string _mstrTEXTOMENSAJE = "";
        public string TEXTOMENSAJE
        {
            get { return mstrTEXTOMENSAJE; }
            set { mstrTEXTOMENSAJE = value; }
        }

        private string mstrUSUARIOACTUANTE = "";
        private string _mstrUSUARIOACTUANTE = "";
        public string USUARIOACTUANTE
        {
            get { return mstrUSUARIOACTUANTE; }
            set { mstrUSUARIOACTUANTE = value; }
        }

        private string mstrUSUARIOAFECTADO = "";
        private string _mstrUSUARIOAFECTADO = "";
        public string USUARIOAFECTADO
        {
            get { return mstrUSUARIOAFECTADO; }
            set { mstrUSUARIOAFECTADO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdtmFECHAHORALOG = mdtmFECHAHORALOG;
            _mdecCODMENSAJE = mdecCODMENSAJE;
            _mstrTEXTOMENSAJE = mstrTEXTOMENSAJE;
            _mstrUSUARIOACTUANTE = mstrUSUARIOACTUANTE;
            _mstrUSUARIOAFECTADO = mstrUSUARIOAFECTADO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdtmFECHAHORALOG = _mdtmFECHAHORALOG;
                mdecCODMENSAJE = _mdecCODMENSAJE;
                mstrTEXTOMENSAJE = _mstrTEXTOMENSAJE;
                mstrUSUARIOACTUANTE = _mstrUSUARIOACTUANTE;
                mstrUSUARIOAFECTADO = _mstrUSUARIOAFECTADO;

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
        public class Sort : IComparer<SE_AUDITORIA>
        {
            public int Compare(SE_AUDITORIA x, SE_AUDITORIA y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
