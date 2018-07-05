using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperaTablasDeSistemas
{
    [Serializable]
    public class dcRecuperaTablasDeSistemas
    {
        public dcRecuperaTablasDeSistemas()
        {
            lstTablasDeSistema = new List<TablasDeSistema>();
        }
        public List<TablasDeSistema> lstTablasDeSistema { get; set; }
    }

    [Serializable]
    public class TablasDeSistema
    {

        private string nombreTablaField;

        /// <comentarios/>
        public string NombreTabla
        {
            get
            {
                return this.nombreTablaField;
            }
            set
            {
                this.nombreTablaField = value;
            }
        }
    }
}
