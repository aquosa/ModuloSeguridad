using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Comun
{
    /// <summary>
    /// Contiene los datos del sistema para la sesión del usuario.
    /// </summary>
    ///TODO 
    ///ver que no se pierda la sesión por un tiempo considerable!! 
    [Serializable]
    public class DatosSistema
    {
        public General DatosGenerales { get; set; }
           
    }
}
