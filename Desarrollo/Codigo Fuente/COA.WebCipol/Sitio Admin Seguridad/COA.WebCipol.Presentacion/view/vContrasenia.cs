using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COA.WebCipol.Presentacion.view
{
    public class vContrasenia : EntidadesBase
    {
        
        public byte OBLIGATORIO { get; set; }
        public string CONTRASENIA { get; set; }
        public string NUEVACONTRASENIA { get; set; }
        public string REPETIRNUEVACONTRASENIA { get; set; }

    }
}