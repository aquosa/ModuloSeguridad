using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperaTerminales
{
    [Serializable]
    public class dcRecuperaTerminales
    {
        public dcRecuperaTerminales()
        {
            lstTerminales = new List<SE_TERMINALES>();
        }
        public List<SE_TERMINALES> lstTerminales { get; set; }
    }
    [Serializable]
    public class SE_TERMINALES
    {

        private string cODTERMINALField;

        /// <comentarios/>
        public string CODTERMINAL
        {
            get
            {
                return this.cODTERMINALField;
            }
            set
            {
                this.cODTERMINALField = value;
            }
        }
    }
}
