using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcEliminarSesionActiva
{
    [Serializable]
    public class dcEliminarSesionActiva
    {
        public dcEliminarSesionActiva()
        {
            lstSist_sesionesactivas = new List<Sist_sesionesactivas>();
        }
        
        
        public List<Sist_sesionesactivas> lstSist_sesionesactivas {get;set;}
    }
    
    [Serializable]
    public class Sist_sesionesactivas
    {
        private string usuarioField;

        private string terminalField;

        /// <comentarios/>
        public string Usuario
        {
            get
            {
                return this.usuarioField;
            }
            set
            {
                this.usuarioField = value;
            }
        }

        /// <comentarios/>
        public string Terminal
        {
            get
            {
                return this.terminalField;
            }
            set
            {
                this.terminalField = value;
            }
        }

    }
}
