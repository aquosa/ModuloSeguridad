using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarPoliticasGenerales
{
    [Serializable]
    public class dcRecuperarPoliticasGenerales
    {
        public dcRecuperarPoliticasGenerales()
        {
            lstSE_PARAMETROS = new List<SE_PARAMETROS>();
        }

        public List<SE_PARAMETROS> lstSE_PARAMETROS { get; set; }
    }
}
