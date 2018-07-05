using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Inicio.view
{
    public class vTipoSeguridadAdministrar : EntidadesBase
    {
        public bool preguntar { get; set; }
    }
    public class vTipoSeguridadCarga : EntidadesBase
    {
        public bool EstadoOptIntegrada { get; set; }
        public bool EstadoOptCIPOL { get; set; }
        public vTipoSeguridad elemento { get; set; }
    }
    public class vTipoSeguridad : EntidadesBase
    {
        public bool validar { get; set; }
        public bool validarDominio { get; set; }
        public bool optIntegrada { get; set; }
        public bool optCIPOL { get; set; }
        public string NombreDominio { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string NombreOrganizacion { get; set; }
    }
    
}