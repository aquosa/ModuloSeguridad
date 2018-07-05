using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_TERMINALES : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTERMINAL;
        private decimal? _mdecIDTERMINAL;
        public decimal? IDTERMINAL
        {
            get { return mdecIDTERMINAL; }
            set { mdecIDTERMINAL = value; }
        }

        private string mstrCODTERMINAL = "";
        private string _mstrCODTERMINAL = "";
        public string CODTERMINAL
        {
            get { return mstrCODTERMINAL; }
            set { mstrCODTERMINAL = value; }
        }

        private string mstrNOMBRENETBIOS = "";
        private string _mstrNOMBRENETBIOS = "";
        public string NOMBRENETBIOS
        {
            get { return mstrNOMBRENETBIOS; }
            set { mstrNOMBRENETBIOS = value; }
        }

        private string mstrUSOHABILITADO = "";
        private string _mstrUSOHABILITADO = "";
        public string USOHABILITADO
        {
            get { return mstrUSOHABILITADO; }
            set { mstrUSOHABILITADO = value; }
        }

        private string mstrMODELOPROCESADOR = "";
        private string _mstrMODELOPROCESADOR = "";
        public string MODELOPROCESADOR
        {
            get { return mstrMODELOPROCESADOR; }
            set { mstrMODELOPROCESADOR = value; }
        }

        private long? mdecCANTMEMORIARAM;
        private long? _mdecCANTMEMORIARAM;
        public long? CANTMEMORIARAM
        {
            get { return mdecCANTMEMORIARAM; }
            set { mdecCANTMEMORIARAM = value; }
        }

        private long? mdecTAMANIODISCO;
        private long? _mdecTAMANIODISCO;
        public long? TAMANIODISCO
        {
            get { return mdecTAMANIODISCO; }
            set { mdecTAMANIODISCO = value; }
        }

        private string mstrMODELOMONITOR = "";
        private string _mstrMODELOMONITOR = "";
        public string MODELOMONITOR
        {
            get { return mstrMODELOMONITOR; }
            set { mstrMODELOMONITOR = value; }
        }

        private string mstrMODELOACELVIDEO = "";
        private string _mstrMODELOACELVIDEO = "";
        public string MODELOACELVIDEO
        {
            get { return mstrMODELOACELVIDEO; }
            set { mstrMODELOACELVIDEO = value; }
        }

        private string mstrDESCADICIONAL = "";
        private string _mstrDESCADICIONAL = "";
        public string DESCADICIONAL
        {
            get { return mstrDESCADICIONAL; }
            set { mstrDESCADICIONAL = value; }
        }

        private decimal? mdecIDAREA;
        private decimal? _mdecIDAREA;
        public decimal? IDAREA
        {
            get { return mdecIDAREA; }
            set { mdecIDAREA = value; }
        }

        private string mstrORIGENACTUALIZACION = "";
        private string _mstrORIGENACTUALIZACION = "";
        public string ORIGENACTUALIZACION
        {
            get { return mstrORIGENACTUALIZACION; }
            set { mstrORIGENACTUALIZACION = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTERMINAL = mdecIDTERMINAL;
            _mstrCODTERMINAL = mstrCODTERMINAL;
            _mstrNOMBRENETBIOS = mstrNOMBRENETBIOS;
            _mstrUSOHABILITADO = mstrUSOHABILITADO;
            _mstrMODELOPROCESADOR = mstrMODELOPROCESADOR;
            _mdecCANTMEMORIARAM = mdecCANTMEMORIARAM;
            _mdecTAMANIODISCO = mdecTAMANIODISCO;
            _mstrMODELOMONITOR = mstrMODELOMONITOR;
            _mstrMODELOACELVIDEO = mstrMODELOACELVIDEO;
            _mstrDESCADICIONAL = mstrDESCADICIONAL;
            _mdecIDAREA = mdecIDAREA;
            _mstrORIGENACTUALIZACION = mstrORIGENACTUALIZACION;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTERMINAL = _mdecIDTERMINAL;
                mstrCODTERMINAL = _mstrCODTERMINAL;
                mstrNOMBRENETBIOS = _mstrNOMBRENETBIOS;
                mstrUSOHABILITADO = _mstrUSOHABILITADO;
                mstrMODELOPROCESADOR = _mstrMODELOPROCESADOR;
                mdecCANTMEMORIARAM = _mdecCANTMEMORIARAM;
                mdecTAMANIODISCO = _mdecTAMANIODISCO;
                mstrMODELOMONITOR = _mstrMODELOMONITOR;
                mstrMODELOACELVIDEO = _mstrMODELOACELVIDEO;
                mstrDESCADICIONAL = _mstrDESCADICIONAL;
                mdecIDAREA = _mdecIDAREA;
                mstrORIGENACTUALIZACION = _mstrORIGENACTUALIZACION;

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
        public class Sort : IComparer<SE_TERMINALES>
        {
            public int Compare(SE_TERMINALES x, SE_TERMINALES y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
