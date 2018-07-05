using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_TERM_USUARIO : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDTERMINAL;
        private decimal? _mdecIDTERMINAL;
        public decimal? IDTERMINAL
        {
            get { return mdecIDTERMINAL; }
            set { mdecIDTERMINAL = value; }
        }

        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDTERMINAL = mdecIDTERMINAL;
            _mdecIDUSUARIO = mdecIDUSUARIO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDTERMINAL = _mdecIDTERMINAL;
                mdecIDUSUARIO = _mdecIDUSUARIO;

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
        public class Sort : IComparer<SE_TERM_USUARIO>
        {
            public int Compare(SE_TERM_USUARIO x, SE_TERM_USUARIO y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
