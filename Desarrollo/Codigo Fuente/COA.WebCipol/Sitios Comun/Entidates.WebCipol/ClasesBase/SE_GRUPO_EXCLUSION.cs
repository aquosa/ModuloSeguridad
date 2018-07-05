using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_GRUPO_EXCLUSION : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDGRUPOACTUAL;
        private decimal? _mdecIDGRUPOACTUAL;
        public decimal? IDGRUPOACTUAL
        {
            get { return mdecIDGRUPOACTUAL; }
            set { mdecIDGRUPOACTUAL = value; }
        }

        private decimal? mdecIDGRUPEXCLUYENTE;
        private decimal? _mdecIDGRUPEXCLUYENTE;
        public decimal? IDGRUPEXCLUYENTE
        {
            get { return mdecIDGRUPEXCLUYENTE; }
            set { mdecIDGRUPEXCLUYENTE = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDGRUPOACTUAL = mdecIDGRUPOACTUAL;
            _mdecIDGRUPEXCLUYENTE = mdecIDGRUPEXCLUYENTE;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDGRUPOACTUAL = _mdecIDGRUPOACTUAL;
                mdecIDGRUPEXCLUYENTE = _mdecIDGRUPEXCLUYENTE;

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
        public class Sort : IComparer<SE_GRUPO_EXCLUSION>
        {
            public int Compare(SE_GRUPO_EXCLUSION x, SE_GRUPO_EXCLUSION y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
