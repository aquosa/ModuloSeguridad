using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COA.WebCipol.Fachada
{
    public class FPadreFachada
    {
        protected Seguridad mobjSeguridadCP;
        protected Seguridad SeguridadCP
        {
            get
            {
                if (mobjSeguridadCP == null) mobjSeguridadCP = new Seguridad();
                return mobjSeguridadCP;
            }
        }
    }
}
