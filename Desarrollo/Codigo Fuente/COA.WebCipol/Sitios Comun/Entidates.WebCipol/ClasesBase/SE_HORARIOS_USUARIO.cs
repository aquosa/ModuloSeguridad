using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_HORARIOS_USUARIO : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDDIA;
        private decimal? _mdecIDDIA;
        public decimal? IDDIA
        {
            get { return mdecIDDIA; }
            set { mdecIDDIA = value; }
        }

        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private decimal? mdecIDHORARIO;
        private decimal? _mdecIDHORARIO;
        public decimal? IDHORARIO
        {
            get { return mdecIDHORARIO; }
            set { mdecIDHORARIO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDDIA = mdecIDDIA;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mdecIDHORARIO = mdecIDHORARIO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDDIA = _mdecIDDIA;
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mdecIDHORARIO = _mdecIDHORARIO;

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
        public class Sort : IComparer<SE_HORARIOS_USUARIO>
        {
            public int Compare(SE_HORARIOS_USUARIO x, SE_HORARIOS_USUARIO y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
