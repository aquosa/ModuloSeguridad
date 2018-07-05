using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaEntornoCIPOLAdministrador
{
    [Serializable]
    public class dcRecuperarDatosParaEntornoCIPOLAdministrador
    {
        public List<SE_PARAMETROS> lstParametros { get; set; }
        public List<SE_CODAUDITORIA> lstCodAuditoria { get; set; }
        public List<SE_MENSAJES> lstMensajes { get; set; }
    }

}
