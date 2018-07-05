using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_INSTANCIAUSUARIO : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private string mstrOBJETO = "";
        private string _mstrOBJETO = "";
        public string OBJETO
        {
            get { return mstrOBJETO; }
            set { mstrOBJETO = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mstrOBJETO = mstrOBJETO;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mstrOBJETO = _mstrOBJETO;

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
        public class Sort : IComparer<SE_INSTANCIAUSUARIO>
        {
            public int Compare(SE_INSTANCIAUSUARIO x, SE_INSTANCIAUSUARIO y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
