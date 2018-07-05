using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vSucesosSeguridadCarga : EntidadesBase
    {
        public string jsoncboadministradores { get; set; }
        public string jsoncboafectado { get; set; }
        public string jsoncbocodigomensaje { get; set; }
    }

    [Serializable]
    public class vSucesosSeguridadItem {
        
        public DateTime fecha { get; set; }
        public string strFecha
        {
            get { return fecha.Date.ToString("dd/MM/yyyy"); }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    fecha = new DateTime(int.Parse(value.Substring(6, 4)), int.Parse(value.Substring(3, 2)), int.Parse(value.Substring(0, 2)));
                }
            }
        }

        public decimal CODMENSAJE
        {
            get;
            set;
        }

        public string TEXTOMENSAJE
        {
            get;
            set;
        }

        /// <comentarios/>
        public string USUARIOACTUANTE
        {
            get;
            set;
        }

        /// <comentarios/>
        public string USUARIOAFECTADO
        {
            get;
            set;
        }
    }

    public class vSucesosSeguridad : EntidadesBase
    {
        public string jsonsucesosseguridad { get; set; }


        public int cantidadRegistros { get; set; }
    }


}