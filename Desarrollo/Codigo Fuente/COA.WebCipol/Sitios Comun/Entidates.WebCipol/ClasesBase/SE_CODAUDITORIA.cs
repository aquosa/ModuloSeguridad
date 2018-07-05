using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_CODAUDITORIA : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecCODAUDITORIA;
        private decimal? _mdecCODAUDITORIA;
        public decimal? CODAUDITORIA
        {
            get { return mdecCODAUDITORIA; }
            set { mdecCODAUDITORIA = value; }
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
            _mdecCODAUDITORIA = mdecCODAUDITORIA;
            _mstrTEXTOAUDITORIA = mstrTEXTOAUDITORIA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecCODAUDITORIA = _mdecCODAUDITORIA;
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
        public class Sort : IComparer<SE_CODAUDITORIA>
        {
            public int Compare(SE_CODAUDITORIA x, SE_CODAUDITORIA y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
