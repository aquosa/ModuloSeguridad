using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_TAREAS_USUARIO : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTAREA;
        private decimal? _mdecIDTAREA;
        public decimal? IDTAREA
        {
            get { return mdecIDTAREA; }
            set { mdecIDTAREA = value; }
        }

        private decimal? mdecIDROL;
        private decimal? _mdecIDROL;
        public decimal? IDROL
        {
            get { return mdecIDROL; }
            set { mdecIDROL = value; }
        }

        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private decimal? mdecCANTIDADAUTORIZADA;
        private decimal? _mdecCANTIDADAUTORIZADA;
        public decimal? CANTIDADAUTORIZADA
        {
            get { return mdecCANTIDADAUTORIZADA; }
            set { mdecCANTIDADAUTORIZADA = value; }
        }

        private string mstrTAREAINHIBIDA = "";
        private string _mstrTAREAINHIBIDA = "";
        public string TAREAINHIBIDA
        {
            get { return mstrTAREAINHIBIDA; }
            set { mstrTAREAINHIBIDA = value; }
        }

        private DateTime? mdtmFECHAULTMODIF;
        private DateTime? _mdtmFECHAULTMODIF;
        public DateTime? FECHAULTMODIF
        {
            get { return mdtmFECHAULTMODIF; }
            set { mdtmFECHAULTMODIF = value; }
        }

        private decimal? mdecIDUSUARIOULTMODIF;
        private decimal? _mdecIDUSUARIOULTMODIF;
        public decimal? IDUSUARIOULTMODIF
        {
            get { return mdecIDUSUARIOULTMODIF; }
            set { mdecIDUSUARIOULTMODIF = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTAREA = mdecIDTAREA;
            _mdecIDROL = mdecIDROL;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mdecCANTIDADAUTORIZADA = mdecCANTIDADAUTORIZADA;
            _mstrTAREAINHIBIDA = mstrTAREAINHIBIDA;
            _mdtmFECHAULTMODIF = mdtmFECHAULTMODIF;
            _mdecIDUSUARIOULTMODIF = mdecIDUSUARIOULTMODIF;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTAREA = _mdecIDTAREA;
                mdecIDROL = _mdecIDROL;
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mdecCANTIDADAUTORIZADA = _mdecCANTIDADAUTORIZADA;
                mstrTAREAINHIBIDA = _mstrTAREAINHIBIDA;
                mdtmFECHAULTMODIF = _mdtmFECHAULTMODIF;
                mdecIDUSUARIOULTMODIF = _mdecIDUSUARIOULTMODIF;

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
        public class Sort : IComparer<SE_TAREAS_USUARIO>
        {
            public int Compare(SE_TAREAS_USUARIO x, SE_TAREAS_USUARIO y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
