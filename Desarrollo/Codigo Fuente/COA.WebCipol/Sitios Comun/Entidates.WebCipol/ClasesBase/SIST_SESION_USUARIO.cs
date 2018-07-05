using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SIST_SESION_USUARIO : IEditableObject
    {
        private bool mblnEdit = false;
        private decimal? mdecIDUSUARIO;
        private decimal? _mdecIDUSUARIO;
        public decimal? IDUSUARIO
        {
            get { return mdecIDUSUARIO; }
            set { mdecIDUSUARIO = value; }
        }

        private decimal? mdecIDSESION;
        private decimal? _mdecIDSESION;
        public decimal? IDSESION
        {
            get { return mdecIDSESION; }
            set { mdecIDSESION = value; }
        }

        private DateTime? mdtmFECHAHORA;
        private DateTime? _mdtmFECHAHORA;
        public DateTime? FECHAHORA
        {
            get { return mdtmFECHAHORA; }
            set { mdtmFECHAHORA = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdecIDUSUARIO = mdecIDUSUARIO;
            _mdecIDSESION = mdecIDSESION;
            _mdtmFECHAHORA = mdtmFECHAHORA;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdecIDUSUARIO = _mdecIDUSUARIO;
                mdecIDSESION = _mdecIDSESION;
                mdtmFECHAHORA = _mdtmFECHAHORA;

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
        public class Sort : IComparer<SIST_SESION_USUARIO>
        {
            public int Compare(SIST_SESION_USUARIO x, SIST_SESION_USUARIO y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
