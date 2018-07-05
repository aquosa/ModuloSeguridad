using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarSupervisores
{
    [Serializable]
    public class dcRecuperarSupervisores
    {
        public dcRecuperarSupervisores()
        {
            lstDtSupervisores = new List<DtSupervisores>();
        }
        public List<DtSupervisores> lstDtSupervisores { get; set; }
    }
    [Serializable]
    public class DtSupervisores
    {

        private string uSUARIOField;

        private string nOMBRESField;

        /// <comentarios/>
        public string USUARIO
        {
            get
            {
                return this.uSUARIOField;
            }
            set
            {
                this.uSUARIOField = value;
            }
        }

        /// <comentarios/>
        public string NOMBRES
        {
            get
            {
                return this.nOMBRESField;
            }
            set
            {
                this.nOMBRESField = value;
            }
        }
    }
}
