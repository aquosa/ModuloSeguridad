using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vGrupoTareaGetCarga : EntidadesBase
    {
        public vGrupoTareaGetCarga()
        {
            elemento = new vGrupoTarea();
        }
        
        public vGrupoTarea elemento { get; set; }
        public string jsoncbogrupo { get; set; }
        public string jsoncbosistemas { get; set; }
        public string jsontareasnoasignandas { get; set; }
        public string jsontareasasignadas { get; set; }
        public string jsongruposnoexcluyentes { get; set; }
        public string jsongrupoexcluyentes { get; set; }
    }
    public class vAdministrarGrupoTareas : EntidadesBase
    {
        public short idgrupro { get; set; }
    }
    public class vGrupoTareaGetItem : EntidadesBase
    {
        public string jsontareasasignadas { get; set; }
        public string jsongruposnoexcluyentes { get; set; }
        public string jsongrupoexcluyentes { get; set; }
    }
    public class vGrupoTarea : EntidadesBase
    {
        public vGrupoTarea()
        {
            lstGrupos = new List<itemGenerico>();
            lstTareas = new List<itemGenerico>();
        }
        public bool update { get; set; }
        public short idgrupo { get; set; }
        public string nombregrupo { get; set; }
        public List<itemGenerico> lstGrupos { get; set; }
        public List<itemGenerico> lstTareas { get; set; }
    }
    public class itemGenerico 
    {
        public int Id { get; set; }
        public string nombre { get; set; }
    }
}