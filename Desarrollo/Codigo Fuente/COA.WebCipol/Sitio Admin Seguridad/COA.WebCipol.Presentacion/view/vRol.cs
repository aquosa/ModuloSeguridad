using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    [Serializable]
    public class vSessionDatosRol
    {
        public COA.WebCipol.Entidades.ClasesWs.dcRecuperarDatosParaABMRoles.dcRecuperarDatosParaABMRoles dcRecuperarDatosParaABMRoles { get; set; }
        public COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.dcRecuperarComposicionRol dcRecuperarComposicionRol { get; set; }
        public List<COA.WebCipol.Entidades.ClasesWs.dcRecuperarComposicionRol.Roles_Composicion> listaOrignal { get; set; }
    }
    public class vRolEliminar : EntidadesBase
    {
        public vRolEliminar()
        {
            rolgetcarga = new vRolGetCarga();
        }
        public vRolGetCarga rolgetcarga { get; set; }
        public bool preguntausuarios { get; set; }
    }
    public class vRolAsignar : EntidadesBase
    {
    }

    public class vRolGetCarga : EntidadesBase
    {
        public vRolGetCarga()
        {
            elemento = new vRol();
        }
        public vRol elemento { get; set; }
        public string jsoncboroles { get; set; }
        public string jsontvtareasdisponibles { get; set; }
        public string jsontvtareasasignadas { get; set; }
    }
    public class vAdministrarRol : EntidadesBase
    {
        public int idrol { get; set; }

    }
    public class vRolGetItem : EntidadesBase
    {
        public string jsontvtareasasignadas { get; set; }
    }
    public class vRol : EntidadesBase
    {
        public vRol() {
            lsttareas = new List<itemRolGenerico>();
        }
        public bool update { get; set; }
        public short idrol { get; set; }
        public string nombrerol { get; set; }
        public List<itemRolGenerico> lsttareas { get; set; }
    }
    public class itemRolGenerico
    {
        public string Id { get; set; }
        public string nombre { get; set; }
        public bool asignada { get; set; }
    }
}