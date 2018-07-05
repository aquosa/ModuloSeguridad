using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Entidades.ClasesWs
{
    public class dcValidarSupervisor
    {
        public string usuario { get; set; }
        public string clave { get; set; }
        public int idusuariosupervisor { get; set; }
        public int idusuario { get; set; }
        public int idtareasupervisor { get; set; }
        public string terminal { get; set; }
    }
}
