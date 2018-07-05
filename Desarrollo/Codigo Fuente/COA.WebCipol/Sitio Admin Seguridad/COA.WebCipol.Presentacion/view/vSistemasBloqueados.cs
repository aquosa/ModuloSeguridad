using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vSistemasBloqueados : EntidadesBase
    {
        public vSistemasBloqueados()
        {
            elemento = new vSistemaBloq();
        }

        public vSistemaBloq elemento { get; set; }
        public string jsonsistemasdesbloqueados { get; set; }
        public string jsonsistemasbloqueados { get; set; }
        public string jsonusuariosdesbloqueados { get; set; }
        public string jsonusuariosbloqueados { get; set; }
    }


    public class vAdministrarSistemas : EntidadesBase
    {
        public short idsistema { get; set; }
    }

    public class vSistemaBloq : EntidadesBase
    {
        public vSistemaBloq()
        {
            lstSistemas = new List<itemGenerico>();
            lstUsuarios = new List<itemGenerico>();
        }
        public bool update { get; set; }
        public short idsistema { get; set; }
        public string nombresistema { get; set; }
        public List<itemGenerico> lstSistemas { get; set; }
        public List<itemGenerico> lstUsuarios{ get; set; }
    }

    [Serializable]
    public class vSessionDatosSistemasBloqueados
    {
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarSistBloqueados.SE_SIST_BLOQUEADOS> lstSISTEMAS_BLOQUEADOS { get; set; }       
    }

    public class SistemaBloqueado
    {
        public short Id { get; set; }
        public string nombre { get; set; }
        public short IdSistemaBloqueado { get; set; }
    }

}