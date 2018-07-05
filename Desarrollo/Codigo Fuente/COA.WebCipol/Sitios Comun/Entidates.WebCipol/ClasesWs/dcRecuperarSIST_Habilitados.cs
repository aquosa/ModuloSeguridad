using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarSIST_Habilitados
{
    [Serializable]
    public class dcRecuperarSIST_Habilitados
    {
        public dcRecuperarSIST_Habilitados()
        {
            lstSE_SIST_HABILITADOS = new List<SE_SIST_HABILITADOS>();
        }
        public List<SE_SIST_HABILITADOS> lstSE_SIST_HABILITADOS { get; set; }
    }

    [Serializable]
    public class SE_SIST_HABILITADOS
    {

        private string dESCSISTEMAField;

        /// <comentarios/>
        public string DESCSISTEMA
        {
            get
            {
                return this.dESCSISTEMAField;
            }
            set
            {
                this.dESCSISTEMAField = value;
            }
        }
    }
}
