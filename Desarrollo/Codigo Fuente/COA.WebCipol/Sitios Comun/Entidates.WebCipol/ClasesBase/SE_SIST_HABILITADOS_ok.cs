using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_SIST_HABILITADOS_ok : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDSISTEMA;
        private decimal? _mdecIDSISTEMA;
        public decimal? IDSISTEMA
        {
            get { return mdecIDSISTEMA; }
            set { mdecIDSISTEMA = value; }
        }

        private string mstrCODSISTEMA = "";
        private string _mstrCODSISTEMA = "";
        public string CODSISTEMA
        {
            get { return mstrCODSISTEMA; }
            set { mstrCODSISTEMA = value; }
        }

        private string mstrDESCSISTEMA = "";
        private string _mstrDESCSISTEMA = "";
        public string DESCSISTEMA
        {
            get { return mstrDESCSISTEMA; }
            set { mstrDESCSISTEMA = value; }
        }

        private DateTime? mdtmFECHAHABILITACION;
        private DateTime? _mdtmFECHAHABILITACION;
        public DateTime? FECHAHABILITACION
        {
            get { return mdtmFECHAHABILITACION; }
            set { mdtmFECHAHABILITACION = value; }
        }

        private string mstrNOMBREEXEC = "";
        private string _mstrNOMBREEXEC = "";
        public string NOMBREEXEC
        {
            get { return mstrNOMBREEXEC; }
            set { mstrNOMBREEXEC = value; }
        }

        private string mstrSISTEMAHABILITADO = "";
        private string _mstrSISTEMAHABILITADO = "";
        public string SISTEMAHABILITADO
        {
            get { return mstrSISTEMAHABILITADO; }
            set { mstrSISTEMAHABILITADO = value; }
        }

        private string mstrICONO = "";
        private string _mstrICONO = "";
        public string ICONO
        {
            get { return mstrICONO; }
            set { mstrICONO = value; }
        }

        private string mstrOBSERVACIONES = "";
        private string _mstrOBSERVACIONES = "";
        public string OBSERVACIONES
        {
            get { return mstrOBSERVACIONES; }
            set { mstrOBSERVACIONES = value; }
        }

        private string mstrPAGINAPORDEFECTO = "";
        private string _mstrPAGINAPORDEFECTO = "";
        public string PAGINAPORDEFECTO
        {
            get { return mstrPAGINAPORDEFECTO; }
            set { mstrPAGINAPORDEFECTO = value; }
        }

        private string mstrDESCRIPCIONCORTA = "";
        private string _mstrDESCRIPCIONCORTA = "";
        public string DESCRIPCIONCORTA
        {
            get { return mstrDESCRIPCIONCORTA; }
            set { mstrDESCRIPCIONCORTA = value; }
        }

        private string mstrIMPACTACAJA = "";
        private string _mstrIMPACTACAJA = "";
        public string IMPACTACAJA
        {
            get { return mstrIMPACTACAJA; }
            set { mstrIMPACTACAJA = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDSISTEMA = mdecIDSISTEMA;
            _mstrCODSISTEMA = mstrCODSISTEMA;
            _mstrDESCSISTEMA = mstrDESCSISTEMA;
            _mdtmFECHAHABILITACION = mdtmFECHAHABILITACION;
            _mstrNOMBREEXEC = mstrNOMBREEXEC;
            _mstrSISTEMAHABILITADO = mstrSISTEMAHABILITADO;
            _mstrICONO = mstrICONO;
            _mstrOBSERVACIONES = mstrOBSERVACIONES;
            _mstrPAGINAPORDEFECTO = mstrPAGINAPORDEFECTO;
            _mstrDESCRIPCIONCORTA = mstrDESCRIPCIONCORTA;
            _mstrIMPACTACAJA = mstrIMPACTACAJA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDSISTEMA = _mdecIDSISTEMA;
                mstrCODSISTEMA = _mstrCODSISTEMA;
                mstrDESCSISTEMA = _mstrDESCSISTEMA;
                mdtmFECHAHABILITACION = _mdtmFECHAHABILITACION;
                mstrNOMBREEXEC = _mstrNOMBREEXEC;
                mstrSISTEMAHABILITADO = _mstrSISTEMAHABILITADO;
                mstrICONO = _mstrICONO;
                mstrOBSERVACIONES = _mstrOBSERVACIONES;
                mstrPAGINAPORDEFECTO = _mstrPAGINAPORDEFECTO;
                mstrDESCRIPCIONCORTA = _mstrDESCRIPCIONCORTA;
                mstrIMPACTACAJA = _mstrIMPACTACAJA;

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
        public class Sort : IComparer<SE_SIST_HABILITADOS_ok>
        {
            public int Compare(SE_SIST_HABILITADOS_ok x, SE_SIST_HABILITADOS_ok y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
