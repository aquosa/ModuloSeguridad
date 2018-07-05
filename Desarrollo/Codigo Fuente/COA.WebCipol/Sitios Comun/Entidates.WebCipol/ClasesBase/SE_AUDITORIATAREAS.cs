using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_AUDITORIATAREAS : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTAREA;
        private decimal? _mdecIDTAREA;
        public decimal? IDTAREA
        {
            get { return mdecIDTAREA; }
            set { mdecIDTAREA = value; }
        }

        private string mstrUSUARIOACTUANTE = "";
        private string _mstrUSUARIOACTUANTE = "";
        public string USUARIOACTUANTE
        {
            get { return mstrUSUARIOACTUANTE; }
            set { mstrUSUARIOACTUANTE = value; }
        }

        private DateTime? mdtmFECHAHORALOG;
        private DateTime? _mdtmFECHAHORALOG;
        public DateTime? FECHAHORALOG
        {
            get { return mdtmFECHAHORALOG; }
            set { mdtmFECHAHORALOG = value; }
        }

        private string mstrTEXTOAUDITORIA = "";
        private string _mstrTEXTOAUDITORIA = "";
        public string TEXTOAUDITORIA
        {
            get { return mstrTEXTOAUDITORIA; }
            set { mstrTEXTOAUDITORIA = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTAREA = mdecIDTAREA;
            _mstrUSUARIOACTUANTE = mstrUSUARIOACTUANTE;
            _mdtmFECHAHORALOG = mdtmFECHAHORALOG;
            _mstrTEXTOAUDITORIA = mstrTEXTOAUDITORIA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTAREA = _mdecIDTAREA;
                mstrUSUARIOACTUANTE = _mstrUSUARIOACTUANTE;
                mdtmFECHAHORALOG = _mdtmFECHAHORALOG;
                mstrTEXTOAUDITORIA = _mstrTEXTOAUDITORIA;

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
        public class Sort : IComparer<SE_AUDITORIATAREAS>
        {
            public int Compare(SE_AUDITORIATAREAS x, SE_AUDITORIATAREAS y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
