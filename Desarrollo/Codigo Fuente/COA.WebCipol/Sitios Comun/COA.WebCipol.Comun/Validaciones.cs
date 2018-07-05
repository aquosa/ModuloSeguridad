using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Comun
{
    public class Validaciones
    {
        /// <summary>
        /// Valida que una cadena no sea nula o vacia
        /// </summary>
        /// <param name="Cadena"></param>
        /// <remarks>Se toma como cadena vacia a "" o " ... "</remarks>
        /// <returns>true si la cadena NO es nula o vacia</returns>
        public static bool ValidarCadenaNulaOVacia(string Cadena)
        {
            return !String.IsNullOrEmpty(Cadena) && !String.IsNullOrWhiteSpace(Cadena);
        }
    }
}
