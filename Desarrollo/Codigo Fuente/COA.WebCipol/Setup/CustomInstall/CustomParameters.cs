using System;
using System.Collections;
using System.Configuration.Install;

namespace CustomInstall
{
    public class CustomParameters
    {
        private static System.Collections.IDictionary state = null;
        public static string PREFIJO_SALUDO = "PrefijoSaludo";

        private string _prefijoSaludo;

        /// <summary>
        /// Constructor para los instalables sin parametros de entrada
        /// </summary>
        /// <param name="savedState"></param>
        public CustomParameters(IDictionary savedState)
        {
            state = savedState;
        }

        public string PrefijoSaludo
        {
            get { return _prefijoSaludo; }
            set { _prefijoSaludo = value; }
        }

        /// <summary>
        /// Recupera los parametros pasados al msi.
        /// </summary>
        /// <param name="installContext"></param>
        public void LoadContext(InstallContext installContext)
        {
            this.PrefijoSaludo = installContext.Parameters[PREFIJO_SALUDO];
        }
    }
}
