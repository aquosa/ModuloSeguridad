using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_USUARIOS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private string mstrUSUARIO = "";
        private string _mstrUSUARIO = "";
        public string USUARIO
        {
            get { return mstrUSUARIO; }
            set { mstrUSUARIO = value; }
        }

        private string mstrNOMBRES = "";
        private string _mstrNOMBRES = "";
        public string NOMBRES
        {
            get { return mstrNOMBRES; }
            set { mstrNOMBRES = value; }
        }

        private string mstrDOMICILIO = "";
        private string _mstrDOMICILIO = "";
        public string DOMICILIO
        {
            get { return mstrDOMICILIO; }
            set { mstrDOMICILIO = value; }
        }

        private DateTime? mdtmFECHAULTUSOCTA;
        private DateTime? _mdtmFECHAULTUSOCTA;
        public DateTime? FECHAULTUSOCTA
        {
            get { return mdtmFECHAULTUSOCTA; }
            set { mdtmFECHAULTUSOCTA = value; }
        }

        private string mstrNRODOCUMENTO = "";
        private string _mstrNRODOCUMENTO = "";
        public string NRODOCUMENTO
        {
            get { return mstrNRODOCUMENTO; }
            set { mstrNRODOCUMENTO = value; }
        }

        private string mstrCANTINTINVUSOCTA = "";
        private string _mstrCANTINTINVUSOCTA = "";
        public string CANTINTINVUSOCTA
        {
            get { return mstrCANTINTINVUSOCTA; }
            set { mstrCANTINTINVUSOCTA = value; }
        }

        private string mstrFORZARCAMBIO = "";
        private string _mstrFORZARCAMBIO = "";
        public string FORZARCAMBIO
        {
            get { return mstrFORZARCAMBIO; }
            set { mstrFORZARCAMBIO = value; }
        }

        private string mstrCTABLOQUEADA = "";
        private string _mstrCTABLOQUEADA = "";
        public string CTABLOQUEADA
        {
            get { return mstrCTABLOQUEADA; }
            set { mstrCTABLOQUEADA = value; }
        }

        private string mstrCOMENTARIO = "";
        private string _mstrCOMENTARIO = "";
        public string COMENTARIO
        {
            get { return mstrCOMENTARIO; }
            set { mstrCOMENTARIO = value; }
        }

        private DateTime? mdtmFECHABLOQUEO;
        private DateTime? _mdtmFECHABLOQUEO;
        public DateTime? FECHABLOQUEO
        {
            get { return mdtmFECHABLOQUEO; }
            set { mdtmFECHABLOQUEO = value; }
        }

        private DateTime? mdtmFECHAALTA;
        private DateTime? _mdtmFECHAALTA;
        public DateTime? FECHAALTA
        {
            get { return mdtmFECHAALTA; }
            set { mdtmFECHAALTA = value; }
        }

        private DateTime? mdtmFECHABAJA;
        private DateTime? _mdtmFECHABAJA;
        public DateTime? FECHABAJA
        {
            get { return mdtmFECHABAJA; }
            set { mdtmFECHABAJA = value; }
        }

        private decimal? mdecIDAREA;
        private decimal? _mdecIDAREA;
        public decimal? IDAREA
        {
            get { return mdecIDAREA; }
            set { mdecIDAREA = value; }
        }

        private decimal? mdecIDTIPODOC;
        private decimal? _mdecIDTIPODOC;
        public decimal? IDTIPODOC
        {
            get { return mdecIDTIPODOC; }
            set { mdecIDTIPODOC = value; }
        }

        private decimal? mdecULTIMOSISTEMA;
        private decimal? _mdecULTIMOSISTEMA;
        public decimal? ULTIMOSISTEMA
        {
            get { return mdecULTIMOSISTEMA; }
            set { mdecULTIMOSISTEMA = value; }
        }

        private string mstrALIAS_USUARIO = "";
        private string _mstrALIAS_USUARIO = "";
        public string ALIAS_USUARIO
        {
            get { return mstrALIAS_USUARIO; }
            set { mstrALIAS_USUARIO = value; }
        }

        private decimal? mdecIDUSRALTA;
        private decimal? _mdecIDUSRALTA;
        public decimal? IDUSRALTA
        {
            get { return mdecIDUSRALTA; }
            set { mdecIDUSRALTA = value; }
        }

        private decimal? mdecIDUSRBAJA;
        private decimal? _mdecIDUSRBAJA;
        public decimal? IDUSRBAJA
        {
            get { return mdecIDUSRBAJA; }
            set { mdecIDUSRBAJA = value; }
        }

        private string mstrEMAIL = "";
        private string _mstrEMAIL = "";
        public string EMAIL
        {
            get { return mstrEMAIL; }
            set { mstrEMAIL = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mstrUSUARIO = mstrUSUARIO;
            _mstrNOMBRES = mstrNOMBRES;
            _mstrDOMICILIO = mstrDOMICILIO;
            _mdtmFECHAULTUSOCTA = mdtmFECHAULTUSOCTA;
            _mstrNRODOCUMENTO = mstrNRODOCUMENTO;
            _mstrCANTINTINVUSOCTA = mstrCANTINTINVUSOCTA;
            _mstrFORZARCAMBIO = mstrFORZARCAMBIO;
            _mstrCTABLOQUEADA = mstrCTABLOQUEADA;
            _mstrCOMENTARIO = mstrCOMENTARIO;
            _mdtmFECHABLOQUEO = mdtmFECHABLOQUEO;
            _mdtmFECHAALTA = mdtmFECHAALTA;
            _mdtmFECHABAJA = mdtmFECHABAJA;
            _mdecIDAREA = mdecIDAREA;
            _mdecIDTIPODOC = mdecIDTIPODOC;
            _mdecULTIMOSISTEMA = mdecULTIMOSISTEMA;
            _mstrALIAS_USUARIO = mstrALIAS_USUARIO;
            _mdecIDUSRALTA = mdecIDUSRALTA;
            _mdecIDUSRBAJA = mdecIDUSRBAJA;
            _mstrEMAIL = mstrEMAIL;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mstrUSUARIO = _mstrUSUARIO;
                mstrNOMBRES = _mstrNOMBRES;
                mstrDOMICILIO = _mstrDOMICILIO;
                mdtmFECHAULTUSOCTA = _mdtmFECHAULTUSOCTA;
                mstrNRODOCUMENTO = _mstrNRODOCUMENTO;
                mstrCANTINTINVUSOCTA = _mstrCANTINTINVUSOCTA;
                mstrFORZARCAMBIO = _mstrFORZARCAMBIO;
                mstrCTABLOQUEADA = _mstrCTABLOQUEADA;
                mstrCOMENTARIO = _mstrCOMENTARIO;
                mdtmFECHABLOQUEO = _mdtmFECHABLOQUEO;
                mdtmFECHAALTA = _mdtmFECHAALTA;
                mdtmFECHABAJA = _mdtmFECHABAJA;
                mdecIDAREA = _mdecIDAREA;
                mdecIDTIPODOC = _mdecIDTIPODOC;
                mdecULTIMOSISTEMA = _mdecULTIMOSISTEMA;
                mstrALIAS_USUARIO = _mstrALIAS_USUARIO;
                mdecIDUSRALTA = _mdecIDUSRALTA;
                mdecIDUSRBAJA = _mdecIDUSRBAJA;
                mstrEMAIL = _mstrEMAIL;

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
        public class Sort : IComparer<SE_USUARIOS>
        {
            public int Compare(SE_USUARIOS x, SE_USUARIOS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
