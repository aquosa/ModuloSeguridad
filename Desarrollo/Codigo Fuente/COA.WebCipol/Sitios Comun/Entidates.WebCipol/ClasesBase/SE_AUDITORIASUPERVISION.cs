using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace COA.WebCipol.Entidades
{
    [System.Serializable()]
    public class SE_AUDITORIASUPERVISION : IEditableObject
    {
        private bool mblnEdit = false;
        private DateTime? mdtmFechaHoraLog;
        private DateTime? _mdtmFechaHoraLog;
        public DateTime? FechaHoraLog
        {
            get { return mdtmFechaHoraLog; }
            set { mdtmFechaHoraLog = value; }
        }

        private decimal? mdecIDUsuario_Supervisor;
        private decimal? _mdecIDUsuario_Supervisor;
        public decimal? IDUsuario_Supervisor
        {
            get { return mdecIDUsuario_Supervisor; }
            set { mdecIDUsuario_Supervisor = value; }
        }

        private decimal? mdecIDUsuario_Supervisado;
        private decimal? _mdecIDUsuario_Supervisado;
        public decimal? IDUsuario_Supervisado
        {
            get { return mdecIDUsuario_Supervisado; }
            set { mdecIDUsuario_Supervisado = value; }
        }

        private decimal? mdecIDTarea;
        private decimal? _mdecIDTarea;
        public decimal? IDTarea
        {
            get { return mdecIDTarea; }
            set { mdecIDTarea = value; }
        }

        private string mstrTerminal = "";
        private string _mstrTerminal = "";
        public string Terminal
        {
            get { return mstrTerminal; }
            set { mstrTerminal = value; }
        }

        void IEditableObject.BeginEdit()
        {
            mblnEdit = true;
            _mdtmFechaHoraLog = mdtmFechaHoraLog;
            _mdecIDUsuario_Supervisor = mdecIDUsuario_Supervisor;
            _mdecIDUsuario_Supervisado = mdecIDUsuario_Supervisado;
            _mdecIDTarea = mdecIDTarea;
            _mstrTerminal = mstrTerminal;

        }
        void IEditableObject.CancelEdit()
        {
            if (mblnEdit)
            {
                mdtmFechaHoraLog = _mdtmFechaHoraLog;
                mdecIDUsuario_Supervisor = _mdecIDUsuario_Supervisor;
                mdecIDUsuario_Supervisado = _mdecIDUsuario_Supervisado;
                mdecIDTarea = _mdecIDTarea;
                mstrTerminal = _mstrTerminal;

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
        public class Sort : IComparer<SE_AUDITORIASUPERVISION>
        {
            public int Compare(SE_AUDITORIASUPERVISION x, SE_AUDITORIASUPERVISION y)
            {
                throw new NotImplementedException();
            }
        }
    }
}
