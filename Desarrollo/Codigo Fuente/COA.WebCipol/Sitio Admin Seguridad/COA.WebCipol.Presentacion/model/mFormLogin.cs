using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntidadesEmpresariales;
using COA.WebCipol.Comun;

namespace COA.WebCipol.Presentacion.model
{
    public class mFormLogin
    {
        public bool ResultadoProcesoInicioSesion { get; set; }
        public DatosSistema DatosSistema { get; set; }
        public string Mensaje { get; set; }
        public DatosCIPOL DatosCipol { get; set; }
    }
}